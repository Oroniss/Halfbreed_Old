using System.Collections.Generic;
using System.IO;

namespace Halfbreed.Menus
{
	public class MainMenu
	{
		private static List<string> _mainMenuOptions = new List<string>{"New Game", "Load Game", "View Achievements",
			"Clear Achievements", "View Commands", "Config"};

		public void TitleMenu()
		{
			while (true)
			{
				int selection = UserInputHandler.SelectFromMenu("Welcome to Halfbreed", _mainMenuOptions, "Escape to Quit");

				switch (selection)
				{
					case 0:
						{
							NewGameParameters parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
							if (!parameters.Cancel)
							{
								parameters.GameID = 0;  //UserDatabaseConnection.GenerateNextGameId();
								GameEngine.SetupNewGame(parameters);
								Levels.LevelEnum startingLevel = Levels.LevelEnum.TESTLEVEL1;
								GameEngine.LevelTransition(startingLevel, 42, 5);
								//Levels.LevelEnum startingLevel = Levels.LevelEnum.TESTLEVEL2;
								//GameEngine.LevelTransition(startingLevel, 49, 42);
								//UserDatabaseConnection.InsertNewSaveGameSummary(GameEngine.GenerateSaveSummary());
								GameEngine.RunGame();
							}
							MainProgram.quit();
							return;
						}
					case 1:
						{
							//int gameId = MenuProvider.LoadGameMenu.SelectSavedGame();
							break;
						}
					case -1:
						{
							MainProgram.quit();
							return;
						}
					case 5:
						{
							UserInputHandler.DisplayConfigMenu();
							break;
						}
				}
			}
		}

	}
}
