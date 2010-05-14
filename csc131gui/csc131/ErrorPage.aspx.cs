using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickPMWebsite
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			Exception ex = Server.GetLastError();
			
			string url = Request.Url.ToString();
			string url2 = Request.Url.OriginalString;
			string physicalPath = Request.PhysicalPath;
			
			string exceptionInfo = "\n\n\nWe're sorry the exception information is not available";
			if (ex != null && ex.Message != null && ex.InnerException != null && ex.InnerException.Message != null && ex.InnerException.StackTrace != null
			    && ex.GetBaseException() != null && ex.GetBaseException().Message != null && ex.GetBaseException().StackTrace != null)
			{
				exceptionInfo = "\n\n\nMessage:" + ex.Message + "\n\n\nStackTrace:"+ ex.StackTrace +
			               "\n\n\nInnerException:" + ex.InnerException.Message + "\n\n\nInnerException Stacktrace:" + ex.InnerException.StackTrace +
			               "\n\n\nBaseExcetion:" + ex.GetBaseException().Message + "\n\n\nBaseException Stacktrace;" + ex.GetBaseException().StackTrace;
		
			}
			string msg = "Remote IP Address:" + Request.UserHostAddress + "\n\n" +
			               "Url:" + url + "\n\n" +
			               "Url.OriginalString: " + url2 + "\n\n" +
			               "PhysicalPath: " + physicalPath +
			               	exceptionInfo;									
			SendEmail.Send("bryan.w.bell@gmail.com", "Unhandled Exception, " + url, msg, this.Request.PhysicalApplicationPath);	
        }
    }
}
