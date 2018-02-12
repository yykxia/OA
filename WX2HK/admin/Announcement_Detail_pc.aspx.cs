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
    public partial class Announcement_Detail_pc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                string anncId = Request["id"];                
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
            if (dt.Rows.Count > 0)
            {
                Grid1.ShowHeader = true;
                Grid1.DataSource = dt;
                Grid1.DataBind();
            }
        }

    }
}