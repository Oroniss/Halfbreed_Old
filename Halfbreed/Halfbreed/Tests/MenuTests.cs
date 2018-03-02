using NUnit.Framework;
using Halfbreed.Menus;
using System.IO;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class MenuTests
	{
		[SetUp]
		public void CreateWorkingFiles()
		{
			var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");
			if (!Directory.Exists(filePath))
				Directory.CreateDirectory(filePath);
			UserDataManager.SetTestHomeDirectory(filePath);
			UserDataManager.SetupDirectoriesAndFiles();
		}

		[Test]
		public void TestCharacterCreationMenu()
		{
			var menu = new CharacterCreationMenu();

			// Test regular functionality
			var keyBoardInput = new string[] { "1", "4", "1", "T", "E", "S", "T", "ENTER", "ENTER" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);
			var newGameParams = menu.StartNewGame();

			Assert.AreEqual(1, newGameParams.DifficultySetting);
			Assert.AreEqual(CharacterClasses.Thief, newGameParams.CharacterClass);
			Assert.IsTrue(newGameParams.UseAchievements);
			Assert.AreEqual("Test", newGameParams.CharacterNote);

			// Test another set of regular functionality
			keyBoardInput = new string[] { "3", "5", "2", "T", "E", "ENTER", "ENTER" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);
			newGameParams = menu.StartNewGame();

			Assert.AreEqual(3, newGameParams.DifficultySetting);
			Assert.AreEqual(CharacterClasses.Paladin, newGameParams.CharacterClass);
			Assert.IsFalse(newGameParams.UseAchievements);
			Assert.AreEqual("Te", newGameParams.CharacterNote);

			// Test with innapropriate inputs.
			keyBoardInput = new string[] { "6", "0", "2", "5", "1", "3", "1", "T", "E", "ENTER", "ENTER" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);
			newGameParams = menu.StartNewGame();

			Assert.AreEqual(2, newGameParams.DifficultySetting);
			Assert.AreEqual(CharacterClasses.Cleric, newGameParams.CharacterClass);
			Assert.IsTrue(newGameParams.UseAchievements);
			Assert.AreEqual("Te", newGameParams.CharacterNote);

			// Test exiting behaves correctly.
			UserInputHandler.clearAllInput();
			keyBoardInput = new string[] { "ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);
			newGameParams = menu.StartNewGame();
			Assert.IsNull(newGameParams);

			UserInputHandler.clearAllInput();
			keyBoardInput = new string[] { "1", "ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);
			newGameParams = menu.StartNewGame();
			Assert.IsNull(newGameParams);

			UserInputHandler.clearAllInput();
			keyBoardInput = new string[] {"3", "6", "ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);
			newGameParams = menu.StartNewGame();
			Assert.IsNull(newGameParams);

			UserInputHandler.clearAllInput();
			keyBoardInput = new string[] { "5", "1", "1", "H", "E", "L", "ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);
			newGameParams = menu.StartNewGame();
			Assert.IsNull(newGameParams);
		}

		[Test]
		public void TestLoadGameMenu()
		{
			// Create summary files
			var gameData1 = new GameData(1, CharacterClasses.Fighter, true, "", 0);
			var summary1 = new UserData.SaveGameSummary(gameData1, "TestLevel1", true, 100000000);
			UserDataManager.WriteSaveGameSummary(summary1);
			var gameData2 = new GameData(3, CharacterClasses.Bard, false, "Testing A Bard", 1);
			var summary2 = new UserData.SaveGameSummary(gameData2, "TestLevel2", true, 0);
			UserDataManager.WriteSaveGameSummary(summary2);

			// Test regular selection
			UserInputHandler.clearAllInput();
			var keyBoardInput = new string[] { "1"};
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);

			var menu = new LoadGameMenu();
			var gameID = menu.SelectSavedGame();
			Assert.AreEqual(0, gameID);

			// Test invalid selections
			UserInputHandler.clearAllInput();
			keyBoardInput = new string[] { "4", "LEFT", "2" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);

			gameID = menu.SelectSavedGame();
			Assert.AreEqual(1, gameID);

			// Test non-selection
			UserInputHandler.clearAllInput();
			keyBoardInput = new string[] { "ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);

			gameID = menu.SelectSavedGame();
			Assert.AreEqual(-1, gameID);

			// Delete a save game summary and check it still worked
			UserInputHandler.clearAllInput();
			keyBoardInput = new string[] { "1", "2", "ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyBoardInput);

			gameID = menu.DeleteSavedGame();
			UserDataManager.DeleteSaveGame(gameID);
			gameID = menu.SelectSavedGame();

			Assert.AreEqual(-1, gameID);
		}

		[TearDown]
		public void CleanupTestFiles()
		{
			var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");
			if (Directory.Exists(filePath))
				Directory.Delete(filePath, true);
		}
	}
}
