using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AAUtil.UnitTest.TestModules
{
    [TestClass]
    public class TestTraceWrap
    {
        [TestMethod]
        public void Test()
        {
            var wrap = new AAUtil.TraceWrap("单元测试");
            wrap.WriteLine(null);
            wrap.WriteData(null, "测试数据");
        }
    }
}
