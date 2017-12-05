using System.Collections.Generic;
using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class DatabaseConnectionTests
	{
		[SetUp] // In visual studio this is [TestInitialize]
		public void TestSetUp()
		{
			string TestDBLocation = TestContext.CurrentContext.TestDirectory;
			Assert.True(DatabaseConnection.CopyAndSwitchToTestDatabase(TestDBLocation));
		}

		[Test]
		public void SaveSummaryFileMethods()
		{
			int nextId = DatabaseConnection.GenerateNextGameId();
			Assert.AreEqual(3, nextId);

			List<SaveGameSummary> currentSaves = DatabaseConnection.GetSaveGameSummaries();

			Assert.AreEqual(2, currentSaves.Count);
			Assert.AreEqual(CharacterClasses.FIGHTER, currentSaves[0].CharacterClass);
			Assert.AreEqual(1, currentSaves[0].DifficultySetting);
			Assert.IsTrue(currentSaves[0].UseAchievements);
			Assert.AreEqual(CharacterClasses.THIEF, currentSaves[1].CharacterClass);
			Assert.IsFalse(currentSaves[1].UseAchievements);

			SaveGameSummary newSaveGameSummary = new SaveGameSummary(nextId, 3, CharacterClasses.PALADIN,
																	 true, "Wall Market", true, 1200000000);

			DatabaseConnection.InsertNewSaveGameSummary(newSaveGameSummary);

			currentSaves = DatabaseConnection.GetSaveGameSummaries();

			Assert.AreEqual(3, currentSaves.Count);
			Assert.AreEqual(CharacterClasses.PALADIN, currentSaves[2].CharacterClass);
			Assert.AreEqual(3, currentSaves[2].DifficultySetting);

			nextId = DatabaseConnection.GenerateNextGameId();

			Assert.AreEqual(4, nextId);

		}

		[TearDown] // In visual studio this is [TestCleanUp]
		public void TestTearDown()
		{
			DatabaseConnection.RemoveTestDb();
		}

	}
}
