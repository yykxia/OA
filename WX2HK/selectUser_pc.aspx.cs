using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IETCsoft.sql;
using FineUI;
using System.Text;

namespace WX2HK
{
    public partial class selectUser_pc : System.Web.UI.Page
    {
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
            //int selectedCount = Grid1.SelectedRowIndexArray.Length;
            //if (selectedCount > 0)
            //{
            //    String selectedUserId = null;
            //    String selectedName = null;
            //    for (int i = 0; i < selectedCount; i++)
            //    {
            //        int find = -1;
            //        int rowIndex = Grid1.SelectedRowIndexArray[i];
            //        selectedUserId = Grid1.DataKeys[Grid1.SelectedRowIndexArray[i]][0].ToString();
            //        selectedName = Grid1.Rows[i].Values[1].ToString();
            //        //去除重复选中的人员
            //        for (int j = 0; j < curdt.Rows.Count; j++)
            //        {
            //            if (curdt.Rows[j]["id"].ToString() == selectedUserId)
            //            {
            //                find = j;
            //            }
            //        }
            //        if (find >= 0)
            //        {
            //            continue;
            //        }
            //        DataRow dr = curdt.NewRow();
            //        dr[0] = selectedUserId;
            //        dr[1] = selectedName;
            //        curdt.Rows.Add(dr);
            //    }
            //    //最后重新绑定datatable，实现动态添加的功能
            //    Grid3.DataSource = curdt;
            //    Grid3.DataBind();
            //}
            //else
            //{
            //    Alert.ShowInTop("请选择至少一名人员。");
            //}

        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Grid3.Rows.Count; i++)
                {
                    String userId = Grid3.DataKeys[i][0].ToString();

                    String selectedName = Grid3.Rows[i].Values[0].ToString();

                    sb.AppendFormat("{0}-{1};", userId, selectedName);

                }
                PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(sb.ToString()) + ActiveWindow.GetHideReference());
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
            //再根据行号删除相应行的数据
            curdt.Rows.RemoveAt(RowIndex);
            //重新绑定数据源
            Grid3.DataSource = curdt;
            Grid3.DataBind();
        }
    }
}