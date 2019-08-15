using DartSharp.Commands;
using DartSharp.Expressions;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class ReturnCommandTests
    {
        [Test]
        public void ExecuteConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression(1);
            ReturnCommand command = new ReturnCommand(expr);
            Context context = new Context();
            Assert.AreEqual(1, command.Execute(context));
            Assert.IsNotNull(context.ReturnValue);
            Assert.AreEqual(1, context.ReturnValue.Value);
            Assert.AreEqual(expr, command.Expression);
        }
    }
}
