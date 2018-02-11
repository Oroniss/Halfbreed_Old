using NUnit.Framework;
using Halfbreed.Menus;
using RLNET;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class MenuTests
	{

		[Test]
		public void TestCharacterCreationMenu()
		{
			NewGameParameters ps = new NewGameParameters();
			ps.CharacterClass = CharacterClasses.Cleric;
			Assert.AreEqual(ps.CharacterClass, CharacterClasses.Cleric);

			UserInputHandler.clearAllInput();

			RLKey[] keys = new RLKey[] { RLKey.Number3, RLKey.Number4, RLKey.Number1 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			NewGameParameters parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(3, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.Necromancer, GameEngine.CharacterClass);
			Assert.IsTrue(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new RLKey[] { RLKey.Number1, RLKey.Number4, RLKey.Number1 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(1, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.Thief, GameEngine.CharacterClass);
			Assert.IsTrue(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new RLKey[] { RLKey.Number5, RLKey.Number1, RLKey.Number2 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(5, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.Dragonlord, GameEngine.CharacterClass);
			Assert.IsFalse(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new RLKey[] { RLKey.Number6, RLKey.Number4, RLKey.Number7, RLKey.Number1, RLKey.Number3, RLKey.Number2 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(4, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.Bard, GameEngine.CharacterClass);
			Assert.IsFalse(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();
		}

		[Test]
		public void TestLoadGameMenu()
		{
			// Need to swap the database context across.

			UserInputHandler.clearAllInput();

			KeyBoardInputSimulator.AddKeyBoardInput(RLKey.Number2);
			//Assert.AreEqual(2, MenuProvider.LoadGameMenu.SelectSavedGame());

			UserInputHandler.clearAllInput();

			KeyBoardInputSimulator.AddKeyBoardInput(RLKey.Number1);
			//Assert.AreEqual(1, MenuProvider.LoadGameMenu.SelectSavedGame());

		}

	}
}
