using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
// *****************************
// Test all the functions in the PropertyManagement CMD.Util class.
//
// *****************************

namespace ProjectManagementTests
{

    /// <summary>
    /// Test the functions in the QuickPM project.
    /// </summary>        
    [TestFixture]
    public class QuickPMUtilTests
    {
        /// <summary>
        /// Setup the test database.
        /// </summary>
        public QuickPMUtilTests()
        {
            //CMD.Util.SetDatabaseName("TestDatabase");
            //CreateDatabaseTables.CreateDatabaseTables.RecreateTables();
        }

        /// <summary>
        /// Setup function.
        /// </summary>
        [SetUp]
        public void Init()
        {
        }

        /// <summary>
        /// Test ConvertMonthToString function, simple tests.
        /// </summary>
        [Test]
        public void ConvertMonthToString()
        {
            Assert.AreEqual("January", QuickPM.Util.ConvertMonthToString(1));
            Assert.AreEqual("February", QuickPM.Util.ConvertMonthToString(2));
            Assert.AreEqual("March", QuickPM.Util.ConvertMonthToString(3));
            Assert.AreEqual("April", QuickPM.Util.ConvertMonthToString(4));
            Assert.AreEqual("May", QuickPM.Util.ConvertMonthToString(5));
            Assert.AreEqual("June", QuickPM.Util.ConvertMonthToString(6));
            Assert.AreEqual("July", QuickPM.Util.ConvertMonthToString(7));
            Assert.AreEqual("August", QuickPM.Util.ConvertMonthToString(8));
            Assert.AreEqual("September", QuickPM.Util.ConvertMonthToString(9));
            Assert.AreEqual("October", QuickPM.Util.ConvertMonthToString(10));
            Assert.AreEqual("November", QuickPM.Util.ConvertMonthToString(11));
            Assert.AreEqual("December", QuickPM.Util.ConvertMonthToString(12));
            try
            {
                QuickPM.Util.ConvertMonthToString(0);
                Assert.Fail();
            }
            catch (Exception e)
            {
                string tmp = e.Message;
            }

            try
            {
                QuickPM.Util.ConvertMonthToString(13);
                Assert.Fail();
            }
            catch (Exception e)
            {
                string tmp = e.Message;
            }
        }

        /// <summary>
        /// Test the ConvertMonthToInt function, simple tests.
        /// </summary>
        [Test]
        public void ConvertMonthToInt()
        {
            Assert.AreEqual(1, QuickPM.Util.ConvertMonthToInt("Jan"));
            Assert.AreEqual(1, QuickPM.Util.ConvertMonthToInt("Jan."));
            Assert.AreEqual(1, QuickPM.Util.ConvertMonthToInt("January"));

            Assert.AreEqual(2, QuickPM.Util.ConvertMonthToInt("Feb"));
            Assert.AreEqual(2, QuickPM.Util.ConvertMonthToInt("Feb."));
            Assert.AreEqual(2, QuickPM.Util.ConvertMonthToInt("February"));

            Assert.AreEqual(3, QuickPM.Util.ConvertMonthToInt("Mar"));
            Assert.AreEqual(3, QuickPM.Util.ConvertMonthToInt("Mar."));
            Assert.AreEqual(3, QuickPM.Util.ConvertMonthToInt("March"));

            Assert.AreEqual(4, QuickPM.Util.ConvertMonthToInt("Apr"));
            Assert.AreEqual(4, QuickPM.Util.ConvertMonthToInt("Apr."));
            Assert.AreEqual(4, QuickPM.Util.ConvertMonthToInt("April"));

            Assert.AreEqual(5, QuickPM.Util.ConvertMonthToInt("May"));
            Assert.AreEqual(5, QuickPM.Util.ConvertMonthToInt("May."));
            Assert.AreEqual(5, QuickPM.Util.ConvertMonthToInt("May"));

            Assert.AreEqual(6, QuickPM.Util.ConvertMonthToInt("Jun"));
            Assert.AreEqual(6, QuickPM.Util.ConvertMonthToInt("Jun."));
            Assert.AreEqual(6, QuickPM.Util.ConvertMonthToInt("June"));

            Assert.AreEqual(7, QuickPM.Util.ConvertMonthToInt("Jul"));
            Assert.AreEqual(7, QuickPM.Util.ConvertMonthToInt("Jul."));
            Assert.AreEqual(7, QuickPM.Util.ConvertMonthToInt("July"));

            Assert.AreEqual(8, QuickPM.Util.ConvertMonthToInt("Aug"));
            Assert.AreEqual(8, QuickPM.Util.ConvertMonthToInt("Aug."));
            Assert.AreEqual(8, QuickPM.Util.ConvertMonthToInt("August"));

            Assert.AreEqual(9, QuickPM.Util.ConvertMonthToInt("Sep"));
            Assert.AreEqual(9, QuickPM.Util.ConvertMonthToInt("Sep."));
            Assert.AreEqual(9, QuickPM.Util.ConvertMonthToInt("September"));

            Assert.AreEqual(10, QuickPM.Util.ConvertMonthToInt("Oct"));
            Assert.AreEqual(10, QuickPM.Util.ConvertMonthToInt("Oct."));
            Assert.AreEqual(10, QuickPM.Util.ConvertMonthToInt("October"));

            Assert.AreEqual(11, QuickPM.Util.ConvertMonthToInt("Nov"));
            Assert.AreEqual(11, QuickPM.Util.ConvertMonthToInt("Nov."));
            Assert.AreEqual(11, QuickPM.Util.ConvertMonthToInt("November"));

            Assert.AreEqual(12, QuickPM.Util.ConvertMonthToInt("Dec"));
            Assert.AreEqual(12, QuickPM.Util.ConvertMonthToInt("Dec."));
            Assert.AreEqual(12, QuickPM.Util.ConvertMonthToInt("December"));
            try
            {
                QuickPM.Util.ConvertMonthToInt("Janu");
                Assert.Fail();
            }
            catch (Exception e)
            {
                string tmp = e.Message;
            }
        }

        /// <summary>
        /// Test the prepad function. The prepad function adds a prefix to the given string such that the resulting string has
        /// wanted length. And all the characters in the prefix string are equal to the given character.
        /// </summary>
        [Test]
        public void prepad()
        {
            Assert.AreEqual("0003", QuickPM.Util.prepad("3", '0', 4));
            Assert.AreEqual("0000", QuickPM.Util.prepad("", '0', 4));
            Assert.AreEqual("3333", QuickPM.Util.prepad("3333", '0', 4));
        }

        /// <summary>
        /// Tests the formatTenantId functions. A TenantId string should be formatted as "####-####". Where # is a digit.
        /// </summary>
        [Test]
        public void formatTenantId()
        {
            Assert.AreEqual("0003-0010", QuickPM.Util.FormatTenantId(3, 10));
            Assert.AreEqual("3333-1111", QuickPM.Util.FormatTenantId(3333, 1111));
            Assert.AreEqual("0030-1010", QuickPM.Util.FormatTenantId(30, 1010));
            Assert.AreEqual("0003-0010", QuickPM.Util.FormatTenantId("3-10"));
            Assert.AreEqual("0000-0000", QuickPM.Util.FormatTenantId("0-0"));
            Assert.AreEqual("4333-1234", QuickPM.Util.FormatTenantId("4333-1234"));
            Assert.AreEqual("0003-0010", QuickPM.Util.FormatTenantId("03-010"));
        }
    }
}
