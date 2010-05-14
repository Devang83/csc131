using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ProjectManagementTests
{
    [TestFixture]
    public class BillingRecordTests
    {
        string tenantId = "0001-0001";
        int propertyNumber = 1;

        
        public BillingRecordTests()
        {
        }

        [SetUp]
        public void Init()
        {
            TestUtil.CreateDatabase("DB1");
            TestUtil.CreateProperty(propertyNumber);
            TestUtil.CreateTenant(tenantId);
            
        }

        [TearDown]
        public void Destroy()
        {
            TestUtil.DestroyDatabase("DB1");
        }

        [Test]
        public void CompareBillingRecordsByStartDate()
        {
            QuickPM.BillingRecord b1 = new QuickPM.BillingRecord();
            b1.StartDate = DateTime.Today;
            QuickPM.BillingRecord b2 = new QuickPM.BillingRecord();
            b2.StartDate = b1.StartDate.AddDays(1);
            Assert.Less(QuickPM.BillingRecord.CompareBillingRecordsByStartDate(b1, b2), 0);
            b2.StartDate = b1.StartDate;
            Assert.AreEqual(QuickPM.BillingRecord.CompareBillingRecordsByStartDate(b1, b2), 0);
            b2.StartDate = b1.StartDate.AddMonths(-1);
            Assert.Greater(QuickPM.BillingRecord.CompareBillingRecordsByStartDate(b1, b2), 0);
        }

        [Test]
        public void CreateBillingRecord()
        {
            QuickPM.BillingRecord b1 = new QuickPM.BillingRecord();
            b1.StartDate = new DateTime(2008, 1, 1);
            b1.EndDate = new DateTime(2008, 2, 1);
            b1.RentTypeIndex = 0;
            b1.Amount = 100m;
            b1.TenantId = tenantId;
            b1.Save();

            b1 = QuickPM.BillingRecord.CreateBillingRecord(b1.Id);
            Assert.AreEqual(b1.StartDate, new DateTime(2008, 1, 1));
            Assert.AreEqual(b1.Amount, 100m);
            Assert.AreEqual(b1.RentTypeIndex, 0);
            Assert.AreEqual(b1.TenantId, tenantId);

            b1.StartDate = DateTime.MinValue;
            QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
            tenant.CreatedDate = DateTime.Today;
            tenant.Save();
            bool exceptionThrown = false;
            try
            {
                b1.Save();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("billing record start before the tenant was created"))
                {
                    exceptionThrown = true;
                }
                else
                {
                    throw e;
                }                
            }
            if (!exceptionThrown)
            {
                throw new Exception("Should have had exception when saving billing record.");
            }
        }

        [Test]
        public void GetBillingRecord()
        {
            QuickPM.BillingRecord b1 = new QuickPM.BillingRecord();
            b1.StartDate = new DateTime(2008, 3, 1);
            b1.EndDate = new DateTime(2008, 4, 1);
            b1.RentTypeIndex = 0;
            b1.Amount = 100m;
            b1.TenantId = tenantId;
            b1.Save();

            b1 = QuickPM.BillingRecord.GetBillingRecord(tenantId, "Rent", b1.StartDate, new QuickPM.DatabaseAccess(QuickPM.Database.ConnectionString));
            Assert.AreEqual(b1.StartDate, new DateTime(2008, 3, 1));
            Assert.AreEqual(b1.Amount, 100m);
            Assert.AreEqual(b1.RentTypeIndex, 0);
            Assert.AreEqual(b1.TenantId, tenantId);
        }

        [Test]
        public void MergeBillingRecords()
        {
            QuickPM.BillingRecord b1 = new QuickPM.BillingRecord();
            b1.StartDate = new DateTime(2007, 1, 1);
            b1.EndDate = new DateTime(2007, 4, 1);
            b1.RentTypeIndex = 0;
            b1.Amount = 100m;
            b1.TenantId = tenantId;
            b1.Save();

            QuickPM.BillingRecord b2 = new QuickPM.BillingRecord();
            b2.StartDate = new DateTime(2007, 4, 2);
            b2.EndDate = new DateTime(2007, 4, 20);
            b2.RentTypeIndex = 0;
            b2.Amount = 100m;
            b2.TenantId = tenantId;
            b2.Save();

            QuickPM.BillingRecord.MergeBillingRecords(tenantId, "Rent");

            List<QuickPM.BillingRecord> billingRecords = QuickPM.BillingRecord.GetBillingRecords(tenantId);
            Assert.AreEqual(1, billingRecords.Count);
        }
    }
}
