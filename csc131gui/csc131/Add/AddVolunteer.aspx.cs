
using System;
using System.Web;
using System.Web.UI;

namespace QuickPMWebsite
{


	public partial class AddVolunteer : System.Web.UI.Page
	{
		public string message = "";
		protected void Page_Load(object sender, EventArgs e)
   		{
				
   		}
				
		
		protected void ButtonAdd_Click(object sender, EventArgs e)
    	{
			VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer();
			v.Address = Address.Text.Trim();
			v.CellPhone = CellPhone.Text.Trim();
			v.Email = Email.Text.Trim();
			v.FirstName = FirstName.Text.Trim();
			v.HomePhone = HomePhone.Text.Trim();
			v.LastName = LastName.Text.Trim();
			v.OfficePhone = OfficePhone.Text.Trim();
			v.Ethnicity = Ethnicity.Text.Trim();
			v.Employer = Employer.Text.Trim();
			v.JobTitle = JobTitle.Text.Trim();
			v.Male = (Male.Text.Trim().ToLower() == "m") ? 1 : 0;
			v.Interests = new System.Collections.Generic.List<string>();
			v.Save();
			message = "<font color=\"red\">Volunteer Added <br/> Go to <a href=\"../Volunteer/Volunteer.aspx?Id=" + v.Id + "\">volunteer</a></font>";
		}
	}
}
