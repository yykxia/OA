using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IETCsoft.sql;
using FineUI;
using Newtonsoft.Json.Linq;

namespace WX2HK.Bills
{
    public partial class BillDetail_RegList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                bindProj();
                DatePicker2.SelectedDate = DateTime.Now;
            }
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
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Bills_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.RelProj and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and RelProj in (select ProjId from OA_Sys_ProjMember where userId='" + GetUser() + "') and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqDte desc";
                }
                else 
                {
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Bills_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.RelProj and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and RelProj='"+ddl_proj.SelectedValue+"' and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqDte desc";
                }
            }
            else
            {
                if (ddl_proj.SelectedItem == null)
                {
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Bills_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.RelProj and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and RelProj in (select ProjId from OA_Sys_ProjMember where userId='" + GetUser() + "') and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqDte desc";
                }
                else
                {
                    sqlCmd = "select A.*,B.chineseName,C.projName from OA_Bills_Main A left join OA_Sys_EmployeeInfo B on A.reqMan=B.id";
                    sqlCmd += " left join OA_sys_Project C on C.id=A.RelProj and c.ProjStatus >0 where reqMan in (select id from OA_Sys_EmployeeInfo where chineseName LIKE '" + reqMan + "') ";
                    sqlCmd += "and RelProj='" + ddl_proj.SelectedValue + "' and ";
                    sqlCmd += "CONVERT(varchar(100), reqDte, 23)<='" + dt2 + "' order by reqDte desc";
                }
            }

            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        private void getSummaryData()
        {
            DataTable curDt = getGridData();
            decimal BillSum = 0;

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            if (curDt != null)
            {
                foreach (DataRow row in curDt.Rows)
                {
                    BillSum += Convert.ToDecimal(row["billTotal"]);
                }

                summary.Add("billTotal", BillSum);
                Grid1.SummaryData = summary;
            }
            else
            {
                summary.Add("billTotal", 0);
                Grid1.SummaryData = summary;
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

        private DataTable getGridData() 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("billTotal");
            if (Grid1.Rows.Count > 0)
            {
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    decimal bindTotal = Convert.ToDecimal(Grid1.Rows[i].Values[3]);
                    DataRow dr = dt.NewRow();
                    dr["billTotal"] = bindTotal;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            else {
                return null;
            }
        }

        protected void btn_mutiSeach_Click(object sender, EventArgs e)
        {
            bindGrid(txb_reqMan.Text, DatePicker1.Text, DatePicker2.Text);
            getSummaryData();
        }
    }
}