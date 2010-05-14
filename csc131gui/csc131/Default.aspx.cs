using System;
using System.Collections;
using System.Configuration;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
// using System.Xml.Linq;

public partial class Home : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Profile.IsAnonymous)
        {
            Response.Redirect("~/login.aspx");
        }

    }
    protected void LinkButtonListPropertyTenants_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListPropertyTenants.aspx");
    }
    protected void LinkButtonImportChecks_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImportChecks.aspx");
    }
    protected void LinkButtonDelinquentTenants_Click(object sender, EventArgs e)
    {
        Response.Redirect("DelinquentTenants.aspx");
    }
}
