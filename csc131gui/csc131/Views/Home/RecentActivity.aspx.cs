using System;
using System.Web;
using System.Web.UI;

namespace QuickPMWebsite.Views.Home
{


	public partial class RecentActivity : System.Web.Mvc.ViewPage
	{		
		public string DisplayRecord(VolunteerTracker.ActiveRecord record) 
		{			
			string type = record.GetType().Name;
			string modifiedDate = record.GetTimeSinceModification();
			string createdDate = record.GetTimeSinceCreation();
			long userId = VolunteerTracker.Database.GetUserId();
			VolunteerTracker.Database.SetUserId(VolunteerTracker.AccessControlList.ROOT_USERID);
			string modifiedBy = new VolunteerTracker.User(record.ModifiedBy).Email;
			VolunteerTracker.Database.SetUserId(userId);
			string linkUrl = "";
			string linkText = "";

			if (record is VolunteerTracker.Person) {	
				//linkUrl = QuickPMWebsite.AppCode.Link.LinkTo((VolunteerTracker.Person)record, Page);
				return "";
			}
			if (record is VolunteerTracker.Volunteer)
			{
				linkUrl = "Volunteer/Volunteer.aspx?Id=" + record.Id;
				linkText = ((VolunteerTracker.Volunteer)(record)).FirstName;
			}
			if (record is VolunteerTracker.Event)
			{
				linkUrl = "Event/Event.aspx?Id=" + record.Id;
				linkText = ((VolunteerTracker.Event)(record)).Name;
			}
			
			string link = "<a href=\"" + linkUrl + "\">" + linkText + "</a>";
			return "<td>" + type + "</td><td>" + link + "</td><td style=\"background-color:#D8D8D8  \">" + modifiedDate + 
				"</td><td style=\"background-color:#D8D8D8  \">" + createdDate + "</td>" + 
				"</td><td style=\"background-color:#D8D8D8  \">" + modifiedBy + "</td>";
		}
	}
}
