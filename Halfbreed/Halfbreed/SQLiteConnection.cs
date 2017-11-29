using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using System;
namespace Halfbreed
{
	public static class DatabaseConnection
	{
		private static string _rootDirectory = Directory.GetCurrentDirectory();

		public static void SetupDirectoriesAndFiles()
		{
			if (!Directory.Exists(_rootDirectory + "/Logs"))
				Directory.CreateDirectory(_rootDirectory + "/Logs");
			if (!Directory.Exists(_rootDirectory + "/Misc"))
				Directory.CreateDirectory(_rootDirectory + "/Misc");

		}

		public static int GenerateNextGameId()
		{
			return 0;
		}

	}

	[Serializable]
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

	}
}
