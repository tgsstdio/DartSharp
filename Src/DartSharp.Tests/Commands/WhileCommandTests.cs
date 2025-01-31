﻿using DartSharp.Commands;
using DartSharp.Expressions;
using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class WhileCommandTests
    {
        [Test]
        public void WhileWithAddExpression()
        {
            var expression = new CompareExpression(ComparisonOperator.Less, new VariableExpression("a"), new ConstantExpression(10));
            var inccommand = new SetVariableCommand("k", new ArithmeticBinaryExpression(ArithmeticOperator.Add, new VariableExpression("k"), new ConstantExpression(1)));
            var addcommand = new SetVariableCommand("a", new ArithmeticBinaryExpression(ArithmeticOperator.Add, new VariableExpression("k"), new VariableExpression("a")));
            var command = new CompositeCommand(new ICommand[] { inccommand, addcommand });
            var whilecommand = new WhileCommand(expression, command);

            Context context = new Context();

            context.SetValue("a", 0);
            context.SetValue("k", 0);

            var result = whilecommand.Execute(context);

            Assert.IsNull(result);
            Assert.IsNotNull(whilecommand.Condition);
            Assert.IsNotNull(whilecommand.Command);
            Assert.AreEqual(4, context.GetValue("k"));
            Assert.AreEqual(10, context.GetValue("a"));
        }
    }
}
