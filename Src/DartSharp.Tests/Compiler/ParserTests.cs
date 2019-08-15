using System.Collections.Generic;
using System.Linq;
using DartSharp.Compiler;
using DartSharp.Expressions;
using DartSharp.Commands;
using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests.Compiler
{
    public class ParserTests
    {
        [Test]
        public void ParseInteger()
        {
            Parser parser = new Parser("123");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.That(expr is ConstantExpression);

            ConstantExpression cexpr = (ConstantExpression)expr;

            Assert.AreEqual(123, cexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [Test]
        public void ParseSimpleString()
        {
            Parser parser = new Parser("\"foo\"");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.That(expr is ConstantExpression);

            ConstantExpression cexpr = (ConstantExpression)expr;

            Assert.AreEqual("foo", cexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [Test]
        public void ParseVariable()
        {
            Parser parser = new Parser("foo");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.That(expr is VariableExpression);

            VariableExpression vexpr = (VariableExpression)expr;

            Assert.AreEqual("foo", vexpr.Name);

            Assert.IsNull(parser.ParseExpression());
        }

        [Test]
        public void ParseSimpleCall()
        {
            Parser parser = new Parser("prints(1)");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.That(expr is CallExpression);

            CallExpression cexpr = (CallExpression)expr;

            Assert.That(cexpr.Expression is VariableExpression);
            Assert.AreEqual(1, cexpr.Arguments.Count());
            Assert.That(cexpr.Arguments.First() is ConstantExpression);
        }

        [Test]
        public void RaiseIfMissingClosingParenthesis()
        {
            Parser parser = new Parser("prints(1");
            Assert.Throws<ParserException>(() => parser.ParseExpression());
        }

        [Test]
        public void ParseEqualsOperator()
        {
            Parser parser = new Parser("a == 1");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.That(expr is CompareExpression);

            CompareExpression cexpr = (CompareExpression)expr;

            Assert.AreEqual(ComparisonOperator.Equal, cexpr.Operation);
            Assert.That(cexpr.LeftExpression is VariableExpression);
            Assert.That(cexpr.RightExpression is ConstantExpression);

            Assert.IsNull(parser.ParseExpression());
        }

        [Test]
        public void ParseLessOperator()
        {
            Parser parser = new Parser("a < 1");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.That(expr is CompareExpression);

            CompareExpression cexpr = (CompareExpression)expr;

            Assert.AreEqual(ComparisonOperator.Less, cexpr.Operation);
            Assert.That(cexpr.LeftExpression is VariableExpression);
            Assert.That(cexpr.RightExpression is ConstantExpression);

            Assert.IsNull(parser.ParseExpression());
        }

        [Test]
        public void ParseSimpleCallWithParenthesisAndTwoArguments()
        {
            Parser parser = new Parser("myfunc(a, b)");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.That(expr is CallExpression);

            CallExpression cexpr = (CallExpression)expr;

            Assert.That(cexpr.Expression is VariableExpression);
            Assert.AreEqual(2, cexpr.Arguments.Count());
            Assert.That(cexpr.Arguments.First() is VariableExpression);
            Assert.That(cexpr.Arguments.Skip(1).First() is VariableExpression);
        }

        [Test]
        public void ParseSimpleCallAsCommand()
        {
            Parser parser = new Parser("prints(1);");
            ICommand cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.That(cmd is ExpressionCommand);

            ExpressionCommand ccmd = (ExpressionCommand) cmd;

            Assert.That(ccmd.Expression is CallExpression);
        }

        [Test]
        public void ParseSimpleCallAsCommandPrecededByNewLine()
        {
            Parser parser = new Parser("\r\nprints(1);");
            ICommand cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.That(cmd is ExpressionCommand);

            ExpressionCommand ccmd = (ExpressionCommand)cmd;

            Assert.That(ccmd.Expression is CallExpression);
        }

        [Test]
        public void ParseTwoSimpleCallsAsCommands()
        {
            Parser parser = new Parser("print(1);\r\nprint(2);\r\n");
            ICommand cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.That(cmd is ExpressionCommand);

            ExpressionCommand ccmd = (ExpressionCommand)cmd;

            Assert.That(ccmd.Expression is CallExpression);

            cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.That(cmd is ExpressionCommand);

            ccmd = (ExpressionCommand)cmd;

            Assert.That(ccmd.Expression is CallExpression);

            Assert.IsNull(parser.ParseCommand());
        }

        [Test]
        public void ParseSimpleAssignmentCommand()
        {
            Parser parser = new Parser("a=1;");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.That(command is SetVariableCommand);

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.That(scommand.Expression is ConstantExpression);

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [Test]
        public void RaiseIfSemicolonIsMissing()
        {
            Parser parser = new Parser("a=1");
            Assert.Throws<ParserException>(() => parser.ParseCommand());
        }

        [Test]
        public void RaiseIfBinaryOperator()
        {
            Parser parser = new Parser("==");
            Assert.Throws<ParserException>(() => parser.ParseExpression());
        }

        [Test]
        public void ParseTwoLineSimpleAssignmentCommands()
        {
            Parser parser = new Parser("a=1;\r\nb=1;");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.That(command is SetVariableCommand);

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.That(scommand.Expression is ConstantExpression);

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.That(command is SetVariableCommand);

            scommand = (SetVariableCommand)command;

            Assert.AreEqual("b", scommand.Name);
            Assert.That(scommand.Expression is ConstantExpression);

            cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [Test]
        public void ParseSeparatedSimpleAssignmentCommands()
        {
            Parser parser = new Parser("a=1;b=1;");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.That(command is SetVariableCommand);

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.That(scommand.Expression is ConstantExpression);

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.That(command is SetVariableCommand);

            scommand = (SetVariableCommand)command;

            Assert.AreEqual("b", scommand.Name);
            Assert.That(scommand.Expression is ConstantExpression);

            cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [Test]
        public void ParseTwoCommands()
        {
            Parser parser = new Parser("a=1;\r\nb=1;");
            IList<ICommand> commands = parser.ParseCommands();

            Assert.IsNotNull(commands);
            Assert.AreEqual(2, commands.Count);

            ICommand command = commands[0];

            Assert.IsNotNull(command);
            Assert.That(command is SetVariableCommand);

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.That(scommand.Expression is ConstantExpression);

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            command = commands[1];

            Assert.IsNotNull(command);
            Assert.That(command is SetVariableCommand);

            scommand = (SetVariableCommand)command;

            Assert.AreEqual("b", scommand.Name);
            Assert.That(scommand.Expression is ConstantExpression);

            cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [Test]
        public void ParseNoCommands()
        {
            Parser parser = new Parser("");
            Assert.IsNull(parser.ParseCommands());
        }

        [Test]
        public void ParseClosingBraceAsNoCommand()
        {
            Parser parser = new Parser("}");
            Assert.IsNull(parser.ParseCommands());
        }

        [Test]
        public void ParseClosingParenthesisAsNoExpression()
        {
            Parser parser = new Parser(")");
            Assert.IsNull(parser.ParseExpression());
        }

        [Test]
        public void ParseEmptyCommand()
        {
            Parser parser = new Parser(";");
            Assert.AreEqual(NullCommand.Instance, parser.ParseCommand());
        }

        [Test]
        public void ParseDefineVariable()
        {
            Parser parser = new Parser("var a;");
            var result = parser.ParseCommand();
            Assert.IsNotNull(result);
            Assert.That(result is DefineVariableCommand);

            DefineVariableCommand dvcmd = (DefineVariableCommand)result;
            Assert.AreEqual("a", dvcmd.Name);
            Assert.IsNull(dvcmd.Expression);
        }

        [Test]
        public void ParseDefineVariableWithValue()
        {
            Parser parser = new Parser("var a = 1;");
            var result = parser.ParseCommand();
            Assert.IsNotNull(result);
            Assert.That(result is DefineVariableCommand);

            DefineVariableCommand dvcmd = (DefineVariableCommand)result;
            Assert.AreEqual("a", dvcmd.Name);
            Assert.IsNotNull(dvcmd.Expression);
            Assert.That(dvcmd.Expression is ConstantExpression);
        }

        [Test]
        public void RaiseNameExpected()
        {
            Parser parser = new Parser("var 1;");
            Assert.Throws<ParserException>(()=> parser.ParseCommand());
        }

        [Test]
        public void ParseCompositeCommand()
        {
            Parser parser = new Parser("{ a = 1; b = 2; }");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is CompositeCommand);

            CompositeCommand command = (CompositeCommand)result;

            Assert.AreEqual(2, command.Commands.Count());
        }

        [Test]
        public void ParseVoidFunctionDefinition()
        {
            Parser parser = new Parser("void myfun() { a = 1; b = 2; }");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is DefineFunctionCommand);

            DefineFunctionCommand command = (DefineFunctionCommand)result;

            Assert.AreEqual("myfun", command.Name);
            Assert.That(command.Command is CompositeCommand);
        }

        [Test]
        public void ParseIntFunctionDefinition()
        {
            Parser parser = new Parser("int myfun() { a = 1; b = 2; }");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is DefineFunctionCommand);

            DefineFunctionCommand command = (DefineFunctionCommand)result;

            Assert.AreEqual("myfun", command.Name);
            Assert.That(command.Command is CompositeCommand);
        }

        [Test]
        public void ParseIntFunctionDefinitionWithParameter()
        {
            Parser parser = new Parser("int myfun(int a) { return a+1; }");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is DefineFunctionCommand);

            DefineFunctionCommand command = (DefineFunctionCommand)result;

            Assert.AreEqual("myfun", command.Name);
            Assert.IsNotNull(command.ArgumentNames);
            Assert.AreEqual(1, command.ArgumentNames.Count());
            Assert.That(command.Command is CompositeCommand);
        }

        [Test]
        public void ParseIntFunctionDefinitionWithTwoParameters()
        {
            Parser parser = new Parser("int myfun(int a, int b) { return a+b; }");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is DefineFunctionCommand);

            DefineFunctionCommand command = (DefineFunctionCommand)result;

            Assert.AreEqual("myfun", command.Name);
            Assert.IsNotNull(command.ArgumentNames);
            Assert.AreEqual(2, command.ArgumentNames.Count());
            Assert.That(command.Command is CompositeCommand);
        }

        [Test]
        public void RaiseIfNoTypeInArgument()
        {
            Parser parser = new Parser("int myfun(a, b) { return a+b; }");
            Assert.Throws<ParserException>(() => parser.ParseCommand());
        }

        [Test]
        public void RaiseIfNoCommaToSeparateArguments()
        {
            Parser parser = new Parser("int myfun(int a int b) { return a+b; }");
            Assert.Throws<ParserException>(() => parser.ParseCommand());
        }

        [Test]
        public void RaiseIfMissingParenthesis()
        {
            Parser parser = new Parser("int myfun( { return a+b; }");
            Assert.Throws<ParserException>(() => parser.ParseCommand());
        }

        [Test]
        public void RaiseIfEndOfInput()
        {
            Parser parser = new Parser("int myfun(");
            Assert.Throws<ParserException>(() => parser.ParseCommand());
        }

        [Test]
        public void ParseMainFunctionDefinition()
        {
            Parser parser = new Parser("main() { a = 1; b = 2; }");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is DefineFunctionCommand);

            DefineFunctionCommand command = (DefineFunctionCommand)result;

            Assert.AreEqual("main", command.Name);
            Assert.That(command.Command is CompositeCommand);
        }

        [Test]
        public void ParseSimpleIfCommand()
        {
            Parser parser = new Parser("if (a) \r\n b = 2;");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is IfCommand);

            IfCommand command = (IfCommand)result;

            Assert.IsNotNull(command.Condition);
            Assert.IsNotNull(command.ThenCommand);
            Assert.IsNull(command.ElseCommand);
        }

        [Test]
        public void ParseSimpleWhileCommand()
        {
            Parser parser = new Parser("while (a) \r\n b = 2;");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is WhileCommand);

            WhileCommand command = (WhileCommand)result;

            Assert.IsNotNull(command.Condition);
            Assert.IsNotNull(command.Command);
        }

        [Test]
        public void ParseNullAsConstant()
        {
            Parser parser = new Parser("null");
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.That(result is ConstantExpression);

            ConstantExpression expression = (ConstantExpression)result;

            Assert.IsNull(expression.Value);
        }

        [Test]
        public void ParseFalseAsConstant()
        {
            Parser parser = new Parser("false");
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.That(result is ConstantExpression);

            ConstantExpression expression = (ConstantExpression)result;

            Assert.AreEqual(false, expression.Value);
        }

        [Test]
        public void ParseTrueAsConstant()
        {
            Parser parser = new Parser("true");
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.That(result is ConstantExpression);

            ConstantExpression expression = (ConstantExpression)result;

            Assert.AreEqual(true, expression.Value);
        }

        [Test]
        public void ParseSimpleSum()
        {
            Parser parser = new Parser("1+2");
            var result = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());

            Assert.IsNotNull(result);
            Assert.That(result is ArithmeticBinaryExpression);

            ArithmeticBinaryExpression expression = (ArithmeticBinaryExpression)result;

            Assert.That(expression.LeftExpression is ConstantExpression);
            Assert.That(expression.RightExpression is ConstantExpression);
        }

        [Test]
        public void ParseDotExpression()
        {
            Parser parser = new Parser("a.length");
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.That(result is DotExpression);

            DotExpression expression = (DotExpression)result;

            Assert.AreEqual("length", expression.Name);
            Assert.IsNull(expression.Arguments);
            Assert.That(expression.Expression is VariableExpression);
        }

        [Test]
        public void ParseDotExpressionWithArguments()
        {
            Parser parser = new Parser("a.slice(1)");
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.That(result is DotExpression);

            DotExpression expression = (DotExpression)result;

            Assert.AreEqual("slice", expression.Name);
            Assert.IsNotNull(expression.Arguments);
            Assert.AreEqual(1, expression.Arguments.Count());
            Assert.That(expression.Expression is VariableExpression);
        }

        [Test]
        public void ParseArrayExpression()
        {
            Parser parser = new Parser("[1,2,3]");
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.That(result is ArrayExpression);

            ArrayExpression expr = (ArrayExpression)result;
            Assert.IsNotNull(expr.Expressions);
            Assert.AreEqual(3, expr.Expressions.Count());
        }

        [Test]
        public void ParseDefineClassCommand()
        {
            Parser parser = new Parser("class MyClass { } ");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.That(result is DefineClassCommand);

            DefineClassCommand command = (DefineClassCommand)result;

            Assert.AreEqual("MyClass", command.Name);
            Assert.IsNull(command.Command);

            Assert.IsNull(parser.ParseCommand());
        }
    }
}
