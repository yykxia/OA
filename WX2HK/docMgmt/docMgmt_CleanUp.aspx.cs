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
    public partial class docMgmt_CleanUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                bindProj();
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
        }

        //
        private void bindGrid(string projId)
        {
            if (string.IsNullOrEmpty(projId)) 
            {
                projId = "%";
            }
            string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
            sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan where doc_leng like '" + projId + "' and docStat='0' order by subDte desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid3.DataSource = dt;
            Grid3.DataBind();
        }

        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            bindGrid(ddl_proj.SelectedValue);
        }

        protected void btn_normal_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int[] selections = Grid3.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {
                int fileid = Convert.ToInt32(Grid3.DataKeys[rowIndex][0]);
                sqlCmd = "UPDATE OA_DocMgmt_DocList SET docStat='1' WHERE ID=" + fileid;
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 0)
                {
                    Alert.ShowInTop("出错了！");
                    return;
                }
            }
            Alert.Show("文件已恢复！");
            bindGrid(ddl_proj.SelectedValue);
        }

        protected void btn_Invalid_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int[] selections = Grid3.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {
                //获取行index
                //int rowindex = Grid3.SelectedRowIndexArray[i];
                int fileid = Convert.ToInt32(Grid3.DataKeys[rowIndex][0]);
                string serverFileName = Grid3.Rows[rowIndex].Values[5].ToString();//服务器文件名
                sqlCmd = "delete from  OA_DocMgmt_DocList WHERE ID=" + fileid;
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 0)
                {
                    Alert.ShowInTop("出错了！");
                    return;
                }
                else
                {
                    string filePath = "~/upload/" + serverFileName;
                    if (File.Exists(Server.MapPath(filePath)))
                    {
                        //如果文件存在则删除
                        File.Delete(Server.MapPath(filePath));
                    }
                }
            }
            Alert.ShowInTop("已删除！");
            bindGrid(ddl_proj.SelectedValue);
        }
    }
}