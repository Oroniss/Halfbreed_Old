using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class MenuTests
	{

		[Test]
		public void TestCharacterCreationMenu()
		{
			NewGameParameters ps = new NewGameParameters();
			ps.CharacterClass = CharacterClasses.CLERIC;
			Assert.AreEqual(ps.CharacterClass, CharacterClasses.CLERIC);

			UserInputHandler.clearAllInput();

			string[] keys = new string[] { "3", "4", "1" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			NewGameParameters parameters = CharacterCreationMenus.StartNewGame();
			GameEngine.SetStartingParameters(parameters);
			Assert.AreEqual(3, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.NECROMANCER, GameEngine.CharacterClass);
			Assert.IsTrue(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new string[] { "1", "4", "1" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = CharacterCreationMenus.StartNewGame();
			GameEngine.SetStartingParameters(parameters);
			Assert.AreEqual(1, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.THIEF, GameEngine.CharacterClass);
			Assert.IsTrue(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new string[] { "5", "1", "2" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = CharacterCreationMenus.StartNewGame();
			GameEngine.SetStartingParameters(parameters);
			Assert.AreEqual(5, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.DRAGONLORD, GameEngine.CharacterClass);
			Assert.IsFalse(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new string[] { "6", "4", "7", "1", "3", "2" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = CharacterCreationMenus.StartNewGame();
			GameEngine.SetStartingParameters(parameters);
			Assert.AreEqual(4, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.BARD, GameEngine.CharacterClass);
			Assert.IsFalse(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();
		}

	}
}
