using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IETCsoft.sql;
using FineUI;
using Newtonsoft.Json.Linq;


namespace WX2HK.Fines
{
    public partial class FineDetail_RegList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindProj();
                DatePicker2.SelectedDate = DateTime.Now;
            }
        }
        //筛选与用户相关的项目
        private void bindProj()
        {
            string sqlCmd = "select B.id,B.projName from OA_Sys_ProjMember A left join OA_sys_Project B on A.projId=B.id and b.ProjStatus >0 where A.userId='" + GetUser() + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_proj.DataTextField = "projName";
            ddl_proj.DataValueField = "id";
            ddl_proj.DataSource = dt;
            ddl_proj.DataBind();
        }
        private void bindGrid(string reqMan, string dt1, string dt2)
        {
            string sqlCmd = "";
            if (string.IsNullOrEmpty(reqMan))
            {
                reqMan = "%";
            }

            if (!string.IsNullOrEmpty(dt1))
            {
                if (ddl_proj.SelectedItem == null)
                {
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Fines_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.ProjId and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and A.ProjId in (select ProjId from OA_Sys_ProjMember where userId='" + GetUser() + "') and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqdte desc";
                }
                else
                {
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Fines_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.ProjId and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and A.ProjId='" + ddl_proj.SelectedValue + "' and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqdte desc";
                }
            }
            else
            {
                if (ddl_proj.SelectedItem == null)
                {
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Fines_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.ProjId and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and A.ProjId in (select ProjId from OA_Sys_ProjMember where userId='" + GetUser() + "') and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqdte desc";
                }
                else
                {
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Fines_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.ProjId and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and A.ProjId='" + ddl_proj.SelectedValue + "' and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqdte desc";
                }
            }

            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        protected void btn_mutiSeach_Click(object sender, EventArgs e)
        {
            bindGrid(txb_reqMan.Text, DatePicker1.Text, DatePicker2.Text);
        }

    }
}