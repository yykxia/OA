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
    public partial class UserMgmt_edit_password : System.Web.UI.Page
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                recvId = Request["id"];
                if (!string.IsNullOrEmpty(recvId)) 
                {
                    loadInfo();
                }
            }
        }

        //
        private void loadInfo() 
        {
            string sqlCmd = "select * from OA_Sys_EmployeeInfo where id=" + recvId;
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            label_loginId.Text = dt.Rows[0]["loginId"].ToString();
            label_chineseName.Text = dt.Rows[0]["chineseName"].ToString();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (txb_password.Text == txb_password_confirm.Text)
            {
                if (!string.IsNullOrEmpty(recvId))
                {
                    string pswMd5 = PasswordUtil.CreateDbPassword(txb_password.Text.Trim());
                    string sqlCmd = "update OA_Sys_EmployeeInfo set password='" + pswMd5 + "' where id=" + recvId;
                    SqlSel.ExeSql(sqlCmd);

                    Alert.Show("密码修改完成！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
                }
            }
            else 
            {
                Alert.ShowInTop("两次输入的密码不一致！请重新输入。");
                return;
            }
        }
    }
}