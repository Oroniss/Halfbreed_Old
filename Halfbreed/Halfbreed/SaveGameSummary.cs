using System;

namespace Halfbreed
{
	public struct SaveGameSummary
	{
		public int GameId;
		public int DifficultySetting;
		public CharacterClasses CharacterClass;
		public bool UseAchievements;
		public string CurrentLevelName;
		public bool StillAlive;
		public long LastSaveTime;

		public SaveGameSummary(int gameId, int difficultySetting, CharacterClasses characterClass,
							   bool useAchievements, string currentLevelName, bool stillAlive, DateTime lastSaveTime)
		{
			GameId = gameId;
			DifficultySetting = difficultySetting;
			CharacterClass = characterClass;
			UseAchievements = useAchievements;
			CurrentLevelName = currentLevelName;
			StillAlive = stillAlive;
			LastSaveTime = ((DateTimeOffset)lastSaveTime).ToUnixTimeSeconds();
		}

		public SaveGameSummary(int gameId, int difficultySetting, CharacterClasses characterClass,
					   bool useAchievements, string currentLevelName, bool stillAlive, long lastSaveTime)
		{
			GameId = gameId;
			DifficultySetting = difficultySetting;
			CharacterClass = characterClass;
			UseAchievements = useAchievements;
			CurrentLevelName = currentLevelName;
			StillAlive = stillAlive;
			LastSaveTime = lastSaveTime;
		}

		public override string ToString()
		{
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds( LastSaveTime ).ToLocalTime();

			string achString = "Off";
			if (UseAchievements)
				achString = "On";
			
			string[] items = new string[] {
				CharacterClassToStringConverter.ConvertCharacterClassToString(CharacterClass), 
				DifficultySetting.ToString(),
				achString, 
				CurrentLevelName,
				dtDateTime.ToString(),
				};
			
			string returnString = "Character Class: {0}, Difficulty: {1}, Use Previous Achievements: {2}" +
				"\nCurrent Level: {3}, Last Save Time: {4}.";
			
			return string.Format(returnString, items);
		}

	}
}
