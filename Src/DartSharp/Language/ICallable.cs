namespace DartSharp.Language
{
    using System.Collections.Generic;

    public interface ICallable
    {
        object Call(Context context, IList<object> arguments);
    }
}
