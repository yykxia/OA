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
    public partial class MissionList : BasePage
    {
        private static string curUserId = string.Empty;//当前用户id

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                if (Session["wxUserId"] == null)
                {
                    string code = string.Empty;
                    code = HttpContext.Current.Request.QueryString["Code"];
                    string curPageName = "admin%2fMissionList.aspx";
                    curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                    Session["wxUserId"] = curUserId;
                }
                else 
                {
                    curUserId = Session["wxUserId"].ToString();
                }
                bindGrid(curUserId);

                //PageContext.RegisterStartupScript(Window1.GetShowReference() + Window1.GetMaximizeReference());
            }
        }

        private void bindGrid(string userId) 
        {
            //string userName = GetUser();
            //string userName = "2673";
            //string sqlCmd = "select top 1 id from OA_Sys_EmployeeInfo where loginId='" + userName + "'";
            //string userId = SqlSel.GetSqlScale(sqlCmd).ToString();
            string sqlCmd = "select (select chineseName from OA_Sys_EmployeeInfo where id=userId) as chineseName,* from OA_Sys_step_empList";
            sqlCmd += " where userId='" + userId + "' and id not in (select nodeId from OA_Sys_NodeDetail)";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            dt.Columns.Add("reqMan");
            dt.Columns.Add("reqDte",typeof(DateTime));
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
            dt.DefaultView.Sort = "reqDte DESC";
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            bindGrid(curUserId);
        }
    }
}