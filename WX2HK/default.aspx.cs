using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IETCsoft.sql;
using System.Data;
using FineUI;

namespace WX2HK
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadData();
                tbxUserName.Focus();
            }
        }

        private void LoadData()
        {
            InitCaptchaCode();
        }

        /// <summary>
        /// 初始化验证码
        /// </summary>
        private void InitCaptchaCode()
        {
            // 创建一个 6 位的随机数并保存在 Session 对象中
            Session["CaptchaImageText"] = GenerateRandomCode();
            imgCaptcha.ImageUrl = "~/captcha/captcha.ashx?w=150&h=30&t=" + DateTime.Now.Ticks;
        }

        /// <summary>
        /// 创建一个 6 位的随机数
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomCode()
        {
            string s = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                s += random.Next(10).ToString();
            }
            return s;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitCaptchaCode();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CaptchaImageText"] == null) 
                {
                    Alert.ShowInTop("验证码已过期!请重试。");
                    InitCaptchaCode();
                    tbxCaptcha.Text = "";
                    return;
                }
                if (tbxCaptcha.Text != Session["CaptchaImageText"].ToString())
                {
                    Alert.ShowInTop("验证码错误！");
                    tbxCaptcha.Text = "";
                    return;
                }

                string userName = tbxUserName.Text.Trim();
                string password = tbxPassword.Text;

                string sqlCmd = "";
                DataTable dt = new DataTable();
                sqlCmd = "select * from OA_Sys_EmployeeInfo where loginId='" + userName + "'";
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                if (dt.Rows.Count > 0)
                {
                    if (Session["loginUser"] != null)
                    {
                        if (Session["loginUser"].ToString() != dt.Rows[0]["id"].ToString())
                        {
                            Alert.ShowInTop("当前浏览器已经登录一个用户，不允许多个用户重复登录！");
                            return;
                        }
                    }

                    if (PasswordUtil.ComparePasswords(dt.Rows[0]["password"].ToString(), password))
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["useStatus"]))
                        {
                            //保存登录用户id
                            Session["loginUser"] = dt.Rows[0]["id"];
                            Response.Redirect("main.aspx");
                        }
                        else
                        {
                            Alert.ShowInTop("用户未启用，请联系管理员！");
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("用户名或密码错误！");
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("用户不存在！");
                    return;
                }


            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }

        }
    }
}