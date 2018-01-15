using System;

using NUnit.Framework;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class StaticDataTests
	{

		[SetUp]
		public void OpenTestDatabases()
		{
			StaticDatabaseConnection.SetupTestContext(TestContext.CurrentContext.TestDirectory);
			StaticDatabaseConnection.openDBConnection();
			StaticData.SetupDictionaries();
		}

		[Test]
		public void TestMaterialDictionary()
		{
			Assert.AreEqual(Enum.GetValues(typeof(Materials)).Length, StaticData.GetNumberOfMaterials());

			Assert.AreEqual(200, StaticData.GetProperties(Materials.COPPER).DefensiveStatTemplate.AcidResistance);
			Assert.AreEqual(400, StaticData.GetProperties(Materials.HESSION).DefensiveStatTemplate.LightResistance);
			Assert.AreEqual(10, StaticData.GetProperties(Materials.TIN).Hardness);
			Assert.AreEqual(Colors.TAN, StaticData.GetProperties(Materials.HESSION).FGColor);
		}

		public void TestMapTileDictionary()
		{
			Assert.AreEqual(Enum.GetValues(typeof(Levels.TileType)).Length, StaticData.GetNumberOfTiles());

			Assert.AreEqual(Colors.DARKWOODBROWN, StaticData.GetMapTileDetails(Levels.TileType.WOODWALL).BGColor);
			Assert.AreEqual(Colors.DARKBROWN, StaticData.GetMapTileDetails(Levels.TileType.WOODWALL).FogColor);
			Assert.AreEqual(20, StaticData.GetMapTileDetails(Levels.TileType.WOODENDEBRIS).Elevation);
			Assert.AreEqual(3, StaticData.GetMapTileDetails(Levels.TileType.PALLET).Elevation);
			Assert.AreEqual(0, StaticData.GetMapTileDetails(Levels.TileType.PLATFORM).MoveModes);
			Assert.IsTrue(StaticData.GetMapTileDetails(Levels.TileType.WOODFLOOR).AllowLOS);
			Assert.IsFalse(StaticData.GetMapTileDetails(Levels.TileType.WOODWALL).AllowLOS);
		}

		[TearDown]
		public void CloseTestDatabases()
		{
			StaticDatabaseConnection.closeDBConnection();
		}

	}
}
