using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.Text;

namespace WX2HK.Attendance
{
    public partial class Attendance_AdminRegister_Edit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tabId = Request["id"];
                label_tabId.Text = tabId;
                LoadInfo(tabId);
            }
        }

        private void LoadInfo(string tabId) 
        {
            string sqlCmd = "select A.*,c.chineseName,d.deptName,t.flowName from OA_Leave_Main A";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId";
            sqlCmd += " left join OA_sys_flow t on t.id=A.flowId where A.id='" + tabId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            if (dt.Rows.Count > 0)
            {
                label_name.Text = dt.Rows[0]["chineseName"].ToString() + '/' + dt.Rows[0]["deptName"].ToString();
                label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
                label_flowName.Text = dt.Rows[0]["flowName"].ToString();
                label_strTime.Text = dt.Rows[0]["strTime"].ToString();
                label_endTime.Text = dt.Rows[0]["endTime"].ToString();
                label_days.Text = dt.Rows[0]["lastDays"].ToString();
                label_hours.Text = dt.Rows[0]["lastHours"].ToString();
                label_reason.Text = dt.Rows[0]["leaveReason"].ToString();
                label_replacer.Text = dt.Rows[0]["workReplacer"].ToString();
            }


            //加载附件信息
            bindHidden(label_tabId.Text, "OA_Leave_Main");
            //审批信息
            bindCheckDetail(label_tabId.Text, "OA_Leave_Main");
        }

        private void bindHidden(string formId, string tabName)
        {
            StringBuilder sb = new StringBuilder();
            string sqlCmd = "select * from OA_Sys_files where formID='" + formId + "' and formDataName='" + tabName + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    sb.AppendFormat("{0};", dt.Rows[i]["fileName"].ToString());
            //}
            //if (!string.IsNullOrEmpty(sb.ToString()))
            //{
            //    hidden_field.Value = sb.ToString().Substring(0, sb.ToString().Length - 1);
            //}
        }

        private void bindCheckDetail(string formId, string formDataName)
        {
            DataTable checkDt = getCheckDtl(formId, formDataName);
            Grid2.DataSource = checkDt;
            Grid2.DataBind();
        }
    }
}