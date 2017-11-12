using System.Collections.Generic;
namespace Halfbreed
{
	public static class GameEngine
	{
		private static int _difficultySetting;
		private static CharacterClasses _characterClass;
		private static bool _useAchievements;
		private static Level _currentLevel;
		private static string _saveFileName;
		private static string _logFileName;

		public static void SetStartingParameters(NewGameParameters startingParameters)
		{
			_difficultySetting = startingParameters.DifficultySetting;
			_characterClass = startingParameters.CharacterClass;
			_useAchievements = startingParameters.UseAchievements;

			string timeString = System.DateTime.Now.ToString("yyMMddHHmmss");
			string currentDirectory = System.IO.Directory.GetCurrentDirectory();
			_saveFileName = currentDirectory + "/Saves/HBS" + timeString + ".hbs";
			_logFileName = currentDirectory + "/Logs/HBL" + timeString + ".hbl";
		}

		public static int DifficultySetting
		{
			get { return _difficultySetting; }
		}
		public static CharacterClasses CharacterClass
		{
			get { return _characterClass; }
		}
		public static bool UseAchievements
		{
			get { return _useAchievements; }
		}
		public static Level CurrentLevel
		{
			get { return _currentLevel; }
		}

		public static void LevelTransition(string newLevelName, int newX, int newY)
		{
			_currentLevel = new Level(newLevelName);
		}

		// TODO: Put these somewhere else
		public static void SaveGame()
		{
			// Game parameters
			// Current Level
			
		}

		public static void LoadGame()
		{
			// Game parameters
			// Current level
			
		}
	}
}
