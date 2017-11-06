using System;
using RLNET;
using NUnit.Framework;

namespace Halfbreed
{
    [TestFixture]
    public class InputTests
    {
        private KeyToStringConverter converter = new KeyToStringConverter();

        [Test]
        public void TestCheckKeyIsValid()
        {
            Assert.IsFalse(converter.checkKeyIsValid(RLKey.Tab));
            Assert.IsTrue(converter.checkKeyIsValid(RLKey.Escape));
        }

        [Test]
        public void TestConvertKeyToString()
        {
            Assert.IsTrue(converter.convertKeyToString(RLKey.Escape) == "ESCAPE");
            Assert.IsTrue(converter.convertKeyToString(RLKey.Left) == "LEFT");
            Assert.IsTrue(converter.convertKeyToString(RLKey.Right) == "RIGHT");
        }
    }
}
