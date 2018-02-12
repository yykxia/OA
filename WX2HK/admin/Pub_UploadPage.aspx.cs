using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;

namespace WX2HK.admin
{
    public partial class Pub_UploadPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if (fileUp.HasFile)
            {
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
                    fileUp.SaveAs(Server.MapPath("~/upload/" + efileName));
                    PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(sfileName+";"+efileName) + ActiveWindow.GetHideReference());
                }
            }
            else 
            {
                Alert.ShowInTop("请选择要上传的文件！");
                return;
            }
        }
    }
}