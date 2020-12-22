using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AAUtil.UnitTest.TestModules.BaseTests
{
    [TestClass]
    public class CompilerTest
    {
        [TestMethod]
        public void TestCallerName()
        {
            var name = CallerName();

            Assert.AreEqual(name, nameof(TestCallerName));

            var name2 = CallerName("111");

            Assert.AreEqual(name2, "111");
        }

        private string CallerName([System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            return callerName;
        }

        [TestMethod]
        public void TestActionName()
        {
            Action a = TestAction;

            Assert.AreEqual(a.Method.Name, nameof(TestAction));

            Action a2 = () => TestAction2("ccc");

            Assert.AreEqual(a2.Method.Name, nameof(TestAction2));
        }

        private void TestAction()
        {
            Console.WriteLine(nameof(TestAction));
        }

        private void TestAction2(string input)
        {
            Console.WriteLine(input);
        }
    }
}
