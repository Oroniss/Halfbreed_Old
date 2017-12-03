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
			
			return string.Format("[SaveGameSummary]");
		}

	}
}
