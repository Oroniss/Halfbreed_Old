using System.Collections.Generic;
namespace Halfbreed
{
	public static class StartGameMenus
	{
		private static List<string> _titleMenu = new List<string>{"New Game", "Load Game", "View Achievements",
			"Clear Achievements", "View Commands", "Config"};

		public static void TitleMenu()
		{
			while (true)
			{
				int selection = UserInputHandler.SelectFromMenu("Welcome to Halfbreed", _titleMenu, "Escape to Quit");

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
