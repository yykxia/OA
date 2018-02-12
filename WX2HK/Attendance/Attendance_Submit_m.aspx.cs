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

namespace WX2HK.Attendance
{
    public partial class Attendance_Submit_m : BasePage
    {
        public string Attendance_TimeStamp = "";
        public string Attendance_Nonce = "";
        public string Attendance_MsgSig = "";
        private static string curUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = "Attendance%2fAttendance_Submit_m.aspx";
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                loadInfo(curUserId);
                Attendance_TimeStamp = WX2HK.ReturnInfo.GetTimeStamp();
                Attendance_Nonce = WX2HK.ReturnInfo.randNonce();
                string curUrl = string.Format("Attendance/Attendance_Submit_m.aspx?code={0}&state=STATE", code);
                Attendance_MsgSig = WX2HK.VerifyLegal.createSign(Attendance_Nonce, Attendance_TimeStamp, curUrl);

                //加载流程信息
                loadFlowInfo("OA_Leave_Main", curUserId);
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

        private void loadInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                string sqlCmd = "select A.*,B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id='" + userId + "'";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                label_name.Text = dt.Rows[0]["loginId"].ToString() + "/" + dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
                label_date.Text = DateTime.Now.ToShortDateString();
            }
            else 
            {
                SimpleForm1.Hidden = true;
                HttpContext.Current.Response.Write("信息不存在或非企业内部人员！");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(numbbox_days.Text) & string.IsNullOrEmpty(numbbox_hours.Text))
                {
                    Alert.ShowInTop("天数和小时至少填一项。");
                    return;
                }

                //if (string.IsNullOrEmpty(hidden_field.Value))
                //{
                //    Alert.Show("请先添加相应的文件，若已添加则确认可用！");
                //    return;
                //}
                decimal days = 0;
                decimal hours = 0;
                if (!string.IsNullOrEmpty(numbbox_days.Text))
                {
                    days = Convert.ToDecimal(numbbox_days.Text);
                }
                if (!string.IsNullOrEmpty(numbbox_hours.Text))
                {
                    hours = Convert.ToDecimal(numbbox_hours.Text);
                }
                string stepId = getStepId(ddl_flow.SelectedValue);//获取步骤id
                string sqlCmd = "insert into OA_Leave_Main ([reqMan],[reqDte],[strTime],[endTime],[lastDays],[leaveReason],[FlowId],[CurrentStepID],[workReplacer],[lastHours]) values ";
                sqlCmd += "('" + curUserId + "','" + DateTime.Now + "','" + DatePicker1.Text + " " + TimePicker1.Text + "','" + DatePicker2.Text + " " + TimePicker2.Text + "','" + days + "',";
                sqlCmd += "'" + TextArea_desc.Text.Trim() + "','" + ddl_flow.SelectedValue + "','" + stepId + "','" + txb_replacer.Text.Trim() + "','" + hours + "')";
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount > 0)
                {
                    //取当前单据id
                    sqlCmd = "select max(id) from OA_Leave_Main";
                    string formId = SqlSel.GetSqlScale(sqlCmd).ToString();
                    //插入附件信息表
                    InsertFiles(formId);

                    Alert.Show("提交成功！");

                    //表单重置
                    SimpleForm1.Reset();
                    PageContext.RegisterStartupScript("clearImg()");
                    //推送信息至相关审批人
                    pushMessage(stepId, "OA_Leave_Main", formId, "考勤申请");

                }
                else
                {
                    Alert.Show("提交失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }


        //插入附件信息
        private void InsertFiles(string formId)
        {
            string access_token = VerifyLegal.GetAccess_Token();//获取access_token

            string sqlCmd = "";
            string fileList = hidden_field.Value;
            if (!string.IsNullOrEmpty(fileList))
            {
                //解析明细Id
                String[] str = fileList.Split(';');
                foreach (string it in str)
                {
                    if (it == "")
                    {
                        break;
                    }

                    string fileName = GetMultimedia(access_token, it);
                    sqlCmd = "insert into OA_Sys_files (FormId,fileName,formDataName) values ('" + formId + "','" + fileName + "','OA_Leave_Main')";
                    SqlSel.ExeSql(sqlCmd);
                }
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

        private string getStepId(string flowId)
        {
            string sqlCmd = "select id from OA_Sys_Flow_Step where flowid='" + flowId + "' and stepOrderNo=1";
            string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
            return stepId;
        }
        protected void lbtn_his_Click(object sender, EventArgs e)
        {
            string pageAddr = string.Format("Attendance_regList.aspx?userid={0}", curUserId);
            PageContext.RegisterStartupScript(Window1.GetShowReference(pageAddr) + Window1.GetMaximizeReference());
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
        }
    }
}