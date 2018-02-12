using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WX2HK.admin
{
    public partial class Pub_DownloadFile_wx : BasePage
    {
        private static string fileName = "";
        private static string curUserId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                fileName = Request["fileName"];
                curUserId = Request["userId"];
            }
        }

        protected void btn_downLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    MediaType mt = MediaType.file;//file型多媒体文件
                    string filePath = Server.MapPath("/image") + "\\" + fileName;//上传文件路径
                    string token = VerifyLegal.GetAccess_Token();
                    UpLoadInfo uploadFile = WxUpLoad(filePath, token, mt);//上传临时多媒体文件,返回mediaId
                    if (uploadFile != null)
                    {
                        string mediaId = uploadFile.media_id;
                        ReturnInfo.pushMessage_File(curUserId, System.Configuration.ConfigurationManager.AppSettings["CorpAppId"], mediaId);//向指定用户发送file型消息
                        btn_downLoad.Hidden = true;
                        label_result.Hidden = false;
                        label_result.Text = "文件下载完成！";
                    }
                    else
                    {
                        btn_downLoad.Hidden = true;
                        label_result.Hidden = false;
                        label_result.Text = "文件下载失败！";
                        return;
                    }
                }
                else 
                {
                    Alert.ShowInTop("文件丢失，请重试！");
                    return;
                }
            }
            catch (Exception ex) 
            {
                Alert.ShowInTop(ex.Message);
            }
        }


    }
}