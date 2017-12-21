using NUnit.Framework;
using RLNET;

namespace Halfbreed
{
	[TestFixture]
	public class NameConverterTests
	{

		[Test]
		public void TestKeyToStringConverter()
		{
			Assert.IsFalse(NameConverter.KeyToStringConverter.checkKeyIsValid(RLKey.Tab));
			Assert.IsTrue(NameConverter.KeyToStringConverter.checkKeyIsValid(RLKey.Escape));

			Assert.AreEqual("ESCAPE", NameConverter.KeyToStringConverter.convertKeyToString(RLKey.Escape));
			Assert.AreEqual("LEFT", NameConverter.KeyToStringConverter.convertKeyToString(RLKey.Left));
			Assert.AreEqual("RIGHT", NameConverter.KeyToStringConverter.convertKeyToString(RLKey.Right));
		}

		[Test]
		public void TestCharacterClassToStringConverter()
		{
			Assert.AreEqual(CharacterClasses.BARD,
							NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass("Bard"));
			Assert.AreEqual(CharacterClasses.PALADIN,
							NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass("Paladin"));
			Assert.AreEqual(CharacterClasses.FIGHTER,
							NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass("Fighter"));
			Assert.AreEqual(CharacterClasses.DRAGONLORD,
							NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass("Dragonlord"));
			Assert.AreEqual(CharacterClasses.RANGER,
							NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass("ranger"));
			Assert.AreEqual(CharacterClasses.DRUID,
							NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass("druid"));
			Assert.AreEqual(CharacterClasses.THIEF,
							NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass("thief"));
		}

		[Test]
		public void TestStringToColorConverter()
		{
			// TODO: Add some more here
			Assert.AreEqual(Palette.BLACK, NameConverter.StringToColorConverter.ConvertStringToColor("Black"));
			Assert.AreEqual(Palette.WHITE, NameConverter.StringToColorConverter.ConvertStringToColor("White"));
			Assert.AreEqual(Palette.WOODBROWN, NameConverter.StringToColorConverter.ConvertStringToColor("Wood Brown"));
			Assert.AreEqual(Palette.DARKWOODBROWN, NameConverter.StringToColorConverter.ConvertStringToColor("Dark Wood Brown"));
		}

	}
}
