using System.Collections.Generic;
using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class SaveDatabaseConnectionTests
	{
		[SetUp] // In visual studio this is [TestInitialize]
		public void TestSetUp()
		{
			string TestDBLocation = TestContext.CurrentContext.TestDirectory;
			Assert.True(UserDatabaseConnection.CopyAndSwitchToTestDatabase(TestDBLocation));
		}

		[Test]
		public void SaveSummaryFileMethods()
		{
			int nextId = UserDatabaseConnection.GenerateNextGameId();
			Assert.AreEqual(3, nextId);

			List<SaveGameSummary> currentSaves = UserDatabaseConnection.GetSaveGameSummaries();

			Assert.AreEqual(2, currentSaves.Count);
			Assert.AreEqual(CharacterClasses.FIGHTER, currentSaves[0].CharacterClass);
			Assert.AreEqual(1, currentSaves[0].DifficultySetting);
			Assert.IsTrue(currentSaves[0].UseAchievements);
			Assert.AreEqual(CharacterClasses.THIEF, currentSaves[1].CharacterClass);
			Assert.IsFalse(currentSaves[1].UseAchievements);

			SaveGameSummary newSaveGameSummary = new SaveGameSummary(nextId, 3, CharacterClasses.PALADIN,
																	 true, 1, 1, true, 1200000000);

			UserDatabaseConnection.InsertNewSaveGameSummary(newSaveGameSummary);

			currentSaves = UserDatabaseConnection.GetSaveGameSummaries();

			Assert.AreEqual(3, currentSaves.Count);
			Assert.AreEqual(CharacterClasses.PALADIN, currentSaves[2].CharacterClass);
			Assert.AreEqual(3, currentSaves[2].DifficultySetting);

			nextId = UserDatabaseConnection.GenerateNextGameId();

			Assert.AreEqual(4, nextId);

		}

		[Test]
		public void TestReadAndWriteSaveGame()
		{
			List<SaveGameSummary> currentSaves = UserDatabaseConnection.GetSaveGameSummaries();

			Assert.AreEqual(1, currentSaves[0].CurrentAct);
			Assert.AreEqual(1, currentSaves[0].CurrentChapter);
			Assert.IsTrue(currentSaves[0].StillAlive);
			Assert.AreEqual(1000000000, currentSaves[0].LastSaveTime);

			SaveGameSummary updatedSave = new SaveGameSummary(currentSaves[0].GameId,
															  currentSaves[0].DifficultySetting,
															  currentSaves[0].CharacterClass,
															  currentSaves[0].UseAchievements,
															  3, 5,
															  true,
															  1200000000);

			UserDatabaseConnection.WriteSaveGame(updatedSave, new object());

			currentSaves = UserDatabaseConnection.GetSaveGameSummaries();

			Assert.AreEqual(3, currentSaves[0].CurrentAct);
			Assert.AreEqual(5, currentSaves[0].CurrentChapter);
			Assert.IsTrue(currentSaves[0].StillAlive);
			Assert.AreEqual(1200000000, currentSaves[0].LastSaveTime);
		}

		[TearDown] // In visual studio this is [TestCleanUp]
		public void TestTearDown()
		{
			UserDatabaseConnection.RemoveTestDb();
		}

	}
}
