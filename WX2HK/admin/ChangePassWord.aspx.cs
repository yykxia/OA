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
    public partial class ChangePassWord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                loadInfo();
            }
        }


        //
        private void loadInfo()
        {
            string sqlCmd = "select * from OA_Sys_EmployeeInfo where id=" + GetUser();
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            label_loginId.Text = dt.Rows[0]["loginId"].ToString();
            label_chineseName.Text = dt.Rows[0]["chineseName"].ToString();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (passwordIsOk(txb_origPsw.Text))
            {
                if (txb_password.Text == txb_password_confirm.Text)
                {

                    string pswMd5 = PasswordUtil.CreateDbPassword(txb_password.Text.Trim());
                    string sqlCmd = "update OA_Sys_EmployeeInfo set password='" + pswMd5 + "' where id=" + GetUser();
                    SqlSel.ExeSql(sqlCmd);

                    SimpleForm1.Reset();

                    Alert.Show("密码修改完成！");
                }
                else
                {
                    Alert.ShowInTop("两次输入的密码不一致！请重新输入。");
                    return;
                }

            }
            else
            {
                Alert.ShowInTop("原密码不匹配！");
                return;
            }
        }

        //验证与原密码是否匹配
        private bool passwordIsOk(string inputStr) 
        {
            string sqlCmd = "select password from OA_Sys_EmployeeInfo where id='" + GetUser() + "'";
            string dbPsw = SqlSel.GetSqlScale(sqlCmd).ToString();
            if (PasswordUtil.ComparePasswords(dbPsw, inputStr))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}