using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WX2HK.PropertyMgmt
{
    public partial class PropertyReq_office_sub : BasePage
    {
        private bool AppendToEnd = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {

                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                JObject defaultObj = new JObject();
                defaultObj.Add("propertyName", "");
                defaultObj.Add("applyCounts", "1");
                defaultObj.Add("applyUnit", "个");
                defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));


                // 在第一行新增一条数据
                btn_add.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

                //加载办公用品类目
                loadProperty();


                //加载流程
                loadFlowInfo("OA_OfficeSupply_Main", GetUser());

                //加载申请人信息
                loadInfo(GetUser());
            }
        }

        //// 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }


        private void loadFlowInfo(string formName, string userId)
        {
            try
            {
                DataTable dt = validFlow(formName, userId);
                ddl_flow.DataTextField = "flowName";
                ddl_flow.DataValueField = "id";
                ddl_flow.DataSource = dt;
                ddl_flow.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }


        private void loadInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                string sqlCmd = "select A.*,B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id='" + userId + "'";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                label_name.Text = dt.Rows[0]["loginId"].ToString() + "/" + dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
                label_date.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void loadProperty() 
        {
            string sqlCmd = "select (propertyNo + '-' + propertyName) as propertyName from OA_Property_Register where propertyType='1' and UseStatus='1' order by propertyName";//类型为办公用品且可用的类目
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_propertyName.DataValueField = "propertyName";
            ddl_propertyName.DataTextField = "propertyName";

            ddl_propertyName.DataSource = dt;
            ddl_propertyName.DataBind();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                if (newAddedList.Count > 0)
                {
                    string stepId = getStepId(ddl_flow.SelectedValue);

                    string sqlCmd = "insert into OA_OfficeSupply_Main (reqMan,reqDte,FlowId,CurrentStepID,others) values ('" + GetUser() + "','" + DateTime.Now + "',";
                    sqlCmd += "'" + ddl_flow.SelectedValue + "','" + stepId + "','" + txa_reason.Text + "')";
                    int exeCount = SqlSel.ExeSql(sqlCmd);
                    if (exeCount > 0)
                    {
                        //取当前单据id
                        sqlCmd = "select max(id) from OA_OfficeSupply_Main";
                        string formId = SqlSel.GetSqlScale(sqlCmd).ToString();
                        //插入申请明细
                        for (int i = 0; i < newAddedList.Count; i++)
                        {
                            string propertyName = newAddedList[i]["propertyName"].ToString();
                            string propertyNo = propertyName.Substring(0, propertyName.IndexOf("-"));
                            sqlCmd = "insert into OA_OfficeSupply_applyItem (officeSupplyId,propertyNo,applyCounts,applyUnit) values ('" + formId + "','" + propertyNo + "',";
                            sqlCmd += "'" + Convert.ToDecimal(newAddedList[i]["applyCounts"]) + "','" + newAddedList[i]["applyUnit"] + "')";
                            SqlSel.ExeSql(sqlCmd);
                        }

                        Alert.Show("提交成功！");
                        SimpleForm1.Reset();
                        Grid1.DataSource = null;
                        Grid1.DataBind();

                        pushMessage(stepId, "OA_OfficeSupply_Main", formId, "办公用品申请");
                    }
                    else
                    {
                        Alert.ShowInTop("提交失败！");
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("尚未添加申领物品，不可提交！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        private string getStepId(string flowId)
        {
            string sqlCmd = "select id from OA_Sys_Flow_Step where flowid='" + flowId + "' and stepOrderNo=1";
            string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
            return stepId;
        }
    }
}