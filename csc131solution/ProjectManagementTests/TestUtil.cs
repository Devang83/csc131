using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectManagementTests
{
    class TestUtil
    {

        static string dbPath = ".";
        public static void CreateDatabase(string name)
        {

            try
            {
                File.Delete(dbPath + "/" + name);                
            }
            catch (System.IO.IOException e)
            {                
            }
            QuickPM.ManageDatabase manageDatabase = new QuickPM.ManageDatabase(".", name);
            manageDatabase.CreateDatabase();
            QuickPM.Database.ConnectionString = "Data Source = " + dbPath + "/" + name;
            
        }

        public static string GetDatabaseConnectionString(string dbName)
        {
            return "Data Source = " + dbPath + "/" + dbName;
        }

        public static void DestroyDatabase(string name)
        {
            while (true)
            {
                try
                {
                    File.Delete(dbPath + "/" + name);
                    break;
                }
                catch (System.IO.IOException e)
                {
                    if (!e.Message.Contains("used by another process"))
                    {
                        throw e;
                    }
                }
            }
        }

        public static void CreateProperty(int number)
        {            
            QuickPM.Property p = new QuickPM.Property(number);
            p.Active = true;
            p.RentTypes.Add("Rent");

            p.ChartOfAccounts.Add(p.RentTypes.IndexOf("Rent"), 1);
            p.Name = "Property #1";
            p.Save();
        }

        public static void CreateTenant(string tenantId)
        {
            QuickPM.Tenant t = new QuickPM.Tenant(tenantId);
            t.CreatedDate = DateTime.MinValue;
            t.Name = "Tenant #1";
            t.Save();
        }
    }
}
