using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using WX2HK;
using Newtonsoft.Json;

namespace WX2HK.Attendance
{
    public partial class Attendence_Register_m : BasePage
    {
        public string Register_TimeStamp = "";
        public string Register_Nonce = "";
        public string Register_MsgSig = "";
        private static string curUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //
                string code = string.Empty;
                code = HttpContext.Current.Request.QueryString["Code"];
                string curPageName = "Attendance%2fAttendence_Register_m.aspx";
                curUserId = WX2HK.ReturnInfo.GetUserId(code, curPageName);//用户企业号Id
                loadInfo(curUserId);

                label_date.Text = DateTime.Now.ToShortDateString();
                //
                Register_TimeStamp = WX2HK.ReturnInfo.GetTimeStamp();
                Register_Nonce = WX2HK.ReturnInfo.randNonce();
                string curUrl = string.Format("Attendance/Attendence_Register_m.aspx?code={0}&state=STATE", code);
                Register_MsgSig = WX2HK.VerifyLegal.createSign(Register_Nonce, Register_TimeStamp, curUrl);
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
            else
            {
                SimpleForm1.Hidden = true;
                HttpContext.Current.Response.Write("信息不存在或非企业内部人员！");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int[] selections = Grid1.SelectedRowIndexArray;

                //double currentLatitude = Convert.ToDouble(latitude.Value);//当前纬度
                //double currentLongitude = Convert.ToDouble(longitude.Value);//当前经度
                //double corpLatitude = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["CorpLatitude"]);//企业中心纬度
                //double corpLongitude = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["CorpLongitude"]);//企业中心经度
                //double dis = GetDistance(currentLatitude, currentLongitude, corpLatitude, corpLongitude);//距离
                if (selections.Length>0)
                {
                    string locationInfo = Grid1.Rows[selections[0]].Values[0].ToString();
                    string sqlCmd = "insert into OA_Attendence_Record (userId,recordDate,recordTime,locationInfo) values ('" + curUserId + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now + "','" + locationInfo + "')";
                    int exeCount = SqlSel.ExeSql(sqlCmd);
                    if (exeCount == 1)
                    {
                        Alert.Show("打卡成功！");
                        btnSubmit.Text = "已打卡";
                        btnSubmit.Enabled = false;
                    }
                    else
                    {
                        Alert.Show("打卡失败！");                        
                    }
                }
                else 
                {
                    Alert.ShowInTop("请先定位您的位置信息！");
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "打卡失败！");
            }
        }

        protected void btn_position_Click(object sender, EventArgs e)
        {
            try
            {
                double currentLatitude = Convert.ToDouble(latitude.Value);//当前纬度
                double currentLongitude = Convert.ToDouble(longitude.Value);//当前经度
                string qqMapApiUrl = string.Format("http://apis.map.qq.com/ws/geocoder/v1/?location={0},{1}&key=HCCBZ-VEYKP-WTYDT-LH2XB-2D4O2-Z6FTM&get_poi=1", currentLatitude, currentLongitude);
                var client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                //地址信息的json字符串
                var data = client.DownloadString(qqMapApiUrl);
                //txa_info.Text = data;
                DataTable adrDt = new DataTable();
                adrDt.Columns.Add("poisInfo", typeof(string));
                JavaScriptSerializer js = new JavaScriptSerializer();
                AdressInfo.requestResult addr = js.Deserialize<AdressInfo.requestResult>(data);
                //正确返回
                if (addr.status == 0)
                {
                    AdressInfo.Pois[] pois = addr.result.pois;
                    foreach (AdressInfo.Pois ps in pois)
                    {
                        if (ps._distance <= 800)
                        {
                            DataRow dr = adrDt.NewRow();
                            dr["poisInfo"] = ps.address + "[" + ps.title + "]";
                            adrDt.Rows.Add(dr);
                        }
                    }
                    Grid1.DataSource = adrDt;
                    Grid1.DataBind();

                }
                else
                {
                    Alert.ShowInTop(addr.message);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        /// <summary>
        /// 获取签到类型
        /// </summary>
        /// <returns>1:签到;2:签退;0:无效</returns>
        private int recordType() 
        {
            string sqlCmd = "select * from OA_Attendence_Record where userId='" + curUserId + "' and recordDate='" + DateTime.Now.ToShortDateString() + "'";
            DataTable dt = new DataTable();
            if (SqlSel.GetSqlSel(ref dt, sqlCmd))
            {
                return 1;
            }
            else if (dt.Rows.Count == 1)
            {
                return 2;
            }
            else 
            {
                return 0;
            }
        }
    }
}