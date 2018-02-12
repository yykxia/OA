using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using FineUI;
using System.Data;
using System.Text;
using IETCsoft.sql;
using System.Net;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WX2HK
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {

        }

        protected string GetUser()
        {
            try
            {
                string sessionUserId = Session["loginUser"].ToString();
                int userId = Convert.ToInt32(sessionUserId);

                return sessionUserId;
            }
            catch {
                PageContext.Redirect("~/default.aspx", "_top");
                return null;
            }
        }

        /// <summary>
        /// 对给定的一个图片文件生成一个指定大小的缩略图，并将缩略图保存到指定位置。
        /// </summary>
        /// <param name="originalImageFile">图片的物理文件地址</param>
        /// <param name="thumbNailImageFile">缩略图的物理文件地址</param>
        public static void MakeThumbNail(string originalImageFile, string thumbNailImageFile)
        {
            try
            {
                int thumbWidth = 231;    //要生成的缩略图的宽度
                int thumbHeight = 80;    //要生成的缩略图的高度

                System.Drawing.Image image = System.Drawing.Image.FromFile(originalImageFile); //利用Image对象装载源图像

                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。

                int srcWidth = image.Width;
                int srcHeight = image.Height;
                //double theRatio = (double)srcHeight / srcWidth;
                //int thumbHeight = Convert.ToInt32(theRatio * thumbWidth);
                Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);
                //if (srcHeight > srcWidth)
                //{
                //    bmp.RotateFlip(RotateFlipType.Rotate90FlipXY);
                //}

                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。

                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);

                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality

                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //下面这个也设成高质量

                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                //下面这个设成High

                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //把原始图像绘制成上面所设置宽高的缩小图

                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

                //保存图像

                bmp.Save(thumbNailImageFile);

                //释放资源
                bmp.Dispose();
                image.Dispose();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
                return;
            }
        }

        #region dataTable转换成Json格式
        /// <summary>  
        /// dataTable转换成Json格式  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        #endregion dataTable转换成Json格式


        /// <summary>  
        /// 插入节点审批对象并推送相关微信信息
        /// </summary>  
        /// <param name="stepId">审批步骤id</param>
        /// <param name="tableName">表单数据库表名</param>
        /// <param name="formId">表单id</param>
        /// <param name="MessageName">审批类型名称</param>
        public static void pushMessage(string stepId,string tableName,string formId,string MessageName)
        {
            StringBuilder sb = new StringBuilder();
            string sqlCmd = "select * from OA_Sys_Step_Emp where stepId='" + stepId + "'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            if (dt.Rows.Count > 0) 
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string deptId = dt.Rows[i]["deptId"].ToString();
                    string dutyId = dt.Rows[i]["dutyId"].ToString();
                    if (deptId == "0") //申请人本部门
                    {
                        sqlCmd = "select deptId from OA_Sys_EmployeeInfo where id=(select reqman from " + tableName + " where id='" + formId + "')";
                        deptId = SqlSel.GetSqlScale(sqlCmd).ToString();
                    }
                    DataTable dtNodeUserList = new DataTable();
                    sqlCmd = "select id from OA_Sys_EmployeeInfo where deptid='" + deptId + "' and dutyId='" + dutyId + "' and useStatus='1'";
                    SqlSel.GetSqlSel(ref dtNodeUserList, sqlCmd);
                    for (int j = 0; j < dtNodeUserList.Rows.Count; j++)
                    {
                        sqlCmd = "insert into OA_Sys_step_empList (stepId,userId,formId,formDataName,formName) values ('" + stepId + "','" + dtNodeUserList.Rows[j]["id"].ToString() + "',";
                        sqlCmd += "'" + formId + "','" + tableName + "','" + MessageName + "')";
                        int exeCount = SqlSel.ExeSql(sqlCmd);//插入节点审批人员明细表
                        //插入成功则向相关人员的微信推送消息
                        if (exeCount > 0)
                        {
                            sb.AppendFormat("{0}|", dtNodeUserList.Rows[j]["id"].ToString());
                        }
                    }
                }
                //跳转至待办任务页面
                string url = targetUrl("admin%2fMissionList.aspx");
                WX2HK.ReturnInfo.pushMessage(sb.ToString(), MessageName, url, "有您的审批信息，请及时查阅。", "");
            }
        }


        //插入附件信息
        //public void InsertFiles(string formId,string formDataName,FineUI.Grid grid)
        //{
        //    string sqlCmd = "";
        //    foreach (GridRow gr in grid.Rows)
        //    {
        //        string fileName = gr.DataKeys[0].ToString();
        //        sqlCmd = "insert into OA_Sys_files (FormId,fileName,formDataName) values ('" + formId + "','" + fileName + "',formDataName)";
        //        SqlSel.ExeSql(sqlCmd);
        //    }
        //}
        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        /// <summary>
        /// 根据表单类型和岗位加载可用流程
        /// </summary>
        /// <param name="formName">表单类型名称</param>
        /// <param name="userId">用户id</param>
        /// <returns>流程相关信息</returns>
        public static DataTable validFlow(string formName,string userId)
        {
            //表单关联所有流程
            string sqlCmd = "select B.flowName,B.id from OA_Sys_FlowRelForm A left join OA_sys_flow B on A.flowId=B.id where A.formDataName='" + formName + "' and B.isUsed='1'";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            //获取登录用户岗位
            sqlCmd = "select dutyId from OA_Sys_EmployeeInfo where id='" + userId + "'";
            string userDuty = SqlSel.GetSqlScale(sqlCmd).ToString();
            //遍历流程，没有指定岗位的显示，指定岗位但人员不在列的不显示
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                string compFlowId = dt.Rows[rowIndex]["id"].ToString();
                sqlCmd = "select * from OA_Sys_FlowRelDuty where flowId='" + compFlowId + "'";
                DataTable tempDt = new DataTable();
                SqlSel.GetSqlSel(ref tempDt, sqlCmd);
                if (tempDt.Rows.Count > 0)
                {
                    DataRow[] findRow = tempDt.Select("dutyId='" + userDuty + "' ");
                    if (findRow.Length == 0)
                    {
                        dt.Rows.RemoveAt(rowIndex);
                    }
                }

            }
            return dt;
        }

        /// <summary>
        /// 验证文件格式合法性
        /// </summary>
        /// <param name="fileType">文件类型</param>
        public static bool isValidFileType(string fileType)
        {
            string[] typeName = new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx", "pdf", "txt" };
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

        public enum MediaType
        {
            /// <summary>
            /// 图片（image）: 2M，支持JPG、PNG格式
            /// </summary>
            image,
            /// <summary>
            /// 语音（voice）：2M，播放长度不超过60s，支持AMR格式
            /// </summary>
            voice,
            /// <summary>
            /// 视频（video）：10MB，支持MP4格式
            /// </summary>
            video,
            /// <summary>
            /// 普通文件（file）：20MB
            /// </summary>
            file
        }

        public class UpLoadInfo
        {
            /// <summary>
            /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// 媒体文件上传后，获取时的唯一标识
            /// </summary>
            public string media_id { get; set; }
            /// <summary>
            /// 媒体文件上传时间戳
            /// </summary>
            public string created_at { get; set; }
        }

        /// <summary>
        /// 微信上传多媒体文件
        /// </summary>
        /// <param name="filepath">文件绝对路径</param>
        public static UpLoadInfo WxUpLoad(string filepath, string token, MediaType mt)
        {
            using (WebClient client = new WebClient())
            {
                byte[] b = client.UploadFile(string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", token, mt.ToString()), filepath);//调用接口上传文件
                string retdata = Encoding.Default.GetString(b);//获取返回值
                if (retdata.Contains("media_id"))//判断返回值是否包含media_id，包含则说明上传成功，然后将返回的json字符串转换成json
                {
                    return JsonConvert.DeserializeObject<UpLoadInfo>(retdata);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取审批信息
        /// </summary>
        /// <param name="formId">表单id</param>
        /// <param name="formDataName">表单数据库名</param>
        /// <returns></returns>
        public static DataTable getCheckDtl(string formId, string formDataName)
        {
            string sqlCmd = "select (D.deptName+ '/' +C.chineseName) as chineseName,B.dealAdc,B.optTime,";
            sqlCmd += "isnull((case when dealType='1' then '同意' when dealType='-1' then '否决' end),'未阅') as nodeAdc ";
            sqlCmd += "from OA_Sys_step_empList A left join OA_Sys_NodeDetail B on A.ID=B.NodeId ";
            sqlCmd += "left join OA_Sys_EmployeeInfo C on C.id= A.userid left join OA_Sys_department D on D.id=C.deptId ";
            sqlCmd += "where A.formId='" + formId + "' and formDataName='" + formDataName + "' order by optTime desc";
            DataTable dt = new DataTable();
            SqlSel.GetSqlSel(ref dt, sqlCmd);
            return dt;
        }

        /// <summary>
        /// 返回需验证成员身份带Code参数的URL编码地址
        /// </summary>
        /// <param name="pageName">当前页面服务器路径</param>
        /// <returns></returns>
        public static string targetUrl(string pageName) 
        {
            //微信企业号id
            string sCorpid = System.Configuration.ConfigurationManager.AppSettings["Corpid"];
            //微信企业号可信域名+当前页面路径
            string enCodeUrl = System.Web.HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["CorpWxUrl"]) + pageName;
            //拼接后的OAuth2.0验证接口获取成员的身份信息的页面地址
            string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect", sCorpid, enCodeUrl);
            return url;
        }

        /// <summary>
        /// 判断当前该节点的单据是否是审批状态
        /// </summary>
        /// <param name="nodeId">审批节点id</param>
        /// <returns></returns>
        public bool NodeChecked(string nodeId) 
        {
            string sqlCmd = "select * from OA_Sys_NodeDetail where nodeId='" + nodeId + "'";
            DataTable dt = new DataTable();
            if (SqlSel.GetSqlSel(ref dt, sqlCmd))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        /// <summary>
        /// json字符串转对象集合
        /// </summary>
        public static List<T> strToList<T>(string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }
    }
}