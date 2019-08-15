using DartSharp.Commands;
using DartSharp.Expressions;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class ExpressionCommandTests
    {
        [Test]
        public void ExecuteConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression(1);
            ExpressionCommand command = new ExpressionCommand(expr);
            Assert.AreEqual(1, command.Execute(null));
            Assert.AreEqual(expr, command.Expression);
        }
    }
}
