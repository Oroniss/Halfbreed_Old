using System.Collections.Generic;

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
		private static string _versionNumber = "0.01";
		private static Entities.Entity _player;
		private static int _currentTime = 0;
		private static int _currentDays; // TODO: Think this through a bit more carefully.
		private static bool _quit;
		private static List<Position> _visibleTiles = new List<Position>();

		public static void SetupNewGame(Menus.NewGameParameters startingParameters)
		{
			_difficultySetting = startingParameters.DifficultySetting;
			_characterClass = startingParameters.CharacterClass;
			_useAchievements = startingParameters.UseAchievements;
			_gameId = startingParameters.GameId;
			_player = new Entities.Entity(startingParameters);
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

		public static Entities.Entity Player
		{
			get { return _player; }
		}

		public static int CurrentTime
		{
			get { return _currentTime; }
		}

		public static void Quit()
		{
			_quit = true;
		}

		public static void LevelTransition(Levels.LevelEnum newLevel, int newX, int newY)
		{
			// TODO: Need to pack up the existing level too.
			// TODO: Need to use correct update move function so that it moves all equipped items as well.
			_currentLevel = new Level(newLevel);
			_player.UpdatePosition(newX, newY);
			_currentLevel.AddEntity(_player);
			_visibleTiles = new List<Position>();
		}

		public static SaveGameSummary GenerateSaveSummary()
		{
			SaveGameSummary summary = new SaveGameSummary(_gameId, _difficultySetting, _characterClass, _useAchievements,
														  1, 1, true, System.DateTime.Now);
			return summary;
		}

		public static List<Position> VisibleTiles
		{
			get { return _visibleTiles; }
			set { _visibleTiles = value; }
		}

		public static void RunGame()
		{
			while (true)
			{
				_currentLevel.ActivateEntities(_currentTime);
				_currentTime++;
				if (_quit)
					return;
			}
		}



	}

}
