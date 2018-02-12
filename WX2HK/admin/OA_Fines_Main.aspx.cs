using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.Text;

namespace WX2HK.admin
{
    public partial class OA_Fines_Main : BasePage
    {
        private static string formId = string.Empty;//关联表单id
        private static string empId = string.Empty;//审批信息节点id
        private static string stepId = string.Empty;//表单当前审批节点
        private static string curStepId = string.Empty;//审批人的当前节点
        private static string checkStatus = string.Empty;//审批状态

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                empId = Request["tabId"];
                loadInfo(empId);
            }
        }

        private void loadInfo(string empId)
        {
            string sqlCmd = "select A.*,B.stepId,c.chineseName,d.deptName,t.projName from OA_Fines_Main A left join OA_Sys_step_empList B on B.formId=A.id";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId left join OA_sys_Project t on t.id=a.projId where B.id='" + empId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            curStepId = dt.Rows[0]["CurrentStepID"].ToString();
            stepId = dt.Rows[0]["stepId"].ToString();
            formId = dt.Rows[0]["id"].ToString();
            //保存申请人id
            txb_Hidden_reqMan.Text = dt.Rows[0]["reqMan"].ToString();

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_total.Text = dt.Rows[0]["fineAmount"].ToString();
            label_reason.Text = dt.Rows[0]["ReqReason"].ToString();
            label_proj.Text = dt.Rows[0]["projName"].ToString();
            label_obj.Text = dt.Rows[0]["Objects"].ToString();
            label_proveEmp.Text = dt.Rows[0]["ProveEmp"].ToString();

            //加载附件信息
            bindHidden(formId, "OA_Fines_Main");
            //审批信息
            bindCheckDetail(formId, "OA_Fines_Main");
            //加载证明人信息
            //loadProvInfo(formId);
            if (NodeChecked(empId))//已审批            
            {
                checkStatus = "1";
                btnSubmit.Hidden = true;
                btn_veto.Hidden = true;
                txa_adc.Hidden = true;
            }
            else
            {
                checkStatus = "0";
            }
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
        //        label_obj.Text = sb.ToString();
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

        //否决申请
        protected void btn_veto_Click(object sender, EventArgs e)
        {
            //插入节点审批明细信息
            string sqlCmd = "insert into OA_Sys_NodeDetail (NodeId,optTime,dealAdc,dealType) values ";
            sqlCmd += "('" + empId + "','" + DateTime.Now + "','" + txa_adc.Text.Trim() + "','-1')";
            SqlSel.ExeSql(sqlCmd);

            Alert.Show("已审批！");
            btn_veto.Hidden = true;
            btnSubmit.Hidden = true;
            bindCheckDetail(formId, "OA_Fines_Main");
            RedirectUrl();

        }
        //同意申请
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string nextStepId = "";

            //插入节点审批明细信息
            string sqlCmd = "insert into OA_Sys_NodeDetail (NodeId,optTime,dealAdc,dealType) values ";
            sqlCmd += "('" + empId + "','" + DateTime.Now + "','" + txa_adc.Text.Trim() + "','1')";
            SqlSel.ExeSql(sqlCmd);

            //是否执行下一步骤
            DataTable tempDt = new DataTable();
            if (curStepId == stepId)
            {
                sqlCmd = "select * from OA_Sys_Flow_Step where flowId=(select flowId from OA_Fines_Main where id ='" + formId + "')";
                SqlSel.GetSqlSel(ref tempDt, sqlCmd);
                DataRow[] dr = tempDt.Select("id='" + curStepId + "'");
                int sortNo = Convert.ToInt32(dr[0]["stepOrderNo"]);//当前审批步骤的序号
                int nextSortNo = sortNo + 1;//下一步骤的序号
                dr = tempDt.Select("stepOrderNo='" + nextSortNo + "'");
                if (dr.Length > 0)
                {
                    nextStepId = dr[0]["id"].ToString();
                    sqlCmd = "update OA_Fines_Main set CurrentStepID='" + nextStepId + "' where id='" + formId + "'";
                    SqlSel.ExeSql(sqlCmd);

                    //执行消息推送和审批人员的添加
                    pushMessage(nextStepId, "OA_Fines_Main", formId, "处罚流程");
                }
                else
                {
                    sqlCmd = "update OA_Fines_Main set CurrentStepID='0' where id='" + formId + "'";
                    SqlSel.ExeSql(sqlCmd);

                    //只会流程发起人
                    WX2HK.ReturnInfo.pushMessage(txb_Hidden_reqMan.Text, "审批完成", "", "您提交的处罚流程已审批完成。", "");
                }

            }
            Alert.Show("已审批！");
            btn_veto.Hidden = true;
            btnSubmit.Hidden = true;
            bindCheckDetail(formId, "OA_Fines_Main");
            RedirectUrl();
        }


        private void RedirectUrl()
        {
            if (checkStatus == "0")
            {
                PageContext.RegisterStartupScript("redictPage();");
            }
            if (checkStatus == "1")
            {
                PageContext.RegisterStartupScript("redictPage_His();");
            }
        }
        private void bindCheckDetail(string formId, string formDataName)
        {
            DataTable checkDt = getCheckDtl(formId, formDataName);
            Grid1.DataSource = checkDt;
            Grid1.DataBind();
        }
        protected void btn_return_Click(object sender, EventArgs e)
        {
            RedirectUrl();
        }
    }
}