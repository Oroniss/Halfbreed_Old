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

			Assert.AreEqual(10, StaticData.GetProperties(Materials.TIN).Hardness);
			Assert.AreEqual(Colors.Tan, StaticData.GetProperties(Materials.HESSION).FGColor);
		}

		public void TestMapTileDictionary()
		{
			Assert.AreEqual(Enum.GetValues(typeof(Levels.TileType)).Length, StaticData.GetNumberOfTiles());

			Assert.AreEqual(Colors.DarkWoodBrown, StaticData.GetMapTileDetails(Levels.TileType.WoodWall).BGColor);
			Assert.AreEqual(Colors.DarkBrown, StaticData.GetMapTileDetails(Levels.TileType.WoodWall).FogColor);
			Assert.AreEqual(20, StaticData.GetMapTileDetails(Levels.TileType.WoodenDebris).Elevation);
			Assert.AreEqual(3, StaticData.GetMapTileDetails(Levels.TileType.Pallet).Elevation);
			Assert.IsTrue(StaticData.GetMapTileDetails(Levels.TileType.Platform).Walkable);
			Assert.IsFalse(StaticData.GetMapTileDetails(Levels.TileType.WoodFloor).BlockLOS);
			Assert.IsTrue(StaticData.GetMapTileDetails(Levels.TileType.WoodWall).BlockLOS);
		}

		[TearDown]
		public void CloseTestDatabases()
		{
			StaticDatabaseConnection.closeDBConnection();
		}

	}
}
