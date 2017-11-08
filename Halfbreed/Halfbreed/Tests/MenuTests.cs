using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class MenuTests
	{

		[Test]
		public void TestCharacterCreationMenu()
		{
			string[] keys = new string[] { "3", "4", "1" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			CharacterCreationMenus.StartNewGame();
			Assert.AreEqual(3, GameParameters.DifficultySetting);
			Assert.AreEqual(CharacterClasses.NECROMANCER, GameParameters.CharacterClass);
			Assert.IsTrue(GameParameters.UseAchievements);

			keys = new string[] { "1", "4", "1" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			CharacterCreationMenus.StartNewGame();
			Assert.AreEqual(1, GameParameters.DifficultySetting);
			Assert.AreEqual(CharacterClasses.THIEF, GameParameters.CharacterClass);
			Assert.IsTrue(GameParameters.UseAchievements);

			keys = new string[] { "5", "1", "2" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			CharacterCreationMenus.StartNewGame();
			Assert.AreEqual(5, GameParameters.DifficultySetting);
			Assert.AreEqual(CharacterClasses.DRAGONLORD, GameParameters.CharacterClass);
			Assert.IsFalse(GameParameters.UseAchievements);

			keys = new string[] { "6", "4", "7", "1", "3", "2" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			CharacterCreationMenus.StartNewGame();
			Assert.AreEqual(4, GameParameters.DifficultySetting);
			Assert.AreEqual(CharacterClasses.BARD, GameParameters.CharacterClass);
			Assert.IsFalse(GameParameters.UseAchievements);


		}

	}
}
