using System.Collections.Generic;
using System.IO;
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
							NewGameParameters parameters = CharacterCreationMenus.StartNewGame();
							if (!parameters.Cancel)
							{
								parameters.GameId = DatabaseConnection.GenerateNextGameId();
								GameEngine.SetStartingParameters(parameters);
								string filePath = Directory.GetCurrentDirectory() + "/LevelFiles/Testing/Test";
								Level lvl = new Level(filePath);
								MainGraphicDisplay.MapConsole.DrawMap(lvl, 5, 5);
								string key = UserInputHandler.getNextKey();
							}
							MainProgram.quit();
							return;

						}
					case 1:
						{
							int gameId = LoadGameMenus.SelectSavedGame();
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
