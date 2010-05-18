
using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

namespace QuickPMWebsite
{


	public partial class Event : System.Web.UI.Page
	{
		public string message = "";
		public List<VolunteerTracker.Attend> attends = new List<VolunteerTracker.Attend>();
		protected void Page_Load(object sender, EventArgs ex)
   		{
			DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);			
			if(Request["Id"] != "" && !this.IsPostBack)
			{
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("12am", "0", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("1am", "1", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("2am", "2", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("3am", "3", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("4am", "4", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("5am", "5", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("6am", "6", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("7am", "7", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("8am", "8", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("9am", "9", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("10am", "10", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("11am", "11", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("12pm", "12", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("1pm", "13", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("2pm", "14", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("3pm", "15", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("4pm", "16", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("5pm", "17", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("6pm", "18", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("7pm", "19", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("8pm", "20", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("9pm", "21", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("10pm", "22", true));
				Time.Items.Add(new System.Web.UI.WebControls.ListItem("11pm", "23", true));								
				long id = long.Parse(Request["Id"]);
				VolunteerTracker.Event e = new VolunteerTracker.Event(id);
				Name.Text = e.Name;
				
				Date.Text = e.Date.ToShortDateString();				
				int hours = e.Date.Hour;				
				Time.Items[hours].Selected = true;
				List<VolunteerTracker.Volunteer> volunteers = VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>();
				foreach(VolunteerTracker.Volunteer v in volunteers)
				{
					Volunteers.Items.Add(new System.Web.UI.WebControls.ListItem(v.FirstName + " " + v.LastName + " (#" + v.Id + ")", v.Id.ToString(), true));
				}
			}
			if (Request["Id"] != "" && Request["Id"] != null) 
			{
				attends = VolunteerTracker.Attend.Find<VolunteerTracker.Attend>("EventId", long.Parse(Request["Id"]));
			}
			
			if (Request.Form["methodName"] == "Delete")
        	{
				long id = long.Parse(Request.Form["methodArgument"]);
            	for(int i = 0; i < attends.Count; i++)
				{
					if(attends[i].VolunteerId == id)
					{
						attends[i].Delete();
						attends.RemoveAt(i);
					}
				}
        	}
   		}
				
		
		protected void ButtonSave_Click(object sender, EventArgs ex)
    	{
			long id = long.Parse(Request["Id"]);
			VolunteerTracker.Event e = new VolunteerTracker.Event(id);
			e.Name = Name.Text.Trim();
			e.Date = DateTime.Parse(Date.Text);
			e.Date = new DateTime(e.Date.Year, e.Date.Month, e.Date.Day, int.Parse(Time.SelectedValue), 0, 0);			
			e.Save();
			message = "<font color=\"red\">Saved</font>";
		}
		
		protected void ButtonAdd_Click(object sender, EventArgs ex)
    	{
			long id = long.Parse(Request["Id"]);
			VolunteerTracker.Event e = new VolunteerTracker.Event(id);
			int selectedId = int.Parse(Volunteers.SelectedValue);
			int hours = int.Parse(Hours.Text.Trim());
			int minutes = int.Parse(Minutes.Text.Trim());
			VolunteerTracker.Attend attend = new VolunteerTracker.Attend();
			attend.Hours = hours;
			attend.Minutes = minutes;
			attend.Notes = "";
			attend.VolunteerId = selectedId;
			attend.EventId = id;
			attend.Notes = VolunteerNotes.Text.Trim();
			attend.Save();			
			attends = VolunteerTracker.Attend.Find<VolunteerTracker.Attend>("EventId", long.Parse(Request["Id"]));
		}
	}
}
