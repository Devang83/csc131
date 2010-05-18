
using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

namespace QuickPMWebsite
{


	public partial class Volunteer : System.Web.UI.Page
	{
		public string message = "";
		public List<VolunteerTracker.Attend> attends = new List<VolunteerTracker.Attend>();
		public VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer();
		protected void Page_Load(object sender, EventArgs e)
   		{
			DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);
			if(Request["Id"] != null) 
			{
				v = new VolunteerTracker.Volunteer(long.Parse(Request["Id"]));				
			}
			if(Request["Id"] != "" && !this.IsPostBack)
			{
				long id = long.Parse(Request["Id"]);
				
				Address.Text = v.Address;
				CellPhone.Text = v.CellPhone;
				Email.Text = v.Email;
				FirstName.Text = v.FirstName;
				HomePhone.Text = v.HomePhone;
				LastName.Text = v.LastName;
				OfficePhone.Text = v.OfficePhone;				
				Employer.Text = v.Employer;				
			}
			if (Request["Id"] != null && Request["Id"].Trim() != "")
			{
				attends = VolunteerTracker.Attend.Find<VolunteerTracker.Attend>("VolunteerId", long.Parse(Request["Id"]));
			}
			
			if (Request.Form["methodName"] == "Delete")
        	{
				string interest = Request.Form["methodArgument"].Trim().ToLower();
            	for(int i = 0; i < v.Interests.Count; i++)
				{
					if(v.Interests[i].Trim().ToLower() == interest)
					{
						v.Interests.RemoveAt(i);
						v.Save();
						return;
					}
				}
        	}
   		}
				
		
		protected void ButtonSave_Click(object sender, EventArgs e)
    	{
			long id = long.Parse(Request["Id"]);
			VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer(id);
			v.Address = Address.Text.Trim();
			v.CellPhone = CellPhone.Text.Trim();
			v.Email = Email.Text.Trim();
			v.FirstName = FirstName.Text.Trim();
			v.HomePhone = HomePhone.Text.Trim();
			v.LastName = LastName.Text.Trim();
			v.OfficePhone = OfficePhone.Text.Trim();
			v.Employer = Employer.Text.Trim();
			v.Save();
			message = "<font color=\"red\">Saved</font>";
		}
		
		protected void ButtonDelete_Click(object sender, EventArgs e)
    	{
			long id = long.Parse(Request["Id"]);
			VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer(id);
			v.Delete();
			message = "<font color=\"red\">Deleted</font>";
		}
		
		protected void ButtonAddInterest_Click(object sender, EventArgs e)
    	{
			long id = long.Parse(Request["Id"]);
			//VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer(id);
			if(Interest.Text.Trim() != String.Empty) 
			{
				v.Interests.Add(Interest.Text.Trim());
				v.Save();
			}			
		}
	}
}
