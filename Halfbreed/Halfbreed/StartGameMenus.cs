using System.Collections.Generic;
namespace Halfbreed
{
	public class StartGameMenus
	{
		private static List<string> _titleMenu = new List<string>{"New Game", "Load Game", "View Achievements",
			"Clear Achievements", "View Commands", "Config"};
		private static List<string> _difficultySettings = new List<string>{
			"Thou art a craven knave, worthy only of contempt",
			"Thou art a pathetic weakling, unwilling to challenge thyself",
			"Thou art either brave or foolhardy, only time will tell which",
			"Thou art swimming far out of thy depth",
			"Thou canst not comprehend the horrors that await thee"};


		public static void TitleMenu()
		{
			while (true)
			{
				GraphicDesplay.MenuConsole.DrawMenu("Welcome to Halfbreed", _titleMenu, "Escape to Quit");
				string key = UserInputHandler.getNextKey();

				switch (key)
				{
					case "1":
						{
							bool proceed = StartNewGame();
							if (!proceed)
							{
								MainProgram.quit();
								return;
							}
							break;
						}
					case "ESCAPE":
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

			return true;
			
		}

		private static int ChooseDifficultySetting()
		{
			return UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?", _difficultySettings,
														  "Escape to quit.");
		}
	}
}
