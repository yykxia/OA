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
    public partial class UserMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //加载人员信息
                bindGrid("");

                btn_add.OnClientClick = Window1.GetShowReference("UserMgmt_edit.aspx");
            }
        }

        private void bindGrid(string queryStr) 
        {
            string sqlCmd = string.Empty;
            if (string.IsNullOrEmpty(queryStr))
            {
                sqlCmd = "select A.*,B.deptName,C.dutyName from OA_Sys_EmployeeInfo A left join OA_sys_department B ON B.ID=A.deptId Left join OA_sys_Duties C on C.ID=A.dutyId order by loginId";
            }
            else 
            {
                sqlCmd = "select A.*,B.deptName,C.dutyName from OA_Sys_EmployeeInfo A left join OA_sys_department B ON B.ID=A.deptId Left join OA_sys_Duties C on C.ID=A.dutyId where loginId like '%" + queryStr + "%' order by loginId";
            }
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        protected void trgb_loginId_TriggerClick(object sender, EventArgs e)
        {
            bindGrid(trgb_loginId.Text);
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            bindGrid(trgb_loginId.Text);
        }

        protected void btn_synCorpToWx_Click(object sender, EventArgs e)
        {
            DataTable wxUserDt = new DataTable();
            string deptId = System.Configuration.ConfigurationManager.AppSettings["CorpDeptId"];//指定企业号通讯录部门Id
            wxUserDt = selectUser.getDeptUserList(deptId);//得到企业号中的所有人员信息
            string sqlCmd = "select * from OA_Sys_EmployeeInfo where useStatus='1'";
            DataTable corpUserDt = new DataTable();
            SqlSel.GetSqlSel(ref corpUserDt, sqlCmd);//获取企业信息中所有在职人员
            for (int i = 0; i < corpUserDt.Rows.Count; i++) 
            {
                string userId = corpUserDt.Rows[i]["id"].ToString();
                DataRow[] drArr = wxUserDt.Select("userid='" + userId + "'");
                //如果成员不存在，则创建
                if (drArr.Length == 0)
                {
                    VerifyLegal.createUser(corpUserDt.Rows[i]["id"].ToString(), corpUserDt.Rows[i]["chineseName"].ToString(), deptId, corpUserDt.Rows[i]["phoneNumber"].ToString());
                }
                else //存在则更新信息
                {
                    VerifyLegal.updateUser(corpUserDt.Rows[i]["id"].ToString(), corpUserDt.Rows[i]["chineseName"].ToString(), deptId, corpUserDt.Rows[i]["phoneNumber"].ToString());
                }

            }

            //删除离职人员信息
            sqlCmd = "select * from OA_Sys_EmployeeInfo where useStatus='0'";
            DataTable leaveUserDt = new DataTable();
            SqlSel.GetSqlSel(ref leaveUserDt, sqlCmd);
            List<string> userList = new List<string>();
            for (int i = 0; i < leaveUserDt.Rows.Count; i++) 
            {
                userList.Add(leaveUserDt.Rows[i]["id"].ToString());
            }

            VerifyLegal.deleteUser(userList);

            Alert.ShowInTop("同步成功！");
        }
    }
}