namespace DartSharp.Expressions
{
    public class ConstantExpression : IExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }

        public object Evaluate(Context context)
        {
            return this.value;
        }
    }
}
