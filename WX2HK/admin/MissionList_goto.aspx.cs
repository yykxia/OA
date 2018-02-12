using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WX2HK.admin
{
    public partial class MissionList_goto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                string empId = Request["empId"];
                string formName = Request["typeId"];
                string clientType=Request["client"];
                string pageUrl = "";
                //if (checkStatus == "0")
                //{
                //    pageUrl = string.Format("{0}_m.aspx?tabId={1}&checkStatus=0", formName, empId);
                //}
                //else
                //{
                //    pageUrl = string.Format("{0}.aspx?tabId={1}", formName, empId);
                //}
                if (clientType == "m") //微信端
                {
                    pageUrl = string.Format("{0}_m.aspx?tabId={1}", formName, empId);
                }
                else
                {
                    pageUrl = string.Format("{0}.aspx?tabId={1}", formName, empId);
                }
                Response.Redirect(pageUrl);
            }
        }
    }
}