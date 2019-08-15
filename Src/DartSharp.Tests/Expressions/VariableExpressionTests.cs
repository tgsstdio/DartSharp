using DartSharp.Expressions;
using NUnit.Framework;

namespace DartSharp.Tests.Expressions
{
    public class VariableExpressionTests
    {
        [Test]
        public void EvaluateUndefinedVariable()
        {
            Context context = new Context();
            VariableExpression expr = new VariableExpression("foo");

            Assert.IsNull(expr.Evaluate(context));
        }

        [Test]
        public void DefineVariableWithName()
        {
            Context context = new Context();
            VariableExpression expr = new VariableExpression("foo");

            Assert.AreEqual("foo", expr.Name);
        }

        [Test]
        public void EvaluateDefinedVariable()
        {
            Context context = new Context();
            context.SetValue("one", 1);
            VariableExpression expr = new VariableExpression("one");

            Assert.AreEqual(1, expr.Evaluate(context));
            Assert.AreEqual("one", expr.Name);
        }
    }
}
