using System.Collections.Generic;

namespace Halfbreed.Menus
{
	public class CharacterCreationMenu
	{
		readonly List<string> _difficultySettings = new List<string>{
					"Thou art a craven knave, worthy only of contempt",
					"Thou art a pathetic weakling, unwilling to challenge thyself",
					"Thou art either brave or foolhardy, only time will tell which",
					"Thou art swimming far out of thy depth",
					"Thou canst not comprehend the horrors that await thee"};
		readonly List<string> _hl12classes = new List<string>{
					"Cleric", "Fighter", "Mage", "Thief"};
		readonly List<string> _hl34classes = new List<string>{
					"Bard", "Blackguard", "Druid", "Necromancer", "Paladin", "Ranger"};
		readonly List<string> _hl5classes = new List<string> { "Dragonlord" };


		public GameData StartNewGame()
		{
			GameData parameters = new GameData();

			var difficultySelection = UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?", 
			                                                _difficultySettings,
														    "Escape to quit.");

			if (difficultySelection == -1)
				return null;
			parameters.DifficultySetting = difficultySelection + 1;


			var classList = _hl12classes;
			if (parameters.DifficultySetting == 3 || parameters.DifficultySetting == 4)
				classList = _hl34classes;
			if (parameters.DifficultySetting == 5)
				classList = _hl5classes;

			var classSelection = UserInputHandler.SelectFromMenu("What is thy calling", classList, "Escape to Quit");

			if (classSelection == -1)
				return null;
			parameters.CharacterClass = (CharacterClasses)System.Enum.Parse(typeof(CharacterClasses), 
				                                                                      classList[classSelection]);
			
			var useAchivementSelection = UserInputHandler.SelectFromMenu(
				"Willst thou make use of the work of thy predecessors",
				new List<string> { "Yes", "No" }, "Escape to Quit");

			if (useAchivementSelection == -1)
				return null;
			parameters.UseAchievements = (useAchivementSelection == 0);

			var characterNote = UserInputHandler.GetText("Specify Character Note");
			if (characterNote == null)
				return null;
			parameters.CharacterNote = characterNote;

			return parameters;
		}

	}
}
