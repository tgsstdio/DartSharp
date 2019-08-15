using DartSharp.Commands;
using NUnit.Framework;

namespace DartSharp.Tests.Commands
{
    public class NullCommandTests
    {
        [Test]
        public void ExecuteNullCommand()
        {
            Assert.IsNull(NullCommand.Instance.Execute(null));
        }
    }
}
