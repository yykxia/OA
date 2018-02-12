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
    public partial class MissionList_pc : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid(GetUser());
            }
        }

        private void bindGrid(string userId)
        {
            try
            {
                string sqlCmd = "select (select chineseName from OA_Sys_EmployeeInfo where id=userId) as chineseName,* from OA_Sys_step_empList";
                sqlCmd += " where userId='" + userId + "' and id not in (select nodeId from OA_Sys_NodeDetail)";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                dt.Columns.Add("reqMan");
                dt.Columns.Add("reqDte", typeof(DateTime));
                dt.Columns.Add("flowName");
                foreach (DataRow dr in dt.Rows)
                {
                    string formName = dr["formDataName"].ToString();
                    string formId = dr["formId"].ToString();
                    sqlCmd = "select a.*,b.chineseName,c.flowName from " + formName + " a left join OA_Sys_EmployeeInfo b  on a.reqMan = b.id left join OA_sys_flow c on a.flowId=c.id where a.id='" + dr["formId"] + "'";
                    DataTable temDt = new DataTable();
                    SqlSel.GetSqlSel(ref temDt, sqlCmd);
                    dr["reqMan"] = temDt.Rows[0]["chineseName"];
                    dr["reqDte"] = temDt.Rows[0]["reqDte"];
                    dr["flowName"] = dr["formName"].ToString() + "(" + temDt.Rows[0]["flowName"] + ")";   
                }
                dt.DefaultView.Sort = "reqDte desc";
                Grid1.DataSource = dt;
                Grid1.DataBind();
            }
            catch (Exception ex) 
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            bindGrid(GetUser());
        }

        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            bindGrid(GetUser());
        }

    }
}