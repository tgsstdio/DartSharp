namespace DartSharp.Language
{
    public interface IMethod
    {
        IType Type { get; }

        object Call(object self, Context context, object[] arguments);
    }
}
