using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace AAUtil.UnitTest.TestModules.RefletionTests
{
    [TestClass]
    public class TypeUtilTest
    {
        protected interface IFooInterface
        {
        }

        protected interface IGenericFooInterface<T>
        {
        }

        protected class FooBase
        {
        }

        protected class FooImplementor
            : FooBase, IFooInterface
        {
        }

        protected class GenericFooBase
            : FooImplementor, IGenericFooInterface<object>
        {

        }

        protected class GenericFooImplementor<T>
            : FooImplementor, IGenericFooInterface<T>
        {
        }


        [TestMethod]
        public void Should_inherit_or_implement_non_generic_interface()
        {
            Assert.IsTrue(typeof(FooImplementor).InheritsOrImplements(typeof(IFooInterface)));

            Assert.IsTrue(typeof(GenericFooBase).InheritsOrImplements(typeof(IGenericFooInterface<>)));

            Assert.IsTrue(typeof(GenericFooImplementor<>).InheritsOrImplements(typeof(IGenericFooInterface<>)));

            Assert.IsTrue(new GenericFooImplementor<string>().GetType().InheritsOrImplements(typeof(IGenericFooInterface<>)));

            Assert.IsTrue(new GenericFooImplementor<string>().GetType().InheritsOrImplements(typeof(GenericFooImplementor<>)));

            Assert.IsFalse(new GenericFooImplementor<string>().GetType().InheritsOrImplements(typeof(IGenericFooInterface<int>)));

            Assert.IsTrue(typeof(FooImplementor).InheritsOrImplements(typeof(FooBase)));

            Assert.IsTrue(typeof(GenericFooImplementor<>).InheritsOrImplements(typeof(FooBase)));

            Assert.IsFalse(typeof(FooImplementor).InheritsOrImplements(typeof(GenericFooImplementor<>)));
        }
    }
}
