using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Data;
using IETCsoft.sql;

namespace WX2HK.docMgmt
{
    public partial class docMgmt : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                //加载文档类型
                bindDocType();
                //
                bindProj();

                DatePicker2.SelectedDate = DateTime.Now;
            }
        }

        //筛选与用户相关的项目
        private void bindProj()
        {
            string sqlCmd = "select B.id,B.projName from OA_Sys_ProjMember A left join OA_sys_Project B on A.projId=B.id where A.userId='" + GetUser() + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_proj.DataTextField = "projName";
            ddl_proj.DataValueField = "id";
            ddl_proj.DataSource = dt;
            ddl_proj.DataBind();
            this.ddl_proj.Items.Insert(0, new FineUI.ListItem("所有项目", "%"));
        }


        //加载文件类型（根据人员岗位筛选）
        private void bindDocType()
        {
            //string sqlCmd = "select * from OA_DocMgmt_DocType where stat='1' and pid='0' order by type_name";
            string sqlCmd = "select A.docTypeId,B.type_name FROM ";
            sqlCmd += "(SELECT docTypeId FROM [OA_DocMgmt_DocType_RelDuty] WHERE dutyId=( SELECT dutyId FROM OA_Sys_EmployeeInfo WHERE ID='" + GetUser() + "') GROUP BY docTypeId";
            sqlCmd += " ) A LEFT JOIN OA_DocMgmt_DocType B ON A.docTypeId=B.Id WHERE B.stat='1' and B.pid='0' ORDER BY type_name";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_docType.DataTextField = "type_name";
            ddl_docType.DataValueField = "docTypeId";
            ddl_docType.DataSource = dt;
            ddl_docType.DataBind();
            this.ddl_docType.Items.Insert(0, new FineUI.ListItem("所有类型", "%"));
        }

        ////加载文档分类
        //private void bindDocType() 
        //{
        //    string sqlCmd = "select * from OA_DocMgmt_DocType where pid='0' and stat='1' order by type_name";
        //    DataTable dt = new DataTable();
        //    SqlSel.GetSqlSel(ref dt, sqlCmd);
        //    ddl_docType.DataTextField = "type_name";
        //    ddl_docType.DataValueField = "id";
        //    ddl_docType.DataSource = dt;
        //    ddl_docType.DataBind();
        //    this.ddl_docType.Items.Insert(0, new FineUI.ListItem("所有类型", "%"));
        //}

        //文档明细列表
        private void bindDocList(string docType) 
        {
            string sqlCmd = "select * from OA_DocMgmt_DocList where docTyp='" + docType + "' and docStat='1'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid3.DataSource = dt;
            Grid3.DataBind();
        }

        //根据上传人名称检索信息
        //protected void trgb_SubMan_TriggerClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txb_SubMan.Text)) 
        //    {
        //        string subManStr = txb_SubMan.Text;
        //        string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //        sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //        sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //        sqlCmd += " where subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '%" + subManStr + "%') and docStat='1'";
        //        DataTable dt = new DataTable();
        //        SqlSel.GetSqlSel(ref dt, sqlCmd);
        //        Grid3.DataSource = dt;
        //        Grid3.DataBind();
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
        //        Grid3.DataSource = dt;
        //        Grid3.DataBind();
        //    }
        //}

        //protected void trgb_fileName_TriggerClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txb_fileName.Text))
        //    {
        //        string fileNameStr = txb_fileName.Text;
        //        string sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
        //        sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
        //        sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
        //        sqlCmd += " where docName like '%" + fileNameStr + "%' and docStat='1' ";
        //        DataTable dt = new DataTable();
        //        SqlSel.GetSqlSel(ref dt, sqlCmd);
        //        Grid3.DataSource = dt;
        //        Grid3.DataBind();
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
        //    Grid3.DataSource = dt;
        //    Grid3.DataBind();
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
        //    Grid3.DataSource = dt;
        //    Grid3.DataBind();
        //}

        protected void btn_mutiSeach_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            string subManStr = "", projStr = "", fileNameStr = "", selectedDocType = ddl_docType.SelectedValue ;
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
            if (string.IsNullOrEmpty(txb_fileName.Text))
            {
                fileNameStr = "%";
            }
            else
            {
                fileNameStr = "%" + txb_fileName.Text + "%";
            }
            //

            System.Text.StringBuilder selDocType = new System.Text.StringBuilder();
            if (ddl_docType.SelectedIndex == 0)
            {
                foreach (FineUI.ListItem ls in ddl_docType.Items)
                {
                    if (ls.Value != "%")
                    {
                        selDocType.AppendFormat("'{0}',", ls.Value);
                    }
                }
            }
            else 
            {
                selDocType.AppendFormat("'{0}',", ddl_docType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(dt1))
            {            //
                if (ddl_proj.SelectedValue == "%")
                {
                    sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName,[docSN],[AgreementPaty],[SubjectMatter],[AgreeAmount],";
                    sqlCmd += "[AmountType],[StartDate],[EndDate],[Remarks],(select docSN from OA_DocMgmt_Propety TT where TT.docId=T.SourceDocId) as addFile from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
                    sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
                    sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
                    sqlCmd += " left join OA_DocMgmt_Propety T ON T.docId=A.id";
                    sqlCmd += " where CONVERT(varchar(100), subDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
                    sqlCmd += " and subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '" + subManStr + "')";
                    sqlCmd += " and doc_leng in (select projId from OA_Sys_ProjMember where userId='" + GetUser() + "')";
                    sqlCmd += " and docName like '" + fileNameStr + "' and docTyp in (" + selDocType.ToString().TrimEnd(',') + ") and checkStamp='1' order by subDte desc";
                }
                else
                {
                    projStr = ddl_proj.SelectedValue;
                    sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName,[docSN],[AgreementPaty],[SubjectMatter],[AgreeAmount],";
                    sqlCmd += "[AmountType],[StartDate],[EndDate],[Remarks],(select docSN from OA_DocMgmt_Propety TT where TT.docId=T.SourceDocId) as addFile from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
                    sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
                    sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
                    sqlCmd += " left join OA_DocMgmt_Propety T ON T.docId=A.id";
                    sqlCmd += " where CONVERT(varchar(100), subDte, 23)>='" + dt1 + "' and CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
                    sqlCmd += " and subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '" + subManStr + "')";
                    sqlCmd += " and doc_leng='" + projStr + "'";
                    sqlCmd += " and docName like '" + fileNameStr + "' and docTyp in (" + selDocType.ToString().TrimEnd(',') + ") and checkStamp='1' order by subDte desc";
                }
            }
            else
            {
                if (ddl_proj.SelectedValue == "%")
                {

                    sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName,[docSN],[AgreementPaty],[SubjectMatter],[AgreeAmount],";
                    sqlCmd += "[AmountType],[StartDate],[EndDate],[Remarks],(select docSN from OA_DocMgmt_Propety TT where TT.docId=T.SourceDocId) as addFile from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
                    sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
                    sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
                    sqlCmd += " left join OA_DocMgmt_Propety T ON T.docId=A.id";
                    sqlCmd += " where CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
                    sqlCmd += " and subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '" + subManStr + "')";
                    sqlCmd += " and doc_leng in (select projId from OA_Sys_ProjMember where userId='" + GetUser() + "')";
                    sqlCmd += " and docName like '" + fileNameStr + "' and docTyp in (" + selDocType.ToString().TrimEnd(',') + ") and checkStamp='1' order by subDte desc";
                }
                else
                {
                    projStr = ddl_proj.SelectedValue;
                    sqlCmd = "select A.*,B.type_name,c.projName,d.chineseName,[docSN],[AgreementPaty],[SubjectMatter],[AgreeAmount],";
                    sqlCmd += "[AmountType],[StartDate],[EndDate],[Remarks],(select docSN from OA_DocMgmt_Propety TT where TT.docId=T.SourceDocId) as addFile from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
                    sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng";
                    sqlCmd += " left join OA_Sys_EmployeeInfo D ON D.ID=A.subMan";
                    sqlCmd += " left join OA_DocMgmt_Propety T ON T.docId=A.id";
                    sqlCmd += " where CONVERT(varchar(100), subDte, 23)<='" + dt2 + "' and docStat='1'";
                    sqlCmd += " and subMan in (select id from OA_Sys_EmployeeInfo where chineseName like '" + subManStr + "')";
                    sqlCmd += " and doc_leng='" + projStr + "' ";
                    sqlCmd += " and docName like '" + fileNameStr + "' and docTyp in (" + selDocType.ToString().TrimEnd(',') + ") and checkStamp='1' order by subDte desc";
                }
            }
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid3.DataSource = dt;
            Grid3.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid3.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            //// 更改每页显示数目时，防止 PageIndex 越界
            //if (Grid1.PageIndex > Grid1.PageCount - 1)
            //{
            //    Grid1.PageIndex = Grid1.PageCount - 1;
            //}
        }

        protected void Grid3_PageIndexChange(object sender, GridPageEventArgs e)
        {

        }

        //protected void Grid2_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        //{
        //    try
        //    {
        //        bindDocList(Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
        //        txb_hd1.Text = Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString();//隐藏域保存选择的目录ID
        //        txb_fileindex.Text = Grid2.Rows[Grid2.SelectedRowIndex].Values[0].ToString();
        //    }
        //    catch (Exception ex) 
        //    {
        //        Alert.Show(ex.Message);
        //    }
        //}
        
        //新增分类
        //protected void btn_newIndex_Click(object sender, EventArgs e)
        //{
        //    try 
        //    {
        //        if (!string.IsNullOrEmpty(txb_AddIndexName.Text))
        //        {
        //            string sqlCmd = "insert into OA_DocMgmt_DocType (type_name,pid,stat) values ";
        //            sqlCmd += "('" + txb_AddIndexName.Text + "','0','1')";
        //            SqlSel.ExeSql(sqlCmd);
        //            bindDocType();
        //        }
        //        else 
        //        {
        //            Alert.ShowInTop("请先输入目录名称后再添加！");
        //        }
        //    }
        //    catch (Exception ex) 
        //    {
        //        Alert.Show(ex.Message);
        //    }
        //}

        //删除分类
        //protected void btn_delIndex_Click(object sender, EventArgs e)
        //{
        //    try 
        //    {
        //        if (Grid2.SelectedRowIndex >= 0)
        //        {
        //            string typeId = Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString();
        //            string sqlCmd = "update OA_DocMgmt_DocType set stat=0 where id='" + typeId + "'";
        //            SqlSel.ExeSql(sqlCmd);
        //            bindDocType();
        //        }
        //        else 
        //        {
        //            Alert.ShowInTop("请先选定要删除的目录！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.Show(ex.Message);
        //    }
        //}

        //protected void btn_upload_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txb_hd1.Text))
        //    {
        //        Alert.ShowInTop("先选择文件目录后再操作！");
        //        return;
        //    }
        //    else
        //    {
        //        if (fileup.HasFile)
        //        {
        //            string curUser = GetUser();//当前登录人员
        //            if (!string.IsNullOrEmpty(curUser))
        //            {

        //                DataTable dt = new DataTable();
        //                string fileName = fileup.ShortFileName;
        //                string sfileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
        //                string filetyp = sfileName.Substring(sfileName.LastIndexOf(".") + 1);
        //                if (!isValidType(filetyp))
        //                {
        //                    Alert.ShowInTop("上传类型无效！请重新上传。");
        //                    return;
        //                }
        //                else
        //                {
        //                    DateTime subdte = DateTime.Now;
        //                    string efileName = DateTime.Now.Ticks.ToString();
        //                    fileup.SaveAs(Server.MapPath("~/upload/" + sfileName));
        //                    string sqlCmd = "insert into OA_DocMgmt_DocList (docName,docTyp,docStat,subman,subDte) values ";
        //                    sqlCmd += "('" + sfileName + "'," + txb_hd1.Text + ",1,'" + curUser + "','" + subdte + "')";
        //                    int exeCount = SqlSel.ExeSql(sqlCmd);
        //                    if (exeCount == 0)
        //                    {
        //                        Alert.ShowInTop("上传出错！");
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        bindDocList(txb_hd1.Text);
        //                        Alert.ShowInTop("上传成功！");
        //                        SimpleForm2.Reset();
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Alert.ShowInTop("请选择上传文件!");
        //            return;
        //        }
        //    }
        //}

        //验证文件格式合法性
        //protected static bool isValidType(string fileType)
        //{
        //    string[] typeName = new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx", "pdf", "jpg", "png", "bmp", "txt" };
        //    int id = Array.IndexOf(typeName, fileType);
        //    if (id != -1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //protected void btn_online_Click(object sender, EventArgs e)
        //{
        //    string sqlCmd = "";
        //    //获取多选的行数
        //    int selectedCount = Grid3.SelectedRowIndexArray.Length;
        //    if (selectedCount == 0)
        //    {
        //        Alert.ShowInTop("请先选择相应文件！");
        //        return;
        //    }
        //    for (int i = 0; i < selectedCount; i++)
        //    {
        //        //获取行index
        //        int rowindex = Grid3.SelectedRowIndexArray[i];
        //        int fileid = Convert.ToInt32(Grid3.DataKeys[rowindex][0]);
        //        sqlCmd = "UPDATE OA_DocMgmt_DocList SET docStat='1' WHERE ID=" + fileid;
        //        int exeCount = SqlSel.ExeSql(sqlCmd);
        //        if (exeCount == 0)
        //        {
        //            Alert.ShowInTop("出错了！");
        //            return;
        //        }
        //    }
        //    Alert.ShowInTop("已生效！");
        //    bindDocList(txb_hd1.Text);
        //}

        //protected void btn_Invalid_Click(object sender, EventArgs e)
        //{
        //    string sqlCmd = "";
        //    //获取多选的行数
        //    int selectedCount = Grid3.SelectedRowIndexArray.Length;
        //    if (selectedCount == 0)
        //    {
        //        Alert.ShowInTop("请先选择相应文件！");
        //        return;
        //    }
        //    for (int i = 0; i < selectedCount; i++)
        //    {
        //        //获取行index
        //        int rowindex = Grid3.SelectedRowIndexArray[i];
        //        int fileid = Convert.ToInt32(Grid3.DataKeys[rowindex][0]);
        //        sqlCmd = "UPDATE OA_DocMgmt_DocList SET docStat='0' WHERE ID=" + fileid;
        //        int exeCount = SqlSel.ExeSql(sqlCmd);
        //        if (exeCount == 0)
        //        {
        //            Alert.ShowInTop("出错了！");
        //            return;
        //        }
        //    }
        //    Alert.ShowInTop("已失效！");
        //    bindDocList(txb_hd1.Text);
        //}

    }
}