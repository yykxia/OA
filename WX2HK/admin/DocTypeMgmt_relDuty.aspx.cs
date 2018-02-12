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
    public partial class DocTypeMgmt_relDuty : System.Web.UI.Page
    {
        private static string typeId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                typeId = Request["id"];
                loadDutyList();
            }
        }

        private void loadDutyList() 
        {
            string sqlCmd = "select * from OA_sys_Duties";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            cbl_duty.DataValueField = "id";
            cbl_duty.DataTextField = "DutyName";
            cbl_duty.DataSource = dt;
            cbl_duty.DataBind();


            //加载已绑定岗位列表
            sqlCmd = "select * from OA_DocMgmt_DocType_RelDuty where docTypeId='" + typeId + "'";
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            string[] formArray = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    formArray[i] = dt.Rows[i]["dutyId"].ToString();
                }
                catch (Exception ex)
                {
                    Alert.ShowInTop(ex.Message);
                }
            }
            cbl_duty.SelectedValueArray = formArray;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";

            //删除现有关联表单数据
            sqlCmd = "delete from OA_DocMgmt_DocType_RelDuty where docTypeId='" + typeId + "'";
            SqlSel.ExeSql(sqlCmd);
            //插入关联表单
            string[] formArray = cbl_duty.SelectedValueArray;
            foreach (string selectItem in formArray)
            {
                sqlCmd = "insert into OA_DocMgmt_DocType_RelDuty ([dutyId],[docTypeId]) values ('" + selectItem + "','" + typeId + "')";
                SqlSel.ExeSql(sqlCmd);
            }
            Alert.Show("保存成功！");
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}