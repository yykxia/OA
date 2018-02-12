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

namespace WX2HK.Fines
{
    public partial class Fines_sub_m : BasePage
    {
        public string FineSub_TimeStamp = "";
        public string FineSub_Nonce = "";
        public string FineSub_MsgSig = "";
        private static string curUserId = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = "Fines%2fFines_sub_m.aspx";
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                loadInfo(curUserId);
                FineSub_TimeStamp = WX2HK.ReturnInfo.GetTimeStamp();
                FineSub_Nonce = WX2HK.ReturnInfo.randNonce();
                string curUrl = string.Format("Fines/Fines_sub_m.aspx?code={0}&state=STATE", code);
                FineSub_MsgSig = WX2HK.VerifyLegal.createSign(FineSub_Nonce, FineSub_TimeStamp, curUrl);

                //加载流程信息
                loadFlowInfo("OA_Fines_Main", curUserId);

                //加载关联项目
                bindProj();

                //btn_provf.OnClientClick = Window1.GetSaveStateReference(txa_proveList.ClientID) +
                //    Window1.GetShowReference("../selectUser.aspx") + Window1.GetMaximizeReference();
                //btn_objects.OnClientClick = Window1.GetSaveStateReference(txa_empList.ClientID) +
                //    Window1.GetShowReference("../selectUser.aspx") + Window1.GetMaximizeReference();
            }
        }

        private void loadFlowInfo(string formName, string userId)
        {
            try
            {
                DataTable dt = validFlow(formName, userId);
                ddl_flow.DataTextField = "flowName";
                ddl_flow.DataValueField = "id";
                ddl_flow.DataSource = dt;
                ddl_flow.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        private void loadInfo(string userWXId)
        {
            if (!string.IsNullOrEmpty(userWXId))
            {
                string sqlCmd = "select A.*,B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id='" + userWXId + "'";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                label_name.Text = dt.Rows[0]["loginId"].ToString() + "/" + dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
                label_date.Text = DateTime.Now.ToShortDateString();
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

        //添加证明人
        protected void btn_provf_Click(object sender, EventArgs e)
        {
            //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txa_proveList.ClientID)
            //        + Window1.GetShowReference("../selectUser.aspx") + Window1.GetMaximizeReference());

            //窗口最大化
            //PageContext.RegisterStartupScript(Window1.GetShowReference() + Window1.GetMaximizeReference());
        }

        //添加处罚对象
        protected void btn_objects_Click(object sender, EventArgs e)
        {
            //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txa_empList.ClientID)
            //        + Window1.GetShowReference("../selectUser.aspx") + Window1.GetMaximizeReference());

            ////窗口最大化
            //PageContext.RegisterStartupScript(Window1.GetShowReference() + Window1.GetMaximizeReference());
        }

        private string getStepId(string flowId)
        {
            string sqlCmd = "select id from OA_Sys_Flow_Step where flowid='" + flowId + "' and stepOrderNo=1";
            string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
            return stepId;
        }
        //插入附件信息
        private void InsertFiles(string formId)
        {
            string access_token = VerifyLegal.GetAccess_Token();//获取access_token

            string sqlCmd = "";
            string fileList = hidden_field.Value;
            //解析明细Id
            String[] str = fileList.Split(';');
            foreach (string it in str)
            {
                if (it == "")
                {
                    break;
                }

                string fileName = GetMultimedia(access_token, it);
                sqlCmd = "insert into OA_Sys_files (FormId,fileName,formDataName) values ('" + formId + "','" + fileName + "','OA_Fines_Main')";
                SqlSel.ExeSql(sqlCmd);
            }


        }

        /// <SUMMARY> 
        /// 下载保存多媒体文件,返回多媒体保存路径 
        /// </SUMMARY> 
        /// <PARAM name="ACCESS_TOKEN"></PARAM> 
        /// <PARAM name="MEDIA_ID"></PARAM> 
        /// <RETURNS></RETURNS> 
        private string GetMultimedia(string ACCESS_TOKEN, string MEDIA_ID)
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
                savepath = Server.MapPath("/image") + "\\" + fileName;
                //savepath = MapPath("file") + "\\" + "uplaodFile" + "\\" + fileName;
                //Uri uriPath = new Uri("http://192.168.4.253:8810/AddtionFile/uploadFile");
                //savepath = "http://192.168.4.253:8810/AddtionFile/uploadFile" + "\\" + fileName;
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hidden_field.Value))
                {
                    string stepId = getStepId(ddl_flow.SelectedValue);
                    string sqlCmd = "insert into OA_Fines_Main (reqMan,reqDte,fineAmount,FlowId,CurrentStepID,ReqReason,projId,Objects,ProveEmp) values ";
                    sqlCmd += "('" + curUserId + "','" + DateTime.Now + "','" + numbbox_total.Text.Trim() + "','" + ddl_flow.SelectedValue + "','" + stepId + "','" + txa_reason.Text + "','" + ddl_proj.SelectedValue + "','" + txa_empList.Text + "','" + txa_proveList.Text + "')";
                    int exeCount = SqlSel.ExeSql(sqlCmd);
                    if (exeCount > 0)
                    {
                        //取当前单据id
                        sqlCmd = "select max(id) from OA_Fines_Main";
                        string formId = SqlSel.GetSqlScale(sqlCmd).ToString();
                        //插入附件信息表
                        InsertFiles(formId);

                        //插入被处罚对象
                        //if (!string.IsNullOrEmpty(txa_empList.Text))
                        //{
                        //    insertObjList(txa_empList.Text, formId);
                        //}
                        ////插入证明人表
                        //if (!string.IsNullOrEmpty(txa_proveList.Text))
                        //{
                        //    insertProveList(txa_proveList.Text, formId);
                        //}

                        Alert.Show("提交成功！");

                        //表单重置
                        SimpleForm1.Reset();
                        PageContext.RegisterStartupScript("clearImg()");
                        //推送信息至相关审批人
                        pushMessage(stepId, "OA_Fines_Main", formId, "处罚流程");

                    }
                }
                else
                {
                    Alert.Show("请先添加相应图片凭据！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void lbtn_his_Click(object sender, EventArgs e)
        {
            string pageAddr = string.Format("FineDetail_reqList_m.aspx?userid={0}", curUserId);
            PageContext.RegisterStartupScript(Window1.GetShowReference(pageAddr) + Window1.GetMaximizeReference());
        }

        //插入证明人
        //private void insertProveList(string userStr, string formId)
        //{
        //    string userId = string.Empty, sqlCmd = string.Empty;
        //    String[] str = userStr.Split(';');//格式为{@id-name;}
        //    foreach (string it in str)
        //    {
        //        if (it == "")
        //        {
        //            break;
        //        }
        //        userId = it.Substring(0, it.IndexOf("-"));
        //        sqlCmd = "insert into OA_Fines_ProveEmp (FormId,userId) values ('" + formId + "','" + userId + "')";
        //        SqlSel.ExeSql(sqlCmd);
        //    }
        //}

        //插入处罚对象
        //private void insertObjList(string userStr, string formId)
        //{
        //    string userId = string.Empty, sqlCmd = string.Empty;
        //    String[] str = userStr.Split(';');//格式为{@id-name;}
        //    foreach (string it in str)
        //    {
        //        if (it == "")
        //        {
        //            break;
        //        }
        //        userId = it.Substring(0, it.IndexOf("-"));
        //        sqlCmd = "insert into OA_Fines_Objects (FormId,userId) values ('" + formId + "','" + userId + "')";
        //        SqlSel.ExeSql(sqlCmd);
        //    }
        //}

    }
}