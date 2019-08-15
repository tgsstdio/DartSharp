using System.Collections.Generic;
using DartSharp.Expressions;
using NUnit.Framework;

namespace DartSharp.Tests
{
    public class ExpressionsTests
    {
        [Test]
        public void EvaluateDotExpressionOnInteger()
        {
            IExpression expression = new DotExpression(new ConstantExpression(1), "ToString", new List<IExpression>());

            Assert.AreEqual("1", expression.Evaluate(null));
        }

        [Test]
        public void EvaluateDotExpressionOnString()
        {
            IExpression expression = new DotExpression(new ConstantExpression("foo"), "Length");

            Assert.AreEqual(3, expression.Evaluate(null));
        }

        [Test]
        public void EvaluateDotExpressionAsTypeInvocation()
        {
            DotExpression dot = new DotExpression(new DotExpression(new DotExpression(new VariableExpression("System"), "IO"), "File"), "Exists", new IExpression[] { new ConstantExpression("unknown.txt") });

            Assert.IsFalse((bool) dot.Evaluate(new Context()));
        }
    }
}
