using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using FineUI;
using System.Net;
using System.Data;
using IETCsoft.sql;

namespace WX2HK.Payment
{
    public partial class Payment_sub_m : BasePage
    {
        public string Payment_TimeStamp = "";
        public string Payment_Nonce = "";
        public string Payment_MsgSig = "";
        private static string curUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = "Payment%2fPayment_sub_m.aspx";
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                loadInfo(curUserId);
                Payment_TimeStamp = WX2HK.ReturnInfo.GetTimeStamp();
                Payment_Nonce = WX2HK.ReturnInfo.randNonce();
                string curUrl = string.Format("Payment/Payment_sub_m.aspx?code={0}&state=STATE", code);
                Payment_MsgSig = WX2HK.VerifyLegal.createSign(Payment_Nonce, Payment_TimeStamp, curUrl);

                //加载流程信息
                loadFlowInfo("OA_PayMent_Main", curUserId);

                //加载关联项目
                bindProj();
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hidden_field.Value))
                {
                    string stepId = getStepId(ddl_flow.SelectedValue);
                    string sqlCmd = "insert into OA_PayMent_Main (reqMan,reqDte,payeeName,payTotal,projId,FlowId,CurrentStepID,others,isAdvPay) values ";
                    sqlCmd += "('" + curUserId + "','" + DateTime.Now + "','" + txb_payeeName.Text + "','" + numbbox_total.Text + "','" + ddl_proj.SelectedValue + "','" + ddl_flow.SelectedValue + "','" + stepId + "','" + txa_reason.Text + "','" + rdb_payType.SelectedValue + "')";
                    int exeCount = SqlSel.ExeSql(sqlCmd);
                    if (exeCount > 0)
                    {
                        //取当前单据id
                        sqlCmd = "select max(id) from OA_PayMent_Main";
                        string formId = SqlSel.GetSqlScale(sqlCmd).ToString();
                        //插入附件信息表
                        InsertFiles(formId);

                        Alert.Show("提交成功！");

                        //表单重置
                        SimpleForm1.Reset();
                        PageContext.RegisterStartupScript("clearImg()");

                        //推送信息至相关审批人
                        pushMessage(stepId, "OA_PayMent_Main", formId, "付款申请");

                    }
                }
                else 
                {
                    Alert.ShowInTop("请先上传相应附件后再提交！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }

        //获取流程第一个节点的步骤id
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
                sqlCmd = "insert into OA_Sys_files (FormId,fileName,formDataName) values ('" + formId + "','" + fileName + "','OA_PayMent_Main')";
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

        protected void lbtn_his_Click(object sender, EventArgs e)
        {
            string pageAddr = string.Format("Payment_ReqList_m.aspx?userid={0}", curUserId);
            PageContext.RegisterStartupScript(Window1.GetShowReference(pageAddr) + Window1.GetMaximizeReference());
        }

    }
}