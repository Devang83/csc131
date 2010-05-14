using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class ManageUsers_ManageUsersMenuPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        HyperLink1.Enabled = Roles.IsUserInRole(Context.Profile.UserName, "Manager");
        HyperLink1.Visible = Roles.IsUserInRole(Context.Profile.UserName, "Manager");
    }
}
