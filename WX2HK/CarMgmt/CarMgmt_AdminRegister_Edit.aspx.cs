using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IETCsoft.sql;
using FineUI;

namespace WX2HK.CarMgmt
{
    public partial class CarMgmt_AdminRegister_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                string tabId = Request["id"];
                label_tabId.Text = tabId;
                //加载现有用车申请信息
                loadInfo(tabId);
            }

        }

        private void loadInfo(string tabId)
        {
            string sqlCmd = "select A.*,c.chineseName,d.deptName,t.flowName,f.projName,";
            sqlCmd += "(case when deptCarId is null then A.carNumb else tt.propertyName end) as carName from OA_Car_Main A";
            sqlCmd += " left join OA_Sys_EmployeeInfo c on c.id=a.reqMan left join OA_sys_department d on d.id=c.deptId left join OA_sys_Project f on f.id=a.projId";
            sqlCmd += " left join OA_sys_flow t on t.id=A.flowId left join OA_Property_Register tt on tt.id=A.deptCarId where A.id='" + tabId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);

            label_name.Text = dt.Rows[0]["chineseName"].ToString() + "/" + dt.Rows[0]["deptName"].ToString();
            label_date.Text = Convert.ToDateTime(dt.Rows[0]["reqDte"]).ToShortDateString();
            label_carName.Text = dt.Rows[0]["carName"].ToString();
            rdbl_status.SelectedValue = dt.Rows[0]["adminRegister"].ToString();
            if (dt.Rows[0]["adminRegister"].ToString() == "1") 
            {
                rdbl_status.Items.RemoveAt(2);//已发车申请不可取消
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["actualUseTime"].ToString())) 
            {
                DatePicker1.SelectedDate = Convert.ToDateTime(dt.Rows[0]["actualUseTime"]);
                TimePicker1.Text = Convert.ToDateTime(dt.Rows[0]["actualUseTime"]).ToShortTimeString();
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["actualBackTime"].ToString()))
            {
                DatePicker2.SelectedDate = Convert.ToDateTime(dt.Rows[0]["actualBackTime"]);
                TimePicker2.Text = Convert.ToDateTime(dt.Rows[0]["actualBackTime"]).ToShortTimeString();
            }

            numbbox_actualMileage.Text = dt.Rows[0]["actualMileage"].ToString();
            numbbox_endMileage.Text = dt.Rows[0]["endMileage"].ToString();
            label_toWhere.Text = dt.Rows[0]["toWhere"].ToString();
            label_reason.Text = dt.Rows[0]["useReason"].ToString();

        }

        protected void rdbl_status_SelectedIndexChanged(object sender, EventArgs e)
        {

            numbbox_actualMileage.Enabled = true;
            DatePicker1.Enabled = true;
            TimePicker1.Enabled = true;
            DatePicker2.Enabled = true;
            TimePicker2.Enabled = true;
            numbbox_endMileage.Enabled = true;

            if (rdbl_status.SelectedValue == "-1") 
            {
                numbbox_actualMileage.Enabled = false;
                DatePicker1.Enabled = false;
                TimePicker1.Enabled = false;
                DatePicker2.Enabled = false;
                TimePicker2.Enabled = false;
                numbbox_endMileage.Enabled = false;
            } 
            if (rdbl_status.SelectedValue == "1")
            {
                DatePicker2.Enabled = false;
                TimePicker2.Enabled = false;
                numbbox_endMileage.Enabled = false;
            }
            if (rdbl_status.SelectedValue == "2")
            {
                numbbox_actualMileage.Enabled = false;
                DatePicker1.Enabled = false;
                TimePicker1.Enabled = false;
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sqlCmd = "";
            int execCount = 0;
            if (rdbl_status.SelectedValue == "1") //发车登记
            {
                if (string.IsNullOrEmpty(DatePicker1.Text) || string.IsNullOrEmpty(TimePicker1.Text) || string.IsNullOrEmpty(numbbox_actualMileage.Text))
                {
                    Alert.ShowInTop("发车信息不可为空！");
                    return;
                }
                else 
                {
                    sqlCmd = "update OA_Car_Main set actualMileage='" + numbbox_actualMileage.Text + "',actualUseTime='" + DatePicker1.Text + "'+' '+'" + TimePicker1.Text + "',";
                    sqlCmd += "adminRegister='1' where id='" + label_tabId.Text + "'";
                    execCount = SqlSel.ExeSql(sqlCmd);
                }
            }
            if (rdbl_status.SelectedValue == "2")//返还登记
            {
                if (string.IsNullOrEmpty(DatePicker2.Text) || string.IsNullOrEmpty(TimePicker2.Text) || string.IsNullOrEmpty(numbbox_endMileage.Text))
                {
                    Alert.ShowInTop("返还信息不可为空！");
                    return;
                }
                else
                {
                    sqlCmd = "update OA_Car_Main set endMileage='" + numbbox_endMileage.Text + "',actualBackTime='" + DatePicker2.Text + "'+' '+'" + TimePicker2.Text + "',";
                    sqlCmd += "adminRegister='2' where id='" + label_tabId.Text + "'";
                    execCount = SqlSel.ExeSql(sqlCmd);

                    //关联车辆解除绑定
                    if (execCount == 1)
                    {
                        sqlCmd = "update OA_Car_Register set OnUsing='0' where id=(select deptCarId from OA_Car_Main where id='" + label_tabId.Text + "')";
                        SqlSel.ExeSql(sqlCmd);
                    }
                }
            } 
            if (rdbl_status.SelectedValue == "-1")//取消申请
            {
                sqlCmd = "update OA_Car_Main set adminRegister='-1' where id='" + label_tabId.Text + "'";
                execCount = SqlSel.ExeSql(sqlCmd);

                //关联车辆解除绑定
                if (execCount == 1)
                {
                    sqlCmd = "update OA_Car_Register set OnUsing='0' where id=(select deptCarId from OA_Car_Main where id='" + label_tabId.Text + "')";
                    SqlSel.ExeSql(sqlCmd);
                }

            }

            if (execCount == 1)
            {
                Alert.Show("登记成功！");
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
            else 
            {
                Alert.ShowInTop("登记失败！请联系管理员。");
                return;
            }
        }
    }
}