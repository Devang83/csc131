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
using System.Web.Profile;

public partial class ManageUsers_AddUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Roles.IsUserInRole(Context.Profile.UserName, "Manager"))
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
            ButtonAddUser.Enabled = false;
            Session["Error"] = "<font color=\"red\">You do not have permission to add users. Please ask your administrator for help.</font>";
        }
    }

    protected void ButtonAddUser_Click(object sender, EventArgs e)
    {
        string userName = TextBoxEmailAddress.Text.Trim();
        string email = TextBoxEmailAddress.Text.Trim();
        Sloppycode.PronounceablePasswordGenerator generator = new Sloppycode.PronounceablePasswordGenerator();
        ArrayList passwords = generator.Generate(1, 8);
        string defaultPassword = passwords[0].ToString();

        MembershipUserCollection members = Membership.GetAllUsers();
        foreach (MembershipUser u in members)
        {
            if (u.UserName == userName)
            {
                Session["Error"] = "<font color=\"red\">Email already in use</font>";
                return;
            }
        }

        Membership.CreateUser(userName, defaultPassword, userName);

        string role = "User";        
        Roles.AddUserToRole(userName, role);
        ProfileBase.Create(userName);
        //this.profile = ProfileBase.Create(userName);
        VolunteerTracker.User user = new VolunteerTracker.User();
        user.Email = email;
        user.Name = userName;
        user.Save();
        //SaveSettings();
		string url = "quickpm.net";
			
		if (ConfigurationManager.AppSettings["DatabaseName"] != null)
		{
			url = ConfigurationManager.AppSettings["DatabaseName"] + ".quickpm.net";
		}
        string message = "Email: " + email + "\r\nPassword: " + defaultPassword + "" + "\r\n" + "To login go to " + url;
        SendEmail.Send(TextBoxEmailAddress.Text.Trim(), "VolunteerTracker Login Information", message, Request.PhysicalApplicationPath);
        Session["ManageUsers-UserAdded"] = userName;
    }

}
