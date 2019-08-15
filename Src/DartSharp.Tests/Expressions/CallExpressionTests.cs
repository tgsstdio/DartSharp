using System.Collections.Generic;
using DartSharp.Commands;
using DartSharp.Expressions;
using DartSharp.Methods;
using System.IO;
using NUnit.Framework;

namespace DartSharp.Tests.Expressions
{
    public class CallExpressionTests
    {
        [Test]
        public void ExecuteCallExpression()
        {
            StringWriter writer = new StringWriter();
            Print print = new Print(writer);
            Context context = new Context();
            IList<ICommand> commandlist = new List<ICommand>();

            CallExpression callexpr = new CallExpression(new ConstantExpression(print), new IExpression[] { new ConstantExpression("Hello, World") });

            object result = callexpr.Evaluate(context);

            Assert.IsNull(result);

            writer.Close();
            Assert.AreEqual("Hello, World\r\n", writer.ToString());
        }
    }
}
