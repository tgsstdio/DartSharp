using DartSharp.Commands;
using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class DefineClassCommandTests
    {
        [Test]
        public void CreateDefineClassCommand()
        {
            DefineClassCommand command = new DefineClassCommand("MyClass", null);
            Assert.AreEqual("MyClass", command.Name);
            Assert.IsNull(command.Command);
        }

        [Test]
        public void ExecuteDefineClassCommand()
        {
            Context context = new Context();
            DefineClassCommand command = new DefineClassCommand("MyClass", null);
            command.Execute(context);
            var result = context.GetValue("MyClass");
            Assert.IsNotNull(result);
            Assert.That(result is IClass);

            IClass klass = (IClass)result;
            Assert.AreEqual("MyClass", klass.Name);
        }
    }
}
