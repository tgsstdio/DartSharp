using DartSharp.Language;
using NUnit.Framework;

namespace DartSharp.Tests.Language
{
    public class PredicateTests
    {
        [Test]
        public void IsFalse()
        {
            Assert.IsTrue(Predicates.IsFalse(null));
            Assert.IsTrue(Predicates.IsFalse(false));
            Assert.IsFalse(Predicates.IsFalse(true));
            Assert.IsTrue(Predicates.IsFalse(string.Empty));
            Assert.IsTrue(Predicates.IsFalse(0));
            Assert.IsTrue(Predicates.IsFalse("foo"));
            Assert.IsTrue(Predicates.IsFalse(10));
        }

        [Test]
        public void IsTrue()
        {
            Assert.IsFalse(Predicates.IsTrue(null));
            Assert.IsFalse(Predicates.IsTrue(false));
            Assert.IsTrue(Predicates.IsTrue(true));
            Assert.IsFalse(Predicates.IsTrue(string.Empty));
            Assert.IsFalse(Predicates.IsTrue(0));
            Assert.IsFalse(Predicates.IsTrue("foo"));
            Assert.IsFalse(Predicates.IsTrue(10));
        }
    }
}
