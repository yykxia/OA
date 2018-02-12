using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IETCsoft.sql;
using FineUI;

namespace WX2HK.PropertyMgmt
{
    public partial class PropertyReq_office_AdminRegister_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tabId = Request["id"];
                label_tabId.Text = tabId;
                //加载现有用车申请信息
                loadInfo(tabId);
            }
        }

        private void loadInfo(string tabId)
        {
            string sqlCmd = "select A.*,c.chineseName,d.deptName from OA_OfficeSupply_Main A";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId where A.id='" + tabId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_reason.Text = dt.Rows[0]["others"].ToString();

            //加载申请用品明细
            reqList(tabId);
        }

        //用品明细
        private void reqList(string formId)
        {
            string sqlCmd = "select A.*,B.propertyName from OA_OfficeSupply_applyItem A left join OA_Property_Register B on A.propertyNo=B.propertyNo where officeSupplyId='" + formId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                string sqlCmd = "";
                if (modifiedDict.Count == Grid1.Rows.Count)
                {
                    foreach (int rowIndex in modifiedDict.Keys)
                    {
                        int itemId = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                        sqlCmd = "update OA_OfficeSupply_applyItem set actualCounts='" + Convert.ToDecimal(Grid1.Rows[rowIndex].Values[3]) + "' where id='" + itemId + "'";
                        SqlSel.ExeSql(sqlCmd);
                    }
                    sqlCmd = "update OA_OfficeSupply_Main set adminRegister='1' where id='" + label_tabId.Text + "'";
                    SqlSel.ExeSql(sqlCmd);
                    Alert.Show("已登记！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
                else
                {
                    Alert.ShowInTop("请先登记领用数量！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        protected void btn_veto_Click(object sender, EventArgs e)
        {
            string sqlCmd = "update OA_OfficeSupply_Main set adminRegister='-1' where id='" + label_tabId.Text + "'";
            SqlSel.ExeSql(sqlCmd);
            Alert.Show("已取消！");
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

    }
}