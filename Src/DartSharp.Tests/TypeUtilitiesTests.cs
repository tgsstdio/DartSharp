namespace DartSharp.Tests
{
    using System;

    using DartSharp;
    using NUnit.Framework;

    public class TypeUtilitiesTests
    {
        [Test]
        public void GetTypeByName()
        {
            Type type = TypeUtilities.GetType("System.Int32");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(int));
        }

        [Test]
        public void GetTypeStoredInContext()
        {
            Context context = new Context();

            context.SetValue("int", typeof(int));

            Type type = TypeUtilities.GetType(context, "int");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(int));
        }

        [Test]
        public void GetTypeInAnotherAssembly()
        {
            Type type = TypeUtilities.GetType(new Context(), "System.Data.DataSet");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(System.Data.DataSet));
        }

        [Test]
        public void RaiseIfUnknownType()
        {
            Assert.Throws<InvalidOperationException>(()=> TypeUtilities.GetType(new Context(), "Foo.Bar"), "Unknown Type 'Foo.Bar'");
        }

        [Test]
        public void AsType()
        {
            Assert.IsNotNull(TypeUtilities.AsType("System.IO.File"));
            Assert.IsNull(TypeUtilities.AsType("Foo.Bar"));
        }

        [Test]
        public void IsNamespace()
        {
            Assert.IsTrue(TypeUtilities.IsNamespace("System"));
            Assert.IsTrue(TypeUtilities.IsNamespace("DartSharp"));
            Assert.IsTrue(TypeUtilities.IsNamespace("DartSharp.Language"));
            Assert.IsTrue(TypeUtilities.IsNamespace("System.IO"));
            Assert.IsTrue(TypeUtilities.IsNamespace("System.Data"));

            Assert.IsFalse(TypeUtilities.IsNamespace("Foo.Bar"));
        }

        [Test]
        public void GetValueFromType()
        {
            Assert.IsFalse((bool)TypeUtilities.InvokeTypeMember(typeof(System.IO.File), "Exists", new object[] { "unknown.txt" }));
        }

        [Test]
        public void GetValueFromEnum()
        {
            Assert.AreEqual(System.UriKind.RelativeOrAbsolute, TypeUtilities.InvokeTypeMember(typeof(System.UriKind), "RelativeOrAbsolute", null));
        }
    }
}
