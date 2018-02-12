using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.CarMgmt
{
    public partial class CarMgmt_AdminRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                loadReqList();
            }
        }

        //加载已审批用车申请
        private void loadReqList() 
        {
            //加载所有已审批完成行政未作废的用车申请
            string sqlCmd = "select (select A.chineseName + '/' + B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id=OA_Car_Main.reqMan) AS reqName,*";
            sqlCmd += ",(case when adminRegister='0' then '待发车' when adminRegister='1' then '待返还' end) as curStatus from OA_Car_Main where CurrentStepId='0' and adminRegister <> '-1' and adminRegister <> '2' order by adminRegister";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
    }
}