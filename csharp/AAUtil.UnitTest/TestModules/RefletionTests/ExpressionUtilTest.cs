using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAUtil.UnitTest.TestModules.RefletionTests
{
    [TestClass]
    public class ExpressionUtilTest
    {
        [TestMethod]
        public void Test1()
        {
            var model = new InnerTestModel
            {
                AA = "测试",
                BB = 111,
                Model2 = new InnerTestModel2 { TTT = "hao" }
            };

            var aaGetter = ExpressionUtil.GetPropGetter<InnerTestModel, string>(nameof(InnerTestModel.AA));
            var aaSetter = ExpressionUtil.GetPropSetter<InnerTestModel, string>(nameof(InnerTestModel.AA));

            var bbGetter = ExpressionUtil.GetPropGetter<InnerTestModel, int>(nameof(InnerTestModel.BB));
            var bbSetter = ExpressionUtil.GetPropSetter<InnerTestModel, int>(nameof(InnerTestModel.BB));

            var aa1 = aaGetter(model);
            Assert.AreEqual(aa1, "测试");

            aaSetter(model, "测试2");
            var aa2 = aaGetter(model);
            Assert.AreEqual(aa2, "测试2");

            var bb1 = bbGetter(model);
            Assert.AreEqual(bb1, 111);

            bbSetter(model, 2222);
            var bb2 = bbGetter(model);
            Assert.AreEqual(bb2, 2222);

            var ccGetter = ExpressionUtil.GetPropGetter<InnerTestModel, object>(nameof(InnerTestModel.Model2));
            var ccSetter = ExpressionUtil.GetPropSetter<InnerTestModel>(nameof(InnerTestModel.Model2), typeof(InnerTestModel2));

            var cc1 = ccGetter(model);
            Assert.IsNotNull(cc1);
            ccSetter(model, null);
            Assert.IsNull(model.Model2);

            ccSetter(model, new InnerTestModel2 { TTT = "bbbbb" });
            Assert.IsNotNull(model.Model2);
        }

        [TestMethod]
        public void Test2()
        {
            var model = new InnerTestModel
            {
                AA = "测试",
                BB = 111,
                Model2 = new InnerTestModel2 { TTT = "hao" }
            };

            var aaGetter = ExpressionUtil.GetPropGetter<InnerTestModel, string>(t => t.AA);
            var aaSetter = ExpressionUtil.GetPropSetter<InnerTestModel, string>(t => t.AA);

            var bbGetter = ExpressionUtil.GetPropGetter<InnerTestModel, int>(t => t.BB);
            var bbSetter = ExpressionUtil.GetPropSetter<InnerTestModel, int>(t => t.BB);

            var aa1 = aaGetter(model);
            Assert.AreEqual(aa1, "测试");

            aaSetter(model, "测试2");
            var aa2 = aaGetter(model);
            Assert.AreEqual(aa2, "测试2");

            var bb1 = bbGetter(model);
            Assert.AreEqual(bb1, 111);

            bbSetter(model, 2222);
            var bb2 = bbGetter(model);
            Assert.AreEqual(bb2, 2222);

            var ccGetter = ExpressionUtil.GetPropGetter<InnerTestModel, object>(t => t.Model2);
            var ccSetter = ExpressionUtil.GetPropSetter<InnerTestModel>(t => t.Model2);

            var cc1 = ccGetter(model);
            Assert.IsNotNull(cc1);
            ccSetter(model, null);
            Assert.IsNull(model.Model2);

            ccSetter(model, new InnerTestModel2 { TTT = "bbbbb" });
            Assert.IsNotNull(model.Model2);
        }

        class InnerTestModel
        {
            public string AA { get; set; }

            public int BB { get; set; }

            public InnerTestModel2 Model2 { get; set; }

            public override string ToString()
            {
                return $"AA:{AA},BB:{BB},Model2:{Model2}";
            }
        }

        class InnerTestModel2
        {
            public string TTT { get; set; }

            public override string ToString()
            {
                return "TTT:" + TTT;
            }
        }
    }
}
