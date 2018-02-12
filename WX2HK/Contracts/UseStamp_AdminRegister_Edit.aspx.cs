using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IETCsoft.sql;
using FineUI;

namespace WX2HK.Contracts
{
    public partial class UseStamp_AdminRegister_Edit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tabId = Request["id"];
                label_tabId.Text = tabId;
                //加载申请信息
                loadInfo(tabId);

                btn_addFlow.OnClientClick = Window2.GetSaveStateReference(txa_defFlow.ClientID, txb_flowId.ClientID)
                    + Window2.GetShowReference("~/admin/FlowMgmt_SelfDefined.aspx");
            }
        }

        private void loadInfo(string tabId)
        {
            string sqlCmd = "select A.*,c.chineseName,d.deptName,flowName from OA_UseStamp_Main A left join oa_sys_flow on A.stampType=oa_sys_flow.id";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId where A.id='" + tabId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_reason.Text = dt.Rows[0]["useFor"].ToString();
            label_stampType.Text = dt.Rows[0]["flowName"].ToString();
            //加载附件信息
            bindHidden(label_tabId.Text, "OA_UseStamp_Main");
            //审批信息
            DataTable checkDt = getCheckDtl(label_tabId.Text, "OA_UseStamp_Main");
            Grid2.DataSource = checkDt;
            Grid2.DataBind();
        }

        private void bindHidden(string formId, string tabName)
        {
            string sqlCmd = "select * from OA_Sys_files where formID='" + formId + "' and formDataName='" + tabName + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //是否增加审批流程
                if (string.IsNullOrEmpty(txb_flowId.Text))
                {
                    string sqlCmd = "update OA_UseStamp_Main set adminRegister='1' where id='" + label_tabId.Text + "'";
                    SqlSel.ExeSql(sqlCmd);
                    Alert.Show("已盖章！");
                }
                else
                {
                    string stepId = getStepId(txb_flowId.Text);//获取步骤id
                    string sqlCmd = "update OA_UseStamp_Main set flowId='" + txb_flowId.Text + "',CurrentStepID='" + stepId + "' where id='" + label_tabId.Text + "'";
                    int exeCount = SqlSel.ExeSql(sqlCmd);
                    if (exeCount > 0)
                    {
                        //推送信息至相关审批人
                        pushMessage(stepId, "OA_UseStamp_Main", label_tabId.Text, "用印申请");
                        Alert.Show("已提交审批！");
                    }
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        protected void btn_veto_Click(object sender, EventArgs e)
        {
            string sqlCmd = "update OA_UseStamp_Main set adminRegister='-1' where id='" + label_tabId.Text + "'";
            SqlSel.ExeSql(sqlCmd);
            Alert.Show("已取消！");
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        private string getStepId(string flowId)
        {
            string sqlCmd = "select id from OA_Sys_Flow_Step where flowid='" + flowId + "' and stepOrderNo=1";
            string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
            return stepId;
        }

    }
}