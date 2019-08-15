using DartSharp.Compiler;
using DartSharp.Commands;
using DartSharp.Methods;
using System.IO;
using NUnit.Framework;

namespace DartSharp.Tests
{
    public class EvaluateTests
    {
        [Test]
        public void EvaluatePrintCommand()
        {
            Context context = new Context();
            StringWriter writer = new StringWriter();
            Print print = new Print(writer);
            context.SetValue("print", print);
            EvaluateCommands("print('Hello, world');", context);
            writer.Close();
            Assert.AreEqual("Hello, world\r\n", writer.ToString());
        }

        [Test]
        public void EvaluateSimpleIfCommand()
        {
            Context context = new Context();
            StringWriter writer = new StringWriter();
            Print print = new Print(writer);
            context.SetValue("print", print);
            context.SetValue("a", 0);
            EvaluateCommands("if (a == 0) print('Hello, world');\r\n", context);
            writer.Close();
            Assert.AreEqual("Hello, world\r\n", writer.ToString());
        }

        [Test]
        public void EvaluateSimpleReturn()
        {
            Context context = new Context();
            EvaluateCommands("return 0;", context);
            Assert.IsNotNull(context.ReturnValue);
            Assert.AreEqual(0, context.ReturnValue.Value);
        }

        [Test]
        public void EvaluateSimpleIfCommandWithElse()
        {
            Context context = new Context();
            StringWriter writer = new StringWriter();
            Print print = new Print(writer);
            context.SetValue("print", print);
            context.SetValue("a", 0);
            EvaluateCommands("if (a == 1) print('Hello'); else print('Hello, world');", context);
            writer.Close();
            Assert.AreEqual("Hello, world\r\n", writer.ToString());
        }

        [Test]
        public void EvaluateSimpleWhile()
        {
            Context context = new Context();
            context.SetValue("a", 0);
            EvaluateCommands("while (a < 10) a = a +1;", context);
            Assert.AreEqual(10, context.GetValue("a"));
        }

        [Test]
        public void EvaluateSimpleArithmeticExpressions()
        {
            Assert.AreEqual(2, EvaluateExpression("1+1", null));
            Assert.AreEqual(-1, EvaluateExpression("1-2", null));
            Assert.AreEqual(6, EvaluateExpression("2*3", null));
            Assert.AreEqual(3.0, EvaluateExpression("6/2", null));
            Assert.AreEqual(10, EvaluateExpression("(2+3)*2", null));
        }

        [Test]
        public void EvaluateStringConcatenation()
        {
            Assert.AreEqual("foobar", EvaluateExpression("'foo' + 'bar'", null));
            Assert.AreEqual("foo1", EvaluateExpression("'foo' + 1", null));
        }

        [Test]
        public void EvaluateNameInStringInterpolation()
        {
            Context context = new Context();
            context.SetValue("name", "World");
            Assert.AreEqual("Hello, World!", EvaluateExpression("'Hello, $name!'", context));
        }

        [Test]
        public void EvaluateNameInSimpleStringInterpolation()
        {
            Context context = new Context();
            context.SetValue("name", "World");
            Assert.AreEqual("World", EvaluateExpression("'$name'", context));
        }

        [Test]
        public void EvaluateExpressionInStringInterpolation()
        {
            Context context = new Context();
            context.SetValue("name", "World");
            Assert.AreEqual("Hello, WORLD!", EvaluateExpression("'Hello, ${name.ToUpper()}!'", context));
        }

        [Test]
        public void EvaluateNameAndExpressionInStringInterpolation()
        {
            Context context = new Context();
            context.SetValue("name", "World");
            Assert.AreEqual("Hello, World, WORLD!", EvaluateExpression("'Hello, $name, ${name.ToUpper()}!'", context));
        }

        [Test]
        public void RaiseIfUnclosedExpressionInInterpolation()
        {
            Assert.Throws<ParserException>(() => EvaluateExpression("'Hello, ${name.ToUpper()!'", null));
        }

        [Test]
        public void RaiseIfNoNameInterpolation()
        {
            Assert.Throws<ParserException>(() => EvaluateExpression("'Hello, $0!'", null));
        }

        [Test]
        public void RaiseIfNoInterpolation()
        {
            Assert.Throws<ParserException>(() => EvaluateExpression("'Hello, $'", null));
        }

        [Test]
        public void RaiseIfBadExpressionInInterpolation()
        {
            Assert.Throws<ParserException>(() => EvaluateExpression("'Hello, ${0+1 2+3}'", null));
        }

        [Test]
        public void EvaluateSimpleCompareExpressions()
        {
            Assert.AreEqual(true, EvaluateExpression("1==1", null));
            Assert.AreEqual(true, EvaluateExpression("1<2", null));
            Assert.AreEqual(false, EvaluateExpression("1<1", null));
            Assert.AreEqual(true, EvaluateExpression("1<=2", null));
            Assert.AreEqual(true, EvaluateExpression("1<=1", null));
            Assert.AreEqual(false, EvaluateExpression("1>=2", null));
            Assert.AreEqual(true, EvaluateExpression("1>=1", null));
        }

        [Test]
        public void EvaluateSimpleFunctionCall()
        {
            Context context = new Context();
            EvaluateCommands("int foo() { return 1; } a = foo();", context);
            Assert.AreEqual(1, context.GetValue("a"));
        }

        [Test]
        public void EvaluateSimpleFunctionCallWithArgument()
        {
            Context context = new Context();
            EvaluateCommands("int inc(int n) { return n+1; } a = inc(1);", context);
            Assert.AreEqual(2, context.GetValue("a"));
        }

        [Test]
        public void EvaluateSimpleDotExpressions()
        {
            Context context = new Context();
            Assert.AreEqual(3, EvaluateExpression("'foo'.Length", context));
            Assert.AreEqual("FOO", EvaluateExpression("'foo'.ToUpper()", context));
            Assert.AreEqual("oo", EvaluateExpression("'foo'.Substring(1)", context));
        }

        [Test]
        public void DefineVariables()
        {
            Context context = new Context();
            EvaluateCommands("var a; int b; double c; String d; bool e;", context);
            Assert.IsTrue(context.HasVariable("a"));
            Assert.IsTrue(context.HasVariable("b"));
            Assert.IsTrue(context.HasVariable("c"));
            Assert.IsTrue(context.HasVariable("d"));
            Assert.IsTrue(context.HasVariable("e"));
        }

        [Test]
        public void DefineTypedVariable()
        {
            Context context = new Context();
            EvaluateCommands("MyModule.MyClass a;", context);
            Assert.IsTrue(context.HasVariable("a"));
        }

        private static object EvaluateExpression(string text, Context context)
        {
            Parser parser = new Parser(text);

            var result = parser.ParseExpression();

            Assert.IsNull(parser.ParseExpression());

            return result.Evaluate(context);
        }

        private static void EvaluateCommands(string text, Context context)
        {
            Parser parser = new Parser(text);

            var result = parser.ParseCommands();

            var command = new CompositeCommand(result);
            command.Execute(context);
        }
    }
}
