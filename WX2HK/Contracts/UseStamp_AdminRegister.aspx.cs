using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.Contracts
{
    public partial class UseStamp_AdminRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                loadReqList();
            }
        }

        //加载待盖章的用印申请
        private void loadReqList()
        {
            //加载所有已审批完成盖章的用印申请
            string sqlCmd = "select (select A.chineseName + '/' + B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id=reqMan) AS reqName,*";
            sqlCmd += " from OA_UseStamp_Main where CurrentStepId='0' and adminRegister = '0' order by reqDte desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
    }
}