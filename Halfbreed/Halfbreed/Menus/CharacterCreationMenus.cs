using System.Collections.Generic;
namespace Halfbreed
{
	public static class CharacterCreationMenus
	{
		private static List<string> _difficultySettings = new List<string>{
					"Thou art a craven knave, worthy only of contempt",
					"Thou art a pathetic weakling, unwilling to challenge thyself",
					"Thou art either brave or foolhardy, only time will tell which",
					"Thou art swimming far out of thy depth",
					"Thou canst not comprehend the horrors that await thee"};
		private static List<string> _hl12classes = new List<string>{
					"Cleric", "Fighter", "Mage", "Thief"};
		private static List<string> _hl34classes = new List<string>{
					"Bard", "Blackguard", "Druid", "Necromancer", "Paladin", "Ranger"};
		private static List<string> _hl5classes = new List<string> { "Dragonlord" };


		public static bool StartNewGame()
		{
			bool difficultyWasSet = SelectAndSetDifficulty();

			if (!difficultyWasSet)
				return false;

			bool characterClasWasSet = SelectAndSetCharacterClass(GameParameters.DifficultySetting);

			if (!characterClasWasSet)
				return false;

			bool useAchievementsWasSet = SelectAndSetUseAchievements();

			if (!useAchievementsWasSet)
				return false;

			return true;
		}

		private static bool SelectAndSetDifficulty()
		{
			int selection = UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?", _difficultySettings,
														  "Escape to quit.");
			if (selection == -1)
				return false;
			GameParameters.setStartingDifficulty(selection + 1);
			return true;
		}

		private static bool SelectAndSetCharacterClass(int difficulty)
		{
			List<string> classList = _hl12classes;

			if (difficulty == 3 || difficulty == 4)
				classList = _hl34classes;

			if (difficulty == 5)
				classList = _hl5classes;

			int selection = UserInputHandler.SelectFromMenu("What is thy calling", classList, "Escape to Quit");

			if (selection == -1)
				return false;

			CharacterClasses characterClass = CharacterClassToStringConverter.ConvertStringToCharacterClass(
				classList[selection]);
			GameParameters.setStartingCharacterClass(characterClass);
			return true;

		}

		private static bool SelectAndSetUseAchievements()
		{
			int selection = UserInputHandler.SelectFromMenu("Willst thou make use of the work of thy predecessors",
															new List<string> { "Yes", "No" }, "Escape to Quit");

			if (selection == -1)
				return false;

			GameParameters.setStartingUseAchievements(selection == 0);
			return true;
		}
	}
}
