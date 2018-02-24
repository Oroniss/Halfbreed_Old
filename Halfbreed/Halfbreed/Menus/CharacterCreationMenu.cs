// Updated for version 0.2.

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

			parameters = SelectDifficultySetting(parameters);

			if (parameters != null)
				parameters = SelectCharacterClass(parameters);

			if (parameters != null)
				parameters = SelectUseAchievements(parameters);

			if (parameters != null)
				parameters = SetCharacterNote(parameters);

			// TODO: Display starting character text.
			// TODO: Can't allocate stat points here so that needs to happen in new game part of main program.

			return parameters;

		}

		GameData SelectDifficultySetting(GameData newGameParameters)
		{
			var difficultySelection = UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?",
														_difficultySettings,
														"Escape to quit.");

			if (difficultySelection == -1)
				return null;
			newGameParameters.DifficultySetting = difficultySelection + 1;
			return newGameParameters;
		}

		GameData SelectCharacterClass(GameData newGameParameters)
		{
			var classList = GetClassList(newGameParameters.DifficultySetting);
			var classSelection = UserInputHandler.SelectFromMenu("What is thy calling", classList, "Escape to Quit");

			if (classSelection == -1)
				return null;
			newGameParameters.CharacterClass = (CharacterClasses)System.Enum.Parse(typeof(CharacterClasses), 
				                                                                      classList[classSelection]);
			return newGameParameters;
		}

		List<string> GetClassList(int difficultySetting)
		{
			var classList = _hl12classes;
			if (difficultySetting == 3 || difficultySetting == 4)
				classList = _hl34classes;
			if (difficultySetting == 5)
				classList = _hl5classes;
			return classList;
		}

		GameData SelectUseAchievements(GameData newGameParameters)
		{
			var useAchivementSelection = UserInputHandler.SelectFromMenu(
			"Willst thou make use of the work of thy predecessors",
			new List<string> { "Yes", "No" }, "Escape to Quit");

			if (useAchivementSelection == -1)
				return null;
			newGameParameters.UseAchievements = (useAchivementSelection == 0);
			return newGameParameters;
		}

		GameData SetCharacterNote(GameData newGameParameters)
		{
			var characterNote = UserInputHandler.GetText("Specify Character Note");
			if (characterNote == null)
				return null;
			newGameParameters.CharacterNote = characterNote;
			return newGameParameters;
		}
	}
}
