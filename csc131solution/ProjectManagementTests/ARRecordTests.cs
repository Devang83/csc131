using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace ProjectManagementTests
{    
    [TestFixture]
    public class ARRecordTests
    {
        public ARRecordTests()
        {
        }

        static string tenantId = "0001-0001";

        [SetUp]
        public void Init()
        {
            TestUtil.CreateDatabase("DB1");
            TestUtil.CreateProperty(1);
            TestUtil.CreateTenant("1-1");            
        }

        [TearDown]
        public void Destroy()
        {
            TestUtil.DestroyDatabase("DB1");
        }


        [Test]
        public void TestCreateARRecord()
        {
            QuickPM.ARRecord arRecord = new QuickPM.ARRecord("1-1", 2008,1);
            Assert.AreEqual(true, arRecord.NewRecord);
            arRecord.Save();

            arRecord = new QuickPM.ARRecord("1-1", 2008, 1);
            Assert.AreEqual(false, arRecord.NewRecord);
        }

        [Test]
        public void AppliedRents()
        {
            decimal checkAmount = 100m;
            QuickPM.ARRecord arRecord = new QuickPM.ARRecord("1-1", 2008, 1);
            QuickPM.Check check = new QuickPM.Check();
            check.TenantId = tenantId;
            check.ARRecordDate = new DateTime(arRecord.Year, arRecord.Month, 1);
            check.ReceivedDate = DateTime.Today;
            check.Amount = checkAmount;
            check.CheckDate = DateTime.Today;
            check.AutoApply(new QuickPM.Period(arRecord.Year, arRecord.Month));
            Dictionary<string, decimal> appliedRents = arRecord.AppliedRents();
            Assert.AreEqual(checkAmount, appliedRents[(new QuickPM.Tenant(tenantId)).RentTypes[0]]);
        }

    }
}
