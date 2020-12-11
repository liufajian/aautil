using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace AAUtil.UnitTest.TestModules.BaseTests
{
    [TestClass]
    public class DataConvertTest
    {
        [TestMethod]
        public void TestParseDate()
        {
            var datestr = "20200202";
            var success = DateTime.TryParse(datestr, out var dt);
            Assert.IsFalse(success);

            success = DateTime.TryParseExact(datestr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            Assert.IsTrue(success);
        }
    }
}
