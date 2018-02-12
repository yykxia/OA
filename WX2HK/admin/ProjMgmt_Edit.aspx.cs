using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;

namespace WX2HK.admin
{
    public partial class ProjMgmt_Edit : System.Web.UI.Page
    {
        private static string recvId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                recvId = Request["id"];//项目id

                if (!string.IsNullOrEmpty(recvId))
                {
                    loadInfo();
                }
            }
        }

        private void loadInfo()
        {
            string sqlCmd = "select * from OA_sys_Project where id=" + recvId; ;
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            txb_ProjName.Text = dt.Rows[0]["ProjName"].ToString();
            txa_ProjDesc.Text = dt.Rows[0]["ProjDesc"].ToString();
            txa_workSpace.Text = dt.Rows[0]["workSpace"].ToString();
            txa_projItems.Text = dt.Rows[0]["projItems"].ToString();
            txb_WorkFor.Text = dt.Rows[0]["workFor"].ToString();
            numb_amount.Text = dt.Rows[0]["projAmount"].ToString();
            Numb_workArea.Text = dt.Rows[0]["workArea"].ToString();

            string useStatus = dt.Rows[0]["ProjStatus"].ToString();

            rdbl_status.SelectedValue = useStatus;
        }
        
        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            if (string.IsNullOrEmpty(recvId))
            {
                sqlCmd = "insert into OA_sys_Project (ProjName,FoundTime,ProjStatus,ProjDesc,workSpace,projItems,workFor,projAmount,workArea) values (";
                sqlCmd += "'" + txb_ProjName.Text + "','" + DateTime.Now + "','" + rdbl_status.SelectedValue + "','" + txa_ProjDesc.Text + "',";
                sqlCmd += "'" + txa_workSpace.Text + "','" + txa_projItems.Text + "','" + txb_WorkFor.Text + "','" + numb_amount.Text + "','" + Numb_workArea.Text + "')";
                SimpleForm1.Reset();
            }
            else
            {
                sqlCmd = "update OA_sys_Project set ProjName='" + txb_ProjName.Text + "',ProjStatus='" + rdbl_status.SelectedValue + "',ProjDesc='" + txa_ProjDesc.Text + "',";
                sqlCmd += "workSpace='" + txa_workSpace.Text + "',projItems='" + txa_projItems.Text + "',workFor='" + txb_WorkFor.Text + "',projAmount='" + numb_amount.Text + "',workArea='" + Numb_workArea.Text + "' where id=" + recvId;

            }
            SqlSel.ExeSql(sqlCmd);
            Alert.Show("保存成功！请关闭当前窗口。");
        }
    }
}