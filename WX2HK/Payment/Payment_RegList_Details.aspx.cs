using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.Text;

namespace WX2HK.Payment
{
    public partial class Payment_RegList_Details : System.Web.UI.Page
    {
        private static string formId = string.Empty;//关联表单id

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formId = Request["tabId"];
                loadInfo(formId);
            }
        }

        private void loadInfo(string formId)
        {
            string sqlCmd = "select A.*,B.stepId,c.chineseName,d.deptName,t.projName,(case when isAdvPay='0' then '预付' else '应付' end) as payType from OA_PayMent_Main A left join OA_Sys_step_empList B on B.formId=A.id";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId left join OA_sys_Project t on t.id=a.projId where A.id='" + formId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_payTotal.Text = dt.Rows[0]["payTotal"].ToString();
            label_reason.Text = dt.Rows[0]["others"].ToString();
            label_proj.Text = dt.Rows[0]["projName"].ToString();
            label_type.Text = dt.Rows[0]["payType"].ToString();

            //加载附件信息
            bindHidden(formId, "OA_PayMent_Main");
            //
            bindCheckDetail(formId, "OA_PayMent_Main");

        }

        private void bindHidden(string formId, string tabName)
        {
            StringBuilder sb = new StringBuilder();
            string sqlCmd = "select * from OA_Sys_files where formID='" + formId + "' and formDataName='" + tabName + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid2.DataSource = dt;
            Grid2.DataBind();
        }

        private void bindCheckDetail(string formId, string formDataName)
        {
            string sqlCmd = "select C.chineseName,B.dealAdc,B.optTime,isnull((case when dealType='1' then '同意' when dealType='-1' then '否决' end),'未阅') as nodeAdc from OA_Sys_step_empList A left join OA_Sys_NodeDetail B on A.ID=B.NodeId ";
            sqlCmd += "left join OA_Sys_EmployeeInfo C on C.id= A.userid ";
            sqlCmd += "where A.formId='" + formId + "' and formDataName='" + formDataName + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

    }
}