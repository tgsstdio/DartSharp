namespace DartSharp.Language
{
    public interface IObject
    {
        IType Type { get; }

        object GetValue(string name);

        void SetValue(string name, object value);

        object Invoke(string name, Context context, object[] parameters);
    }
}
