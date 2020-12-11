using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AAUtil.UnitTest.TestModules.BaseTests
{
    [TestClass]
    public class CompilerTest
    {
        [TestMethod]
        public void TestCallerName()
        {
            var name= CallerName();

            Assert.AreEqual(name, nameof(TestCallerName));

            var name2 = CallerName("111");

            Assert.AreEqual(name2, "111");
        }

        private string CallerName([System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            return callerName;
        }
    }
}
