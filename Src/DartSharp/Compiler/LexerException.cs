namespace DartSharp.Compiler
{
    using System;

    public class LexerException : Exception
    {
        public LexerException(string msg)
            : base(msg)
        {
        }
    }
}
