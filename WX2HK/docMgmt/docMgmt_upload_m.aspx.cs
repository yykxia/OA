using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Net;
using System.Data;
using IETCsoft.sql;

namespace WX2HK.docMgmt
{
    public partial class docMgmt_upload_m : System.Web.UI.Page
    {
        public string docUpload_TimeStamp = "";
        public string docUpload_Nonce = "";
        public string docUpload_MsgSig = "";
        private static string curUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = "docMgmt%2fdocMgmt_upload_m.aspx";
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                loadInfo();
                docUpload_TimeStamp = WX2HK.ReturnInfo.GetTimeStamp();
                docUpload_Nonce = WX2HK.ReturnInfo.randNonce();
                string curUrl = string.Format("docMgmt/docMgmt_upload_m.aspx?code={0}&state=STATE", code);
                docUpload_MsgSig = WX2HK.VerifyLegal.createSign(docUpload_Nonce, docUpload_TimeStamp, curUrl);

                //
                bindProj();
                //
                bindDocType();
            }
        }

        private void loadInfo() 
        {
            if (!string.IsNullOrEmpty(curUserId))
            {

                string sqlCmd = "select A.*,B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id='" + curUserId + "'";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                if (dt.Rows.Count > 0)
                {
                    label_name.Text = dt.Rows[0]["loginId"].ToString() + "/" + dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
                }
                else 
                {
                    SimpleForm1.Hidden = true;
                    HttpContext.Current.Response.Write("登录用户尚未在企业登记，请联系管理员！");
                }
            }
            else 
            {
                SimpleForm1.Hidden = true;
                HttpContext.Current.Response.Write("信息不存在或非企业内部人员！");

            }
        }

        //筛选与用户相关的项目
        private void bindProj()
        {
            string sqlCmd = "select B.id,B.projName from OA_Sys_ProjMember A left join OA_sys_Project B on A.projId=B.id where A.userId='" + curUserId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_proj.DataTextField = "projName";
            ddl_proj.DataValueField = "id";
            ddl_proj.DataSource = dt;
            ddl_proj.DataBind();
        }

        //加载文件类型
        private void bindDocType()
        {
            string sqlCmd = "select A.docTypeId,B.type_name FROM ";
            sqlCmd += "(SELECT docTypeId FROM [OA_DocMgmt_DocType_RelDuty] WHERE dutyId=( SELECT dutyId FROM OA_Sys_EmployeeInfo WHERE ID='" + curUserId + "') GROUP BY docTypeId";
            sqlCmd += " ) A LEFT JOIN OA_DocMgmt_DocType B ON A.docTypeId=B.Id WHERE B.stat='1' and B.pid='0' ORDER BY type_name";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_docType.DataTextField = "type_name";
            ddl_docType.DataValueField = "docTypeId";
            ddl_docType.DataSource = dt;
            ddl_docType.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hidden_field.Value))
                {
                    InsertFiles(txa_nameFile.Text);
                }
                else 
                {
                    Alert.Show("请先添加相应的文件，若已添加则确认可用！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        //插入附件信息
        private void InsertFiles(string definedName)
        {
            string access_token = VerifyLegal.GetAccess_Token();//获取access_token

            string sqlCmd = "";
            string fileList = hidden_field.Value;
            //解析明细Id
            String[] str = fileList.Split(';');

            int sortIndex = 0;//文件名后缀

            foreach (string it in str)
            {
                if (it == "")
                {
                    break;
                }
                sortIndex = sortIndex + 1;

                string fileName = GetMultimedia(access_token, it,"/upload");
                sqlCmd = "insert into OA_DocMgmt_DocList (docName,docTyp,docStat,subman,subDte,doc_leng,docPath) values ";
                sqlCmd += "('" + definedName + "-" + sortIndex.ToString() + "','" + ddl_docType.SelectedValue + "',1,'" + curUserId + "','" + DateTime.Now + "',";
                sqlCmd += "'" + ddl_proj.SelectedValue + "','" + fileName + "')";
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 0)
                {
                    Alert.ShowInTop("上传出错！");
                    return;
                }
                else 
                {
                    //插入其他文件信息
                    sqlCmd = "select max(id) from OA_DocMgmt_DocList";
                    int docId = Convert.ToInt32(SqlSel.GetSqlScale(sqlCmd));
                    sqlCmd = "insert into OA_DocMgmt_Propety (docId,docSN,AgreementPaty,SubjectMatter,AgreeAmount,AmountType,StartDate,EndDate,Remarks) values ";
                    sqlCmd += "('" + docId + "','" + txb_docSN.Text + "','" + txa_AgreementPaty.Text + "','" + txa_SubjectMatter.Text + "','" + numb_amount.Text + "',";
                    sqlCmd += "'" + txb_AmountType.Text + "','" + DatePicker1.Text + "','" + DatePicker2.Text + "','" + txa_Remarks.Text + "')";
                    SqlSel.ExeSql(sqlCmd);
                }
            }

            Alert.Show("文件全部上传成功！");
            //表单重置
            SimpleForm1.Reset();



        }

        /// <SUMMARY> 
        /// 下载保存多媒体文件,返回多媒体保存路径 
        /// </SUMMARY> 
        /// <PARAM name="ACCESS_TOKEN"></PARAM> 
        /// <PARAM name="MEDIA_ID"></PARAM> 
        /// <PARAM name="serverPath">服务器存储路径</PARAM> 
        /// <RETURNS></RETURNS> 
        private string GetMultimedia(string ACCESS_TOKEN, string MEDIA_ID,string serverPath)
        {
            string fileName = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            string stUrl = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                strpath = myResponse.ResponseUri.ToString();
                WebClient mywebclient = new WebClient();
                fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                savepath = Server.MapPath(serverPath) + "\\" + fileName;
                try
                {
                    mywebclient.DownloadFile(strpath, savepath);
                    //fileName = savepath;
                }
                catch (Exception ex)
                {
                    Alert.ShowInTop(ex.Message);
                }

            }
            //Alert.Show(file);
            return fileName;

        }

    }
}