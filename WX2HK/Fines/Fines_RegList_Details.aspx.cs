using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.Text;

namespace WX2HK.Fines
{
    public partial class Fines_RegList_Details : System.Web.UI.Page
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
            string sqlCmd = "select A.*,B.stepId,c.chineseName,d.deptName,t.projName from OA_Fines_Main A left join OA_Sys_step_empList B on B.formId=A.id";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId left join OA_sys_Project t on t.id=a.ProjId where A.id='" + formId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_total.Text = dt.Rows[0]["fineAmount"].ToString();
            label_reason.Text = dt.Rows[0]["ReqReason"].ToString();
            label_proj.Text = dt.Rows[0]["projName"].ToString();
            label_count.Text = dt.Rows[0]["Objects"].ToString();
            label_proveEmp.Text = dt.Rows[0]["ProveEmp"].ToString();

            //加载附件信息
            bindHidden(formId, "OA_Fines_Main");
            //加载证明人信息
            //loadProvInfo(formId);

            //
            //loadCostItems(formId);

        }

        //加载证明人
        //private void loadProvInfo(string formId)
        //{
        //    string sqlCmd = "select B.chineseName from OA_Fines_ProveEmp A left join OA_Sys_EmployeeInfo B on A.userid=B.id where A.formId='" + formId + "'";
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

        //加载处罚对象
        //private void loadCostItems(string formId)
        //{
        //    string sqlCmd = "select B.chineseName from OA_Fines_Objects A left join OA_Sys_EmployeeInfo B ON A.userid=B.ID where formId='" + formId + "'";
        //    DataTable dt = new DataTable();
        //    SqlSel.GetSqlSel(ref dt, sqlCmd);
        //    if (dt.Rows.Count > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sb.AppendFormat("{0}/", dt.Rows[i]["chineseName"].ToString());
        //        }
        //        label_count.Text = sb.ToString();
        //    }
        //}

        private void bindHidden(string formId, string tabName)
        {
            StringBuilder sb = new StringBuilder();
            string sqlCmd = "select * from OA_Sys_files where formID='" + formId + "' and formDataName='" + tabName + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid2.DataSource = dt;
            Grid2.DataBind();
        }
    }
}