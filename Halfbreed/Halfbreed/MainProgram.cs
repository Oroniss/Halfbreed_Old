using System;
using System.Threading;
using System.Collections.Generic;
using RLNET;

namespace Halfbreed
{
    public static class MainProgram
    {
		static string _versionNumber = "0.01";

		static RLRootConsole rootConsole;
		static bool _quit;

		static GameData _gameData;

		static Entities.Player _player;
		static Level _currentLevel;
		static int _currentTime;

		static List<XYCoordinateStruct> _visibleTiles = new List<XYCoordinateStruct>();

        public static void Main()
        {
			UserDataManager.SetupDirectoriesAndFiles();
			var configParameters = UserDataManager.ReadConfigParameters();

			UserInputHandler.ExtraKeys = configParameters.ExtraKeys;
			UserDataManager.FullLogging = configParameters.FullLogging;
			// TODO: Set the gm option here too.

			// TODO: Pull these magic numbers out and line them up with the MainGraphicsDisplay
			// TODO: Also sort out a config file to store them.
			rootConsole = new RLRootConsole("terminal8x8.png", 160, 80, 8, 8, 1, "Halfbreed");

            rootConsole.Update += RootConsoleUpdate;
            rootConsole.Render += RootConsoleRender;

			Thread mainLoopThread = new Thread(RunStartMenu);
			mainLoopThread.Start();
            rootConsole.Run();
        }

        static void RootConsoleRender(object sender, EventArgs e)
        {
			if (MainGraphicDisplay.IsDirty)
			{
				rootConsole.Clear();
				rootConsole = MainGraphicDisplay.CopyDisplayToRootConsole(rootConsole);
				rootConsole.Draw();
			}
		}

        static void RootConsoleUpdate(object sender, EventArgs e)
        {
            RLKeyPress key = rootConsole.Keyboard.GetKeyPress();
            if (key != null)
            {
				UserInputHandler.addKeyboardInput(key.Key);
            }
        }

		static void RunStartMenu()
		{
			var gameId = MenuProvider.MainMenu.DisplayMainMenu();
			if (gameId == -1)
			{
				Quit();
				return;
			}

			// Load up gameID.
			GameData newParams = new GameData();
			SetupNewGame(newParams);
			Levels.LevelEnum startingLevel = Levels.LevelEnum.TESTLEVEL1;
			LevelTransition(startingLevel, 42, 5);
			//Levels.LevelEnum startingLevel = Levels.LevelEnum.TESTLEVEL2;
			//GameEngine.LevelTransition(startingLevel, 49, 42);
			RunGame();
			Quit();

		}

		static void RunGame()
		{
			while (true)
			{
				_currentLevel.ActivateEntities();
				_currentTime++;

				if (_quit)
					return;
			}
		}

		public static void Quit()
		{
			_quit = true;
			rootConsole.Close();
		}

		public static void LevelTransition(Levels.LevelEnum newLevel, int newX, int newY)
		{
			// TODO: Need to use correct update move function so that it moves all equipped items as well.
			_currentLevel = new Level(newLevel);
			_player.UpdatePosition(newX, newY);
			_currentLevel.AddActor(_player);
		}

		static void SetupNewGame(GameData startingParameters)
		{
			_gameData = startingParameters;
			_player = new Entities.Player(startingParameters);
		}

		public static GameData GameData
		{
			get { return _gameData; }	}

		public static Entities.Player Player
		{
			get { return _player; }
		}

		public static Level CurrentLevel
		{
			get { return _currentLevel; }
		}

		public static int CurrentTime
		{
			get { return _currentTime; }
		}

		public static List<XYCoordinateStruct> VisibleTiles
		{
			get { return _visibleTiles; }
			set { _visibleTiles = value; }
		}
    }

	[System.Serializable]
	public class GameData
	{
		public int DifficultySetting;
		public CharacterClasses CharacterClass;
		public bool UseAchievements;
		public string CharacterNote;
		public int GameID;

		public GameData()
		{
			DifficultySetting = 1;
			CharacterClass = CharacterClasses.Fighter;
			UseAchievements = true;
			CharacterNote = "";
		}

		public GameData(int difficulty, CharacterClasses characterClass, bool useAchievements, string characterNote,
		                int gameId)
		{
			DifficultySetting = difficulty;
			CharacterClass = characterClass;
			UseAchievements = useAchievements;
			CharacterNote = characterNote;
			GameID = gameId;
		}
	}
}
