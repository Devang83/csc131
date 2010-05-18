
using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

namespace QuickPMWebsite
{


	public partial class FindVolunteers : System.Web.UI.Page
	{
		public List<VolunteerTracker.Volunteer> results = new List<VolunteerTracker.Volunteer>();
		public string resultsMessage = "";
		protected void Page_Load(object sender, EventArgs ex)
   		{
			DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);						
   		}
				
		
		protected void Search_Click(object sender, EventArgs ex)
    	{
			string name = Name.Text.Trim();
			string employer = Employer.Text.Trim();
			string interest = Interest.Text.Trim();
			results = new List<VolunteerTracker.Volunteer>();
			List<VolunteerTracker.Volunteer> v1 = new List<VolunteerTracker.Volunteer>();
			List<VolunteerTracker.Volunteer> v2 = new List<VolunteerTracker.Volunteer>();
			List<VolunteerTracker.Volunteer> v3 = new List<VolunteerTracker.Volunteer>();
			List<VolunteerTracker.Volunteer> v4 = new List<VolunteerTracker.Volunteer>();
			if (name != String.Empty)
			{
				v1 = VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>("FirstName", name);
				v2 = VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>("LastName", name);
			}
			
			if (employer != String.Empty)
			{
				v3 = VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>("Employer", employer);
			}
			
			if (interest != String.Empty)
			{
				v4 = VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>();
				v4 = v4.FindAll(v => v.Interests.FindIndex(inter => inter.Trim().ToLower() == interest.Trim().ToLower()) != -1);
			}
			if (interest == String.Empty && name == String.Empty && employer == String.Empty)
			{
				results = VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>();
				return;
			}
			results.AddRange(v1);
			results.AddRange(v2);
			results.AddRange(v3);
			results.AddRange(v4);
			if (results.Count == 0)
			{
				resultsMessage = "NO RESULTS";
			}
		}
		
	}
}

