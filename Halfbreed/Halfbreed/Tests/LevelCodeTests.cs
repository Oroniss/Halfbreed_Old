using System;
using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class LevelCodeTests
	{
		private string testLevelMapFilePath = TestContext.CurrentContext.TestDirectory + "/LevelFiles/Testing/TestMap.txt";

		[Test]
		public void TestLevelMapConstruction()
		{
			// TODO: Put a test in that tries to load every level to make sure it parses.
			LevelMap testMap = new LevelMap(testLevelMapFilePath);

			Assert.AreEqual(25, testMap.Height);
			Assert.AreEqual(44, testMap.Width);

		}

		[Test]
		public void TestLevelMapIsValidMapCoords()
		{
			LevelMap testMap = new LevelMap(testLevelMapFilePath);

			Assert.IsTrue(testMap.IsValidMapCoord(5, 5));
			Assert.IsTrue(testMap.IsValidMapCoord(0, 0));
			Assert.IsTrue(testMap.IsValidMapCoord(43, 24));
			Assert.IsTrue(testMap.IsValidMapCoord(0, 24));
			Assert.IsTrue(testMap.IsValidMapCoord(43, 0));
			Assert.IsTrue(testMap.IsValidMapCoord(25, 15));

			Assert.IsFalse(testMap.IsValidMapCoord(-1, 0));
			Assert.IsFalse(testMap.IsValidMapCoord(0, -1));
			Assert.IsFalse(testMap.IsValidMapCoord(24, 43));
			Assert.IsFalse(testMap.IsValidMapCoord(50, 50));
			Assert.IsFalse(testMap.IsValidMapCoord(44, 5));
			Assert.IsFalse(testMap.IsValidMapCoord(-5, 25));


		}

		[Test]
		public void TestLevelMapGetTile()
		{
			LevelMap testMap = new LevelMap(testLevelMapFilePath);

			Assert.AreEqual(TileType.WOODWALL, testMap.GetTile(0, 0));
			Assert.AreEqual(TileType.WOODWALL, testMap.GetTile(43, 24));
			Assert.AreEqual(TileType.WOODFLOOR, testMap.GetTile(1, 1));
			Assert.AreEqual(TileType.WOODFLOOR, testMap.GetTile(42, 1));
			Assert.AreEqual(TileType.WOODENDEBRIS, testMap.GetTile(37, 8));
			Assert.AreEqual(TileType.WOODENDEBRIS, testMap.GetTile(35, 10));
		}

		[Test]
		public void TestRevealed()
		{
			LevelMap testMap = new LevelMap(testLevelMapFilePath);

			Assert.IsFalse(testMap.isRevealed(0, 0));
			Assert.IsFalse(testMap.isRevealed(15, 15));
			Assert.IsFalse(testMap.isRevealed(20, 20));
			Assert.IsFalse(testMap.isRevealed(15, 0));

			testMap.revealTile(0, 0);
			testMap.revealTile(10, 0);
			testMap.revealTile(0, 15);

			Assert.IsFalse(testMap.isRevealed(15, 0));
			Assert.IsFalse(testMap.isRevealed(0, 10));

			Assert.IsTrue(testMap.isRevealed(0, 0));
			Assert.IsTrue(testMap.isRevealed(10, 0));
			Assert.IsTrue(testMap.isRevealed(0, 15));

		}

		[Test]
		public void TestEntities()
		{
			LevelMap testMap = new LevelMap(testLevelMapFilePath);

			testMap.addEntity(15, 15, 0);
			testMap.addEntity(10, 20, 1);
			testMap.addEntity(15, 15, 2);

			Assert.AreEqual(testMap.getEntities(0, 0).Count, 0);
			Assert.AreEqual(testMap.getEntities(15, 15).Count, 2);
			Assert.IsTrue(testMap.getEntities(15, 15).Contains(2));
			Assert.IsFalse(testMap.getEntities(15, 15).Contains(1));
			Assert.AreEqual(testMap.getEntities(20, 10).Count, 0);
			Assert.IsTrue(testMap.getEntities(10, 20).Contains(1));

			testMap.removeEntity(15, 15, 0);

			Assert.AreEqual(testMap.getEntities(15, 15).Count, 1);
			Assert.IsTrue(testMap.getEntities(15, 15).Contains(2));
			Assert.IsFalse(testMap.getEntities(15, 15).Contains(0));
		}
	}
}
