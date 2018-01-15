using NUnit.Framework;
using RLNET;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class EnumConverterTests
	{

		[Test]
		public void TestKeyToString()
		{
			Assert.AreEqual("ESCAPE", EnumConverter.ConvertEnumToString(RLKey.Escape));
			Assert.AreEqual("LEFT", EnumConverter.ConvertEnumToString(RLKey.Left));
			Assert.AreEqual("RIGHT", EnumConverter.ConvertEnumToString(RLKey.Right));
		}

		[Test]
		public void TestStringToCharacterClass()
		{
			Assert.AreEqual(CharacterClasses.BARD,
			                EnumConverter.ConvertStringToCharacterClass("Bard"));
			Assert.AreEqual(CharacterClasses.PALADIN,
							EnumConverter.ConvertStringToCharacterClass("Paladin"));
			Assert.AreEqual(CharacterClasses.FIGHTER,
							EnumConverter.ConvertStringToCharacterClass("Fighter"));
			Assert.AreEqual(CharacterClasses.DRAGONLORD,
							EnumConverter.ConvertStringToCharacterClass("Dragonlord"));
			Assert.AreEqual(CharacterClasses.RANGER,
							EnumConverter.ConvertStringToCharacterClass("ranger"));
			Assert.AreEqual(CharacterClasses.DRUID,
							EnumConverter.ConvertStringToCharacterClass("druid"));
			Assert.AreEqual(CharacterClasses.THIEF,
							EnumConverter.ConvertStringToCharacterClass("thief"));
		}

		[Test]
		public void TestCharacterClassToString()
		{
			Assert.AreEqual("Bard", EnumConverter.ConvertEnumToString(CharacterClasses.BARD));
			Assert.AreEqual("Paladin", EnumConverter.ConvertEnumToString(CharacterClasses.PALADIN));
			Assert.AreEqual("Ranger", EnumConverter.ConvertEnumToString(CharacterClasses.RANGER));
			Assert.AreEqual("Mage", EnumConverter.ConvertEnumToString(CharacterClasses.MAGE));
		}
			}
}
