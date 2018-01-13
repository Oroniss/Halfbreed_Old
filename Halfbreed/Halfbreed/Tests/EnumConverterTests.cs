using NUnit.Framework;
using System;
using RLNET;

namespace Halfbreed
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

		[Test]
		public void TestStringToColor()
		{
			// TODO: Add some more here
			Assert.AreEqual(Colors.BLACK, EnumConverter.ConvertStringToColor("Black"));
			Assert.AreEqual(Colors.WHITE, EnumConverter.ConvertStringToColor("White"));
			Assert.AreEqual(Colors.WOODBROWN, EnumConverter.ConvertStringToColor("Wood Brown"));
			Assert.AreEqual(Colors.DARKWOODBROWN, EnumConverter.ConvertStringToColor("Dark Wood Brown"));

			Assert.AreEqual(Enum.GetValues(typeof(Colors)).Length, Converters.StringToColorConverter.NumberOfColors);

		}

		[Test]
		public void TestStringToMaterial()
		{
			Assert.AreEqual(Materials.COPPER, EnumConverter.ConvertStringToMaterial("Copper"));
			Assert.AreEqual(Materials.HESSION, EnumConverter.ConvertStringToMaterial("Hession"));
			Assert.AreNotEqual(Materials.FUR, EnumConverter.ConvertStringToMaterial("Tin"));

			Assert.AreEqual(Enum.GetValues(typeof(Materials)).Length, Converters.MaterialToStringConverter.NumberOfMaterials);
		}

		[Test]
		public void TestStringToTileType()
		{
			Assert.AreEqual(TileType.WOODFLOOR, EnumConverter.ConvertStringToTileType("Wood Floor"));
			Assert.AreEqual(TileType.WOODWALL, EnumConverter.ConvertStringToTileType("Wood Wall"));
			Assert.AreEqual(TileType.WOODENDEBRIS, EnumConverter.ConvertStringToTileType("Wooden Debris"));

			Assert.AreEqual(Enum.GetValues(typeof(TileType)).Length, Converters.StringToTileTypeConverter.GetNumberOfTiles());
		}

		[Test]
		public void TestStringToEntityTrait()
		{
			Assert.AreEqual(EntityTraits.CLOTH, EnumConverter.ConvertStringToTrait("Cloth"));
			Assert.AreEqual(EntityTraits.FURNISHING, EnumConverter.ConvertStringToTrait("Furnishing"));
			Assert.AreEqual(EntityTraits.RUINED, EnumConverter.ConvertStringToTrait("Ruined"));
			Assert.AreEqual(EntityTraits.IMMUNETOACID, EnumConverter.ConvertStringToTrait("Immune to Acid"));
			Assert.AreEqual(EntityTraits.IMMUNETOPOISON, EnumConverter.ConvertStringToTrait("Immune to Poison"));
			Assert.AreEqual(EntityTraits.STONE, EnumConverter.ConvertStringToTrait("Stone"));

			Assert.AreEqual(Enum.GetValues(typeof(EntityTraits)).Length, Converters.StringToTraitConverter.GetNumberOfTraits());
		}

	}
}
