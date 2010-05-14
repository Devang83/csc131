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
using System.Web.Profile;
//using System.Xml.Linq;

public partial class ManageUsers_EditPassword : System.Web.UI.Page
{
	protected string msg = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	
	protected ProfileBase GetProfile()
	{
		string userName = Request["userName"];
		if (userName == Context.Profile.UserName)
		{
			return Context.Profile;
		}
		return ProfileBase.Create(Request["userName"]);
	}
	
    protected void LinkButtonSave_Click(object sender, EventArgs e)
    {
        string currentPassword = CurrentPassword.Text.Trim();
        string newPassword = NewPassword.Text.Trim();
        string repeatNewPassword = RepeatNewPassword.Text.Trim();
        if (newPassword != repeatNewPassword)
        {
            msg = "New Passwords do not match.";
            return;
        }
        if (!Membership.ValidateUser(GetProfile().UserName, currentPassword))
        {
            msg = "The password entered does not match your current password.";
            return;
        }
        MembershipUser user = Membership.GetUser(GetProfile().UserName);
        if (!user.ChangePassword(currentPassword, newPassword))
        {
            msg = "Error changing password";
        }
        msg = "Your new password has been saved";
    }

}
