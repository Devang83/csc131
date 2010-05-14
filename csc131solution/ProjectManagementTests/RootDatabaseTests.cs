using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
// *****************************
// Test all the functions in the PropertyManagement CMD.Database class.
//
// *****************************

namespace ProjectManagementTests
{

    /// <summary>
    /// Test the functions in the QuickPM.Database class.
    /// </summary>        
    [TestFixture]
    public class QuickPMDatabaseTests
    {
        /// <summary>
        /// Setup the test database.
        /// </summary>
        public QuickPMDatabaseTests()
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
        /// Test FormatSqlString function for correct functionality.
        /// </summary>
        [Test]
        public void TestFormatSqlString()
        {
            Assert.AreEqual("''", QuickPM.Database.FormatSqlString("'"));
        }

    }
}
