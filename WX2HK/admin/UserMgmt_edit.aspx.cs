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
    public partial class UserMgmt_edit : System.Web.UI.Page
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                recvId = Request["id"];
                //加载所有角色信息
                loadRoleItem();
                //加载部门信息
                loadDeptItem();
                //加载岗位信息
                loadDutyItem();

                if (!string.IsNullOrEmpty(recvId)) 
                {
                    loadInfo();
                }
            }
        }

        //显示人员信息
        private void loadInfo() 
        {
            string sqlCmd = "select * from OA_Sys_EmployeeInfo where id='" + recvId+"' order by loginId";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            txb_loginID.Text = dt.Rows[0]["loginId"].ToString();
            txb_chineseName.Text = dt.Rows[0]["chineseName"].ToString();
            numbb_phoneNumb.Text = dt.Rows[0]["phoneNumber"].ToString();
            int useStatus = Convert.ToInt32(dt.Rows[0]["useStatus"]);
            //是否在用
            if (useStatus == 0)
            {
                ckeckBox_enabled.Checked = false;
            }
            else
            {
                ckeckBox_enabled.Checked = true;
            }
            //加载部门信息
            ddl_depart.SelectedValue = dt.Rows[0]["deptId"].ToString();
            //加载岗位信息
            ddl_duty.SelectedValue = dt.Rows[0]["dutyId"].ToString();
            //加载角色信息
            checkBoxListBind(recvId);
        }

        //显示所有角色列表
        private void loadRoleItem()
        {
            string sqlCmd = "select * from OA_Sys_Role where useStatus=1";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ckb_roleList.DataSource = dt;
            ckb_roleList.DataValueField = "id";
            ckb_roleList.DataTextField = "roleName";
            ckb_roleList.DataBind();
        }

        //显示所有部门列表
        private void loadDeptItem() 
        {
            string sqlCmd = "select * from OA_sys_department";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_depart.DataTextField = "deptName";
            ddl_depart.DataValueField = "id";
            ddl_depart.DataSource = dt;
            ddl_depart.DataBind();

        }

        //显示所有岗位列表
        private void loadDutyItem()
        {
            string sqlCmd = "select * from OA_sys_Duties";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_duty.DataTextField = "DutyName";
            ddl_duty.DataValueField = "id";
            ddl_duty.DataSource = dt;
            ddl_duty.DataBind();

        }

        //
        private void checkBoxListBind(string userId) 
        {
            DataTable dt = new DataTable();
            string sqlCmd = "select * from OA_Sys_UserRole where userid='" + userId + "'";
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            string[] roleArray = new string[dt.Rows.Count];//创建一个角色数组
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleArray[i] = dt.Rows[i]["roleId"].ToString();
            }
            ckb_roleList.SelectedValueArray = roleArray;

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int userStatus = ckeckBox_enabled.Checked ? 1 : 0;
            if (string.IsNullOrEmpty(recvId))
            {
                sqlCmd = "insert into OA_Sys_EmployeeInfo (loginId,password,chineseName,deptId,dutyId,useStatus,phoneNumber) values (";
                sqlCmd += "'" + txb_loginID.Text + "','" + PasswordUtil.CreateDbPassword("123456") + "','" + txb_chineseName.Text + "',";
                sqlCmd += "'" + ddl_depart.SelectedValue + "','" + ddl_duty.SelectedValue + "','" + userStatus + "','" + numbb_phoneNumb.Text + "')";
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 1)
                {
                    sqlCmd = "select max(id) from OA_Sys_EmployeeInfo";
                    string newUserId = SqlSel.GetSqlScale(sqlCmd).ToString();

                    //更新角色信息
                    string[] selectValueArray = ckb_roleList.SelectedValueArray;
                    foreach (string item in selectValueArray)
                    {
                        sqlCmd = "insert into OA_Sys_UserRole (userid,roleid,addtime) values (";
                        sqlCmd += "'" + newUserId + "','" + item + "','" + DateTime.Now + "')";
                        SqlSel.ExeSql(sqlCmd);
                    }
                }
            }
            else 
            {
                sqlCmd = "update OA_Sys_EmployeeInfo set loginId='" + txb_loginID.Text + "',chineseName='" + txb_chineseName.Text + "',deptId='" + ddl_depart.SelectedValue + "',";
                sqlCmd += "dutyId='" + ddl_duty.SelectedValue + "',useStatus='" + userStatus + "',phoneNumber='" + numbb_phoneNumb.Text + "' where id=" + recvId;
                SqlSel.ExeSql(sqlCmd);

                //删除用户现有角色
                sqlCmd += "delete from OA_Sys_UserRole where userid=" + recvId;
                SqlSel.ExeSql(sqlCmd);

                string[] selectValueArray = ckb_roleList.SelectedValueArray;
                foreach (string item in selectValueArray)
                {
                    sqlCmd = "insert into OA_Sys_UserRole (userid,roleid,addtime) values (";
                    sqlCmd += "'" + recvId + "','" + item + "','" + DateTime.Now + "')";
                    SqlSel.ExeSql(sqlCmd);
                }
            }

            Alert.ShowInTop("保存成功！");
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }

    }
}