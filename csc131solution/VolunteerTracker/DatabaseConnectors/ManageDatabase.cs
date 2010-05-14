using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolunteerTracker
{
    public class ManageDatabase
    {

        public static string tableUsers = "Users";
        public static string tablePeople = "People";
		public static string tableEvents = "Events";
		public static string tableVolunteers = "Volunteers";
		public static string tableAttendance = "Attendance";
		
        
        public string DatabasePath { get; set; }
        public string DatabaseName { get; set; }
		public string DatabaseUserName { get; set; }
		public string DatabasePassword { get; set; }		
		

        public ManageDatabase(string databasePath, string databaseName, string databaseUserName, string databasePassword)
        {
            DatabasePath = databasePath.Trim();
            DatabaseName = databaseName.Trim();
			DatabasePassword = databasePassword.Trim();
			DatabaseUserName = databaseUserName.Trim();
            Database.ConnectionString = Database.CreateConnectionString(DatabasePath, DatabaseName, DatabaseUserName, DatabasePassword);
        }


        public void DeleteTables()
        {

            Database.ConnectionString = Database.CreateConnectionString(DatabasePath, DatabaseName, DatabaseUserName, DatabasePassword);
            VolunteerTracker.DatabaseAccess databaseAccess = VolunteerTracker.Database.NewDatabaseAccess();

            string[] tables = { tablePeople,                                 
                                 tableUsers, tableEvents, tableVolunteers, tableAttendance};
            for (int i = 0; i < tables.Length; i++)
            {
                try
                {
                    databaseAccess.ExecuteNonQuery("DROP TABLE " + tables[i]);
                }
                catch (Exception e)
                {
                    if (e.Message.IndexOf("no such table") < 0 && e.Message.IndexOf("does not exist") < 0)
                        throw new Exception(e.Message, e.InnerException);
                }
            }
            databaseAccess.Dispose();
        }


        public void CreateTables()
        {
            Database.ConnectionString = Database.CreateConnectionString(DatabasePath, DatabaseName, DatabaseUserName, DatabasePassword);
            VolunteerTracker.DatabaseAccess databaseAccess = VolunteerTracker.Database.NewDatabaseAccess();
			DatabaseType dbType = DatabaseAccess.GetDatabaseType();
            string createString = @"CREATE TABLE " + tablePeople + @"
            (
             Id                 " + dbType.GetPrimaryKey() + @" not null,
             CreatedDate        " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
             ModifiedDate       " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
			 ModifiedBy         " + dbType.GetDbColumnType(typeof(long))   + @" not null,
             AssociatedKey      " + dbType.GetDbColumnType(typeof(string)) + @" not null,             
             Name               " + dbType.GetDbColumnType(typeof(string)) + @" not null,
             Title              " + dbType.GetDbColumnType(typeof(string)) + @" not null,
             CellPhone          " + dbType.GetDbColumnType(typeof(string)) + @" not null,
             OfficePhone        " + dbType.GetDbColumnType(typeof(string)) + @" not null,
             HomePhone          " + dbType.GetDbColumnType(typeof(string)) + @" not null,
             Email              " + dbType.GetDbColumnType(typeof(string)) + @" not null,
             Fax                " + dbType.GetDbColumnType(typeof(string)) + @" not null,
             Address            " + dbType.GetDbColumnType(typeof(string)) + @" not null,             
             ACL                " + dbType.GetDbColumnType(typeof(string)) + @" not null -- access control list, contains a list of VolunteerTracker.User.Id's and their access level 
            )";
            databaseAccess.ExecuteNonQuery(createString);
                    


            createString = @"CREATE TABLE " + tableUsers + @"
            (
            Id                  " + dbType.GetPrimaryKey() + @" not null,            
            CreatedDate         " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
            ModifiedDate        " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
			ModifiedBy          " + dbType.GetDbColumnType(typeof(long))   + @" not null,            
            Name                " + dbType.GetDbColumnType(typeof(string)) + @" not null,
            Email               " + dbType.GetDbColumnType(typeof(string)) + @" not null,
            ACL                 " + dbType.GetDbColumnType(typeof(string)) + @" not null -- access control list, contains a list of VolunteerTracker.User.Id's and their access level
            )";
            databaseAccess.ExecuteNonQuery(createString);

			
			createString = @"CREATE TABLE " + tableEvents + @"
            (
            Id                  " + dbType.GetPrimaryKey() + @" not null,            
            CreatedDate         " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
            ModifiedDate        " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
			ModifiedBy          " + dbType.GetDbColumnType(typeof(long))   + @" not null,            
            Name                " + dbType.GetDbColumnType(typeof(string)) + @" not null,
			Date                " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
            Email               " + dbType.GetDbColumnType(typeof(string)) + @" not null,
            ACL                 " + dbType.GetDbColumnType(typeof(string)) + @" not null -- access control list, contains a list of VolunteerTracker.User.Id's and their access level
            )";
            databaseAccess.ExecuteNonQuery(createString);

			
			createString = @"CREATE TABLE " + tableVolunteers + @"
            (
            Id                  " + dbType.GetPrimaryKey() + @" not null,            
            CreatedDate         " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
            ModifiedDate        " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
			ModifiedBy          " + dbType.GetDbColumnType(typeof(long))   + @" not null,            
            FirstName           " + dbType.GetDbColumnType(typeof(string)) + @" not null,
			LastName            " + dbType.GetDbColumnType(typeof(string)) + @" not null,
            Email               " + dbType.GetDbColumnType(typeof(string)) + @" not null,
			Address             " + dbType.GetDbColumnType(typeof(string)) + @" not null,
			CellPhone           " + dbType.GetDbColumnType(typeof(string)) + @" not null,
			HomePhone           " + dbType.GetDbColumnType(typeof(string)) + @" not null,
			OfficePhone         " + dbType.GetDbColumnType(typeof(string)) + @" not null,
            ACL                 " + dbType.GetDbColumnType(typeof(string)) + @" not null -- access control list, contains a list of VolunteerTracker.User.Id's and their access level
            )";
            databaseAccess.ExecuteNonQuery(createString);

			
			createString = @"CREATE TABLE " + tableAttendance + @"
            (
            Id                  " + dbType.GetPrimaryKey() + @" not null,            
            CreatedDate         " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
            ModifiedDate        " + dbType.GetDbColumnType(typeof(DateTime)) + @" not null,
			ModifiedBy          " + dbType.GetDbColumnType(typeof(long))   + @" not null,            
            VolunteerId           " + dbType.GetDbColumnType(typeof(int)) + @" not null,
			EventId            " + dbType.GetDbColumnType(typeof(int)) + @" not null,
            ACL                 " + dbType.GetDbColumnType(typeof(string)) + @" not null -- access control list, contains a list of VolunteerTracker.User.Id's and their access level
            )";
            databaseAccess.ExecuteNonQuery(createString);            
            databaseAccess.Dispose();        
        }               				
        

        

        public void RecreateTables()
        {
            DeleteTables();
            CreateTables();
        }
        public void CreateUser()
        {
            throw new Exception("Not implemented");
        }

        public void CreateDatabase()
        {
			Console.WriteLine("Entering CreateDatabase");			
            Database.ConnectionString = Database.CreateConnectionString(DatabasePath, DatabaseName, DatabaseUserName, DatabasePassword);
            Console.WriteLine("Allocating new databaseAccess");
			VolunteerTracker.DatabaseAccess databaseAccess = VolunteerTracker.Database.NewDatabaseAccess();
			Console.WriteLine("Getting dbType");
			DatabaseType dbType = DatabaseAccess.GetDatabaseType();
            string createString = @"CREATE DATABASE " + DatabaseName + "";
            Console.WriteLine("executing createString: " + createString);
			try{
				databaseAccess.ExecuteNonQuery("drop database " + DatabaseName);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error dropping database " + e.Message);	
			}			
			databaseAccess.ExecuteNonQuery(createString);
            Console.WriteLine("done executing createString");
			databaseAccess.Dispose();
            CreateTables();
        }

		public void AddDefaultVolunteersAndEvents()
		{
			
			Database.SetConnectionString(Database.CreateConnectionString(DatabasePath, DatabaseName, DatabaseUserName, DatabasePassword));
            VolunteerTracker.Volunteer v1 = new Volunteer(1);			
            v1.FirstName = "Default Volunteer";
            v1.LastName = "";
			v1.CellPhone = "";
			v1.HomePhone = "";
			v1.OfficePhone = "";
            v1.Save();			
			
			VolunteerTracker.Event e = new VolunteerTracker.Event();
			e.Name = "Default Event";
			e.Save();			           
		}							

        public void DeleteDatabase()
        {
            throw new Exception("Not implemented!");
        }

        public void RecreateDatabase()
        {
            CreateDatabase();
            CreateUser();
        }
        /*public void Main(string[] args)
        {
            VolunteerTracker.Database.InitConnectionSettings();
            System.Console.Write("SQL DatabaseName:");
            VolunteerTracker.Database.DatabaseName = System.Console.ReadLine().Trim();
            System.Console.WriteLine();
            RecreateTables();
        }*/
 
    }
}
