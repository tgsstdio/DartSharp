namespace DartSharp.Commands
{

    using DartSharp.Expressions;

    public class ExpressionCommand : ICommand
    {
        private IExpression expression;

        public ExpressionCommand(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression Expression { get { return this.expression; } }

        public object Execute(Context context)
        {
            object value = this.expression.Evaluate(context);
            return value;
        }
    }
}
