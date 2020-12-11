using AAUtil.Converts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AAUtil.UnitTest.TestModules.ConvertTests
{
    [TestClass]
    public class DateConvertTest
    {

        [TestMethod]
        public void TryParseDateTest()
        {
            var success = DateConvert.TryParseDate("  ", out var dt);
            Assert.IsFalse(success);

            success = DateConvert.TryParseDate("2020020  ", out dt);
            Assert.IsFalse(success);

            success = DateConvert.TryParseDate("2020020202020  ", out dt);
            Assert.IsTrue(success && dt == new DateTime(2020, 2, 2));

            success = DateConvert.TryParseDate("2020020aaaa", out dt);
            Assert.IsFalse(success);

            success = DateConvert.TryParseDate("2020a02a02aaaa", out dt);
            Assert.IsFalse(success);

            success = DateConvert.TryParseDate("20200202aaaa", out dt);
            Assert.IsTrue(success && dt == new DateTime(2020, 2, 2));

            success = DateConvert.TryParseDate("2020,0202aaaa", out dt);
            Assert.IsTrue(success && dt == new DateTime(2020, 2, 2));

            success = DateConvert.TryParseDate("2020-02-02aaaa", out dt);
            Assert.IsTrue(success && dt == new DateTime(2020, 2, 2));

            success = DateConvert.TryParseDate("2020020 13:55", out dt);
            Assert.IsFalse(success);

            success = DateConvert.TryParseDate("  20200202 13:55", out dt);
            Assert.IsTrue(success && dt == new DateTime(2020, 2, 2));

            success = DateConvert.TryParseDate(DateTime.Now.ToShortDateString(), out dt);
            Assert.IsTrue(success && dt == DateTime.Today);

            success = DateConvert.TryParseDate(DateTime.Now.ToString(), out dt);
            Assert.IsTrue(success && dt == DateTime.Today);
        }
    }
}
