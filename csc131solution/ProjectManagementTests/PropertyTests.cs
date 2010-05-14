using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ProjectManagementTests
{
    [TestFixture]
    public class PropertyTests
    {
        static int propertyNumber = 1;
        public PropertyTests()
        {
        }


        [SetUp]
        public void Init()
        {
            TestUtil.CreateDatabase("DB1");
            TestUtil.CreateProperty(propertyNumber);
            QuickPM.BillingRecord.ClearCache();
            QuickPM.MonetaryTransaction.ClearCache();
        }

        [TearDown]
        public void Destroy()
        {
            TestUtil.DestroyDatabase("DB1");
        }


        [Test]
        public void CreateProperty()
        {
            string address = "add1";
            int rentindex1 = 0;
            string rent1 = "Min Rent";
            int rentindex2 = 1;
            string rent2 = "Addl Rent";
            string legalName = "Legal Name";
            string name = "Name";
            int chartOfAccount = 1001;
            bool active = false;
            //Create a new Property.
            QuickPM.Property p = new QuickPM.Property(2);
            p.Active = active;
            p.Address = address;
            p.ChartOfAccounts = new Dictionary<int, int>();
            p.ChartOfAccounts[rentindex1] = 1001;
            p.RentTypes.Add(rent1);
            p.LegalName = legalName;
            p.Name = name;
            p.AddRentType(rent2);

            p.Save();

            //Load the property from the database;
            p = new QuickPM.Property(2);
            Assert.AreEqual(address, p.Address);
            Assert.AreEqual(active, p.Active);
            Assert.AreEqual(p.ChartOfAccounts[rentindex1], chartOfAccount);
            Assert.AreEqual(p.RentTypes[0], rent1);
            Assert.AreEqual(legalName, p.LegalName);
            Assert.AreEqual(name, p.Name);            
        }


        [Test]
        public void AddDocument()
        {
            
            QuickPM.Property p = new QuickPM.Property(propertyNumber);

            QuickPM.Document doc = new QuickPM.Document();
            doc.Save();
            p.AddDocumentId(doc.Id);
            p.Save();            
            p = new QuickPM.Property(propertyNumber);
            Assert.AreEqual(p.DocumentIds[0], doc.Id);
        }

        [Test]
        public void AddRentType()
        {
            QuickPM.Property p = new QuickPM.Property(propertyNumber);
            Assert.AreEqual(false, p.AddRentType(null));
            Assert.AreEqual(false, p.AddRentType(""));
            p.AddRentType("r");
            Assert.AreEqual(true, p.AddRentType("r"));

            Assert.AreEqual(false, p.AddRentType("^"));//It's an error to use ^ in the rent type name.
        }

        [Test]
        public void CompareProperties()
        {
            QuickPM.Property p = new QuickPM.Property(propertyNumber);
            QuickPM.Property p2 = new QuickPM.Property(propertyNumber + 1);
            Assert.AreEqual(-1, QuickPM.Property.Compare(p, p2));
        }

        [Test]
        public void FindDelinquentTenants()
        {
            string rent1 = "Rent";
            QuickPM.Property p = new QuickPM.Property(propertyNumber);
            p.AddRentType(rent1);
            p.Save();
            QuickPM.Tenant tenant = new QuickPM.Tenant(QuickPM.Util.FormatTenantId(propertyNumber.ToString() + "-01"));
            tenant.CreatedDate = DateTime.MinValue;
            tenant.Save();
            Dictionary<string, decimal> rents = new Dictionary<string,decimal>();
            rents.Add(rent1, 100);
            QuickPM.Util.AddBillingAndARRecords(tenant.Id, 1, 1, new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), rents);
            p.FindDelinquentTenants((float)1.0);
            return;
            /*Assert.AreEqual(1, p.FindDelinquentTenants((float)1.0).Count);            
            Assert.AreEqual(1, p.FindDelinquentTenants((int)60).Count);
            Assert.AreEqual(0, p.FindDelinquentTenants((float)10000).Count);
            QuickPM.Check check = new QuickPM.Check();
            check.TenantId = tenant.Id;
            check.ARRecordDate = new DateTime(DateTime.Today.Year - 1, 1, 1);
            check.CheckDate = DateTime.Today;
            check.Number = "1";
            check.ReceivedDate = DateTime.Today;
            check.Amount = 100;
            check.AutoApply(new QuickPM.Period(check.ARRecordDate.Year, check.ARRecordDate.Month));
            check.Save();

            Assert.AreEqual(0, p.FindDelinquentTenants((int)600).Count);*/
        }

        [Test]
        public void GetDocumentIds()
        {
            QuickPM.Property p = new QuickPM.Property(propertyNumber);
            Assert.AreEqual(0, p.DocumentIds.Count);

            QuickPM.Document doc = new QuickPM.Document();
            
            doc.Save();
            p.DocumentIds.Add(doc.Id);
            p.Save();
            p = new QuickPM.Property(p.Number);
            Assert.AreEqual(1, p.DocumentIds.Count);
            Assert.AreEqual(doc.Id, p.DocumentIds[0]);
            Assert.AreEqual(doc.Id, p.GetDocumentIds()[0]);

            p.RemoveDocumentId(doc.Id);
            p.Save();
            p = new QuickPM.Property(p.Number);
            Assert.AreEqual(0, p.DocumentIds.Count);
        }

        [Test]
        public void GetTenantIds()
        {
            QuickPM.Property p = new QuickPM.Property(propertyNumber);
            QuickPM.Tenant tenant = new QuickPM.Tenant(QuickPM.Util.FormatTenantId(propertyNumber.ToString() + "-01"));            
            tenant.Save();

            Assert.AreEqual(1, p.GetTenantIds().Count);
            Assert.AreEqual(1, p.GetTenantIds(true).Count);            
            Assert.AreEqual(tenant.Id, p.GetTenantIds()[0]);
            Assert.AreEqual(tenant.Id, p.GetTenantIds(true)[0]);

            
            tenant.EndDate = new DateTime(DateTime.Today.Year - 1, 1, 1);
            tenant.CreatedDate = tenant.EndDate;
            tenant.Save();
            Assert.AreEqual(0, p.GetTenantIds().Count);
            Assert.AreEqual(1, p.GetTenantIds(true).Count);
        }

        [Test]
        public void Setters()
        {
            QuickPM.Property p = new QuickPM.Property();
            p.Number = 1;
            p.DocumentIds = new List<long>();
            p.RemitInfoId = p.Number;
            List<string> rentTypes = new List<string>(new string[] { "Rent"});
            p.RentTypes = rentTypes;
            rentTypes.Add("^");
            try
            {
                p.RentTypes = rentTypes;
            }
            catch (Exception e)
            {
                if (e.Message != "Invalid rent type.")
                {
                    throw e;
                }
            }
        }
    }
}
