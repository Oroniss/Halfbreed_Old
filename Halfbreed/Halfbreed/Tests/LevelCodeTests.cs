using System;
using NUnit.Framework;
using Halfbreed.Entities;

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
		public void TestFurnishingSetup()
		{
			Level testLevel = new Level(testLevelMapFilePath);

			Assert.AreEqual("Pallet", testLevel.GetFurnishings(42, 19)[0].EntityName);
			// TODO: Think of a better way to do this.
		}

		[Test]
		public void TestDrawingEntities()
		{
			Level testLevel = new Level(testLevelMapFilePath);

			Assert.IsTrue(testLevel.HasEntity(42, 19));
			Assert.AreEqual('.', testLevel.GetDrawingEntity(42, 19).Symbol);
			Assert.AreEqual(Colors.TAN, testLevel.GetDrawingEntity(42, 19).FGColor);
			Assert.IsTrue(testLevel.HasEntity(24, 16));
			Assert.AreEqual('#', testLevel.GetDrawingEntity(24, 16).Symbol);
			Assert.AreEqual(Colors.REDBROWN, testLevel.GetDrawingEntity(24, 16).FGColor);

			Assert.IsFalse(testLevel.HasEntity(4, 20));
			Assert.IsFalse(testLevel.HasEntity(41, 5));

		}

		[TearDown]
		public void CloseDBConnection()
		{
			EntityDatabaseConnection.closeDBConnection();
		}
	}
}
