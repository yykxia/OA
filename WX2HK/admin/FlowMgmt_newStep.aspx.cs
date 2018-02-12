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
    public partial class FlowMgmt_newStep : System.Web.UI.Page
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                recvId = Request["FlowId"];
                if (!string.IsNullOrEmpty(recvId)) 
                {
                    loadInfo();
                }
            }
        }

        //
        private void loadInfo() 
        {
            string sqlCmd = "select * from OA_sys_flow where id=" + recvId;
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            label_flowName.Text = dt.Rows[0]["flowName"].ToString();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(recvId))
            {
                string sqlCmd = "insert into OA_Sys_Flow_Step (flowId,stepName,stepOrderNo) values (";
                sqlCmd += "'" + recvId + "','" + txb_stepName.Text.Trim() + "','" + getMaxSort() + "')";
                SqlSel.ExeSql(sqlCmd);
                Alert.ShowInParent("编辑成功，请退出当前窗口！");
                SimpleForm1.Reset();

                //PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
            }
        }

        //获取当前流程最大步骤号
        private int getMaxSort() 
        {
            int maxSort = 0;
            DataTable dt = new DataTable();
            string sqlCmd = "select isnull(max(stepOrderNo),0) as maxSort from OA_Sys_Flow_Step where flowId=" + recvId;
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            maxSort = Convert.ToInt32(dt.Rows[0]["maxSort"]) + 1;
            return maxSort;
        }
    }
}