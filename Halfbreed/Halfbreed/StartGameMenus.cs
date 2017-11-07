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
			"Thou cannot comprehend the horrors that await thee"};


		public static void TitleMenu()
		{
			while (true)
			{
				GraphicDesplay.MenuConsole.DrawSelectFromMenu("Welcome to Halfbreed", _titleMenu, "Escape to Quit");
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
			GraphicDesplay.MenuConsole.DrawSelectFromMenu("How great a challenge dost thou seek?", _difficultySettings,
			                                              "Escape to quit.");
			while (true)
			{
				string key = UserInputHandler.getNextKey();
				switch (key)
				{
					case "1":
						return 1;
					case "2":
						return 2;
					case "3":
						return 3;
					case "4":
						return 4;
					case "5":
						return 5;
					case "ESCAPE":
						return -1;
				}
			}
			
		}
	}
}
