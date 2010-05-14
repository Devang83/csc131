using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace ProjectManagementTests
{
    [TestFixture]
    public class BillTests
    {
        public BillTests()
        {
        }

        static string tenantId = "0001-0001";
        
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
        public void TestCreateBill()
        {
            string tenantId = QuickPM.Util.FormatTenantId(1, 1);
            QuickPM.Period period = new QuickPM.Period(2008, 1);
            QuickPM.Bill b = QuickPM.Bill.GetBill(tenantId, "Rent", period.Year, period.Month);
            Assert.AreEqual(0m, b.Amount);
            Assert.AreEqual(b.RentTypeIndex, 0);
            Assert.AreEqual(b.TenantId, tenantId);
            Assert.AreEqual(b.Year, period.Year);
            Assert.AreEqual(b.Month, period.Month);
            Assert.AreEqual(b.BillingType, QuickPM.TypeOfBill.Monthly);            

        }

        [Test]
        public void Save()
        {
            decimal billAmount = 100m;
            decimal billAmount2 = 150m;
            QuickPM.Bill b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 2);
            b.Amount = billAmount;
            b.Save();

            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 2);
            Assert.AreEqual(billAmount, b.Amount);

            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 3);
            b.Amount = billAmount;
            b.Save();


            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 4);
            b.Amount = billAmount;
            b.Save();

            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 3);
            Assert.AreEqual(billAmount, b.Amount);

            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 3);
            b.Amount = billAmount2;
            b.Save();

            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 3);
            Assert.AreEqual(billAmount2, b.Amount);

            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 4);
            Assert.AreEqual(billAmount, b.Amount);
        }

        [Test]
        public void GetBill()
        {
            /*QuickPM.Bill b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 5);
            b.BillingType = QuickPM.TypeOfBill.OneTime;
            b.Amount = 100m;
            b.Save();
            b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 5);
            Assert.AreEqual(QuickPM.TypeOfBill.OneTime, b.BillingType);
            Assert.AreEqual(100m, b.Amount);*/
            QuickPM.BillingRecord bRecord = new QuickPM.BillingRecord();
            bRecord.TenantId = tenantId;
            bRecord.StartDate = new DateTime(2008, 5, 1);
            bRecord.EndDate = new DateTime(2008, 5, 14);
            bRecord.Amount = 100m;
            bRecord.RentTypeIndex = 0;
            bRecord.Save();
            QuickPM.Bill b = QuickPM.Bill.GetBill(tenantId, "Rent", 2008, 5);
            Assert.Less(b.Amount, 100m);


        }
    }
}
