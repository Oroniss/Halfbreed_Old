// Tidied up code for version 0.02.

using System.Collections.Generic;

namespace Halfbreed.Menus
{
	public class MainMenu
	{
		static readonly List<string> _mainMenuOptions = new List<string>{"New Game", "Load Game", "Delete Save Game", 
			"View Achievements", "Clear Achievements", "View Commands", "Config"};

		public int DisplayMainMenu()
		{
			while (true)
			{
				var selection = UserInputHandler.SelectFromMenu("Welcome to Halfbreed", _mainMenuOptions, 
				                                                "Escape to Quit");

				switch (selection)
				{
					case 0:
						{
							var parameters = MenuProvider.CharacterCreationMenu.StartNewGame();
							if (parameters == null)
								return -1;

							parameters.GameID = UserDataManager.GetNextGameId();
							var saveGame = new UserData.SaveGameSummary(parameters, "NEWGAME", true, 
							                                            System.DateTime.Now);
							UserDataManager.WriteSaveGameSummary(saveGame);
							return parameters.GameID;
						}
					case 1:
						{
							var gameId = MenuProvider.LoadGameMenu.SelectSavedGame();
							if (gameId == -1)
								break;
							return gameId;
						}
					case 2:
						{
							var gameID = MenuProvider.LoadGameMenu.DeleteSavedGame();
							if (gameID != -1)
								UserDataManager.DeleteSaveGame(gameID);
							break;
						}
					case 5:
						{
							MenuProvider.ViewKeysDisplay.ViewKeys();
							break;
						}
					case 6:
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
