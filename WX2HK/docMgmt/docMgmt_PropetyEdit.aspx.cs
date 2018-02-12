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
    public partial class docMgmt_PropetyEdit : System.Web.UI.Page
    {
        private static string recvId = string.Empty;
        private static string UpdateAction = string.Empty;//0:保存，1：更新

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                recvId = Request["id"];
                ddlBind();
                loadInfo();
            }
        }
        //加载项目中同类型文档作为来源文档
        private void ddlBind() 
        {
            string sqlCmd = "select A.*,B.docSN from OA_DocMgmt_DocList A left join OA_DocMgmt_Propety B ON A.ID=b.docId where docTyp=(select docTyp from OA_DocMgmt_DocList where id='" + recvId + "') ";
            sqlCmd += "and doc_leng=(select doc_leng from OA_DocMgmt_DocList where id='" + recvId + "') and id <> '" + recvId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            ddl_sourceDoc.DataTextField = "docSN";
            ddl_sourceDoc.DataValueField = "id";
            ddl_sourceDoc.DataSource = dt;
            ddl_sourceDoc.DataBind();
        }

        private void loadInfo() 
        {
            string sqlCmd = "select * from [OA_DocMgmt_Propety] where docId='" + recvId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            if (dt.Rows.Count > 0)
            {
                UpdateAction = "1";//按钮动作为：更新
                txb_docSN.Text = dt.Rows[0]["docSN"].ToString();
                ddl_sourceDoc.SelectedValue = dt.Rows[0]["SourceDocId"].ToString();
                txa_AgreementPaty.Text = dt.Rows[0]["AgreementPaty"].ToString();
                txa_SubjectMatter.Text = dt.Rows[0]["SubjectMatter"].ToString();
                numb_amount.Text = dt.Rows[0]["AgreeAmount"].ToString();
                txb_AmountType.Text = dt.Rows[0]["AmountType"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["StartDate"].ToString()))
                {
                    DatePicker1.SelectedDate = Convert.ToDateTime(dt.Rows[0]["StartDate"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["EndDate"].ToString()))
                {
                    DatePicker2.SelectedDate = Convert.ToDateTime(dt.Rows[0]["EndDate"]);
                }
                txa_Remarks.Text = dt.Rows[0]["Remarks"].ToString();
            }
            else 
            {
                UpdateAction = "0";//按钮动作为：保存
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            if (UpdateAction == "0")
            {
                sqlCmd = "insert into OA_DocMgmt_Propety (docId,docSN,SourceDocId,AgreementPaty,SubjectMatter,AgreeAmount,AmountType,StartDate,EndDate,Remarks) values ";
                sqlCmd += "('" + recvId + "','" + txb_docSN.Text + "','" + ddl_sourceDoc.SelectedValue + "','" + txa_AgreementPaty.Text + "','" + txa_SubjectMatter.Text + "','" + numb_amount.Text + "',";
                sqlCmd += "'" + txb_AmountType.Text + "','" + DatePicker1.Text + "','" + DatePicker2.Text + "','" + txa_Remarks.Text + "')";
            }
            else
            {
                sqlCmd = "update OA_DocMgmt_Propety set docSN='" + txb_docSN.Text + "',SourceDocId='" + ddl_sourceDoc.SelectedValue + "',";
                sqlCmd += "AgreementPaty='" + txa_AgreementPaty.Text + "',SubjectMatter='" + txa_SubjectMatter.Text + "',AgreeAmount='" + numb_amount.Text + "',";
                sqlCmd += "AmountType='" + txb_AmountType.Text + "',StartDate='" + DatePicker1.Text + "',EndDate='" + DatePicker2.Text + "',Remarks='" + txa_Remarks.Text + "'";
            }
            sqlCmd = sqlCmd.Replace("''", "null");//空值保存为null
            SqlSel.ExeSql(sqlCmd);
            Alert.Show("保存成功！");
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}