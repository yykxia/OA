using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IETCsoft.sql;

namespace WX2HK.docMgmt
{
    public partial class docList_m : System.Web.UI.Page
    {
        private static string curUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = "docMgmt%2fdocList_m";
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id

                //加载文档类型
                bindDocType();
                //
                bindProj();

                DatePicker2.SelectedDate = DateTime.Now;
            }
        }

        //加载文档分类
        private void bindDocType()
        {
            string sqlCmd = "select * from OA_DocMgmt_DocType where pid='0' and stat='1'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_docType.DataTextField = "type_name";
            ddl_docType.DataValueField = "id";
            ddl_docType.DataSource = dt;
            ddl_docType.DataBind();
            this.ddl_docType.Items.Insert(0, new FineUI.ListItem("所有类型", "%"));
        }

        //筛选与用户相关的项目
        private void bindProj()
        {
            string sqlCmd = "select B.id,B.projName from OA_Sys_ProjMember A left join OA_sys_Project B on A.projId=B.id where A.userId='" + curUserId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_proj.DataTextField = "projName";
            ddl_proj.DataValueField = "id";
            ddl_proj.DataSource = dt;
            ddl_proj.DataBind();
        }

        //文档明细列表
        private void bindDocList(string docType)
        {
            string sqlCmd = "select * from OA_DocMgmt_DocList where docTyp='" + docType + "' and docStat='1'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        ////根据上传人名称检索信息
        //protected void trgb_SubMan_TriggerClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(trgb_SubMan.Text))
        //    {
        //        string subManStr = trgb_SubMan.Text;
        //        string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //        sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //        sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //        sqlCmd += " where subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '%" + subManStr + "%') and docStat='1'";
        //        DataTable dt = new DataTable();
        //        SqlSel.GetSqlSel(ref dt, sqlCmd);
        //        Grid1.DataSource = dt;
        //        Grid1.DataBind();
        //    }
        //}

        //protected void trgb_proj_TriggerClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(trgb_proj.Text))
        //    {
        //        string projStr = trgb_proj.Text;
        //        string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //        sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //        sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //        sqlCmd += " where doc_leng in (select id from OA_sys_Project where projName like '%" + projStr + "%') and docStat='1'";
        //        DataTable dt = new DataTable();
        //        SqlSel.GetSqlSel(ref dt, sqlCmd);
        //        Grid1.DataSource = dt;
        //        Grid1.DataBind();
        //    }
        //}

        //protected void trgb_fileName_TriggerClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(trgb_fileName.Text))
        //    {
        //        string fileNameStr = trgb_fileName.Text;
        //        string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //        sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //        sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //        sqlCmd += " where docName like '%" + fileNameStr + "%' and docStat='1' ";
        //        DataTable dt = new DataTable();
        //        SqlSel.GetSqlSel(ref dt, sqlCmd);
        //        Grid1.DataSource = dt;
        //        Grid1.DataBind();
        //    }
        //}

        //protected void btn_uploadDateFilter_Click(object sender, EventArgs e)
        //{
        //    string dt1 = DatePicker1.Text;
        //    string dt2 = DatePicker2.Text;
        //    string sqlCmd = "";
        //    if (!string.IsNullOrEmpty(dt1))
        //    {
        //        sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //        sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //        sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //        sqlCmd += " where CONVERT(varchar(100), subDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
        //    }
        //    else
        //    {
        //        sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //        sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //        sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //        sqlCmd += " where CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
        //    }
        //    DataTable dt = new DataTable();
        //    SqlSel.GetSqlSel(ref dt, sqlCmd);
        //    Grid1.DataSource = dt;
        //    Grid1.DataBind();
        //}

        //protected void ddl_docType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedDocType = ddl_docType.SelectedValue;
        //    string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //    sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //    sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //    sqlCmd += " where docTyp like '" + selectedDocType + "' and docStat='1'";
        //    DataTable dt = new DataTable();
        //    SqlSel.GetSqlSel(ref dt, sqlCmd);
        //    Grid1.DataSource = dt;
        //    Grid1.DataBind();
        //}

        protected void btn_mutiSeach_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            string subManStr = "", projStr = "", fileNameStr = "", selectedDocType = ddl_docType.SelectedValue;
            string dt1 = DatePicker1.Text;
            string dt2 = DatePicker2.Text;
            //
            if (string.IsNullOrEmpty(txb_SubMan.Text))
            {
                subManStr = "%";
            }
            else
            {
                subManStr = "%" + txb_SubMan.Text + "%";
            }
            //
            if (string.IsNullOrEmpty(ddl_proj.SelectedValue))
            {
                projStr = "%";
            }
            else
            {
                projStr = ddl_proj.SelectedValue;
            }
            //
            if (string.IsNullOrEmpty(txb_fileName.Text))
            {
                fileNameStr = "%";
            }
            else
            {
                fileNameStr = "%" + txb_fileName.Text + "%";
            }
            //
            if (!string.IsNullOrEmpty(dt1))
            {
                sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
                sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
                sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
                sqlCmd += " where CONVERT(varchar(100), subDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
                sqlCmd += " and subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '" + subManStr + "')";
                sqlCmd += " and doc_leng in (select id from OA_sys_Project where projName like '" + projStr + "')";
                sqlCmd += " and docName like '" + fileNameStr + "' and docTyp like '" + selectedDocType + "'";
            }
            else
            {
                sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
                sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
                sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
                sqlCmd += " where CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
                sqlCmd += " and subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '" + subManStr + "')";
                sqlCmd += " and doc_leng in (select id from OA_sys_Project where projName like '" + projStr + "')";
                sqlCmd += " and docName like '" + fileNameStr + "' and docTyp like '" + selectedDocType + "'";
            }
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }


    }
}