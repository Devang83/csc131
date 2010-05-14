using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickPMWebsite.Views.Property
{


	public partial class PropertyMaster : System.Web.Mvc.ViewMasterPage
	{
		public string GetCurrentPageName()
		{
			
			string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;			
			string[] segs = sPath.Split(new char[] {'/'});
			string pageName = segs[segs.Length - 2];
			return pageName;	
			
		}
		
	}
	
	
}