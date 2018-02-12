using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IETCsoft.sql;
using FineUI;
using System.Text;

namespace WX2HK.admin
{
    public partial class FlowMgmt_SelfDefined : BasePage
    {
        private static string recvId = string.Empty;
        private static string defFlowId = string.Empty;//生成的自定义流程主Id

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid2();
            }
        }

        #region BindGrid2/BindGrid1
        //列举部门信息
        private void BindGrid2()
        {
            string sqlCmd = "select * from OA_sys_department";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid2.DataSource = dt;
            Grid2.DataBind();
        }
        //显示部门人员信息
        private void BindGrid1()
        {
            //String sqlCmd = "select DepartId from dbo.View_Rs_employeeinfo where Gh_id = '"+userId+"'";
            //String bmId = Convert.ToString(SqlSel.GetSqlScale(sqlCmd));

            String bmId = Convert.ToString(Grid2.DataKeys[Grid2.SelectedRowIndex][0]);

            String sqlCmd = "select * from OA_Sys_EmployeeInfo where deptId ='" + bmId + "' and useStatus=1";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();

        }

        #endregion

        //点击部门事件
        protected void Grid2_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            BindGrid1();
        }
        //点击添加人员事件
        protected void Grid1_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            btn1.Enabled = true;
        }

        //搜索框
        protected void tbxMyBox1_TriggerClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(trgbox1.Text))
            {
                //执行搜索
                string userName = trgbox1.Text;
                string sqlCmd = "SELECT * FROM OA_Sys_EmployeeInfo WHERE ChineseName like '%" + userName.Trim() + "%' and useStatus=1";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                Grid1.DataSource = dt;
                Grid1.DataBind();
            }
        }

        //点击添加事件
        protected void btn1_Click(object sender, EventArgs e)
        {
            addRow();
        }
        //实现从grid1往grid3添加选中人员的方法
        protected void addRow()
        {
            //初始化datatable，遍历当前grid3的所有数据并存放进去
            DataTable curdt = new DataTable();
            curdt.Columns.Add("id");
            curdt.Columns.Add("chineseName");
            for (int j = 0; j < Grid3.Rows.Count; j++)
            {
                DataRow dr = curdt.NewRow();
                dr[0] = Grid3.DataKeys[j][0];
                dr[1] = Grid3.Rows[j].Values[0];
                curdt.Rows.Add(dr);
            }
            //再遍历grid1选定行的数据，添加到datatable
            int[] selections = Grid1.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {

                String selectedUserId = null;
                String selectedName = null;
                int find = -1;
                selectedUserId = Grid1.DataKeys[rowIndex][0].ToString();
                selectedName = Grid1.Rows[rowIndex].Values[1].ToString();
                //去除重复选中的人员
                for (int j = 0; j < curdt.Rows.Count; j++)
                {
                    if (curdt.Rows[j]["id"].ToString() == selectedUserId)
                    {
                        find = j;
                    }
                }
                if (find >= 0)
                {
                    continue;
                }
                DataRow dr = curdt.NewRow();
                dr[0] = selectedUserId;
                dr[1] = selectedName;
                curdt.Rows.Add(dr);
            }
            Grid3.DataSource = curdt;
            Grid3.DataBind();
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid3.Rows.Count > 0 || Grid1.SelectedRowIndexArray.Length > 0)
                {
                    //插入自定义流程主表
                    string sqlCmd = "insert into OA_sys_flow (flowFounder,createTime,isUsed) values ";
                    sqlCmd += " ('" + GetUser() + "','" + DateTime.Now + "','2')";
                    string flowId = "";
                    if (SqlSel.ExeSql(sqlCmd) == 1)
                    {
                        StringBuilder flowInfo = new StringBuilder();
                        sqlCmd = "select max(id) from OA_sys_flow";
                        flowId = SqlSel.GetSqlScale(sqlCmd).ToString();

                        if (Grid3.Rows.Count > 0)
                        {
                            if (rdbtn_flowType.SelectedValue == "1")
                            {
                                flowInfo.Append("[串行]");
                                for (int i = 0; i < Grid3.Rows.Count; i++)
                                {
                                    sqlCmd = "insert into OA_Sys_Flow_Step (flowId,stepOrderNo) values('" + flowId + "','" + (i + 1) + "')";
                                    SqlSel.ExeSql(sqlCmd);
                                    sqlCmd = "select max(id) from OA_Sys_Flow_Step";
                                    string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
                                    string userId = Grid3.DataKeys[i][0].ToString();
                                    DataTable userInfo = getUserInfo(userId);
                                    sqlCmd = "insert into OA_Sys_Step_Emp (stepId,deptId,dutyId) values ('" + stepId + "','" + userInfo.Rows[0]["deptId"].ToString() + "','" + userInfo.Rows[0]["dutyId"].ToString() + "')";
                                    SqlSel.ExeSql(sqlCmd);
                                    flowInfo.AppendFormat("{0};", Grid3.Rows[i].Values[0].ToString());
                                }

                            }
                            if (rdbtn_flowType.SelectedValue == "2")
                            {
                                flowInfo.Append("[并行]");
                                sqlCmd = "insert into OA_Sys_Flow_Step (flowId,stepOrderNo) values('" + flowId + "','1')";
                                SqlSel.ExeSql(sqlCmd);
                                sqlCmd = "select max(id) from OA_Sys_Flow_Step";
                                string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
                                for (int i = 0; i < Grid3.Rows.Count; i++)
                                {
                                    string userId = Grid3.DataKeys[i][0].ToString();
                                    DataTable userInfo = getUserInfo(userId);
                                    sqlCmd = "insert into OA_Sys_Step_Emp (stepId,deptId,dutyId) values ('" + stepId + "','" + userInfo.Rows[0]["deptId"].ToString() + "','" + userInfo.Rows[0]["dutyId"].ToString() + "')";
                                    SqlSel.ExeSql(sqlCmd);
                                    flowInfo.AppendFormat("{0};", Grid3.Rows[i].Values[0].ToString());
                                }

                            }

                        }
                        else
                        {
                            if (Grid1.SelectedRowIndexArray.Length > 0)
                            {
                                if (rdbtn_flowType.SelectedValue == "1")
                                {
                                    flowInfo.Append("[串行]");
                                    int[] selections = Grid1.SelectedRowIndexArray;
                                    foreach (int rowIndex in selections)
                                    {
                                        sqlCmd = "insert into OA_Sys_Flow_Step (flowId,stepOrderNo) values('" + flowId + "','" + (rowIndex + 1) + "')";
                                        SqlSel.ExeSql(sqlCmd);
                                        sqlCmd = "select max(id) from OA_Sys_Flow_Step";
                                        string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
                                        string userId = Grid1.DataKeys[rowIndex][0].ToString();
                                        DataTable userInfo = getUserInfo(userId);
                                        sqlCmd = "insert into OA_Sys_Step_Emp (stepId,deptId,dutyId) values ('" + stepId + "','" + userInfo.Rows[0]["deptId"].ToString() + "','" + userInfo.Rows[0]["dutyId"].ToString() + "')";
                                        SqlSel.ExeSql(sqlCmd);
                                        flowInfo.AppendFormat("{0};", Grid1.Rows[rowIndex].Values[1].ToString());
                                    }

                                }
                                if (rdbtn_flowType.SelectedValue == "2")
                                {
                                    flowInfo.Append("[并行]");
                                    sqlCmd = "insert into OA_Sys_Flow_Step (flowId,stepOrderNo) values('" + flowId + "','1')";
                                    SqlSel.ExeSql(sqlCmd);
                                    sqlCmd = "select max(id) from OA_Sys_Flow_Step";
                                    string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
                                    int[] selections = Grid1.SelectedRowIndexArray;
                                    foreach (int rowIndex in selections)
                                    {
                                        string userId = Grid1.DataKeys[rowIndex][0].ToString();
                                        DataTable userInfo = getUserInfo(userId);
                                        sqlCmd = "insert into OA_Sys_Step_Emp (stepId,deptId,dutyId) values ('" + stepId + "','" + userInfo.Rows[0]["deptId"].ToString() + "','" + userInfo.Rows[0]["dutyId"].ToString() + "')";
                                        SqlSel.ExeSql(sqlCmd);
                                        flowInfo.AppendFormat("{0};", Grid1.Rows[rowIndex].Values[1].ToString());
                                    }

                                }
                            }
                        }
                        Alert.Show("流程添加成功！");
                        PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(flowInfo.ToString(), flowId) + ActiveWindow.GetHideReference());
                    }
                }
                else 
                {
                    Alert.ShowInTop("未添加任何审批人员！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        protected void btn_return_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }

        protected void Grid3_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            //获取选中行行号
            int RowIndex = Grid3.SelectedRowIndex;
            //当前数据源
            DataTable curdt = getDataTableGrid3();
            //再根据行号删除相应行的数据
            curdt.Rows.RemoveAt(RowIndex);
            //重新绑定数据源
            Grid3.DataSource = curdt;
            Grid3.DataBind();
        }

        //获取Grid3数据源
        private DataTable getDataTableGrid3() 
        {
            //初始化datatable，将现有数据存放进去
            DataTable curdt = new DataTable();
            curdt.Columns.Add("id");
            curdt.Columns.Add("chineseName");
            for (int j = 0; j < Grid3.Rows.Count; j++)
            {
                DataRow dr = curdt.NewRow();
                dr[0] = Grid3.DataKeys[j][0];
                dr[1] = Grid3.Rows[j].Values[0];
                curdt.Rows.Add(dr);
            }
            return curdt;
        }

        //步骤上移操作
        protected void btn_SortUp_Click(object sender, EventArgs e)
        {
            int[] selections = Grid3.SelectedRowIndexArray;
            if (selections.Length > 0)
            {
                int rowIndex = selections[0];
                if (rowIndex != 0)
                {
                    //更新选中行的步骤序号
                    updateStepSort(rowIndex);
                    Grid3.SelectedRowIndex = rowIndex - 1;
                }
                else
                {
                }
            }
        }

        //调整步骤的序号
        private void updateStepSort(int curIndex)
        {
            DataTable dt = new DataTable();
            dt = getDataTableGrid3();
            //添加一个排序列
            dt.Columns.Add("sort");
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                dt.Rows[i]["sort"] = i + 1;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //上个节点的排序 + 1
                if (i == curIndex - 1)
                {
                    dt.Rows[i]["sort"] = curIndex + 1;
                }
                //当前节点的排序 - 1
                if (i == curIndex)
                {
                    dt.Rows[i]["sort"] = curIndex;
                }
            }

            dt.DefaultView.Sort = "sort";//根据sort重新排序

            Grid3.DataSource = dt;

            Grid3.DataBind();
        }

        private DataTable getUserInfo(string userId) 
        {
            string sqlCmd = "select * from OA_Sys_EmployeeInfo where id='" + userId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            return dt;
        }
    }
}