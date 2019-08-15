using System;
using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests.Language
{
    public class BaseClassTests
    {
        [Test]
        public void ClassWithoutSuperclass()
        {
            IClass klass = new BaseClass("Object", null);
            Assert.AreEqual("Object", klass.Name);
            Assert.IsNull(klass.Super);
        }

        [Test]
        public void ClassWithSuperclass()
        {
            IClass super = new BaseClass("Object", null);
            IClass klass = new BaseClass("Rectangle", super);
            Assert.AreEqual("Rectangle", klass.Name);
            Assert.AreEqual(super, klass.Super);
        }

        [Test]
        public void UnknowVariableAsNullType()
        {
            IClass klass = new BaseClass("Object", null);
            Assert.IsNull(klass.GetVariableType("a"));
        }

        [Test]
        public void DefineVariable()
        {
            IClass type = new BaseClass("int", null);
            IClass klass = new BaseClass("MyClass", null);
            klass.DefineVariable("age", type);
            var result = klass.GetVariableType("age");
            Assert.IsNotNull(result);
            Assert.AreEqual(type, result);
        }

        [Test]
        public void DefineVariableAndGetVariableFromSuper()
        {
            IClass type = new BaseClass("int", null);
            IClass super = new BaseClass("MySuperclass", null);
            IClass klass = new BaseClass("MyClass", super);
            super.DefineVariable("age", type);
            var result = klass.GetVariableType("age");
            Assert.IsNotNull(result);
            Assert.AreEqual(type, result);
        }

        [Test]
        public void RaiseIfVariableIsAlreadyDefined()
        {
            IClass type = new BaseClass("int", null);
            IClass klass = new BaseClass("MyClass", null);
            klass.DefineVariable("age", type);
            Assert.Throws<InvalidOperationException>(() => klass.DefineVariable("age", type));
        }

        [Test]
        public void DefineMethod()
        {
            IClass type = new BaseClass("String", null);
            IClass klass = new BaseClass("MyClass", null);
            IMethod getname = new FuncMethod(type, (obj, context, arguments) => ((IObject)obj).GetValue("name"));
            klass.DefineMethod("getName", getname);
            var result = klass.GetMethod("getName");
            Assert.IsNotNull(result);
            Assert.AreEqual(type, result.Type);
        }

        [Test]
        public void DefineAndGetMethodFromSuper()
        {
            IClass type = new BaseClass("String", null);
            IClass super = new BaseClass("MySuperClass", null);
            IClass klass = new BaseClass("MyClass", super);
            IMethod getname = new FuncMethod(type, (obj, context, arguments) => ((IObject)obj).GetValue("name"));
            super.DefineMethod("getName", getname);
            var result = klass.GetMethod("getName");
            Assert.IsNotNull(result);
            Assert.AreEqual(type, result.Type);
        }

        [Test]
        public void RaiseIfMethodIsAlreadyDefined()
        {
            IClass type = new BaseClass("String", null);
            IClass klass = new BaseClass("MyClass", null);
            IMethod getname = new FuncMethod(type, (obj, context, arguments) => ((IObject)obj).GetValue("name"));
            klass.DefineMethod("getName", getname);
            Assert.Throws< InvalidOperationException>(() => klass.DefineMethod("getName", getname));
        }

        [Test]
        public void CreateNewInstance()
        {
            IClass klass = new BaseClass("MyClass", null);
            var result = klass.NewInstance(null);

            Assert.IsNotNull(result);
            Assert.That(result is BaseObject);

            BaseObject obj = (BaseObject)result;
            Assert.AreEqual(klass, obj.Type);
        }
    }
}
