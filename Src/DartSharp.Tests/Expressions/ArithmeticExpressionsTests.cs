using DartSharp.Expressions;
using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests
{
    [TestFixture]
    public class ArithmeticExpressionsTests
    {
        [Test]
        public void CreateBinaryExpression()
        {
            IExpression leftExpression = new ConstantExpression(1);
            IExpression rightExpression = new ConstantExpression(2);
            BinaryExpression expression = new ArithmeticBinaryExpression(ArithmeticOperator.Add, leftExpression, rightExpression);

            Assert.IsTrue(expression.LeftExpression == leftExpression);
            Assert.IsTrue(expression.RightExpression == rightExpression);
        }

        [Test]
        public void CreateUnaryExpression()
        {
            IExpression valueExpression = new ConstantExpression(1);
            UnaryExpression expression = new ArithmeticUnaryExpression(ArithmeticOperator.Minus, valueExpression);

            Assert.IsTrue(expression.Expression == valueExpression);
        }

        [Test]
        public void EvaluateAddOperation()
        {
            Assert.AreEqual(2, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Add, 1, 1));
            Assert.AreEqual(2.4, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Add, 1.2, 1.2));
        }

        [Test]
        public void EvaluateSubtractOperation()
        {
            Assert.AreEqual(1, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Subtract, 2, 1));
            Assert.AreEqual(2.2, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Subtract, 3.4, 1.2));
        }

        [Test]
        public void EvaluateMultiplyOperation()
        {
            Assert.AreEqual(6, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Multiply, 2, 3));
            Assert.AreEqual(6.8, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Multiply, 3.4, 2));
        }

        [Test]
        public void EvaluateDivideOperation()
        {
            Assert.AreEqual(1.5, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Divide, 3, 2));
            Assert.AreEqual(1.7, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Divide, 3.4, 2));
        }

        [Test]
        public void EvaluateMinusOperation()
        {
            Assert.AreEqual(-1, EvaluateArithmeticUnaryOperator(ArithmeticOperator.Minus, 1));
            Assert.AreEqual(-1.7, EvaluateArithmeticUnaryOperator(ArithmeticOperator.Minus, 1.7));
        }

        [Test]
        public void EvaluatePlusOperation()
        {
            Assert.AreEqual(1, EvaluateArithmeticUnaryOperator(ArithmeticOperator.Plus, 1));
            Assert.AreEqual(-1.7, EvaluateArithmeticUnaryOperator(ArithmeticOperator.Plus, -1.7));
        }

        [Test]
        public void EvaluateModOperation()
        {
            Assert.AreEqual(1, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Modulo, 3, 2));
            Assert.AreEqual(0, EvaluateArithmeticBinaryOperator(ArithmeticOperator.Modulo, 6, 3));
        }

        private static object EvaluateArithmeticBinaryOperator(ArithmeticOperator operation, object left, object right)
        {
            IExpression expression = new ArithmeticBinaryExpression(operation, new ConstantExpression(left), new ConstantExpression(right));

            return expression.Evaluate(null);
        }

        private static object EvaluateArithmeticUnaryOperator(ArithmeticOperator operation, object value)
        {
            IExpression expression = new ArithmeticUnaryExpression(operation, new ConstantExpression(value));

            return expression.Evaluate(null);
        }
    }
}
