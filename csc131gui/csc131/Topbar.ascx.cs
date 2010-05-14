using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
// using System.Xml.Linq;

public partial class TopbarControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserLoginStatus.LogoutPageUrl = ResolveUrl("~/");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
    }
    protected void UserLoginStatus_LoggingOut(object sender, System.EventArgs e)
    {
        Response.Redirect("~/");
    }
}
