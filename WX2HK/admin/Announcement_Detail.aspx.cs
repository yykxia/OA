using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.Text;

namespace WX2HK.admin
{
    public partial class Announcement_Detail : BasePage
    {
        public string Announcement_TimeStamp = "";
        public string Announcement_Nonce = "";
        public string Announcement_MsgSig = "";
        private static string curUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                string anncId = Request["id"];

                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = string.Format("admin%2fAnnouncement_Detail.aspx?id={0}", anncId);
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                Announcement_TimeStamp = WX2HK.ReturnInfo.GetTimeStamp();
                Announcement_Nonce = WX2HK.ReturnInfo.randNonce();
                string curUrl = string.Format("admin/Announcement_Detail.aspx?id={1}&code={0}&state=STATE", code, anncId);
                Announcement_MsgSig = WX2HK.VerifyLegal.createSign(Announcement_Nonce, Announcement_TimeStamp, curUrl);

                //加载信息
                loadAnncInfo(anncId);
                //附件信息
                if (!string.IsNullOrEmpty(anncId))
                {
                    bindHidden(anncId, "OA_Announcement");
                }

            }
        }

        private void loadAnncInfo(string anncId) 
        {
            string sqlCmd = "select * from OA_Announcement where id='" + anncId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            if (dt.Rows.Count > 0) 
            {
                Label_title.Text = dt.Rows[0]["AnncTitle"].ToString();//公告标题
                Label_date.Text = dt.Rows[0]["CreateTime"].ToString();//发布时间
                label_context.Text = dt.Rows[0]["AnncContext"].ToString();//公告内容

            }
        }


        private void bindHidden(string formId, string tabName)
        {
            string sqlCmd = "select * from OA_Sys_files where formID='" + formId + "' and formDataName='" + tabName + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            dt.Columns.Add("userId");
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                dt.Rows[i]["userId"] = curUserId;
            }
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
        }
    }
}