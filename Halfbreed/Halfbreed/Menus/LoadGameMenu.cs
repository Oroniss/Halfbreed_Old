// Updated for version 0.02.

using System.Collections.Generic;

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
			var currentSaves = UserDataManager.GetCurrentSaves();
			var saveStrings = new List<string>();
			foreach (var saveSummary in currentSaves)
				saveStrings.Add(saveSummary.ToString());
			
			if (currentSaves.Count == 0)
				menuText = "Sorry, there are no surviving champions";

			var selection = UserInputHandler.SelectFromMenu(menuText, saveStrings, "Escape to Cancel");

			if (selection == -1)
				return -1;

			return currentSaves[selection].GameData.GameID;
		}
	}
}
