using DartSharp.Expressions;
using NUnit.Framework;

namespace DartSharp.Tests.Expressions
{
    public class ConstantExpressionTests
    {
        [Test]
        public void CreateAndEvaluateInteger()
        {
            ConstantExpression expr = new ConstantExpression(1);

            Assert.AreEqual(1, expr.Evaluate(null));
            Assert.AreEqual(1, expr.Value);
        }
    }
}
