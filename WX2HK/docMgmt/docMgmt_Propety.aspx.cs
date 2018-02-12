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
    public partial class docMgmt_Propety : System.Web.UI.Page
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                recvId = Request["id"];
                loadInfo();
            }
        }

        //加载项目中同类型文档作为来源文档
        //private void ddlBind()
        //{
        //    string sqlCmd = "select A.*,B.docSN from OA_DocMgmt_DocList A left join OA_DocMgmt_Propety B ON A.ID=b.docId where docTyp=(select docTyp from OA_DocMgmt_DocList where id='" + recvId + "') ";
        //    sqlCmd += "and doc_leng=(select doc_leng from OA_DocMgmt_DocList where id='" + recvId + "') and id <> '" + recvId + "'";
        //    DataTable dt = new DataTable();
        //    SqlSel.GetSqlSel(ref dt, sqlCmd);
        //    ddl_sourceDoc.DataTextField = "docName";
        //    ddl_sourceDoc.DataValueField = "id";
        //    ddl_sourceDoc.DataSource = dt;
        //    ddl_sourceDoc.DataBind();
        //}


        private void loadInfo()
        {
            string sqlCmd = "select *,(select docSN from OA_DocMgmt_Propety A where A.docId=B.SourceDocId) as sourceDoc from [OA_DocMgmt_Propety] B where docId='" + recvId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            if (dt.Rows.Count > 0)
            {
                txb_docSN.Text = dt.Rows[0]["docSN"].ToString();
                label_sourceDoc.Text = dt.Rows[0]["sourceDoc"].ToString();
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
        }

    }
}