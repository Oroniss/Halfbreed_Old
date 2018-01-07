using System;
using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class LevelCodeTests
	{
		private string testLevelMapFilePath = TestContext.CurrentContext.TestDirectory + "/LevelFiles/Testing/TestLevel";

		[SetUp]
		public void SetupDatabaseConnection()
		{
			EntityDatabaseConnection.SetupTestContext(TestContext.CurrentContext.TestDirectory);
			EntityDatabaseConnection.openDBConnection();
			Entities.EntityData.SetupDictionaries();
		}

		[Test]
		public void TestLevelMapConstruction()
		{
			// TODO: Put a test in that tries to load every level to make sure it parses.
			Level testLevel = new Level(testLevelMapFilePath);

			Assert.AreEqual(25, testLevel.Height);
			Assert.AreEqual(44, testLevel.Width);

		}

		[Test]
		public void TestLevelMapIsValidMapCoords()
		{
			Level testLevel = new Level(testLevelMapFilePath);

			Assert.IsTrue(testLevel.IsValidMapCoord(5, 5));
			Assert.IsTrue(testLevel.IsValidMapCoord(0, 0));
			Assert.IsTrue(testLevel.IsValidMapCoord(43, 24));
			Assert.IsTrue(testLevel.IsValidMapCoord(0, 24));
			Assert.IsTrue(testLevel.IsValidMapCoord(43, 0));
			Assert.IsTrue(testLevel.IsValidMapCoord(25, 15));

			Assert.IsFalse(testLevel.IsValidMapCoord(-1, 0));
			Assert.IsFalse(testLevel.IsValidMapCoord(0, -1));
			Assert.IsFalse(testLevel.IsValidMapCoord(24, 43));
			Assert.IsFalse(testLevel.IsValidMapCoord(50, 50));
			Assert.IsFalse(testLevel.IsValidMapCoord(44, 5));
			Assert.IsFalse(testLevel.IsValidMapCoord(-5, 25));


		}

		[Test]
		public void TestLevelMapGetTile()
		{
			Level testLevel = new Level(testLevelMapFilePath);

			Assert.AreEqual(TileType.WOODWALL, testLevel.GetTile(0, 0));
			Assert.AreEqual(TileType.WOODWALL, testLevel.GetTile(43, 24));
			Assert.AreEqual(TileType.WOODFLOOR, testLevel.GetTile(1, 1));
			Assert.AreEqual(TileType.WOODFLOOR, testLevel.GetTile(42, 1));
			Assert.AreEqual(TileType.WOODENDEBRIS, testLevel.GetTile(37, 8));
			Assert.AreEqual(TileType.WOODENDEBRIS, testLevel.GetTile(35, 10));
		}

		[Test]
		public void TestRevealed()
		{
			Level testLevel = new Level(testLevelMapFilePath);

			Assert.IsFalse(testLevel.isRevealed(0, 0));
			Assert.IsFalse(testLevel.isRevealed(15, 15));
			Assert.IsFalse(testLevel.isRevealed(20, 20));
			Assert.IsFalse(testLevel.isRevealed(15, 0));

			testLevel.revealTile(0, 0);
			testLevel.revealTile(10, 0);
			testLevel.revealTile(0, 15);

			Assert.IsFalse(testLevel.isRevealed(15, 0));
			Assert.IsFalse(testLevel.isRevealed(0, 10));

			Assert.IsTrue(testLevel.isRevealed(0, 0));
			Assert.IsTrue(testLevel.isRevealed(10, 0));
			Assert.IsTrue(testLevel.isRevealed(0, 15));

		}

		[Test]
		public void TestFurnishings()
		{
			Level testLevel = new Level(testLevelMapFilePath);

			//Assert.AreEqual(testLevel.getEntities(0, 0).Count, 0);
			//Assert.AreEqual(testLevel.getEntities(15, 15).Count, 2);
			//Assert.IsTrue(testLevel.getEntities(15, 15).Contains(2));
			//Assert.IsFalse(testLevel.getEntities(15, 15).Contains(1));
			//Assert.AreEqual(testLevel.getEntities(20, 10).Count, 0);
			//Assert.IsTrue(testLevel.getEntities(10, 20).Contains(1));

			//testLevel.removeEntity(15, 15, 0);

			//Assert.AreEqual(testLevel.getEntities(15, 15).Count, 1);
			//Assert.IsTrue(testLevel.getEntities(15, 15).Contains(2));
			//Assert.IsFalse(testLevel.getEntities(15, 15).Contains(0));
		}

		[TearDown]
		public void CloseDBConnection()
		{
			EntityDatabaseConnection.closeDBConnection();
		}
	}
}
