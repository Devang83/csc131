using System;
using System.Data;
using System.Configuration;
// using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
// using System.Xml.Linq;

/// <summary>
/// Summary description for DatabaseSettings
/// </summary>
/// 
namespace QuickPMWebsite
{
    public class DatabaseSettings
    {        
        public DatabaseSettings()
        {

        }

        public static void UpdateDatabaseConnectionString(ProfileBase profile, HttpRequest request)
        {


            if (request != null)
            {
                //VolunteerTracker.Document.RootPath = request.PhysicalApplicationPath + "App_Data";
                //VolunteerTracker.ExpenseList.RootPath = request.PhysicalApplicationPath + "App_Data";
            }
			
			string databaseFileName = ConfigurationManager.AppSettings["DatabaseFileName"];
			string databaseName = ConfigurationManager.AppSettings["DatabaseName"];
			string databaseUserName = ConfigurationManager.AppSettings["DatabaseUserName"];
			string databasePassword = ConfigurationManager.AppSettings["DatabasePassword"];
			string databaseConnector = ConfigurationManager.AppSettings["DatabaseConnector"];
  			if (databaseConnector.ToLower() == "postgresql")
			{
				VolunteerTracker.DatabaseAccess.SetDatabaseType(new VolunteerTracker.PSQL());
			}
			if (databaseConnector.ToLower() == "sqlite")
			{
				VolunteerTracker.DatabaseAccess.SetDatabaseType(new VolunteerTracker.SQLite());
			}
			
			VolunteerTracker.Database.SetConnectionString(VolunteerTracker.Database.CreateConnectionString(databaseFileName, databaseName, databaseUserName, databasePassword));			
            if (profile.IsAnonymous)
            {                
                VolunteerTracker.Database.SetUserId(VolunteerTracker.AccessControlList.ROOT_USERID);
                return;
            }            			
            MembershipUser mu = Membership.GetUser(profile.UserName);			
            VolunteerTracker.Database.SetUserId(VolunteerTracker.AccessControlList.ROOT_USERID);
            bool userExists = false;
			System.Collections.Generic.List<VolunteerTracker.User> users = VolunteerTracker.User.Find<VolunteerTracker.User>();			
			foreach (VolunteerTracker.User user in users)
			{
				if(user.Email.Trim().ToLower() == mu.Email.Trim().ToLower())
				{
					VolunteerTracker.User pmUser = new VolunteerTracker.User(mu.Email);
            		VolunteerTracker.Database.SetUserId(pmUser.Id);
					userExists = true;
					break;
				}
			}
			if (!userExists)
			{
				VolunteerTracker.User user = new VolunteerTracker.User(mu.Email);
				user.Save();
				VolunteerTracker.Database.SetUserId(user.Id);
			}			
			//
            
        }
    }
}
