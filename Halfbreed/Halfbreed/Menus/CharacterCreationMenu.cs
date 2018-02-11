// Revised for version 0.2.

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


		public NewGameParameters StartNewGame()
		{
			NewGameParameters parameters = new NewGameParameters();

			ChooseDifficulty(parameters);

			if (parameters.Cancel)
				return parameters;

			ChooseCharacterClass(parameters);

			if (parameters.Cancel)
				return parameters;

			ChooseUseAchievements(parameters);

			return parameters;
		}

		void ChooseDifficulty(NewGameParameters parameters)
		{
			var selection = UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?", 
			                                                _difficultySettings,
														    "Escape to quit.");
			if (selection == -1)
				parameters.Cancel = true;
			else
				parameters.DifficultySetting = selection + 1;
		}

		void ChooseCharacterClass(NewGameParameters parameters)
		{
			var classList = _hl12classes;
			if (parameters.DifficultySetting == 3 || parameters.DifficultySetting == 4)
				classList = _hl34classes;
			if (parameters.DifficultySetting == 5)
				classList = _hl5classes;

			var selection = UserInputHandler.SelectFromMenu("What is thy calling", classList, "Escape to Quit");

			if (selection == -1)
				parameters.Cancel = true;
			else
				parameters.CharacterClass = (CharacterClasses)System.Enum.Parse(typeof(CharacterClasses), 
				                                                                      classList[selection]);
		}

		void ChooseUseAchievements(NewGameParameters parameters)
		{
			var selection = UserInputHandler.SelectFromMenu("Willst thou make use of the work of thy predecessors",
															new List<string> { "Yes", "No" }, "Escape to Quit");

			if (selection == -1)
				parameters.Cancel = true;
			else
				parameters.UseAchievements = (selection == 0);
		}
	}

	[System.Serializable]
	public class NewGameParameters
	{
		public bool Cancel;
		public int DifficultySetting;
		public CharacterClasses CharacterClass;
		public bool UseAchievements;
		public int GameID;

		public NewGameParameters()
		{
			Cancel = false;
			DifficultySetting = 1;
			CharacterClass = CharacterClasses.Fighter;
			UseAchievements = true;
		}

		public NewGameParameters(int difficulty, CharacterClasses characterClass, bool useAchievements, int gameId)
		{
			DifficultySetting = difficulty;
			CharacterClass = characterClass;
			UseAchievements = useAchievements;
			GameID = gameId;
		}
	}
}
