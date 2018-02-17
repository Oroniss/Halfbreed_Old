using System.Collections.Generic;

namespace Halfbreed.Menus
{
	public class MainMenu
	{
		static List<string> _mainMenuOptions = new List<string>{"New Game", "Load Game", "View Achievements",
			"Clear Achievements", "View Commands", "Config"};

		public int DisplayMainMenu()
		{
			while (true)
			{
				var selection = UserInputHandler.SelectFromMenu("Welcome to Halfbreed", _mainMenuOptions, "Escape to Quit");

				switch (selection)
				{
					case 0:
						{
							GameData parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
							if (parameters == null)
								return -1;

							// Create new save game summary.
							// Return game id.
							return 0;
						}
					case 1:
						{
							//int gameId = MenuProvider.LoadGameMenu.SelectSavedGame();
							// return gameId;
							break;
						}
					case 5:
						{
							UserInputHandler.DisplayConfigMenu();
							break;
						}
					case -1:
						{
							return -1;
						}
				}
			}
		}

	}
}
