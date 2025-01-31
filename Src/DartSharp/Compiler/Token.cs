﻿namespace DartSharp.Compiler
{
    public class Token
    {
        private string value;
        private TokenType type;

        public Token(string value, TokenType type)
        {
            this.value = value;
            this.type = type;
        }

        public string Value { get { return this.value; } }

        public TokenType Type { get { return this.type; } }
    }
}
