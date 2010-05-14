
using System;
using System.Web;
using System.Web.UI;

namespace QuickPMWebsite
{


	public partial class Volunteer : System.Web.UI.Page
	{
		public string message = "";
		protected void Page_Load(object sender, EventArgs e)
   		{
			DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);
			if(Request["Id"] != "" && !this.IsPostBack)
			{
				long id = long.Parse(Request["Id"]);
				VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer(id);
				Address.Text = v.Address;
				CellPhone.Text = v.CellPhone;
				Email.Text = v.Email;
				FirstName.Text = v.FirstName;
				HomePhone.Text = v.HomePhone;
				LastName.Text = v.LastName;
				OfficePhone.Text = v.OfficePhone;				
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
			v.Save();
			message = "<font color=\"red\">Saved</font>";
		}
	}
}
