using NUnit.Framework;
using Halfbreed.Menus;
using RLNET;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class MenuTests
	{

		[SetUp] // In visual studio this is [TestInitialize]
		public void TestSetUp()
		{
			string TestDBLocation = TestContext.CurrentContext.TestDirectory;
			Assert.True(UserDatabaseConnection.CopyAndSwitchToTestDatabase(TestDBLocation));
		}

		[Test]
		public void TestCharacterCreationMenu()
		{
			NewGameParameters ps = new NewGameParameters();
			ps.CharacterClass = CharacterClasses.CLERIC;
			Assert.AreEqual(ps.CharacterClass, CharacterClasses.CLERIC);

			UserInputHandler.clearAllInput();

			RLKey[] keys = new RLKey[] { RLKey.Number3, RLKey.Number4, RLKey.Number1 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			NewGameParameters parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(3, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.NECROMANCER, GameEngine.CharacterClass);
			Assert.IsTrue(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new RLKey[] { RLKey.Number1, RLKey.Number4, RLKey.Number1 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(1, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.THIEF, GameEngine.CharacterClass);
			Assert.IsTrue(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new RLKey[] { RLKey.Number5, RLKey.Number1, RLKey.Number2 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(5, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.DRAGONLORD, GameEngine.CharacterClass);
			Assert.IsFalse(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();

			keys = new RLKey[] { RLKey.Number6, RLKey.Number4, RLKey.Number7, RLKey.Number1, RLKey.Number3, RLKey.Number2 };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
			GameEngine.SetupNewGame(parameters);
			Assert.AreEqual(4, GameEngine.DifficultySetting);
			Assert.AreEqual(CharacterClasses.BARD, GameEngine.CharacterClass);
			Assert.IsFalse(GameEngine.UseAchievements);

			UserInputHandler.clearAllInput();
		}

		[Test]
		public void TestLoadGameMenu()
		{
			// Need to swap the database context across.

			UserInputHandler.clearAllInput();

			KeyBoardInputSimulator.AddKeyBoardInput(RLKey.Number2);
			Assert.AreEqual(2, MenuProvider.LoadGameMenu.SelectSavedGame());

			UserInputHandler.clearAllInput();

			KeyBoardInputSimulator.AddKeyBoardInput(RLKey.Number1);
			Assert.AreEqual(1, MenuProvider.LoadGameMenu.SelectSavedGame());

		}

		[TearDown] // In visual studio this is [TestCleanUp]
		public void TestTearDown()
		{
			UserDatabaseConnection.RemoveTestDb();
		}
	}
}
