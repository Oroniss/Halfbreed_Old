using NUnit.Framework;
using Halfbreed.Menus;
using System.Collections.Generic;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class MenuTests
	{
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
			// This one may need the extended test setup to create/copy configuration files.
		}

		[Test]
		public void TestMainMenu()
		{
			// This one likely will as well.
		}
	}
}
