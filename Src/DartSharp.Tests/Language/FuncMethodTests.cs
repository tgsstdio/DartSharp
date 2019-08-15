using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests.Language
{
    public class FuncMethodTests
    {
        [Test]
        public void DefineAndCallFuncMethod()
        {
            IMethod method = new FuncMethod(null, (obj, context, args) => ((string)obj).Length);
            Assert.IsNull(method.Type);
            Assert.AreEqual(3, method.Call("foo", null, null));
        }
    }
}
