using System.Collections.Generic;
namespace Halfbreed
{
	public class StartGameMenus
	{
		private static List<string> _titleMenu = new List<string>{"New Game", "Load Game", "View Achievements",
			"Clear Achievements", "View Commands", "Config"};

		// Character creation
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


		public static void TitleMenu()
		{
			while (true)
			{
				int selection = UserInputHandler.SelectFromMenu("Welcome to Halfbreed", _titleMenu, "Escape to Quit");

				switch (selection)
				{
					case 0:
						{
							bool proceed = StartNewGame();
							if (!proceed)
							{
								MainProgram.quit();
								return;
							}
							break;
						}
					case -1:
						{
							MainProgram.quit();
							return;
						}
				}
			}
		}

		private static bool StartNewGame()
		{
			int difficulty = ChooseDifficultySetting();

			if (difficulty == -1)
				return false;

			string characterClass = ChooseCharacterClass(difficulty);

			if (characterClass == "QUIT")
				return false;

			var useAchievements = ChooseUseAchievements();

			if (useAchievements == -1)
				return false;
			
			return true;
		}

		private static int ChooseDifficultySetting()
		{
			// +1 to account for 0 based indexing vs 1 based indexing
			return UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?", _difficultySettings,
														  "Escape to quit.") + 1;
		}

		private static string ChooseCharacterClass(int difficulty)
		{
			List<string> classList = _hl12classes;

			if (difficulty == 3 || difficulty == 4)
				classList = _hl34classes;

			if (difficulty == 5)
				classList = _hl5classes;

			int selection = UserInputHandler.SelectFromMenu("What is thy calling", classList, "Escape to Quit");
			if (selection == -1)
				return "QUIT";
			else
				return classList[selection];
		}

		private static int ChooseUseAchievements()
		{
			return UserInputHandler.SelectFromMenu("Willst thou make use of the work of thy predecessors",
															new List<string> { "Yes", "No" }, "Escape to Quit");
		}
	}
}
