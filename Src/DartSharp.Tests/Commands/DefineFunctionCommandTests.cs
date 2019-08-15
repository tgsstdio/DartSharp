using System.Collections.Generic;
using DartSharp.Commands;
using DartSharp.Expressions;
using DartSharp.Methods;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class DefineFunctionCommandTests
    {
        [Test]
        public void CreateAndExecuteDefineFunctionCommand()
        {
            IEnumerable<ICommand> commands = new ICommand[] {
                new SetVariableCommand("a", new ConstantExpression(1)),
                new SetVariableCommand("b", new ConstantExpression(2))
            };

            CompositeCommand body = new CompositeCommand(commands);
            DefineFunctionCommand command = new DefineFunctionCommand("foo", null, body);

            Context context = new Context();
            Assert.IsNull(command.Execute(context));
            Assert.AreEqual("foo", command.Name);
            Assert.IsNull(command.ArgumentNames);
            Assert.AreEqual(body, command.Command);

            var result = context.GetValue("foo");

            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(DefinedFunction)));

            DefinedFunction dfunc = (DefinedFunction)result;

            Assert.AreEqual(2, dfunc.Call(context, null));
        }
    }
}
