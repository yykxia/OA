using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IETCsoft.sql;
using FineUI;

namespace WX2HK.docMgmt
{
    public partial class docMgmt_PropetyEdit_Batch : System.Web.UI.Page
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                recvId = Request["docList"];
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {           
            String[] str = recvId.Split(';');
            foreach (string it in str)
            {
                if (it == "")
                {
                    break;
                }
                string sqlCmd = "insert into OA_DocMgmt_Propety (docId,docSN,AgreementPaty,SubjectMatter,AgreeAmount,AmountType,StartDate,EndDate,Remarks) values ";
                sqlCmd += "('" + it + "','" + txb_docSN.Text + "','" + txa_AgreementPaty.Text + "','" + txa_SubjectMatter.Text + "','" + numb_amount.Text + "',";
                sqlCmd += "'" + txb_AmountType.Text + "','" + DatePicker1.Text + "','" + DatePicker2.Text + "','" + txa_Remarks.Text + "')";
                SqlSel.ExeSql(sqlCmd);
            }
            Alert.Show("编辑完成！");
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}