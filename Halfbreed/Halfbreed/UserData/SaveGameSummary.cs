using System;

namespace Halfbreed.UserData
{
	[Serializable]
	public struct SaveGameSummary
	{
		public GameData GameData;
		public string CurrentLevelName;
		public bool StillAlive;
		public long LastSaveTime;

		public SaveGameSummary(GameData gameData, string currentLevelName, bool stillAlive, DateTime lastSaveTime)
		{
			GameData = gameData;
			StillAlive = stillAlive;
			CurrentLevelName = currentLevelName;
			LastSaveTime = ((DateTimeOffset)lastSaveTime).ToUnixTimeSeconds();
		}

		public SaveGameSummary(GameData gameData, string currentLevelName, bool stillAlive, long lastSaveTime)
		{
			GameData = gameData;
			CurrentLevelName = currentLevelName;
			StillAlive = stillAlive;
			LastSaveTime = lastSaveTime;
		}

		public override string ToString()
		{
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(LastSaveTime).ToLocalTime();

			string achString = "Off";
			if (GameData.UseAchievements)
				achString = "On";

			string[] items = new string[] {
				GameData.CharacterClass.ToString(),
				GameData.DifficultySetting.ToString(),
				achString,
				CurrentLevelName,
				dtDateTime.ToString(),
				GameData.CharacterNote
				};

			string returnString = "Character Class: {0}, Difficulty: {1}, Use Previous Achievements: {2}" +
				"\nCurrent Level: {3}, Last Save Time: {4}." +
				"\n{5}";
			
			return string.Format(returnString, items);
		}

	}
}
