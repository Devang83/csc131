using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

// *****************************
// Test all the functions in the PropertyManagement CMD.Verify class.
//
// *****************************

namespace ProjectManagementTests
{
    /// <summary>
    /// All the tests for the QuickPM.Verify class are in the following class.
    /// </summary>
    [TestFixture]
    public class QuickPMVerifyTests
    {
        /// <summary>
        /// Empty.
        /// </summary>
        [SetUp]
        public void Init()
        {
        }

        /// <summary>
        /// Test verifyNumber.
        /// </summary>
        [Test]
        public void verifyNumberTest()
        {
            Assert.AreEqual(true, QuickPM.Verify.VerifyNumber(""));
            Assert.AreEqual(true,QuickPM.Verify.VerifyNumber("0.0"));
            Assert.AreEqual(true, QuickPM.Verify.VerifyNumber("0"));
            Assert.AreEqual(true, QuickPM.Verify.VerifyNumber("-1"));
            Assert.AreEqual(true, QuickPM.Verify.VerifyNumber("100.0"));
            Assert.AreEqual(false, QuickPM.Verify.VerifyNumber("-1.01-"));
            Assert.AreEqual(false, QuickPM.Verify.VerifyNumber("111a122"));
            Assert.AreEqual(false, QuickPM.Verify.VerifyNumber("absc"));
            Assert.AreEqual(false, QuickPM.Verify.VerifyNumber("\'123\'"));
            Assert.AreEqual(true, QuickPM.Verify.VerifyNumber("123,1234"));
        }
    }
}
