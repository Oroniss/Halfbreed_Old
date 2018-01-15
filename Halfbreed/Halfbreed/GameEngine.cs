
namespace Halfbreed
{
	public static class GameEngine
	{
		private static int _difficultySetting;
		private static CharacterClasses _characterClass;
		private static bool _useAchievements;
		private static Level _currentLevel;
		private static int _currentAct = 1;
		private static int _currentChapter = 1;
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

		public static int CurrentAct
		{
			get { return _currentAct; }
		}

		public static int CurrentChapter
		{
			get { return _currentChapter; }
		}

		public static void LevelTransition(string newLevelName, int newX, int newY)
		{
			// TODO: Add player at correct coordinates.
			// TODO: Need to use correct update move function so that it moves all equipped items as well.
			_currentLevel = new Level(newLevelName);
		}

		public static SaveGameSummary GenerateSaveSummary()
		{
			SaveGameSummary summary = new SaveGameSummary(_gameId, _difficultySetting, _characterClass, _useAchievements,
														  1, 1, true, System.DateTime.Now);
			return summary;
		}



	}

}
