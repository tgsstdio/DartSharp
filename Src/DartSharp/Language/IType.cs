namespace DartSharp.Language
{
    public interface IType
    {
        string Name { get; }

        IType Super { get; }

        IType GetVariableType(string name);

        IMethod GetMethod(string name);

        object NewInstance(object[] arguments);
    }
}
