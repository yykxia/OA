using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.Fines
{
    public partial class FineDetail_reqList_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string curUser = Request["userId"];
                //加载历史申请明细
                loadHisReqList(curUser);
            }
        }


        private void loadHisReqList(string userId)
        {
            string sqlCmd = "select A.*,isnull(B.stepName,'已完成') as checkStatus,C.flowName from OA_Fines_Main A";
            sqlCmd += " left join OA_sys_flow C ON C.ID=A.FlowId";
            sqlCmd += " LEFT JOIN OA_Sys_Flow_Step B ON A.CurrentStepID=B.id WHERE reqMan='" + userId + "' order by reqDte desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
    }
}