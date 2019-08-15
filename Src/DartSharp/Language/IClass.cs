namespace DartSharp.Language
{
    public interface IClass : IType
    {
        void DefineVariable(string name, IType type);

        void DefineMethod(string name, IMethod method);
    }
}
