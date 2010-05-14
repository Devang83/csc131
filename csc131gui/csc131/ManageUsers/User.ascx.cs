using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Profile;

public partial class ManageUsers_User : System.Web.UI.UserControl
{    
    protected ProfileBase profile = null;
    protected string msg = null;
	
	
	protected bool GetCanEdit()
    {
        return Roles.IsUserInRole(Context.Profile.UserName, "Manager");        
    }

    protected ProfileBase GetProfile()    
    {
        string userName = Request.Params["userName"];
        if (userName == null)
        {
            return Context.Profile;
        }                
        if (userName.Trim() == Context.Profile.UserName)
        {
          return Context.Profile;
        }
        
        return ProfileBase.Create(userName);
    }

    private void InitControls()
    {
        string userName = Request.Params["Username"];
        if (userName == null)
        {
            return;
        }
        MembershipUser user = Membership.GetUser(userName);
        if (user == null)
        {
            return;
        }
        VolunteerTracker.User pmUser = new VolunteerTracker.User(user.Email);
        pmUser.Save();		
        if (TextBoxEmailAddress.Text == "")
        {

            TextBoxName.Text = pmUser.Name;
            TextBoxEmailAddress.Text = user.Email;
        }
        string[] roles = Roles.GetRolesForUser(userName);
        if (!RadioButtonUser.Checked && !RadioButtonManager.Checked)
        {
            foreach (string role in roles)
            {
                if (role == "User")
                {
                    RadioButtonUser.Checked = true;
                    RadioButtonManager.Checked = false;                    
                }
                else if (role == "Manager")
                {
                    RadioButtonUser.Checked = false;
                    RadioButtonManager.Checked = true;                    
                }
            }
        }
        long currentUserId = VolunteerTracker.Database.GetUserId();
        VolunteerTracker.Database.SetUserId(pmUser.Id);
        
        

        
        VolunteerTracker.Database.SetUserId(currentUserId);
        VolunteerTracker.Database.SetUserId((new VolunteerTracker.User(user.Email)).Id);
        if (!RadioButtonUser.Checked && !RadioButtonManager.Checked)
        {
            RadioButtonUser.Checked = true;
        }
        VolunteerTracker.Database.SetUserId(currentUserId);
        bool canEdit = GetCanEdit();
        RadioButtonManager.Enabled = canEdit;
        RadioButtonUser.Enabled = canEdit;
        if (!canEdit)
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
        }
        LinkButtonSaveEmailName.Enabled = canEdit;
        LinkButtonSaveEmailName.Visible = canEdit;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);        
        string userName = Request.Params["Username"];        
        if (userName == null)
        {
            return;
        }
        MembershipUser user = Membership.GetUser(userName);
        if (user == null)
        {
            return;
        }
        if (!IsPostBack)
        {
            InitControls();
        }
        if (!Roles.IsUserInRole(HttpContext.Current.Profile.UserName, "Manager"))
        {            
            this.RadioButtonManager.Enabled = false;
            this.RadioButtonUser.Enabled = false;            
        }

        long currentUserId = VolunteerTracker.Database.GetUserId();
        if (GetCanEdit())
        {
            VolunteerTracker.Database.SetUserId(VolunteerTracker.AccessControlList.ROOT_USERID);
        }
        if (Request.Form["methodName"] == "SetAccessValue")
        {            
        }
		
		if (Request.Form["methodName"] == "SetAll")
		{
			SetAll(Request.Form["methodArgument"]);
		}
		
        VolunteerTracker.Database.SetUserId(currentUserId);
    }
	
	


    protected void RadioButtonManager_CheckedChanged(object sender, EventArgs e)
    {
        RadioButtonUser.Checked = !RadioButtonManager.Checked;
        SaveSettings();
    }

    protected void RadioButtonUser_CheckedChanged(object sender, EventArgs e)
    {
        RadioButtonManager.Checked = !RadioButtonUser.Checked;
        SaveSettings();
    }
	
	private void SetAll(string val)
	{		
		int accessValue = 0;
		if (int.TryParse(val, out accessValue))
		{
			SetAll(accessValue);
		}
		else
		{
			msg = "Error saving settings";
		}
		
	}
	
	private void SetAll(int val)
	{
		if (val < 0)
		{
			msg = "Error saving settings.";
			return;
		}
		long currentUserId = VolunteerTracker.Database.GetUserId();
		VolunteerTracker.Database.SetUserId(VolunteerTracker.AccessControlList.ROOT_USERID);
		VolunteerTracker.Database.SetUserId(currentUserId);
	}

    
	
	
	

    protected long GetUserId()
    {
        MembershipUser user = null;
        ProfileBase profile = GetProfile();
        if (profile == null)
        {
            return -1;
        }
		
		long userId = VolunteerTracker.Database.GetUserId();
		VolunteerTracker.Database.SetUserId(VolunteerTracker.AccessControlList.ROOT_USERID);
        user = Membership.GetUser(profile.UserName);        
		VolunteerTracker.User user2 = new VolunteerTracker.User(user.Email);
		user2.Save();
		VolunteerTracker.Database.SetUserId(userId);
        return user2.Id;
    }



    protected void SaveSettings()
    {
        ProfileBase profile = GetProfile();
        if (profile == null)
        {
            return;
        }
              
        string[] roles = Roles.GetRolesForUser(profile.UserName);
        foreach (string role in roles)
        {
            Roles.RemoveUserFromRole(profile.UserName, role);
        }
        if (RadioButtonUser.Checked)
        {
            Roles.AddUserToRole(profile.UserName, "User");
        }
        if (RadioButtonManager.Checked)
        {
            Roles.AddUserToRole(profile.UserName, "Manager");
        }        
        
    }
    
    protected void LinkButtonSaveEmailName_Click(object sender, EventArgs e)
    {
        ProfileBase profile = GetProfile();
        if (profile == null)
        {
            return;
        }         
        MembershipUser user = Membership.GetUser(profile.UserName);
        VolunteerTracker.User pmUser = new VolunteerTracker.User(user.Email);
        user.Email = TextBoxEmailAddress.Text.Trim().ToLower();
        pmUser.Email = user.Email;
        pmUser.Name = TextBoxName.Text.Trim();
        pmUser.Save();
        user.IsApproved = true;
        Membership.UpdateUser(user);

        TextBoxEmailAddress.Text = user.Email;
        msg = "Saved email & name";
    }
}
