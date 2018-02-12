using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.Attendance
{
    public partial class Attendance_regList : BasePage
    {
        private static string userId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DatePicker1.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//当月第一天
                DatePicker2.SelectedDate = DateTime.Now;
                userId = Request["userid"];
                reqList(userId);
            }
        }

        //个人考勤申请记录
        private void reqList(string userId) 
        {
            string sqlCmd = "select A.*,isnull(B.stepName,'已完成') as checkStatus,C.flowName from OA_Leave_Main A";
            sqlCmd += " left join OA_sys_flow C ON C.ID=A.FlowId";
            sqlCmd += " LEFT JOIN OA_Sys_Flow_Step B ON A.CurrentStepID=B.id WHERE reqMan='" + userId + "'";
            sqlCmd += " and convert(nvarchar(20),strTime,23)>='" + DatePicker1.Text + "' and ";
            sqlCmd += "convert(nvarchar(20),strTime,23)<='" + DatePicker2.Text + "' order by reqDte desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            reqList(userId);
        }
    }
}