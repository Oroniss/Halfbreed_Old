﻿using System.Collections.Generic;

namespace Halfbreed
{
	public static class LoadGameMenus
	{

		public static int SelectSavedGame()
		{
			List<SaveGameSummary> currentSaves = DatabaseConnection.GetSaveGameSummaries();
			List<string> saveStrings = new List<string>();
			foreach (SaveGameSummary saveSummary in currentSaves)
				saveStrings.Add(saveSummary.ToString());
			
			string menuText = "Select your champion";
			if (currentSaves.Count == 0)
				menuText = "Sorry, there are no surviving champions";

			int selection = UserInputHandler.SelectFromMenu(menuText, saveStrings, "Escape to Cancel");

			if (selection == -1)
				return -1;

			return currentSaves[selection].GameId;
		}
	
	
	}
}