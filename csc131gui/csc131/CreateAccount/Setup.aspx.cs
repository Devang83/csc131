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
using QuickPM;

public partial class CreateAccount_Setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LinkButtonCreateAccount_Click(object sender, EventArgs e)
    {
        SendEmail.Send("bryan.w.bell@gmail.com", "QuickPM.net Account Information", "Default message", Request.PhysicalApplicationPath);
        /*
        string accountName = TextBoxAccountName.Text.Trim();
        string email = TextBoxEmail.Text.Trim();
        string password = TextBoxPassword.Text.Trim();
        string repeatPassword = TextBoxRepeatPassword.Text.Trim();
        if (password != repeatPassword)
        {
            Session["CreateAccountError"] = "<font color=\"red\">Passwords don't match</font>";
            return;
        }        
        string databaseName = accountName + ".db";
        string userName = email;

        MembershipUserCollection members = Membership.GetAllUsers();
        foreach (MembershipUser u in members)
        {
            if (u.UserName == userName)
            {
                Session["CreateAccountError"] = "<font color=\"red\">That email is already in use.</font>";
                return;
            }
        }        
        ManageDatabase manageDatabase = new ManageDatabase("|DataDirectory|", databaseName);
        try
        {
            manageDatabase.CreateDatabase();            
            manageDatabase.AddDefaultPropertiesAndTenants();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("already exists"))
            {
                Session["CreateAccountError"] = "<font color=\"red\">Account Name already exists</font>";
                return;
            }
            else
            {
                Session["CreateAccountError"] = "<font color=\"red\">Error creating database</font>";
                return;
            }
        }
        ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings["UsersConnectionString"];
        string connectionString = conn.ConnectionString;
        //DatabaseAccess databaseAccess = new DatabaseAccess(userConnectionString);
        
        MembershipUser user = Membership.CreateUser(userName, password, email);
        //Roles.AddUserToRole(accountName, "Administrator");
        Roles.AddUserToRole(userName, "Manager");
        ProfileCommon profile = Profile.GetProfile(user.UserName);
        profile.DatabaseName = databaseName;
        profile.WriteList = new string[] { "all" };
        profile.ReadList = new string[] { "all" };
        profile.Name = "";//firstName + " " + lastName;
        profile.Save();
        Session["CreateAccountSuccessful"] = true;        
        Session["CreateAccountAccountName"] = accountName;
        Session["CreateAccountEmail"] = email;
        Session["CreateAccountPassword"] = password;
        string message = "Your Account Name is " + accountName;
        message += "\r\n" + "Email:" + email;
        message += "\r\n" + "Password:" + password;                
        SendEmail.Send(email, "QuickPM.net Account Information", message, Request.PhysicalApplicationPath);*/
    }
}
