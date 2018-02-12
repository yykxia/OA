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
    public partial class ProjMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                ProjBind(ddl_status.SelectedValue);
            }
        }

        private void ProjBind(string projStatus) 
        {
            string sqlCmd = "select * from OA_sys_Project where ProjStatus='" + projStatus + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid2.DataSource = dt;
            Grid2.DataBind();
        }

        private void ProjMemberBind(string projId) 
        {
            if (!string.IsNullOrEmpty(projId)) 
            {
                string sqlCmd = "select A.id,B.chineseName,C.deptName,A.isManager from OA_Sys_ProjMember A left join OA_Sys_EmployeeInfo B on A.userId=B.id ";
                sqlCmd += "left join OA_sys_department C on C.id=B.deptId where A.projId='" + projId + "'";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                Grid1.DataSource = dt;
                Grid1.DataBind();
            }
        }

        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProjBind(ddl_status.SelectedValue);
            }
            catch (Exception ex) 
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        protected void btn_newIndex_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference("ProjMgmt_Edit.aspx", "新建项目"));
        }

        protected void Window1_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            ProjBind(ddl_status.SelectedValue);
        }

        protected void btn_addMember_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndex >= 0)
            {
                string projId = Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString();
                string url = string.Format("SelectUsers.aspx?id={0}", projId);
                PageContext.RegisterStartupScript(Window2.GetShowReference(url));
            }
            else 
            {
                Alert.ShowInTop("请先选定左侧相应的项目再进行管理！");
            }
        }

        protected void Grid2_RowClick(object sender, GridRowClickEventArgs e)
        {
            try 
            {
                ProjMemberBind(Grid2.DataKeys[e.RowIndex][0].ToString());
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }

        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    int rowID = e.RowIndex;
                    string memberId = Grid1.DataKeys[rowID][0].ToString();
                    string sqlCmd = "delete from OA_Sys_ProjMember where id='" + memberId + "'";
                    SqlSel.ExeSql(sqlCmd);
                    ProjMemberBind(Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
                    Alert.ShowInTop("已删除!");
                }
                if (e.CommandName == "isManager")
                {
                    int rowID = e.RowIndex;
                    CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn(e.ColumnIndex);
                    bool checkState = checkField.GetCheckedState(e.RowIndex);
                    string memberId = Grid1.DataKeys[rowID][0].ToString();
                    string memberName = Grid1.Rows[rowID].Values[0].ToString();
                    if (checkState == true)
                    {
                        string sqlCmd = "update OA_Sys_ProjMember set isManager='1' where id='" + memberId + "'";
                        SqlSel.ExeSql(sqlCmd);
                        Alert.ShowInTop(string.Format("{0} 已设置为项目负责人！", memberName));
                    }
                    else 
                    {
                        string sqlCmd = "update OA_Sys_ProjMember set isManager='0' where id='" + memberId + "'";
                        SqlSel.ExeSql(sqlCmd);
                        Alert.ShowInTop(string.Format("已取消 {0} 项目负责人权限！", memberName));
                    }
                    ProjMemberBind(Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }

        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            ProjMemberBind(Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
        }
    }
}