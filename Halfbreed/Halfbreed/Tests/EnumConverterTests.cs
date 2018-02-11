using NUnit.Framework;
using RLNET;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class EnumConverterTests
	{

		[Test]
		public void TestStringToCharacterClass()
		{
			Assert.AreEqual(CharacterClasses.Bard,
			                EnumConverter.ConvertStringToCharacterClass("Bard"));
			Assert.AreEqual(CharacterClasses.Paladin,
							EnumConverter.ConvertStringToCharacterClass("Paladin"));
			Assert.AreEqual(CharacterClasses.Fighter,
							EnumConverter.ConvertStringToCharacterClass("Fighter"));
			Assert.AreEqual(CharacterClasses.Dragonlord,
							EnumConverter.ConvertStringToCharacterClass("Dragonlord"));
			Assert.AreEqual(CharacterClasses.Ranger,
							EnumConverter.ConvertStringToCharacterClass("ranger"));
			Assert.AreEqual(CharacterClasses.Druid,
							EnumConverter.ConvertStringToCharacterClass("druid"));
			Assert.AreEqual(CharacterClasses.Thief,
							EnumConverter.ConvertStringToCharacterClass("thief"));
		}

		[Test]
		public void TestCharacterClassToString()
		{
			Assert.AreEqual("Bard", EnumConverter.ConvertEnumToString(CharacterClasses.Bard));
			Assert.AreEqual("Paladin", EnumConverter.ConvertEnumToString(CharacterClasses.Paladin));
			Assert.AreEqual("Ranger", EnumConverter.ConvertEnumToString(CharacterClasses.Ranger));
			Assert.AreEqual("Mage", EnumConverter.ConvertEnumToString(CharacterClasses.Mage));
		}
			}
}
