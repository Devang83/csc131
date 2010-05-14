using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ProjectManagementTests
{
    [TestFixture]
    class BillingInfoTests
    {
        string tenantId = "0001-0001";
        int propertyNumber = 1;

        public BillingInfoTests()
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
        public void TestSetters()
        {
            QuickPM.Tenant t = new QuickPM.Tenant(tenantId);
            t.BillInfo.Address = "A";
            t.BillInfo.BillSame = false;
            t.BillInfo.AssociatedProfile = t;
            t.BillInfo.City = "C";
            t.BillInfo.KeyContactId = 1;
            t.BillInfo.Name = "N";
            t.BillInfo.State = "S";
            t.BillInfo.Zip = "Z";
            t.BillInfo.Save();

            t = new QuickPM.Tenant(tenantId);
            Assert.AreEqual("A", t.BillInfo.Address);
            Assert.AreEqual(false, t.BillInfo.BillSame);
            Assert.AreEqual(t, t.BillInfo.AssociatedProfile);
            Assert.AreEqual("C", t.BillInfo.City);
            Assert.AreEqual(1, t.BillInfo.KeyContactId);
            Assert.AreEqual("N", t.BillInfo.Name);
            Assert.AreEqual("S", t.BillInfo.State);
            Assert.AreEqual("Z", t.BillInfo.Zip);
        }
    }
}
