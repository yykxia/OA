using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.CarMgmt
{
    public partial class CarMgmt_Sub : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = GetUser();
                bindProj();
                loadInfo(userId);
                loadFlowInfo("OA_Car_Main", userId);
                carList();
            }
        }

        //显示可用车辆
        private void carList() 
        {
            string sqlCmd = "select * from OA_Property_Register where UseStatus='1' and OnUsing='0' and propertyType='2'";//资产类型为2的是单位车辆
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_carList.DataTextField = "propertyName";
            ddl_carList.DataValueField = "id";
            ddl_carList.DataSource = dt;
            ddl_carList.DataBind();
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

        private void loadInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                string sqlCmd = "select A.*,B.deptName from OA_Sys_EmployeeInfo A left join OA_sys_department B on A.deptId=B.id where A.id='" + userId + "'";
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


        //验证图片格式合法性
        protected static bool isValidType(string fileType)
        {
            string[] typeName = new string[] { "jpg", "JPG", "png", "bmp" };
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
                string stepId = getStepId(ddl_flow.SelectedValue);//获取步骤id
                string sqlCmd = "";
                if (checkBox_personal.Checked == true) //个人用车
                {
                    sqlCmd = "insert into OA_Car_Main ([reqMan],[reqDte],[carNumb],[curMileage],[withWho],[useReason],[FlowId],[CurrentStepID],[useDate],[backTime],[toWhere],[projId]) values ";
                    sqlCmd += "('" + GetUser() + "','" + DateTime.Now + "','" + txb_carNumb.Text.Trim() + "','" + numbbox_curMileage.Text + "','" + txb_withWho.Text + "',";
                    sqlCmd += "'" + TextArea_desc.Text.Trim() + "','" + ddl_flow.SelectedValue + "','" + stepId + "','" + DatePicker1.Text + " " + TimePicker1.Text + "',";
                    sqlCmd += "'" + DatePicker2.Text + " " + TimePicker2.Text + "','" + txb_toWhere.Text + "','" + ddl_proj.SelectedValue + "')";
                    int exeCount = SqlSel.ExeSql(sqlCmd);
                    if (exeCount > 0)
                    {
                        //取当前单据id
                        sqlCmd = "select max(id) from OA_Car_Main";
                        string formId = SqlSel.GetSqlScale(sqlCmd).ToString();
                        //插入附件信息表
                        InsertFiles(formId);

                        Alert.Show("提交成功！");

                        //表单重置
                        SimpleForm1.Reset();
                        Grid1.DataSource = null;
                        Grid1.DataBind();
                        //推送信息至相关审批人
                        pushMessage(stepId, "OA_Car_Main", formId, "用车申请");

                    }
                    else
                    {
                        Alert.Show("提交失败！");
                        return;
                    }

                }
                else //单位用车
                {
                    sqlCmd = "insert into OA_Car_Main ([reqMan],[reqDte],[deptCarId],[curMileage],[withWho],[useReason],[FlowId],[CurrentStepID],[useDate],[backTime],[toWhere],[projId]) values ";
                    sqlCmd += "('" + GetUser() + "','" + DateTime.Now + "','" + ddl_carList.SelectedValue + "','" + numbbox_curMileage.Text + "','" + txb_withWho.Text + "',";
                    sqlCmd += "'" + TextArea_desc.Text.Trim() + "','" + ddl_flow.SelectedValue + "','" + stepId + "','" + DatePicker1.Text + " " + TimePicker1.Text + "',";
                    sqlCmd += "'" + DatePicker2.Text + " " + TimePicker2.Text + "','" + txb_toWhere.Text + "','" + ddl_proj.SelectedValue + "')";
                    int exeCount = SqlSel.ExeSql(sqlCmd);
                    if (exeCount > 0)
                    {
                        //更新单位车辆征用状态
                        sqlCmd = "update OA_Property_Register set OnUsing='1' where id='" + ddl_carList.SelectedValue + "'";
                        SqlSel.ExeSql(sqlCmd);
                        //取当前单据id
                        sqlCmd = "select max(id) from OA_Car_Main";
                        string formId = SqlSel.GetSqlScale(sqlCmd).ToString();
                        //插入附件信息表
                        InsertFiles(formId);

                        Alert.Show("提交成功！");

                        //表单重置
                        SimpleForm1.Reset();
                        Grid1.DataSource = null;
                        Grid1.DataBind();
                        //推送信息至相关审批人
                        pushMessage(stepId, "OA_Car_Main", formId, "用车申请");

                    }
                    else
                    {
                        Alert.Show("提交失败！");
                        return;
                    }

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
                sqlCmd = "insert into OA_Sys_files (FormId,fileName,formDataName) values ('" + formId + "','" + fileName + "','OA_Car_Main')";
                SqlSel.ExeSql(sqlCmd);
            }
        }


        private string getStepId(string flowId)
        {
            string sqlCmd = "select id from OA_Sys_Flow_Step where flowid='" + flowId + "' and stepOrderNo=1";
            string stepId = SqlSel.GetSqlScale(sqlCmd).ToString();
            return stepId;
        }

        protected void checkBox_personal_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (checkBox_personal.Checked == true)
            {
                txb_carNumb.Enabled = true;
            }
            if (checkBox_personal.Checked == false) 
            {
                txb_carNumb.Enabled = false;
            }
        }
    }
}