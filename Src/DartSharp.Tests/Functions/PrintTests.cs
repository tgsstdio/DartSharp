using System;
using DartSharp.Methods;
using System.IO;
using NUnit.Framework;

namespace DartSharp.Tests.Methods
{
    public class PrintTests
    {
        [Test]
        public void WriteLine()
        {
            StringWriter writer = new StringWriter();
            Print print = new Print(writer);
            var result = print.Call(null, new object[] { "hello" });
            Assert.IsNull(result);
            writer.Close();
            Assert.AreEqual("hello\r\n", writer.ToString());
        }

        [Test]
        public void RaiseIfTwoArguments()
        {
            StringWriter writer = new StringWriter();
            Print print = new Print(writer);
            Assert.Throws<InvalidOperationException>(() => print.Call(null, new object[] { "hello", "world" }));
        }

        [Test]
        public void WriteEmptyLine()
        {
            StringWriter writer = new StringWriter();
            Print print = new Print(writer);
            var result = print.Call(null, null);
            Assert.IsNull(result);
            writer.Close();
            Assert.AreEqual("\r\n", writer.ToString());
        }
    }
}
