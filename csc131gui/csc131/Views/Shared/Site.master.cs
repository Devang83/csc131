using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QuickPMWebsite.Views.Shared
{


	public partial class Site : System.Web.Mvc.ViewMasterPage
	{
		void Page_Init( object sender, EventArgs e )
		{
    		DatabaseSettings.UpdateDatabaseConnectionString(Context.Profile, Request);

		}
	}
}
