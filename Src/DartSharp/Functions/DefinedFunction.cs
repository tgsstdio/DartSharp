namespace DartSharp.Methods
{
    using DartSharp.Commands;
    using DartSharp.Language;
    using System.Collections.Generic;

    public class DefinedFunction : ICallable
    {
        private ICommand command;
        private IList<string> argnames;
        private int arity;

        public DefinedFunction(IList<string> argnames, ICommand command)
        {
            this.argnames = argnames;
            this.command = command;

            if (argnames != null)
                this.arity = argnames.Count;
        }

        public object Call(Context context, IList<object> arguments)
        {
            Context newctx = new Context(context);

            if (this.arity > 0)
                for (int k = 0; k < this.arity; k++)
                    newctx.SetValue(this.argnames[k], arguments[k]);

            return this.command.Execute(newctx);
        }
    }
}
