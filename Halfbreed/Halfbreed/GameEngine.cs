
namespace Halfbreed
{
	public static class GameEngine
	{
		private static int _difficultySetting;
		private static CharacterClasses _characterClass;
		private static bool _useAchievements;
		private static Level _currentLevel;
		private static int _gameId;

		public static void SetStartingParameters(Menus.NewGameParameters startingParameters)
		{
			_difficultySetting = startingParameters.DifficultySetting;
			_characterClass = startingParameters.CharacterClass;
			_useAchievements = startingParameters.UseAchievements;
			_gameId = startingParameters.GameId;
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

		public static SaveGameSummary GenerateSaveSummary()
		{
			// TODO: Fix once level title goes in.
			SaveGameSummary summary = new SaveGameSummary(_gameId, _difficultySetting, _characterClass, _useAchievements,
														  1, 1, true, System.DateTime.Now);
			return summary;
		}



	}

}
