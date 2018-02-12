using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.IO;

namespace WX2HK.docMgmt
{
    public partial class docMgmt_CheckUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindProj();

                bindGrid("%", "0");
            }
        }

        private void bindProj()
        {
            string sqlCmd = "select * from OA_sys_Project";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_proj.DataTextField = "projName";
            ddl_proj.DataValueField = "id";
            ddl_proj.DataSource = dt;
            ddl_proj.DataBind();
            ddl_proj.Items.Insert(0, new FineUI.ListItem("所有项目", "%"));
        }

        //
        private void bindGrid(string projId,string checkStatus)
        {
            if (string.IsNullOrEmpty(projId))
            {
                projId = "%";
            }
            string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName,";
            sqlCmd += "(case when checkStamp='0' then '未审' when checkStamp='-1' then '否决' when checkStamp='1' then '通过' end) as checkStatus";
            sqlCmd += " from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
            sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan where doc_leng like '" + projId + "' and docStat='1' and checkStamp='" + checkStatus + "' order by subDte desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid3.DataSource = dt;
            Grid3.DataBind();
        }

        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            bindGrid(ddl_proj.SelectedValue,rdbtn_checkStatus.SelectedValue);
        }
        //审核通过
        protected void btn_checkAllRight_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int[] selections = Grid3.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {
                int fileid = Convert.ToInt32(Grid3.DataKeys[rowIndex][0]);
                sqlCmd = "UPDATE OA_DocMgmt_DocList SET checkStamp='1' WHERE ID=" + fileid;
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 0)
                {
                    Alert.ShowInTop("出错了！");
                    return;
                }
            }
            Alert.Show("文档已通过！");
            bindGrid(ddl_proj.SelectedValue, rdbtn_checkStatus.SelectedValue);
        }
        //审核否决
        protected void btn_checkRefuse_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int[] selections = Grid3.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {
                int fileid = Convert.ToInt32(Grid3.DataKeys[rowIndex][0]);
                sqlCmd = "UPDATE OA_DocMgmt_DocList SET checkStamp='-1' WHERE ID=" + fileid;
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 0)
                {
                    Alert.ShowInTop("出错了！");
                    return;
                }
            }
            Alert.Show("文档已被否决！");
            bindGrid(ddl_proj.SelectedValue, rdbtn_checkStatus.SelectedValue);
        }
        //撤销审核
        protected void btn_normal_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int[] selections = Grid3.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {
                int fileid = Convert.ToInt32(Grid3.DataKeys[rowIndex][0]);
                sqlCmd = "UPDATE OA_DocMgmt_DocList SET checkStamp='0' WHERE ID=" + fileid;
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 0)
                {
                    Alert.ShowInTop("出错了！");
                    return;
                }
            }
            Alert.Show("审核已撤销！");
            bindGrid(ddl_proj.SelectedValue, rdbtn_checkStatus.SelectedValue);
        }

        protected void rdbtn_checkStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid(ddl_proj.SelectedValue, rdbtn_checkStatus.SelectedValue);
        }
    }
}