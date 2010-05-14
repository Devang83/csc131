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

public partial class Login : System.Web.UI.UserControl
{
    public static bool AutoLogin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBoxEmail.Focus();
        if (HttpContext.Current.Profile.IsAnonymous && (Request.Url.Host == "localhost" || Request.Url.Host == "127.0.0.1") && AutoLogin)
        {
            string userEmail = "quickpm@quickpm.net";
            string userName = Membership.GetUserNameByEmail(userEmail);
            string password = "quickpm";
            if (Membership.ValidateUser(userName, password))
            {
                FormsAuthentication.RedirectFromLoginPage(userName, true);
            }
        }
    }
    protected void ButtonLogin_Click(object sender, EventArgs e)
    {        
        string userEmail = TextBoxEmail.Text.Trim().ToLower();
        string userName = Membership.GetUserNameByEmail(userEmail);
        
        string password = TextBoxPassword.Text.Trim();
        if (Membership.ValidateUser(userName, password))
        {
            //FormsAuthentication.Authenticate(userName, password);
            FormsAuthentication.RedirectFromLoginPage(userName, CheckBoxRemember.Checked);
        }
        else
        {
            Session["LoginStatus"] = "Email or password incorrect";
        }


    }
    
}
