using System.Collections.Generic;
using Halfbreed.UserData;

namespace Halfbreed.Menus
{
	public class LoadGameMenu
	{
		
		public int SelectSavedGame()
		{
			List<SaveGameSummary> currentSaves = UserDataManager.GetCurrentSaves();
			List<string> saveStrings = new List<string>();
			foreach (SaveGameSummary saveSummary in currentSaves)
				saveStrings.Add(saveSummary.ToString());
			
			string menuText = "Select your champion";
			if (currentSaves.Count == 0)
				menuText = "Sorry, there are no surviving champions";

			int selection = UserInputHandler.SelectFromMenu(menuText, saveStrings, "Escape to Cancel");

			if (selection == -1)
				return -1;

			return currentSaves[selection].GameData.GameID;
		}

	
	
	}
}
