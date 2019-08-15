namespace DartSharp.Methods
{
    using DartSharp.Language;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Print : ICallable
    {
        private TextWriter writer;

        public Print(TextWriter writer)
        {
            this.writer = writer;
        }

        public object Call(Context context, IList<object> arguments)
        {
            if (arguments == null || arguments.Count == 0)
                this.writer.WriteLine();
            else if (arguments.Count > 1)
                throw new InvalidOperationException("print accepts only one argument");
            else
                this.writer.WriteLine(arguments[0]);

            this.writer.Flush();

            return null;
        }
    }
}
