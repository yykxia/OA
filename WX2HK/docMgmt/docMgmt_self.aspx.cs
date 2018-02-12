using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.docMgmt
{
    public partial class docMgmt_self : BasePage
    {
        static string curUser = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                curUser = GetUser();
                //加载个人文档列表
                bindGrid();

                //
                bindProj();
                //
                bindDocType();
            }
        }


        //
        private void bindGrid() 
        {
            string sqlCmd = "select A.*,B.type_name,c.projName,(case when d.docid is null then '无' else '已编辑' end) as editStatus,";
            sqlCmd+="(case when checkStamp='0' then '未审' when checkStamp='-1' then '否决' when checkStamp='1' then '通过' end) as checkStatus,";
            sqlCmd += "(case when checkStamp='0' then '编辑' else '' end) as editOrNo";
            sqlCmd += " from OA_DocMgmt_DocList A left join OA_DocMgmt_DocType B ON B.ID=A.docTyp";
            sqlCmd += " left join OA_sys_Project C on c.id=A.doc_leng left join OA_DocMgmt_Propety D on D.docId=A.id";
            sqlCmd += " where subMan='" + curUser + "' and docStat='1' order by projName,subDte desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            Grid3.DataSource = dt;
            Grid3.DataBind();
        }
        //protected void Grid3_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "isShare")
        //        {
        //            CheckBoxField checkField = (CheckBoxField)Grid3.FindColumn(e.ColumnIndex);
        //            bool checkState = checkField.GetCheckedState(e.RowIndex);
        //            string docId = Grid3.DataKeys[e.RowIndex][0].ToString();
        //            ShareDoc(docId, checkState);
        //        }
        //    }
        //    catch (Exception ex) 
        //    {
        //        Alert.ShowInTop(ex.Message);
        //    }
        //}

        //筛选与用户相关的项目
        private void bindProj() 
        {
            string sqlCmd = "select B.id,B.projName from OA_Sys_ProjMember A left join OA_sys_Project B on A.projId=B.id where A.userId='" + curUser + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_proj.DataTextField = "projName";
            ddl_proj.DataValueField = "id";
            ddl_proj.DataSource = dt;
            ddl_proj.DataBind();
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
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if (fileup.HasFile)
            {
                if (!string.IsNullOrEmpty(curUser))
                {

                    DataTable dt = new DataTable();
                    string fileName = fileup.ShortFileName;
                    string sfileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    string filetyp = sfileName.Substring(sfileName.LastIndexOf(".") + 1);
                    if (!isValidType(filetyp))
                    {
                        Alert.ShowInTop("上传类型无效！请重新上传。");
                        return;
                    }
                    else
                    {
                        DateTime subdte = DateTime.Now;
                        string efileName = DateTime.Now.Ticks.ToString() + "." + filetyp;
                        fileup.SaveAs(Server.MapPath("~/upload/" + efileName));
                        string sqlCmd = "insert into OA_DocMgmt_DocList (docName,docTyp,docStat,subman,subDte,doc_leng,docPath) values ";
                        sqlCmd += "('" + sfileName + "','" + ddl_docType.SelectedValue + "',1,'" + curUser + "','" + subdte + "','" + ddl_proj.SelectedValue + "','" + efileName + "')";
                        int exeCount = SqlSel.ExeSql(sqlCmd);
                        if (exeCount == 0)
                        {
                            Alert.ShowInTop("上传出错！");
                            return;
                        }
                        else
                        {
                            bindGrid();
                            Alert.ShowInTop("上传成功！");
                            fileup.Reset();
                        }
                    }
                }
            }
            else
            {
                Alert.ShowInTop("请选择上传文件!");
                return;
            }
            
        }

        //验证文件格式合法性
        protected static bool isValidType(string fileType)
        {
            string[] typeName = new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx", "pdf", "jpg", "JPG", "png", "bmp", "txt" };
            int id = Array.IndexOf(typeName, fileType);
            if (id != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //文档共享
        //private void ShareDoc(string docId,bool shareStat) 
        //{
        //    string stat = "";
        //    if (shareStat)
        //    {
        //        stat = "1";
        //    }
        //    else 
        //    {
        //        stat = "0";
        //    }
        //    string sqlCmd = "update OA_DocMgmt_DocList set isShare='" + stat + "' where id=" + docId;
        //    int exeCount = SqlSel.ExeSql(sqlCmd);
        //    if (exeCount == 0)
        //    {
        //        Alert.ShowInTop("出错了！");
        //        return;
        //    }
        //}
        protected void btn_Invalid_Click(object sender, EventArgs e)
        {
            //获取多选的行数
            int selectedCount = Grid3.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请先选择相应文件！");
                return;
            }
            if (existInvalidDoc())
            {
                PageContext.RegisterStartupScript(Confirm.GetShowReference("确认删除？", String.Empty, MessageBoxIcon.Question, PageManager1.GetCustomEventReference("Confirm_OK"), PageManager1.GetCustomEventReference("Confirm_Cancel")));
            }
            else 
            {
                Alert.ShowInTop("已审核通过的文档不可删除，请查验！");
                return;
            }
        }

        private void executeDel()
        {
            string sqlCmd = "";
            //获取多选的行数
            int selectedCount = Grid3.SelectedRowIndexArray.Length;
            for (int i = 0; i < selectedCount; i++)
            {
                //获取行index
                int rowindex = Grid3.SelectedRowIndexArray[i];
                int fileid = Convert.ToInt32(Grid3.DataKeys[rowindex][0]);
                sqlCmd = "UPDATE OA_DocMgmt_DocList SET docStat='0' WHERE ID=" + fileid;
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount == 0)
                {
                    Alert.ShowInTop("出错了！");
                    return;
                }
            }
            Alert.ShowInTop("已删除！");
            bindGrid();
        }

        protected string GetEditUrl(object id)
        {
            return Window1.GetShowReference("docMgmt_PropetyEdit.aspx?id=" + id);
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int[] selections = Grid3.SelectedRowIndexArray;
            if (selections.Length > 0)
            {
                if (existInvalidDoc())//判断是否已审核
                {
                    if (existEditDoc())
                    {
                        if (isOneProj())
                        {
                            if (isOneDocType())
                            {
                                foreach (int rowIndex in selections)
                                {
                                    sb.AppendFormat("{0};", Grid3.DataKeys[rowIndex][0].ToString());
                                }
                                PageContext.RegisterStartupScript(Window1.GetShowReference("docMgmt_PropetyEdit_Batch.aspx?docList=" + sb.ToString()));
                            }
                            else
                            {
                                Alert.ShowInTop("非同一类型文档不可同时编辑！");
                                return;
                            }
                        }
                        else
                        {
                            Alert.ShowInTop("非同一项目文档不可同时编辑！");
                            return;
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("已编辑文档不支持批量编辑，请单独修改文档信息！");
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("存在已审核文档，不可再编辑！");
                    return;
                }
            }
        }

        //验证是否同一项目
        private bool isOneProj()
        {
            int[] selections = Grid3.SelectedRowIndexArray;
            string projName = Grid3.Rows[selections[0]].Values[3].ToString();//取选择的第一行项目名称
            int find = -1;
            for (int i = 1; i < selections.Length; i++)
            {
                string compPorjName = Grid3.Rows[selections[i]].Values[3].ToString();
                if (projName != compPorjName)
                {
                    find = i;
                }
            }

            if (find == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //验证是否同一文件类型
        private bool isOneDocType()
        {
            int[] selections = Grid3.SelectedRowIndexArray;
            string TypeName = Grid3.Rows[selections[0]].Values[2].ToString();//取选择的第一行文档类型
            int find = -1;
            for (int i = 1; i < selections.Length; i++)
            {
                string compTypeName = Grid3.Rows[selections[i]].Values[2].ToString();
                if (TypeName != compTypeName)
                {
                    find = i;
                }
            }

            if (find == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool existEditDoc() 
        {
            int[] selections = Grid3.SelectedRowIndexArray;
            int find = -1;
            for (int i = 0; i < selections.Length; i++)
            {
                string EditStatus = Grid3.Rows[selections[i]].Values[4].ToString();
                if (EditStatus == "已编辑")
                {
                    find = i;
                    break;
                }
            }

            if (find == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断删除是否可用
        private bool existInvalidDoc()
        {
            int[] selections = Grid3.SelectedRowIndexArray;
            int find = -1;
            for (int i = 0; i < selections.Length; i++)
            {
                string EditStatus = Grid3.Rows[selections[i]].Values[5].ToString();
                if (EditStatus == "通过")
                {
                    find = i;
                    break;
                }
            }

            if (find == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //页面客户端事件
        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")//确认删除文档事件
            {
                executeDel();
            }
            else if (e.EventArgument == "Confirm_Cancel")
            { 
                
            }
        }


    }
}