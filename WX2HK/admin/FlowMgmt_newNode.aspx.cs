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
    public partial class FlowMgmt_newNode : System.Web.UI.Page
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                recvId = Request["stepId"];
                if (!string.IsNullOrEmpty(recvId)) 
                {
                    loadDeptInfo();
                    loadDutyInfo();
                    loadStepInfo();
                }
            }
        }

        //加载部门信息
        private void loadDeptInfo() 
        {
            DataTable dt1 = new DataTable();
            string sqlCmd = "select * from OA_sys_department";
            SqlSel.GetSqlSel(ref dt1, sqlCmd);
            ddl_dept.DataValueField = "id";
            ddl_dept.DataTextField = "deptName";
            ddl_dept.DataSource = dt1;
            ddl_dept.DataBind();
            this.ddl_dept.Items.Insert(0, new FineUI.ListItem("本部门", "0"));
        }

        //加载部门信息
        private void loadDutyInfo()
        {
            DataTable dt1 = new DataTable();
            string sqlCmd = "select * from OA_sys_Duties";
            SqlSel.GetSqlSel(ref dt1, sqlCmd);
            ddl_duty.DataValueField = "id";
            ddl_duty.DataTextField = "DutyName";
            ddl_duty.DataSource = dt1;
            ddl_duty.DataBind();
        }

        private void loadStepInfo() 
        {
            string sqlCmd = "select * from OA_Sys_Flow_Step where id=" + recvId;
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            label_stepName.Text = dt.Rows[0]["stepName"].ToString();
        }
        
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(recvId))
            {
                string sqlCmd = "insert into OA_Sys_Step_Emp (stepId,deptId,dutyId) values ( ";
                sqlCmd += "'" + recvId + "','" + ddl_dept.SelectedValue + "','" + ddl_duty.SelectedValue + "')";
                SqlSel.ExeSql(sqlCmd);
                Alert.ShowInParent("保存成功，请关闭当前窗口查看。");
                SimpleForm1.Reset();
            }
        }
    }
}