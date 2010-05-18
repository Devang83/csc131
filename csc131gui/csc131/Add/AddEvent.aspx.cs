
using System;
using System.Web;
using System.Web.UI;

namespace QuickPMWebsite
{


	public partial class AddEvent : System.Web.UI.Page
	{
		public string message = "";
		protected void Page_Load(object sender, EventArgs e)
   		{
			DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);			
   		}
				
		
		protected void ButtonSave_Click(object sender, EventArgs ex)
    	{
			
			VolunteerTracker.Event e = new VolunteerTracker.Event();
			e.Name = Name.Text.Trim();
			e.Date = DateTime.Parse(Date.Text);
			e.Email = "";
			e.Notes = "";
			e.Save();
			//message = "<font color=\"red\">Saved</font>";
			Response.Redirect("../Event/Event.aspx?Id=" + e.Id);
		}
	}
}
