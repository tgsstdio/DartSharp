using System.Linq;
using DartSharp.Expressions;
using System.Collections;
using NUnit.Framework;

namespace DartSharp.Tests.Expressions
{
    public class ArrayExpressionTests
    {
        [Test]
        public void EvaluateArrayExpression()
        {
            ArrayExpression expr = new ArrayExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) });

            Assert.IsNotNull(expr.Expressions);
            Assert.AreEqual(3, expr.Expressions.Count());

            object result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.That(result is IList);

            IList list = (IList)result;
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }
    }
}
