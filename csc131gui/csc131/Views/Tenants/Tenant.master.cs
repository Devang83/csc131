using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickPMWebsite.Views.Tenants
{


	public partial class Tenant : System.Web.Mvc.ViewMasterPage
	{
		
		public string GetCurrentPageName()
		{
			
			string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;			
			string[] segs = sPath.Split(new char[] {'/'});
			string pageName = segs[segs.Length - 2];
			return pageName;	
			
		}
		
		public QuickPM.Tenant GetTenant()
		{		
			if (Request["TenantId"] == null && ViewData["Id"] == null)
			{
				return null;
			}
			if (Request["TenantId"] != null)
			{
				return new QuickPM.Tenant(Request["TenantId"]);
			}
			
			if (ViewData["Id"] != null)
			{
				return new QuickPM.Tenant((long)ViewData["Id"]);
			}						
			return null;
		}
	}
	
	
}
