namespace DartSharp.Expressions
{
    public interface IExpression
    {
        object Evaluate(DartSharp.Context context);
    }
}
