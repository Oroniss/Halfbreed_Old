using System.Collections.Generic;
namespace Halfbreed
{
	public static class MainMenu
	{
		private static List<string> _mainMenuOptions = new List<string>{"New Game", "Load Game", "View Achievements",
			"Clear Achievements", "View Commands", "Config"};

		public static void TitleMenu()
		{
			while (true)
			{
				int selection = UserInputHandler.SelectFromMenu("Welcome to Halfbreed", _mainMenuOptions, "Escape to Quit");

				switch (selection)
				{
					case 0:
						{
							bool proceed = CharacterCreationMenus.StartNewGame();
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

	}
}
