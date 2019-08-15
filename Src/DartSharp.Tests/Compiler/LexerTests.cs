using DartSharp.Compiler;
using NUnit.Framework;

namespace DartSharp.Tests.Compiler
{
    public class LexerTests
    {
        [Test]
        public void GetName()
        {
            Lexer lexer = new Lexer("foo");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  foo   ");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetNameWithLineComment()
        {
            Lexer lexer = new Lexer("  foo  // Foo Variable ");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetNamesWithMultiLineComment()
        {
            Lexer lexer = new Lexer("  foo  /* Foo \r\n Variable */ bar");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
            
            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("bar", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void RaiseIfCommentIsNotClosed()
        {
            Lexer lexer = new Lexer("/* Comment wo/end ");
            Assert.Throws<LexerException>(() => lexer.NextToken());
        }

        [Test]
        public void RaiseIfCommentIsNotClosedByAMissingChar()
        {
            Lexer lexer = new Lexer("/* Comment wo/end *");
            Assert.Throws<LexerException>(() => lexer.NextToken());
        }

        [Test]
        public void GetAssignmentOperator()
        {
            Lexer lexer = new Lexer("=");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("=", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetDivideOperator()
        {
            Lexer lexer = new Lexer("/");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("/", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetEqualOperator()
        {
            Lexer lexer = new Lexer("==");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("==", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetPointSeparator()
        {
            Lexer lexer = new Lexer(".");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(".", token.Value);
            Assert.AreEqual(TokenType.Separator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("123", token.Value);
            Assert.AreEqual(TokenType.Integer, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetIntegerWithSpaces()
        {
            Lexer lexer = new Lexer("  123   ");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("123", token.Value);
            Assert.AreEqual(TokenType.Integer, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetSimpleString()
        {
            Lexer lexer = new Lexer("\"foo\"");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetSimpleStringSingleQuote()
        {
            Lexer lexer = new Lexer("'foo'");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void RaiseUnclosedString()
        {
            Lexer lexer = new Lexer("'foo");
            Assert.Throws<LexerException>(() => lexer.NextToken());
        }

        [Test]
        public void RaiseForUnknownOperator()
        {
            Lexer lexer = new Lexer("^");
            Assert.Throws<LexerException>(() => lexer.NextToken());
        }

        [Test]
        public void GetEndOfCommand()
        {
            Lexer lexer = new Lexer(";");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(";", token.Value);
            Assert.AreEqual(TokenType.Separator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetCommaAsSeparator()
        {
            Lexer lexer = new Lexer(",");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(",", token.Value);
            Assert.AreEqual(TokenType.Separator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetParenthesis()
        {
            Lexer lexer = new Lexer("()");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("(", token.Value);
            Assert.AreEqual(TokenType.Separator, token.Type);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(")", token.Value);
            Assert.AreEqual(TokenType.Separator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetSquareBrackets()
        {
            Lexer lexer = new Lexer("[]");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("[", token.Value);
            Assert.AreEqual(TokenType.Separator, token.Type);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("]", token.Value);
            Assert.AreEqual(TokenType.Separator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void SkipNewLine()
        {
            Lexer lexer = new Lexer("\n");
            Token token = lexer.NextToken();

            Assert.IsNull(token);
        }

        [Test]
        public void SkipCarriageReturnNewLine()
        {
            Lexer lexer = new Lexer("\r\n");
            Token token = lexer.NextToken();

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void SkipNewLineCarriageReturn()
        {
            Lexer lexer = new Lexer("\n\r");
            Token token = lexer.NextToken();

            Assert.IsNull(lexer.NextToken());
        }

        [Test]
        public void GetSimpleAssignmentCommand()
        {
            Lexer lexer = new Lexer("a=123");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("a", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("=", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("123", token.Value);
            Assert.AreEqual(TokenType.Integer, token.Type);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
