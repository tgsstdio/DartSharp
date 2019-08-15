using DartSharp.Expressions;
using DartSharp.Commands;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class SetVariableCommandTests
    {
        [Test]
        public void SetVariable()
        {
            Context context = new Context();
            ConstantExpression expr = new ConstantExpression(1);
            SetVariableCommand command = new SetVariableCommand("One", expr);

            object result = command.Execute(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, context.GetValue("One"));
            Assert.AreEqual(expr, command.Expression);
            Assert.AreEqual("One", command.Name);
        }
    }
}

