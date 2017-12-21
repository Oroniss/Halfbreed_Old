using NUnit.Framework;
using System;
using RLNET;

namespace Halfbreed
{
	[TestFixture]
	public class NameConverterTests
	{

		[Test]
		public void TestKeyToStringConverter()
		{
			Assert.AreEqual("ESCAPE", EnumConverter.ConvertEnumToString(RLKey.Escape));
			Assert.AreEqual("LEFT", EnumConverter.ConvertEnumToString(RLKey.Left));
			Assert.AreEqual("RIGHT", EnumConverter.ConvertEnumToString(RLKey.Right));
		}

		[Test]
		public void TestCharacterClassToStringConverter()
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
		public void TestStringToColorConverter()
		{
			// TODO: Add some more here
			Assert.AreEqual(Colors.BLACK, EnumConverter.ConvertStringToColor("Black"));
			Assert.AreEqual(Colors.WHITE, EnumConverter.ConvertStringToColor("White"));
			Assert.AreEqual(Colors.WOODBROWN, EnumConverter.ConvertStringToColor("Wood Brown"));
			Assert.AreEqual(Colors.DARKWOODBROWN, EnumConverter.ConvertStringToColor("Dark Wood Brown"));

		}

		[Test]
		public void CheckDictionaryConsistency()
		{
			// TODO: Write a function on the converter that validates everything has the right number of entries.
		}

	}
}
