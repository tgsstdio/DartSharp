using System;
using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests.Language
{
    public class BaseObjectTests
    {
        private IClass type;

        [SetUp]
        public void Setup()
        {
            IClass type = new BaseClass("String", null);
            this.type = new BaseClass("MyClass", null);
            IMethod getname = new FuncMethod(null, (obj, context, arguments) => ((IObject)obj).GetValue("name"));
            this.type.DefineVariable("name", type);
            this.type.DefineMethod("getName", getname);
        }

        [Test]
        public void GetObjectType()
        {
            IObject obj = new BaseObject(this.type);
            Assert.AreEqual(this.type, obj.Type);
        }

        [Test]
        public void GetInstanceVariableAsNull()
        {
            IObject obj = new BaseObject(type);
            Assert.IsNull(obj.GetValue("name"));
        }

        [Test]
        public void RaiseIfClassIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BaseObject(null));
        }

        [Test]
        public void SetAndGetInstanceVariable()
        {
            IObject obj = new BaseObject(type);
            obj.SetValue("name", "Adam");
            Assert.AreEqual("Adam", obj.GetValue("name"));
        }

        [Test]
        public void RaiseWhenGetUndefinedVariable()
        {
            IObject obj = new BaseObject(type);
            Assert.Throws<InvalidOperationException>(() => obj.GetValue("length"));
        }

        [Test]
        public void RaiseWhenSetUndefinedVariable()
        {
            IObject obj = new BaseObject(type);
            Assert.Throws<InvalidOperationException>(() => obj.SetValue("length", 100));
        }

        [Test]
        public void InvokeGetName()
        {
            IObject obj = new BaseObject(type);
            obj.SetValue("name", "Adam");
            Assert.AreEqual("Adam", obj.Invoke("getName", null, null));
        }

        [Test]
        public void RaiseWhenInvokeUndefinedMethod()
        {
            IObject obj = new BaseObject(type);
            Assert.Throws<InvalidOperationException>(() => obj.Invoke("getLength", null, null));
        }
    }
}
