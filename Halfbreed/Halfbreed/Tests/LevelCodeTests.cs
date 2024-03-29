using NUnit.Framework;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class LevelCodeTests
	{
		private string _testContext = TestContext.CurrentContext.TestDirectory;

		[SetUp]
		public void SetupDatabaseConnection()
		{
			Level.SetToTestDirectory(_testContext);
			StaticDatabaseConnection.SetupTestContext(TestContext.CurrentContext.TestDirectory);
			StaticDatabaseConnection.openDBConnection();
			StaticData.SetupDictionaries();
		}

		[Test]
		public void TestLevelMapConstruction()
		{
			// TODO: Put a test in that tries to load every level to make sure it parses.
			Level testLevel = new Level(Levels.LevelEnum.TESTLEVEL1);

			Assert.AreEqual(25, testLevel.Height);
			Assert.AreEqual(44, testLevel.Width);

		}

		[Test]
		public void TestLevelMapIsValidMapCoords()
		{
			Level testLevel = new Level(Levels.LevelEnum.TESTLEVEL1);

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
			Level testLevel = new Level(Levels.LevelEnum.TESTLEVEL1);

			Assert.AreEqual(Colors.DarkWoodBrown, testLevel.GetBGColor(0, 0));
			Assert.AreEqual(Colors.DarkWoodBrown, testLevel.GetBGColor(43, 24));
			Assert.AreEqual(Colors.WoodBrown, testLevel.GetBGColor(1, 1));
			Assert.AreEqual(Colors.RedBrown, testLevel.GetBGColor(42, 1)); // Pallet is here.
			Assert.AreEqual(Colors.DarkBrown, testLevel.GetBGColor(37, 8)); // Debris
			Assert.AreEqual(Colors.DarkBrown, testLevel.GetBGColor(35, 10)); // Debris
		}

		[Test]
		public void TestRevealed()
		{
			Level testLevel = new Level(Levels.LevelEnum.TESTLEVEL1);

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
		public void TestDrawingEntities()
		{
			Level testLevel = new Level(Levels.LevelEnum.TESTLEVEL1);

			Assert.IsTrue(testLevel.HasDrawingEntity(42, 19));
			Assert.AreEqual('.', testLevel.GetDrawingEntity(42, 19).Symbol);
			Assert.AreEqual(Colors.DarkWoodBrown, testLevel.GetDrawingEntity(42, 19).FGColor); // Pallet
			Assert.IsTrue(testLevel.HasDrawingEntity(24, 16));
			Assert.AreEqual('#', testLevel.GetDrawingEntity(24, 16).Symbol);
			Assert.AreEqual(Colors.DarkWoodBrown, testLevel.GetDrawingEntity(24, 16).FGColor); // Chest

			Assert.IsFalse(testLevel.HasDrawingEntity(4, 20));
			Assert.IsFalse(testLevel.HasDrawingEntity(41, 5));
		}

		[Test]
		public void TestPassible()
		{
			Level testLevel = new Level(Levels.LevelEnum.TESTLEVEL1);

			Assert.IsTrue(testLevel.IsPassible(1, 1));
			Assert.IsTrue(testLevel.IsPassible(42, 1));
			Assert.IsTrue(testLevel.IsPassible(42, 23));
			Assert.IsFalse(testLevel.IsPassible(1, 23));
			Assert.IsTrue(testLevel.IsSwimmable(36, 4));
			Assert.IsFalse(testLevel.IsWalkable(36, 4));
			Assert.IsFalse(testLevel.IsSwimmable(35, 4));
			Assert.IsTrue(testLevel.IsFlyable(36, 4));
		}

		[Test]
		public void TestGetEntitiesWithComponent()
		{
			Level testLevel = new Level(Levels.LevelEnum.TESTLEVEL1);

			Assert.AreEqual(2, testLevel.GetEntitiesWithComponent(16, 10, Entities.ComponentType.INTERACTIBLE).Count);
			Assert.AreEqual(0, testLevel.GetEntitiesWithComponent(16, 10, Entities.ComponentType.MOVEON).Count);
			Assert.AreEqual(1, testLevel.GetEntitiesWithComponent(1, 1, Entities.ComponentType.INTERACTIBLE).Count);
		}

		[TearDown]
		public void CloseDBConnection()
		{
			StaticDatabaseConnection.closeDBConnection();
		}
	}
}
