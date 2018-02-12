using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.Text;

namespace WX2HK.Bills
{
    public partial class Bills_RegList_Details : System.Web.UI.Page
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
            string sqlCmd = "select A.*,B.stepId,c.chineseName,d.deptName,t.projName from OA_Bills_Main A left join OA_Sys_step_empList B on B.formId=A.id";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId left join OA_sys_Project t on t.id=a.RelProj where A.id='" + formId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_count.Text = dt.Rows[0]["VoucherCount"].ToString();
            label_total.Text = dt.Rows[0]["billTotal"].ToString();
            label_reason.Text = dt.Rows[0]["ReqReason"].ToString();
            label_proj.Text = dt.Rows[0]["projName"].ToString();
            label_proveEmp.Text = dt.Rows[0]["ProveEmp"].ToString();

            //加载附件信息
            bindHidden(formId, "OA_Bills_Main");
            //加载证明人信息
            //loadProvInfo(formId);

            //
            loadCostItems(formId);

            //
            bindCheckDetail(formId, "OA_Bills_Main");

        }


        //private void loadProvInfo(string formId)
        //{
        //    string sqlCmd = "select B.chineseName from OA_Bills_ProveEmp A left join OA_Sys_EmployeeInfo B on A.ProveEmpId=B.id where A.formId='" + formId + "'";
        //    DataTable dt = new DataTable();
        //    SqlSel.GetSqlSel(ref dt, sqlCmd);
        //    if (dt.Rows.Count > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sb.AppendFormat("{0}/", dt.Rows[i]["chineseName"].ToString());
        //        }
        //        label_proveEmp.Text = sb.ToString();
        //    }
        //}

        //加载费用项
        private void loadCostItems(string formId)
        {
            string sqlCmd = "select B.CostItemName from OA_Bills_RelCostItems A left join OA_Bills_costLists B ON A.CostItemId=B.ID where formId='" + formId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            if (dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.AppendFormat("{0}/", dt.Rows[i]["CostItemName"].ToString());
                }
                label_costItems.Text = sb.ToString();
            }
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

        private void bindCheckDetail(string formId,string formDataName)
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