
using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuickPMWebsite
{
	
	
	public class Global : System.Web.HttpApplication
	{
		
		 public static void RegisterRoutes(RouteCollection routes)
        {
			//Ignore the webforms directories            
			/*routes.IgnoreRoute("Add/{*pathInfo}");
			routes.IgnoreRoute("Billings/{*pathInfo}");
			routes.IgnoreRoute("CalendarControl/{*pathInfo}");
			routes.IgnoreRoute("Checks/{*pathInfo}");
			routes.IgnoreRoute("CreateAccount/{*pathInfo}");
			routes.IgnoreRoute("Css/{*pathInfo}");
			routes.IgnoreRoute("Documents/{*pathInfo}");
			routes.IgnoreRoute("images/{*pathInfo}");
			routes.IgnoreRoute("Insurance/{*pathInfo}");
			routes.IgnoreRoute("Javascript/{*pathInfo}");
			routes.IgnoreRoute("ManageUsers/{*pathInfo}");
			routes.IgnoreRoute("Maps/{*pathInfo}");
			routes.IgnoreRoute("Notices/{*pathInfo}");
			routes.IgnoreRoute("People/{*pathInfo}");
			routes.IgnoreRoute("Properties/{*pathInfo}");
			routes.IgnoreRoute("Property/{*pathInfo}");
			routes.IgnoreRoute("Public/{*pathInfo}");
			routes.IgnoreRoute("Reports/{*pathInfo}");
			routes.IgnoreRoute("Tenant/{*pathInfo}");
			routes.IgnoreRoute("WebServices/{*pathInfo}");
			routes.IgnoreRoute("WorkOrders/{*pathInfo}");*/		
			//ignore webforms files
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");            
			routes.MapRoute("Default", "{controller}/{action}/{id}");
			/*routes.MapRoute(
               "Default",
				// Route name
               "{controller}/{action}/{id}",
				// URL with parameters
               new { controller = "Home", action = "Index", id = "" }
				// Parameter defaults
                );*/
        }
		
		protected virtual void Application_Start(object sender, EventArgs e)
		{
			
			RegisterRoutes(RouteTable.Routes);
		}
		
		protected virtual void Session_Start(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_BeginRequest(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_EndRequest(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_Error(object sender, EventArgs e)
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
		
			} else if (ex != null && ex.Message != null && ex.InnerException != null && ex.InnerException.Message != null && ex.InnerException.StackTrace != null)
			{			
					exceptionInfo = "\n\n\nMessage:" + ex.Message + "\n\n\nStackTrace:"+ ex.StackTrace +
			               "\n\n\nInnerException:" + ex.InnerException.Message + "\n\n\nInnerException Stacktrace:" + ex.InnerException.StackTrace;
			} else if (ex != null && ex.Message != null && ex.StackTrace != null) 
			{
				exceptionInfo = "\n\n\nMessage:" + ex.Message + "\n\n\nStackTrace:"+ ex.StackTrace;			    	
			}
			
			string msg = "Remote IP Address:" + Request.UserHostAddress + "\n\n" +
			               "Url:" + url + "\n\n" +
			               "Url.OriginalString: " + url2 + "\n\n" +
			               "PhysicalPath: " + physicalPath +
			               	exceptionInfo;									
			SendEmail.Send("bryan.w.bell@gmail.com", "Unhandled Exception for QuickPM, " + url, msg, this.Request.PhysicalApplicationPath);		
			string errorInfo = @"<b>We're sorry an error has been encountered</b> 
								<br>bryan.w.bell@gmail.com has been sent an email with the error information.";

  			Context.Response.Write (errorInfo);

  			// --------------------------------------------------
  			// To let the page finish running we clear the error
  			// --------------------------------------------------			
  			Context.Server.ClearError ();

		}
		
		protected virtual void Session_End(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_End(object sender, EventArgs e)
		{
		}
	}
}
