using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a1");
            ProjectManagementTests.PropertyTests propertyTests = new ProjectManagementTests.PropertyTests();
            Console.WriteLine("a2");
            propertyTests.Destroy();
            Console.WriteLine("a3");
            propertyTests.Init();
            Console.WriteLine("a4");
            propertyTests.AddDocument();
            Console.WriteLine("a5");
            propertyTests.FindDelinquentTenants();
            Console.WriteLine("a6");
            propertyTests.Destroy();
            Console.WriteLine("a7");
            /*ProjectManagementTests.ARRecordTests arRecordTests = new ProjectManagementTests.ARRecordTests();
            arRecordTests.Destroy();
            arRecordTests.Init();
            arRecordTests.AppliedRents();
            arRecordTests.Destroy();*/
            /*ProjectManagementTests.BillingRecordTests bTests = new ProjectManagementTests.BillingRecordTests();
            bTests.Destroy();
            bTests.Init();            
            bTests.MergeBillingRecords();
            bTests.Destroy();*/
            string tmp = System.Console.ReadLine();
        }
    }
}
