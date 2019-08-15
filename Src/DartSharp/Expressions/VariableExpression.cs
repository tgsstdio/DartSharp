namespace DartSharp.Expressions
{
    public class VariableExpression : IExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            object result = context.GetValue(this.name);

            return result;
        }
    }
}

