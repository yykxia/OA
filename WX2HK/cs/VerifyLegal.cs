using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections;
using System.Security.Cryptography;
using FineUI;
using IETCsoft.sql;

namespace WX2HK
{
    public class VerifyLegal
    {

        enum WXBizMsgCryptErrorCode
        {
            WXBizMsgCrypt_OK = 0,
            WXBizMsgCrypt_ValidateSignature_Error = -40001,
            WXBizMsgCrypt_ParseXml_Error = -40002,
            WXBizMsgCrypt_ComputeSignature_Error = -40003,
            WXBizMsgCrypt_IllegalAesKey = -40004,
            WXBizMsgCrypt_ValidateCorpid_Error = -40005,
            WXBizMsgCrypt_EncryptAES_Error = -40006,
            WXBizMsgCrypt_DecryptAES_Error = -40007,
            WXBizMsgCrypt_IllegalBuffer = -40008,
            WXBizMsgCrypt_EncodeBase64_Error = -40009,
            WXBizMsgCrypt_DecodeBase64_Error = -40010
        };

        //public static string GetAccess_Token(string Corpid, string Corpsecret) 
        //{
        //    string sUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", Corpid, Corpsecret);
        //    var client = new System.Net.WebClient();
        //    client.Encoding = System.Text.Encoding.UTF8;
        //    var data = client.DownloadString(sUrl);
        //    //HttpContext.Current.Response.Write(data);
        //    var serializer = new JavaScriptSerializer();
        //    var obj = serializer.Deserialize<Dictionary<string, string>>(data);
        //    string accessToken;
        //    obj.TryGetValue("access_token", out accessToken);
        //    return accessToken;

        //}

        public static string GetAccess_Token()
        {
            string Corpid = System.Configuration.ConfigurationManager.AppSettings["Corpid"];
            string Corpsecret = System.Configuration.ConfigurationManager.AppSettings["Corpsecret"];
            string sUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", Corpid, Corpsecret);
            var client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var data = client.DownloadString(sUrl);
            //HttpContext.Current.Response.Write(data);
            var serializer = new JavaScriptSerializer();
            var obj = serializer.Deserialize<Dictionary<string, string>>(data);
            string accessToken;
            obj.TryGetValue("access_token", out accessToken);
            return accessToken;

        }

        
        public class Rootobject
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public Department[] department { get; set; }
        }
        //部门实体类
        public class Department
        {
            public int id { get; set; }
            public string name { get; set; }
            public int parentid { get; set; }
            public int order { get; set; }
        }

