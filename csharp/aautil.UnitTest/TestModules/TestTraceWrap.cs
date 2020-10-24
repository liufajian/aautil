using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aautil.UnitTest.TestModules
{
    [TestClass]
    public class TestTraceWrap
    {
        [TestMethod]
        public void Test()
        {
            var wrap = new aautil.TraceWrap("单元测试");
            wrap.WriteLine(null);
            wrap.WriteData(null, "测试数据");
        }
    }
}
