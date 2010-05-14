
using System;
using System.Web;
using System.Web.UI;

namespace QuickPMWebsite
{


	public partial class Event : System.Web.UI.Page
	{
		public string message = "";
		protected void Page_Load(object sender, EventArgs ex)
   		{
			DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);
			if(Request["Id"] != "" && !this.IsPostBack)
			{
				long id = long.Parse(Request["Id"]);
				VolunteerTracker.Event e = new VolunteerTracker.Event(id);
				Name.Text = e.Name;
				Date.Text = e.Date.ToShortDateString();				
			}
   		}
				
		
		protected void ButtonSave_Click(object sender, EventArgs ex)
    	{
			long id = long.Parse(Request["Id"]);
			VolunteerTracker.Event e = new VolunteerTracker.Event(id);
			e.Name = Name.Text.Trim();
			e.Date = DateTime.Parse(Date.Text);
			e.Save();
			message = "<font color=\"red\">Saved</font>";
		}
	}
}
