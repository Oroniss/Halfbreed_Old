using System.Collections.Generic;
using Halfbreed.UserData;

namespace Halfbreed.Menus
{
	public class LoadGameMenu
	{
		
		public int SelectSavedGame()
		{
			return DisplaySaveMenu("Select your champion");
		}

		public int DeleteSavedGame()
		{
			return DisplaySaveMenu("Which champion do you wish to remove? This is irrevocable!");
		}

		int DisplaySaveMenu(string menuText)
		{
			List<SaveGameSummary> currentSaves = UserDataManager.GetCurrentSaves();
			List<string> saveStrings = new List<string>();
			foreach (SaveGameSummary saveSummary in currentSaves)
				saveStrings.Add(saveSummary.ToString());
			
			if (currentSaves.Count == 0)
				menuText = "Sorry, there are no surviving champions";

			int selection = UserInputHandler.SelectFromMenu(menuText, saveStrings, "Escape to Cancel");

			if (selection == -1)
				return -1;

			return currentSaves[selection].GameData.GameID;
			
		}
	
	}
}
