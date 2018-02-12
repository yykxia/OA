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
    public partial class Attendance_AdminRegister : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                DateTime curDt = DateTime.Now;
                DateTime strDt = new DateTime(curDt.Year, DateTime.Now.Month, 1);//当月第一天
                DatePicker1.SelectedDate = strDt;
                DatePicker2.SelectedDate = strDt.AddMonths(1).AddDays(-1);//当月最后一天
                loadDeptInfo();
            }
        }

        private void bindGrid(string userId,string dt1,string dt2) 
        {
            string sqlCmd = "";
            if (userId == "all") 
            {
                sqlCmd = "select A.*,c.chineseName,d.deptName,t.flowName from OA_Leave_Main A";
                sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId";
                sqlCmd += " left join OA_sys_flow t on t.id=A.flowId where A.CurrentStepID='0' and reqMan in (select id from OA_Sys_EmployeeInfo";
                sqlCmd += " where deptId like '" + ddl_dept.SelectedValue + "') and convert(nvarchar(20),strTime,23)>='" + dt1 + "' and convert(nvarchar(20),strTime,23)<='" + dt2 + "' order by reqMan,reqDte,flowId";
            }
            else
            {
                sqlCmd = "select A.*,c.chineseName,d.deptName,t.flowName from OA_Leave_Main A";
                sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId";
                sqlCmd += " left join OA_sys_flow t on t.id=A.flowId where A.CurrentStepID='0' and reqMan='" + userId + "'";
                sqlCmd += " and convert(nvarchar(20),strTime,23)>='" + dt1 + "' and convert(nvarchar(20),strTime,23)<='" + dt2 + "' order by reqMan,reqDte,flowId";
            }
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        private void loadDeptInfo() 
        {
            string sqlCmd = "select * from OA_sys_department order by deptName";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_dept.DataTextField = "deptName";
            ddl_dept.DataValueField = "id";
            ddl_dept.DataSource = dt;
            ddl_dept.DataBind();
            this.ddl_dept.Items.Insert(0, new FineUI.ListItem("所有部门", "%"));
        }

        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                //加载人员信息
                loadUserInfo(ddl_dept.SelectedValue);
                //
                bindGrid("all", DatePicker1.Text, DatePicker2.Text);
            }
            catch (Exception ex) 
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        private void loadUserInfo(string deptId) 
        {
            string sqlCmd = "select * from OA_Sys_EmployeeInfo where deptId like '" + deptId + "'";
            DataTable empDt = new DataTable();
            SqlSel.GetSqlSel(ref empDt, sqlCmd);
            ddl_emp.DataTextField = "chineseName";
            ddl_emp.DataValueField = "id";
            ddl_emp.DataSource = empDt;
            ddl_emp.DataBind();
            this.ddl_emp.Items.Insert(0, new FineUI.ListItem("所有人员", "%"));
            ddl_emp.SelectedIndex = 0;
        }

        protected void ddl_emp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_emp.SelectedValue != "%")
                {
                    bindGrid(ddl_emp.SelectedValue, DatePicker1.Text, DatePicker2.Text);
                }
                else 
                {
                    bindGrid("all", DatePicker1.Text, DatePicker2.Text);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            string sqlCmd = "select A.*,c.chineseName,d.deptName,t.flowName from OA_Leave_Main A";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId";
            sqlCmd += " left join OA_sys_flow t on t.id=A.flowId where A.CurrentStepID='0'";
            sqlCmd += " and convert(nvarchar(20),strTime,23)>='" + DatePicker1.Text + "' and convert(nvarchar(20),strTime,23)<='" + DatePicker2.Text + "' order by reqMan,reqDte,flowId";

            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
            ddl_dept.SelectedIndex = 0;
        }
    }
}