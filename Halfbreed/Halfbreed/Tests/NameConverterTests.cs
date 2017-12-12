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
			Assert.IsFalse(KeyToStringConverter.checkKeyIsValid(RLKey.Tab));
			Assert.IsTrue(KeyToStringConverter.checkKeyIsValid(RLKey.Escape));

			Assert.AreEqual("ESCAPE", KeyToStringConverter.convertKeyToString(RLKey.Escape));
			Assert.AreEqual("LEFT", KeyToStringConverter.convertKeyToString(RLKey.Left));
			Assert.AreEqual("RIGHT", KeyToStringConverter.convertKeyToString(RLKey.Right));
		}

		[Test]
		public void TestCharacterClassToStringConverter()
		{
			Assert.AreEqual(CharacterClasses.BARD,
							CharacterClassToStringConverter.ConvertStringToCharacterClass("Bard"));
			Assert.AreEqual(CharacterClasses.PALADIN,
							CharacterClassToStringConverter.ConvertStringToCharacterClass("Paladin"));
			Assert.AreEqual(CharacterClasses.FIGHTER,
							CharacterClassToStringConverter.ConvertStringToCharacterClass("Fighter"));
			Assert.AreEqual(CharacterClasses.DRAGONLORD,
							CharacterClassToStringConverter.ConvertStringToCharacterClass("Dragonlord"));
			Assert.AreEqual(CharacterClasses.RANGER,
							CharacterClassToStringConverter.ConvertStringToCharacterClass("ranger"));
			Assert.AreEqual(CharacterClasses.DRUID,
							CharacterClassToStringConverter.ConvertStringToCharacterClass("druid"));
			Assert.AreEqual(CharacterClasses.THIEF,
							CharacterClassToStringConverter.ConvertStringToCharacterClass("thief"));
		}

		[Test]
		public void TestStringToColorConverter()
		{
			// TODO: Add some more here
			Assert.AreEqual(Palette.BLACK, StringToColorConverter.ConvertStringToColor("Black"));
			Assert.AreEqual(Palette.WHITE, StringToColorConverter.ConvertStringToColor("White"));
			Assert.AreEqual(Palette.WOODBROWN, StringToColorConverter.ConvertStringToColor("Wood Brown"));
			Assert.AreEqual(Palette.DARKWOODBROWN, StringToColorConverter.ConvertStringToColor("Dark Wood Brown"));
		}

	}
}
