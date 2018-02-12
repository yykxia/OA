using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using Newtonsoft.Json.Linq;

namespace WX2HK.admin
{
    public partial class FlowMgmt : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                bindGird1();

                //
                //btn_newNode.OnClientClick = Window1.GetShowReference(string.Format("FlowMgmt_newStep.aspx?flowId={0}", Grid1.DataKeys[Grid1.SelectedRowIndex][0]));
                //添加流程
                //btn_newFlow.OnClientClick = Window1.GetShowReference("FlowMgmt_flowEdit.aspx");
            }
        }

        //加载流程主表
        private void bindGird1() 
        {
            string sqlCmd = "select * from OA_sys_flow where isUsed='1'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        private void bindGrid2(string flowId) 
        {
            string sqlCmd = "select * from OA_Sys_Flow_Step where flowId='" + flowId + "' order by stepOrderNo";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid2.DataSource = dt;
            Grid2.DataBind();
        }

        private void bindGrid3(string stepId)
        {
            //string sqlCmd = "select * from OA_Sys_Flow_Step where flowId='" + flowId + "' order by stepOrderNo";
            //DataTable dt = new DataTable();
            //SqlSel.GetSqlSel(ref dt, sqlCmd);
            //Grid2.DataSource = dt;
            //Grid2.DataBind();
            string sqlCmd = "select *,(case when deptName is null then '本部门' else deptName end) as dept from OA_Sys_Step_Emp left join OA_sys_department on OA_Sys_Step_Emp.deptId=OA_sys_department.id ";
            sqlCmd += "left join OA_sys_Duties on OA_sys_Duties.id=OA_Sys_Step_Emp.dutyId where stepId='" + stepId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid3.DataSource = dt;
            Grid3.DataBind();
        }

        protected void Grid1_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            bindGrid2(Grid1.DataKeys[e.RowIndex][0].ToString());
            //DataTable dt = null;
            Grid3.DataSource = null;
            Grid3.DataBind();
        }


        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            bindGrid2(Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString());
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            bindGird1();
        }

        protected void btn_newStep_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndex >= 0)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(string.Format("FlowMgmt_newStep.aspx?flowId={0}", Grid1.DataKeys[Grid1.SelectedRowIndex][0])));
            }
            else 
            {
                Alert.ShowInTop("请先选择相应的流程后再编辑步骤！");
                return;
            }
        }

        protected void btn_newFlow_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference("FlowMgmt_flowEdit.aspx"));
        }

        protected void btn_newNode_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndex >= 0)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(string.Format("FlowMgmt_newNode.aspx?stepId={0}", Grid2.DataKeys[Grid2.SelectedRowIndex][0])));
            }
            else
            {
                Alert.ShowInTop("请先选择相应步骤后再添加策略！");
                return;
            }
        }

        protected void Grid3_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int rowID = Convert.ToInt32(Grid3.DataKeys[e.RowIndex][0]);
                deleteFromDb(rowID);

            }
        }

        //服务器端删除
        protected void deleteFromDb(int DbId)
        {
            string sqlCmd = "delete from OA_Sys_Step_Emp where id=" + DbId;
            SqlSel.ExeSql(sqlCmd);
            bindGrid3(Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
        }

        protected void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            bindGrid3(Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
        }

        protected void Grid2_RowClick(object sender, GridRowClickEventArgs e)
        {
            bindGrid3(Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
        }

        //步骤上移操作
        protected void btn_SortUp_Click(object sender, EventArgs e)
        {
            int[] selections = Grid2.SelectedRowIndexArray;
            if (selections.Length > 0) 
            {
                string selStepId = Grid2.DataKeys[selections[0]][0].ToString();//选中行的步骤id
                if (Grid2.Rows[selections[0]].Values[1].ToString() != "1")
                {
                    //更新选中行的步骤序号
                    updateStepSort(selStepId, Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString());
                }
                else 
                {
                    Alert.ShowInTop("当前步骤已在最顶层！");
                }
            }
        }

        //调整步骤的序号
        private void updateStepSort(string stepId,string flowId) 
        {
            string sqlCmd = "select * from OA_Sys_Flow_Step where flowId='" + flowId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            DataRow[] curSortDr = dt.Select("id='" + stepId + "'");
            int curSort = Convert.ToInt32(curSortDr[0]["stepOrderNo"]);//当前行的序号

            DataRow[] lastSortDr = dt.Select("stepOrderNo='" + (curSort - 1) + "'");
            string lastStepId = lastSortDr[0]["id"].ToString();//上一个步骤的stepid

            sqlCmd = "update OA_Sys_Flow_Step set stepOrderNo=(stepOrderNo-1) where id='" + stepId + "'";//当前步骤的序号-1
            SqlSel.ExeSql(sqlCmd);

            sqlCmd = "update OA_Sys_Flow_Step set stepOrderNo=(stepOrderNo+1) where id='" + lastStepId + "'";//上一个步骤的序号+1
            SqlSel.ExeSql(sqlCmd);

            bindGrid2(flowId);//最后重新刷新
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string flowId = Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString();//流程id
                string stepId = Grid2.DataKeys[e.RowIndex][0].ToString();//步骤id
                int orderNo = Convert.ToInt32(Grid2.Rows[e.RowIndex].Values[1]);
                //判断当前步骤是否有正在执行的单据
                if (StepExistsInForms(stepId))
                {
                    Alert.ShowInTop("当前步骤存在待审批的表单，无法删除！");
                    return;
                }
                else 
                {
                    //删除该步骤
                    string sqlCmd = "delete from OA_Sys_Flow_Step where id='" + stepId + "'";
                    SqlSel.ExeSql(sqlCmd);
                    //后面的步骤顺序上移
                    sqlCmd = "update OA_Sys_Flow_Step set stepOrderNo=(stepOrderNo-1) where flowId='" + flowId + "' and stepOrderNo>'" + orderNo + "'";
                    SqlSel.ExeSql(sqlCmd);

                    bindGrid2(flowId);//最后重新刷新
                }
            }
        }

        //
        private bool StepExistsInForms(string stepId) 
        {
            string sqlCmd = "select * from [OA_Sys_step_empList] where id not in (select NodeId from OA_Sys_NodeDetail)";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            DataRow[] dr = dt.Select("stepId='" + stepId + "'");
            if (dr.Length > 0)
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