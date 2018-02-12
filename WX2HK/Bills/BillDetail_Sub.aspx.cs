using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.Bills
{
    public partial class BillDetail_Sub : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = GetUser();
                loadInfo(userId);
                //
                loadFlowInfo("OA_Bills_Main", userId);
                //
                loadCostItem();

                //
                bindProj();
            }
        }

        private void loadFlowInfo(string formName, string userId)
        {
            try
            {
                DataTable dt = validFlow(formName, userId);
                ddl_flow.DataTextField = "flowName";
                ddl_flow.DataValueField = "id";
                ddl_flow.DataSource = dt;
                ddl_flow.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        //显示所有费用项列表
        private void loadCostItem()
        {
            string sqlCmd = "select * from OA_Bills_costLists where bStatus=1";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ckbl_costItems.DataSource = dt;
            ckbl_costItems.DataValueField = "id";
            ckbl_costItems.DataTextField = "CostItemName";
            ckbl_costItems.DataBind();
        }

        private void loadInfo(string userWXId)
        {
            if (!string.IsNullOrEmpty(userWXId))
            {
                string sqlCmd = "select A.*,B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id='" + userWXId + "'";
                DataTable dt = new DataTable();
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                label_name.Text = dt.Rows[0]["loginId"].ToString() + "/" + dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
                label_date.Text = DateTime.Now.ToShortDateString();
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
        }


        protected void uploadFile_FileSelected(object sender, EventArgs e)
        {
            if (fileup.HasFile)
            {
                if (!string.IsNullOrEmpty(GetUser()))
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
                        string efileName = DateTime.Now.Ticks.ToString() + "." + filetyp;
                        fileup.SaveAs(Server.MapPath("~/image/" + efileName));
                        DataTable curDt = getFileDataTable();
                        DataRow dr = curDt.NewRow();
                        dr[0] = efileName;
                        dr[1] = sfileName;
                        curDt.Rows.Add(dr);
                        Grid1.DataSource = curDt;
                        Grid1.DataBind();
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

        //获取现有文件列表
        private DataTable getFileDataTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("fileUrl");
                dt.Columns.Add("fileName");
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    GridRow grow = Grid1.Rows[i];
                    dr[0] = grow.DataKeys[0].ToString();
                    dr[1] = grow.Values[0].ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
                return null;
            }
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                DataTable curDt = getFileDataTable();
                curDt.Rows.RemoveAt(e.RowIndex);
                Grid1.DataSource = curDt;
                Grid1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string stepId = getStepId(ddl_flow.SelectedValue);
                string sqlCmd = "insert into OA_Bills_Main (reqMan,reqDte,billTotal,VoucherCount,FlowId,CurrentStepID,ReqReason,RelProj,ProveEmp) values ";
                sqlCmd += "('" + GetUser() + "','" + DateTime.Now + "','" + numbbox_total.Text.Trim() + "','" + numbbox_count.Text.Trim() + "','" + ddl_flow.SelectedValue + "','" + stepId + "','" + txa_reason.Text + "','" + ddl_proj.SelectedValue + "','" + txa_proveList.Text + "')";
                int exeCount = SqlSel.ExeSql(sqlCmd);
                if (exeCount > 0)
                {
                    //取当前单据id
                    sqlCmd = "select max(id) from OA_Bills_Main";
                    string formId = SqlSel.GetSqlScale(sqlCmd).ToString();
                    //插入附件信息表
                    InsertFiles(formId);

                    //插入关联费用项
                    string[] selectValueArray = ckbl_costItems.SelectedValueArray;
                    foreach (string item in selectValueArray)
                    {
                        sqlCmd = "insert into OA_Bills_RelCostItems ([formId],[CostItemId]) values (";
                        sqlCmd += "'" + formId + "','" + item + "')";
                        SqlSel.ExeSql(sqlCmd);
                    }

                    //插入证明人表
                    //if (!string.IsNullOrEmpty(txa_proveList.Text))
                    //{
                    //    insertProveList(txa_proveList.Text, formId);
                    //}

                    Alert.Show("提交成功！");

                    //表单重置
                    SimpleForm1.Reset();
                    Grid1.DataSource = null;
                    Grid1.DataBind();
                    //推送信息至相关审批人
                    pushMessage(stepId, "OA_Bills_Main", formId, "费用报销");

                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        //插入附件信息
        private void InsertFiles(string formId)
        {
            string sqlCmd = "";
            foreach (GridRow gr in Grid1.Rows)
            {
                string fileName = gr.DataKeys[0].ToString();
                sqlCmd = "insert into OA_Sys_files (FormId,fileName,formDataName) values ('" + formId + "','" + fileName + "','OA_Bills_Main')";
                SqlSel.ExeSql(sqlCmd);
            }
        }


        private string getStepId(string flowId)
        {
            string sqlCmd = "select id from OA_Sys_Flow_Step where flowid='" + flowId + "' and stepOrderNo=1";
            string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
            return stepId;
        }

        protected void btn_provf_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txa_proveList.ClientID)
                    + Window1.GetShowReference("../selectUser_pc.aspx"));
        }

        //插入证明人
        private void insertProveList(string userStr, string formId)
        {
            string userId = string.Empty, sqlCmd = string.Empty;
            String[] str = userStr.Split(';');//格式为{@id-name;}
            foreach (string it in str)
            {
                if (it == "")
                {
                    break;
                }
                userId = it.Substring(0, it.IndexOf("-"));
                sqlCmd = "insert into OA_Bills_ProveEmp (FormId,ProveEmpId) values ('" + formId + "','" + userId + "')";
                SqlSel.ExeSql(sqlCmd);
            }
        }


    }
}