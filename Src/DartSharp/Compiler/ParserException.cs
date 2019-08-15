namespace DartSharp.Compiler
{
    using System;

    public class ParserException : Exception
    {
        public ParserException(string msg)
            : base(msg)
        {
        }
    }
}
