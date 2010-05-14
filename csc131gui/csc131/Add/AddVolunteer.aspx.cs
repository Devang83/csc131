
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
			v.Save();
			message = "<font color=\"red\">Volunteer Added</font>";
		}
	}
}
