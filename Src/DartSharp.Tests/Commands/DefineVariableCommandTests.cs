using DartSharp.Expressions;
using DartSharp.Commands;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class DefineVariableCommandTests
    {
        [Test]
        public void DefineVariable()
        {
            Context context = new Context();
            DefineVariableCommand expr = new DefineVariableCommand(null, "a");

            Assert.IsNull(expr.Execute(context));
            Assert.IsTrue(context.HasVariable("a"));
            Assert.IsNull(context.GetValue("a"));
            Assert.AreEqual("a", expr.Name);
            Assert.IsNull(expr.TypeExpression);
            Assert.IsNull(expr.Expression);
        }

        [Test]
        public void DefineVariableWithInitialValue()
        {
            Context context = new Context();
            DefineVariableCommand expr = new DefineVariableCommand(null, "a", new ConstantExpression(1));

            Assert.AreEqual(1, expr.Execute(context));
            Assert.IsTrue(context.HasVariable("a"));
            Assert.AreEqual(1, context.GetValue("a"));
            Assert.AreEqual("a", expr.Name);
            Assert.IsNull(expr.TypeExpression);
        }

        [Test]
        public void DefineVariableWithTypeAndInitialValue()
        {
            Context context = new Context();
            IExpression typeexpr = new VariableExpression("List");
            DefineVariableCommand expr = new DefineVariableCommand(typeexpr, "a", new ConstantExpression(1));

            Assert.AreEqual(1, expr.Execute(context));
            Assert.IsTrue(context.HasVariable("a"));
            Assert.AreEqual(1, context.GetValue("a"));
            Assert.AreEqual("a", expr.Name);
            Assert.AreEqual(typeexpr, expr.TypeExpression);
        }
    }
}
