
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;

namespace QuickPMWebsite.Controllers
{


	public class HomeController : Controller
	{
		
		public ActionResult Index()
        {
            ViewData["Message"] = "This is ASP.NET MVC!";
            return View();
        }
		
		public ActionResult ForgotPassword()
		{
			return View();
		}
		
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult ForgotPassword(FormCollection formCollection)
		{
			Console.WriteLine("Forgot Password called");
			string email = formCollection["textboxemail"];
			
        	string userName = Membership.GetUserNameByEmail(email);

        	if (userName == "")
        	{
            	ViewData["Error"] = "<font color=\"red\">No user with that email address exists</font>";
            	return View();
        	}

        	MembershipUser user = Membership.GetUser(userName);
        
        	string password = user.ResetPassword();        
        	Sloppycode.PronounceablePasswordGenerator generator = new Sloppycode.PronounceablePasswordGenerator();
        	ArrayList passwords = generator.Generate(1, 8);       
        	string newpassword = passwords[0].ToString();
        	user.ChangePassword(password, newpassword);
        	string subject = "Quickpm.net password. This is an automated message, please do not respond";
			string url = "quickpm.net";
			
			if(ConfigurationManager.AppSettings["DatabaseName"] != null)
			{
				url = ConfigurationManager.AppSettings["DatabaseName"] + ".quickpm.net";
			}
        	string msg = "Email " + email + "\r\n" + "Password " + newpassword + "\r\n" + "To login go to " + url;
        	SendEmail.Send(email, subject, msg, this.Request.PhysicalApplicationPath);                

	        ViewData["ForgotPasswordSent"] = true;
    	    ViewData["ForgotPasswordEmail"] = email;
			return View();
		}
		public ActionResult ExpiringLeases(long id) 
		{			
			ViewData["fullPage"] = false;
			
			ViewData["Id"] = id;
			ViewData["Expirations"] = null;
			return View();
		}
		
		
		//delegate VolunteerTracker.ActiveRecord Del(VolunteerTracker.Property p);
		
		public ActionResult RecentActivity(long id, bool? fullPage)
		{					
			long numRecords = 5;
			ViewData["fullPage"] = true;
			if(fullPage.HasValue && fullPage.Value == false)
			{
				ViewData["fullPage"] = false;
			}
			if (id != -1)
			{
				numRecords = id;
			}
			
			List<VolunteerTracker.ActiveRecord> activity = new List<VolunteerTracker.ActiveRecord>();			
			activity.AddRange(VolunteerTracker.ActiveRecord.GetRecentActivity<VolunteerTracker.Event>(numRecords).ConvertAll<VolunteerTracker.ActiveRecord>(delegate(VolunteerTracker.Event obj) {return (VolunteerTracker.ActiveRecord)obj;}));			
			activity.AddRange(VolunteerTracker.ActiveRecord.GetRecentActivity<VolunteerTracker.Volunteer>(numRecords).ConvertAll<VolunteerTracker.ActiveRecord>(delegate(VolunteerTracker.Volunteer obj) {return (VolunteerTracker.ActiveRecord)obj;}));			
			/*activity.AddRange();*/
			
			
			activity.Sort(delegate(VolunteerTracker.ActiveRecord r1, VolunteerTracker.ActiveRecord r2){
				if (r2 == null && r1 == null) {
					return 0;
				}
				if (r2 == null) 
				{
					return -1;
				}
				if (r1 == null)
				{
					return 1;
				}
				return r2.ModifiedDate.CompareTo(r1.ModifiedDate);
			});			
			if (activity.Count > numRecords) 
			{
				activity.RemoveRange((int)activity.Count - (int)(activity.Count - numRecords), (int)(activity.Count - numRecords));
			}			
			ViewData["Activity"] = activity;
			//ViewData["Message"] = "activity.Count:" + activity.Count.ToString();
			return View();
		}
	}
}
