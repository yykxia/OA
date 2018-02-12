using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.admin
{
    public partial class Announcement_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                bindGrid();
            }
        }

        private void bindGrid() 
        {
            string sqlCmd = "select * from OA_Announcement where isDeleted='0' order by CreateTime desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        protected void btn_newAnnc_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference("Announcement.aspx"));
        }

        protected void Grid1_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int rowID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);
                string sqlCmd = "update OA_Announcement set isDeleted='1' where id='"+rowID+"'";
                SqlSel.ExeSql(sqlCmd);
                bindGrid();

                Alert.ShowInTop("删除成功!");
            }
        }
    }
}