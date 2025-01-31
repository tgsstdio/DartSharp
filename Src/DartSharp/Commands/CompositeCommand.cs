﻿namespace DartSharp.Commands
{
    using System.Collections.Generic;

    public class CompositeCommand : ICommand
    {
        private IEnumerable<ICommand> commands;

        public CompositeCommand(IEnumerable<ICommand> commands)
        {
            this.commands = commands;
        }

        public IEnumerable<ICommand> Commands { get { return this.commands; } }

        public object Execute(Context context)
        {
            object result = null;

            foreach (var command in this.commands)
            {
                result = command.Execute(context);
                if (context.ReturnValue != null)
                    return context.ReturnValue.Value;
            }

            return result;
        }
    }
}
