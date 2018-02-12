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
    public partial class FlowMgmt_flowEdit : BasePage
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                recvId = Request["id"];//流程id

                DutyList();

                //加载流程信息
                if (!string.IsNullOrEmpty(recvId)) 
                {
                    loadInfo();
                }
            }
        }
        //加载所有岗位
        private void DutyList() 
        {
            string sqlCmd = "select * from OA_sys_Duties";
            DataTable allDutyDt = new DataTable();
            SqlSel.GetSqlSel(ref allDutyDt, sqlCmd);
            cbl_relDuty.DataTextField = "DutyName";
            cbl_relDuty.DataValueField = "Id";
            cbl_relDuty.DataSource = allDutyDt;
            cbl_relDuty.DataBind();
        }
        //
        private void loadInfo() 
        {
            string sqlCmd = "select * from OA_sys_flow where id=" + recvId; ;
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            txb_flowName.Text = dt.Rows[0]["flowName"].ToString();
            txa_flowDesc.Text = dt.Rows[0]["flowDesc"].ToString();

            int useStatus = Convert.ToInt32(dt.Rows[0]["isUsed"]);
            //是否在用
            if (useStatus == 0)
            {
                ckeckBox_enabled.Checked = false;
            }
            else
            {
                ckeckBox_enabled.Checked = true;
            }

            //加载表单关联信息
            sqlCmd = "select * from OA_Sys_FlowRelForm where flowId='" + recvId + "'";
            DataTable temDt = new DataTable();
            SqlSel.GetSqlSel(ref temDt, sqlCmd);
            string[] formArray = new string[temDt.Rows.Count];
            for (int i = 0; i < temDt.Rows.Count; i++) 
            {
                try
                {
                    formArray[i] = temDt.Rows[i]["formDataName"].ToString();

                }
                catch (Exception ex) 
                {
                    Alert.ShowInTop(ex.Message,"插入表单信息出错！");
                }
            }
            cbl_relForm.SelectedValueArray = formArray;

            //关联岗位勾选
            sqlCmd = "select * from OA_Sys_FlowRelDuty where flowId='" + recvId + "'";
            DataTable dutyDt = new DataTable();
            SqlSel.GetSqlSel(ref dutyDt, sqlCmd);
            string[] dutyArray = new string[dutyDt.Rows.Count];
            for (int i = 0; i < dutyDt.Rows.Count; i++)
            {
                try
                {
                    dutyArray[i] = dutyDt.Rows[i]["dutyId"].ToString();

                }
                catch (Exception ex)
                {
                    Alert.ShowInTop(ex.Message, "插入岗位信息出错！");
                }
            }
            cbl_relDuty.SelectedValueArray = dutyArray;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int useStatus = ckeckBox_enabled.Checked ? 1 : 0;
            if (string.IsNullOrEmpty(recvId))
            {
                sqlCmd = "insert into OA_sys_flow (flowName,createTime,isUsed,flowDesc) values (";
                sqlCmd += "'" + txb_flowName.Text + "','" + DateTime.Now + "','" + useStatus + "','" + txa_flowDesc.Text + "')";
                SqlSel.ExeSql(sqlCmd);

                //获取流程id
                sqlCmd = "select max(id) from OA_sys_flow";
                int flowId = Convert.ToInt32(SqlSel.GetSqlScale(sqlCmd));
                //插入关联表单
                string[] formArray = cbl_relForm.SelectedValueArray;
                foreach (string selectItem in formArray)
                {
                    sqlCmd = "insert into OA_Sys_FlowRelForm ([flowId],[formDataName]) values ('" + flowId + "','" + selectItem + "')";
                    SqlSel.ExeSql(sqlCmd);
                }

                //维护关联岗位数据
                sqlCmd = "delete from OA_Sys_FlowRelDuty where flowid='" + flowId + "'";
                SqlSel.ExeSql(sqlCmd);
                //插入关联表单
                string[] dutyArray = cbl_relDuty.SelectedValueArray;
                foreach (string selectItem in dutyArray)
                {
                    sqlCmd = "insert into OA_Sys_FlowRelDuty ([flowId],[dutyId]) values ('" + flowId + "','" + selectItem + "')";
                    SqlSel.ExeSql(sqlCmd);
                }
            }
            else 
            {
                sqlCmd = "update OA_sys_flow set flowName='" + txb_flowName.Text + "',createTime='" + DateTime.Now + "',isUsed='" + useStatus + "',flowDesc='" + txa_flowDesc.Text + "' where id=" + recvId;
                SqlSel.ExeSql(sqlCmd);

                //删除现有关联表单数据
                sqlCmd = "delete from OA_Sys_FlowRelForm where flowid='" + recvId + "'";
                SqlSel.ExeSql(sqlCmd);
                //插入关联表单
                string[] formArray = cbl_relForm.SelectedValueArray;
                foreach (string selectItem in formArray)
                {
                    sqlCmd = "insert into OA_Sys_FlowRelForm ([flowId],[formDataName]) values ('" + recvId + "','" + selectItem + "')";
                    SqlSel.ExeSql(sqlCmd);
                }

                //维护关联岗位数据
                sqlCmd = "delete from OA_Sys_FlowRelDuty where flowid='" + recvId + "'";
                SqlSel.ExeSql(sqlCmd);
                //插入关联表单
                string[] dutyArray = cbl_relDuty.SelectedValueArray;
                foreach (string selectItem in dutyArray)
                {
                    sqlCmd = "insert into OA_Sys_FlowRelDuty ([flowId],[dutyId]) values ('" + recvId + "','" + selectItem + "')";
                    SqlSel.ExeSql(sqlCmd);
                }

            }
            Alert.Show("保存成功！请关闭当前窗口。");
            //PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }
    }
}