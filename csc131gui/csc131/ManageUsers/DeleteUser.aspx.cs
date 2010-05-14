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

public partial class ManageUsers_DeleteUser : System.Web.UI.Page
{
    protected MembershipUser theUser = null;
    protected string error = "";
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request["username"] != null)
        {
            theUser = Membership.GetUser(Request["username"]);
            if (theUser == null)
            {
                Response.Redirect("~/");
            }
            if (!Roles.IsUserInRole(Context.Profile.UserName, "Manager"))
            {
                LinkButtonNo.Enabled = false;
                LinkButtonYes.Enabled = false;
                LinkButtonNo.Visible = false;
                LinkButtonYes.Visible = false;
                error = "<font color=\"red\">You do not have permission to delete users. Please ask your administrator for help.</font><br/>";
            }
        }
        else
        {
            Response.Redirect("~/");
        }
    }

    protected void LinkButtonYes_Click(object sender, EventArgs e)
    {
        Membership.DeleteUser(theUser.UserName, true);
        string[] roles = Roles.GetRolesForUser(theUser.UserName);
        Roles.RemoveUserFromRoles(theUser.UserName, roles);
        Response.Redirect("~/ManageUsers/ManageUsersMenuPage.aspx");
    }

    protected void LinkButtonNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ManageUsers/ManageUsersMenuPage.aspx");
    }
}
