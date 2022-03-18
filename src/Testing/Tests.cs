using System;
using NUnit.Framework;
using PathFinder;

namespace Testing
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual("Halo, Dunia","Halo, Dunia");
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual("Halo", Halo.halo());
        }
    }
}