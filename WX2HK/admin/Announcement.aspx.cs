using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.IO;

namespace WX2HK.admin
{
    public partial class Announcement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_publish_Click(object sender, EventArgs e)
        {
            try
            {
                string context = txb_title.Text;
                string sqlCmd = "insert into OA_Announcement (AnncTitle,AnncContext,AnncAuthor,CreateTime,isDeleted) ";
                sqlCmd += "values ('" + txb_title.Text + "','" + HtmlEditor1.Text + "','" + GetUser() + "','" + DateTime.Now + "','0')";
                int execCount = SqlSel.ExeSql(sqlCmd);
                if (execCount == 1) 
                {
                    sqlCmd = "select max(id) from OA_Announcement";
                    int anncId = Convert.ToInt32(SqlSel.GetSqlScale(sqlCmd));
                    //插入附件
                    InsertFiles(anncId.ToString());
                    //微信推送
                    if (isPushWX.Checked)
                    {
                        DataTable wxUserDt = selectUser.getDeptUserList(System.Configuration.ConfigurationManager.AppSettings["CorpDeptId"]);//得到部门中的所有人员信息
                        System.Text.StringBuilder toUsers = new System.Text.StringBuilder();
                        for (int i = 0; i < wxUserDt.Rows.Count; i++)
                        {
                            toUsers.AppendFormat("{0}|", wxUserDt.Rows[i]["userid"].ToString());
                        }
                        string url = targetUrl(string.Format("admin%2fAnnouncement_Detail.aspx?id={0}", anncId));
                        ReturnInfo.pushMessage(toUsers.ToString(), "公告信息", url, context, "");
                    }
                    Alert.Show("公告发布成功！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
                return;
            }
        }

        //插入附件信息
        private void InsertFiles(string formId)
        {
            string sqlCmd = "";
            foreach (GridRow gr in Grid1.Rows)
            {
                string fileName = gr.DataKeys[0].ToString();//服务器文件名
                string realFileName = gr.Values[1].ToString();//原文件名
                sqlCmd = "insert into OA_Sys_files (FormId,fileName,realFileName,formDataName) values ('" + formId + "','" + fileName + "','" + realFileName + "','OA_Announcement')";
                SqlSel.ExeSql(sqlCmd);
            }
        }

        protected void fileUp_FileSelected(object sender, EventArgs e)
        {
            if (fileUp.HasFile)
            {
                if (!string.IsNullOrEmpty(GetUser()))
                {

                    DataTable dt = new DataTable();
                    string fileName = fileUp.ShortFileName;
                    string sfileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    string filetyp = sfileName.Substring(sfileName.LastIndexOf(".") + 1);
                    if (!isValidFileType(filetyp))
                    {
                        Alert.ShowInTop("上传类型无效！请重新上传。");
                        return;
                    }
                    else
                    {
                        string efileName = DateTime.Now.Ticks.ToString() + "." + filetyp;
                        fileUp.SaveAs(Server.MapPath("~/image/" + efileName));
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
                    dr[1] = grow.Values[1].ToString();
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

                string filePath = "~/image/" + Grid1.Rows[e.RowIndex].DataKeys[0].ToString();
                if (File.Exists(Server.MapPath(filePath)))
                {
                    //如果文件存在则删除
                    File.Delete(Server.MapPath(filePath));
                }
                Grid1.DataSource = curDt;
                Grid1.DataBind();
            }
        }
    }
}