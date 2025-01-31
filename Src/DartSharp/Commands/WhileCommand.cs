﻿namespace DartSharp.Commands
{
    using DartSharp.Expressions;
    using DartSharp.Language;

    public class WhileCommand : ICommand
    {
        private IExpression condition;
        private ICommand command;

        public WhileCommand(IExpression condition, ICommand command)
        {
            this.condition = condition;
            this.command = command;
        }

        public IExpression Condition { get { return this.condition; } }

        public ICommand Command { get { return this.command; } }

        public object Execute(Context context)
        {
            while (Predicates.IsTrue(this.condition.Evaluate(context)))
                this.command.Execute(context);

            return null;
        }
    }
}
