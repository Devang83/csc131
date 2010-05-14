using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ProjectManagementTests
{
    [TestFixture]
    class CheckTests
    {
        static string tenantId = "0001-0001";
        public CheckTests()
        {
        }

        [SetUp]
        public void Init()
        {

            TestUtil.CreateDatabase("DB1");
            TestUtil.CreateProperty(1);
            TestUtil.CreateTenant(tenantId);
        }

        [TearDown]
        public void Destroy()
        {
            TestUtil.DestroyDatabase("DB1");
        }

        [Test]
        public void TestAddCheck()
        {
            decimal checkAmount = 100m;
            string checkNumber = "1";
            QuickPM.Tenant t = new QuickPM.Tenant(tenantId);
            QuickPM.Period p = new QuickPM.Period(2008, 1);
            QuickPM.Util.AddARRecord(tenantId, p.Year, p.Month);
            QuickPM.Check c = new QuickPM.Check();
            c.TenantId = tenantId;
            c.Amount = checkAmount;
            c.ARRecordDate = new DateTime(p.Year, p.Month, 1);
            c.ReceivedDate = c.ARRecordDate.AddDays(1);
            c.CheckDate = c.ReceivedDate.AddDays(1);
            c.Number = checkNumber;
            c.Save();


            c = new QuickPM.Check(c.Id);

            Assert.AreEqual(checkAmount, c.Amount);
            Assert.AreEqual(checkNumber, c.Number);
            Assert.AreEqual(new DateTime(p.Year, p.Month, 1), c.ARRecordDate);
            Assert.AreEqual((new DateTime(p.Year,p.Month, 1)).AddDays(1), c.ReceivedDate);
            Assert.AreEqual((new DateTime(p.Year, p.Month, 1)).AddDays(2), c.CheckDate);
            Assert.AreEqual(tenantId, c.TenantId);
            c.Delete();

        }

        [Test]
        public void AutoAppyCheck()
        {
            decimal checkAmount = 100m;
            QuickPM.Period p = new QuickPM.Period(2008, 1);
            string checkNumber = "2";
            QuickPM.Check c = new QuickPM.Check();
            c.TenantId = tenantId;
            c.Number = checkNumber;
            c.Amount = checkAmount;
            c.ARRecordDate = new DateTime(p.Year, p.Month, 1);
            c.AutoApply(p);
            Assert.AreEqual(1, c.AppliedTo.Count);
            Assert.AreEqual(checkAmount, c.AppliedTo[0].Amount);
            Assert.AreEqual(0, c.AppliedTo[0].RentTypeIndex);
            Assert.AreEqual(c.ARRecordDate, c.AppliedTo[0].Date);
            c.Delete();

        }

        [Test]
        public void DeleteCheck()
        {
            QuickPM.Check c = new QuickPM.Check();
            c.Number = "1";
            c.TenantId = tenantId;
            c.Save();
            List<QuickPM.Check> checks = QuickPM.Database.GetChecks(tenantId, c.Number);
            Assert.AreEqual(1, checks.Count);
            c.Delete();
            checks = QuickPM.Database.GetChecks(tenantId, c.Number);
            Assert.AreEqual(0, checks.Count);
        }

        [Test]
        public void MoneyInMoneyOut()
        {
            QuickPM.Check c = new QuickPM.Check();
            Assert.AreEqual(true, c.MoneyIn());
            Assert.AreEqual(false, c.MoneyOut());
        }

    }
}