        //同步部门信息
        public static void sycnDeptInfo(string parentId) 
        {
            DataTable dt = new DataTable();
            string token = GetAccess_Token();
            string wxUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}&id={1}", token, parentId);
            var client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var data = client.DownloadString(wxUrl);
            //DataTable userList = JsonToDataTable(data);
            //解析json
            JavaScriptSerializer js = new JavaScriptSerializer();
            Rootobject returnObj = js.Deserialize<Rootobject>(data);
            int errcode = returnObj.errcode;
            if (errcode == 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                js.Serialize(returnObj.department, sb);
                List<Department> deptLlist = new List<Department>();
                deptLlist =WX2HK.selectUser.JSONStringToList<Department>(sb.ToString());
                dt = WX2HK.selectUser.ToDataTable(deptLlist);
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    //若部门已存在则更新


                    //若部门不存在则直接创建
                }
            }
            else
            {
                Alert.Show(string.Format("sycnDeptInfo:{0}", returnObj.errmsg));
            }
        }

        //更新部门信息
        public static void updateDepartment(string deptId, string deptName, string parentId)
        {
            try
            {
                //json格式
                String jsonContext = "{" +
                    "\"id\":\"" + deptId + "\"," +
                    "\"name\":\"" + deptName + "\"," +
                    "\"parentid\":\"" + parentId + "\"" +
                "}";
                //获取accessToken
                string access_token = GetAccess_Token();
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}", access_token);
                WX2HK.ReturnInfo.PostWebRequest(url, jsonContext, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(string.Format("updateDepartment:{0}", ex.Message));
            }
        }
        //创建新部门
        public static void createDepartment(string deptId, string deptName, string parentId)
        {
            try
            {
                //json格式
                String jsonContext = "{" +
                    "\"id\":\"" + deptId + "\"," +
                    "\"name\":\"" + deptName + "\"," +
                    "\"parentid\":\"" + parentId + "\"" +
                "}";
                //获取accessToken
                string access_token = GetAccess_Token();
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", access_token);
                WX2HK.ReturnInfo.PostWebRequest(url, jsonContext, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(string.Format("createDepartment:{0}", ex.Message));
            }
        }
        //删除部门
        //public static void deleteDeptment(string deptId) 
        //{
        //    string access_token = GetAccess_Token();
        //    string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", access_token);
        //    WX2HK.ReturnInfo.PostWebRequest(url, jsonContext, Encoding.UTF8);
        //}

        //创建成员信息
        public static void createUser(string userId, string userName, string deptId, string mobileNumber) 
        {
            try 
            {
                //json格式
                String jsonContext = "{" +
                    "\"userid\":\"" + userId + "\"," +
                    "\"name\":\"" + userName + "\"," +
                    "\"department\":[" + deptId + "]," +
                    "\"mobile\":\"" + mobileNumber + "\"" +
                "}";
                //获取accessToken
                string access_token = GetAccess_Token();
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}", access_token);
                WX2HK.ReturnInfo.PostWebRequest(url, jsonContext, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(string.Format("createUser:{0}", ex.Message));
            }
        }

        //更新成员信息
        public static void updateUser(string userId, string userName, string deptId, string mobileNumber)
        {
            try
            {
                //json格式
                String jsonContext = "{" +
                    "\"userid\":\"" + userId + "\"," +
                    "\"name\":\"" + userName + "\"," +
                    "\"department\":[" + deptId + "]," +
                    "\"mobile\":\"" + mobileNumber + "\"" +
                "}";
                //获取accessToken
                string access_token = GetAccess_Token();
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}", access_token);
                WX2HK.ReturnInfo.PostWebRequest(url, jsonContext, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(string.Format("updateUser:{0}", ex.Message));
            }
        }
        //删除成员
        public static void deleteUser(List<string> userList) 
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (string it in userList) 
                {
                    sb.AppendFormat("\"{0}\",", it);
                }
                string userListStr = sb.ToString().Substring(0, sb.ToString().Length - 1);//去除最后的,号
                //json格式
                String jsonContext = "{" +
                    "\"useridlist\":[" + userListStr + "]" +
                "}";
                //获取accessToken
                string access_token = GetAccess_Token();
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token={0}", access_token);
                WX2HK.ReturnInfo.PostWebRequest(url, jsonContext, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(string.Format("deleteUser:{0}", ex.Message));
            }
        }

        //生成签名
        public static string createSign(string randString, string timestamp, string url) 
        {
            string corpUrl = System.Configuration.ConfigurationManager.AppSettings["CorpWxUrl"] + url;
            //获取access_token
            string token = GetAccess_Token();
            //根据token获取jsapi
            string apiUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", token);
            var client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var data = client.DownloadString(apiUrl);
            //HttpContext.Current.Response.Write(data);
            var serializer = new JavaScriptSerializer();
            var obj = serializer.Deserialize<Dictionary<string, string>>(data);
            string jsApiTicket;
            obj.TryGetValue("ticket", out jsApiTicket);

            String string1 = "";
            String signature = "";
            string1 = "jsapi_ticket=" + jsApiTicket +
                          "&noncestr=" + randString +
                          "&timestamp=" + timestamp +
                          "&url=" + corpUrl;
            signature = GetSha1(string1);
            return signature;
        }

        public static string GetSha1(string str)
        {

            //建立SHA1对象

            SHA1 sha = new SHA1CryptoServiceProvider();

            //将mystr转换成byte[]

            ASCIIEncoding enc = new ASCIIEncoding();

            byte[] dataToHash = enc.GetBytes(str);

            //Hash运算

            byte[] dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string

            string hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;

        }

        public static string SHA1Sign(string data)
        {
            byte[] temp1 = Encoding.UTF8.GetBytes(data);

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] temp2 = sha.ComputeHash(temp1);
            sha.Clear();

            // 注意， 不能用这个
            //string output = Convert.ToBase64String(temp2);

            string output = BitConverter.ToString(temp2);
            output = output.Replace("-", "");
            output = output.ToLower();
            return output;
        }
    }
}