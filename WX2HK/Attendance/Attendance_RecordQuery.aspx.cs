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
    public partial class Attendance_RecordQuery : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DatePicker1.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//当月第一天
                DatePicker2.SelectedDate = DateTime.Now;
                loadDeptInfo();
            }
        }

        private void bindGrid(string userId, string dt1, string dt2)
        {
            string sqlCmd = "";
            if (userId == "all")
            {
                sqlCmd = "select A.*,c.chineseName,d.deptName from OA_Attendence_Record A";
                sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.userId left join OA_sys_department d on d.id=c.deptId";
                sqlCmd += " where userId in (select id from OA_Sys_EmployeeInfo";
                sqlCmd += " where deptId like '" + ddl_dept.SelectedValue + "') and recordDate>='" + dt1 + "' and recordDate<='" + dt2 + "' order by recordDate";
            }
            else
            {
                sqlCmd = "select A.*,c.chineseName,d.deptName from OA_Attendence_Record A";
                sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.userId left join OA_sys_department d on d.id=c.deptId";
                sqlCmd += " where userId='" + userId + "'";
                sqlCmd += " and recordDate>='" + dt1 + "' and recordDate<='" + dt2 + "' order by recordDate";
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
                //bindGrid("all", DatePicker1.Text, DatePicker2.Text);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        private void loadUserInfo(string deptId)
        {
            string sqlCmd = "select * from OA_Sys_EmployeeInfo where deptId like '" + deptId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_emp.DataTextField = "chineseName";
            ddl_emp.DataValueField = "id";
            ddl_emp.DataSource = dt;
            ddl_emp.DataBind();
            this.ddl_emp.Items.Insert(0, new FineUI.ListItem("所有人员", "%"));
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
            string sqlCmd = "select A.*,c.chineseName,d.deptName from OA_Attendence_Record A";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.userId left join OA_sys_department d on d.id=c.deptId";
            sqlCmd += " where recordDate>='" + DatePicker1.Text + "' and recordDate<='" + DatePicker2.Text + "' order by recordDate";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

    }
}