﻿using System;
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
    public partial class OA_Car_Main_m : BasePage
    {
        private static string formId = string.Empty;//关联表单id
        private static string empId = string.Empty;//审批信息节点id
        private static string stepId = string.Empty;//表单当前审批节点
        private static string curStepId = string.Empty;//审批人的当前节点
        private static string checkStatus = string.Empty;//审批状态

        public string CarMgmt_TimeStamp = "";
        public string CarMgmt_Nonce = "";
        public string CarMgmt_MsgSig = "";
        private static string curUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                empId = Request["tabId"];
                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = string.Format("admin%2fOA_Car_Main_m.aspx?tabId={0}", empId);
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                CarMgmt_TimeStamp = WX2HK.ReturnInfo.GetTimeStamp();
                CarMgmt_Nonce = WX2HK.ReturnInfo.randNonce();
                string curUrl = string.Format("admin/OA_Car_Main_m.aspx?tabId={0}&code={1}&state=STATE", empId, code);
                CarMgmt_MsgSig = WX2HK.VerifyLegal.createSign(CarMgmt_Nonce, CarMgmt_TimeStamp, curUrl);

                loadInfo(empId);
            }
        }

        private void loadInfo(string empId)
        {
            string sqlCmd = "select A.*,B.stepId,c.chineseName,d.deptName,t.flowName,f.projName,";
            sqlCmd += "(case when deptCarId is null then A.carNumb else tt.propertyName end) as carName from OA_Car_Main A left join OA_Sys_step_empList B on B.formId=A.id";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId left join OA_sys_Project f on f.id=a.projId";
            sqlCmd += " left join OA_sys_flow t on t.id=A.flowId left join OA_Property_Register tt on tt.id=A.deptCarId where B.id='" + empId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            curStepId = dt.Rows[0]["CurrentStepID"].ToString();
            stepId = dt.Rows[0]["stepId"].ToString();
            formId = dt.Rows[0]["id"].ToString();

            txb_Hidden_reqMan.Text = dt.Rows[0]["reqMan"].ToString();//流程发起人

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_carName.Text = dt.Rows[0]["carName"].ToString();
            label_strTime.Text = dt.Rows[0]["useDate"].ToString();
            label_endTime.Text = dt.Rows[0]["backTime"].ToString();
            label_curMileage.Text = dt.Rows[0]["curMileage"].ToString();
            label_toWhere.Text = dt.Rows[0]["toWhere"].ToString();
            label_reason.Text = dt.Rows[0]["useReason"].ToString();
            label_withWho.Text = dt.Rows[0]["withWho"].ToString();
            label_projName.Text = dt.Rows[0]["projName"].ToString();

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

            //加载附件信息
            bindHidden(formId, "OA_Car_Main");
            //加载证明人信息
            //loadProvInfo(formId);
            //审批信息
            bindCheckDetail(formId, "OA_Car_Main");

        }


        private void bindHidden(string formId, string tabName)
        {
            StringBuilder sb = new StringBuilder();
            string sqlCmd = "select * from OA_Sys_files where formID='" + formId + "' and formDataName='" + tabName + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            dt.Columns.Add("userId");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["userId"] = curUserId;
            }
            Grid1.DataSource = dt;
            Grid1.DataBind();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    sb.AppendFormat("{0};", dt.Rows[i]["fileName"].ToString());
            //}
            //if (!string.IsNullOrEmpty(sb.ToString()))
            //{
            //    hidden_field.Value = sb.ToString().Substring(0, sb.ToString().Length - 1);
            //}
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
            bindCheckDetail(formId, "OA_Car_Main");
            //只会流程发起人
            //WX2HK.ReturnInfo.pushMessage(txb_Hidden_reqMan.Text, "审批流程终止", "", "您提交的报销申请已审批完成", "");

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
                sqlCmd = "select * from OA_Sys_Flow_Step where flowId=(select flowId from OA_Car_Main where id ='" + formId + "')";
                SqlSel.GetSqlSel(ref tempDt, sqlCmd);
                DataRow[] dr = tempDt.Select("id='" + curStepId + "'");
                int sortNo = Convert.ToInt32(dr[0]["stepOrderNo"]);//当前审批步骤的序号
                int nextSortNo = sortNo + 1;//下一步骤的序号
                dr = tempDt.Select("stepOrderNo='" + nextSortNo + "'");
                if (dr.Length > 0)
                {
                    nextStepId = dr[0]["id"].ToString();
                    sqlCmd = "update OA_Car_Main set CurrentStepID='" + nextStepId + "' where id='" + formId + "'";
                    SqlSel.ExeSql(sqlCmd);

                    //执行消息推送和审批人员的添加
                    pushMessage(nextStepId, "OA_Car_Main", formId, "用车申请");
                }
                else
                {
                    //流程结束
                    sqlCmd = "update OA_Car_Main set CurrentStepID='0' where id='" + formId + "'";
                    SqlSel.ExeSql(sqlCmd);
                    //只会流程发起人
                    WX2HK.ReturnInfo.pushMessage(txb_Hidden_reqMan.Text, "审批完成", "", "您提交的用车申请已审批完成。", "");
                }

            }
            Alert.Show("已审批！");
            btn_veto.Hidden = true;
            btnSubmit.Hidden = true;
            bindCheckDetail(formId, "OA_Car_Main");
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
            Grid2.DataSource = checkDt;
            Grid2.DataBind();
        }

        protected void btn_return_Click(object sender, EventArgs e)
        {
            RedirectUrl();
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
        }
    }
}