using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VolunteerTracker;
using Npgsql;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           		
			
			Database.SetUserId(AccessControlList.ROOT_USERID);
			DatabaseAccess.SetDatabaseType(new SQLite());
			
			ManageDatabase md = new ManageDatabase("", "volunteertracker.db", "", "");
		    //md.CreateDatabase();
			md.RecreateTables();
			
			Database.SetConnectionString(Database.CreateConnectionString("", "volunteertracker.db", "", ""));
			//Database.SetUserId(1);
			VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer();
			v.FirstName = "Test";
			v.LastName = "";
			v.Address = "";
			v.CellPhone = "";
			v.HomePhone = "";
			v.OfficePhone = "";			
			v.Email = "";
			v.Employer = "";
			v.Ethnicity = "";
			v.JobTitle = "";
			v.Birthday = new DateTime();
			v.Male = 0;			
			v.Save();
			
			
			List<VolunteerTracker.Volunteer> vs = VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>();
			
			
			/*Database.RecordExists<Property>(new Property(0));
			long[] propIds = QuickPM.Util.GetPropertyIds(true);
			QuickPM.CachedActiveRecord.ClearCache();
			QuickPM.Property p = new QuickPM.Property(0);
			Console.WriteLine("p.CanRead(1) " + p.ACL.CanRead(1));
			Console.WriteLine("(3) Active " + p.Active);
			Console.WriteLine("(3) Name " + p.Name);
			*/
			
        }

      
    }
}
