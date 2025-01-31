﻿using NUnit.Framework;

namespace DartSharp.Tests
{
    public class ContextTests
    {
        [Test]
        public void GetUndefinedValue()
        {
            Context context = new Context();
            Assert.IsNull(context.GetValue("Foo"));
            Assert.IsNull(context.ReturnValue);
        }

        [Test]
        public void SetAndGetValue()
        {
            Context context = new Context();
            context.SetValue("One", 1);
            Assert.AreEqual(1, context.GetValue("One"));
        }

        [Test]
        public void SetAndGetValueWithParent()
        {
            Context parent = new Context();
            Context context = new Context(parent);
            context.SetValue("One", 1);
            Assert.AreEqual(1, context.GetValue("One"));
            Assert.IsNull(parent.GetValue("One"));
        }

        [Test]
        public void GetValueFromParent()
        {
            Context parent = new Context();
            Context context = new Context(parent);
            parent.SetValue("One", 1);
            Assert.AreEqual(1, context.GetValue("One"));
        }

        [Test]
        public void DefineAndHasVariable()
        {
            Context context = new Context();
            Assert.IsFalse(context.HasVariable("a"));
            context.DefineVariable("a");
            Assert.IsTrue(context.HasVariable("a"));
            Assert.IsNull(context.GetValue("a"));
        }

        [Test]
        public void DefineAndHasVariableInParent()
        {
            Context parent = new Context();
            Context context = new Context(parent);

            Assert.IsFalse(parent.HasVariable("a"));
            Assert.IsFalse(context.HasVariable("a"));
            parent.DefineVariable("a");
            Assert.IsTrue(parent.HasVariable("a"));
            Assert.IsTrue(context.HasVariable("a"));
            Assert.IsNull(context.GetValue("a"));
        }
    }
}

