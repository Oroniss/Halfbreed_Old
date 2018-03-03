// Tidied for version 0.02.

using System;
using System.Threading;
using RLNET;

namespace Halfbreed
{
    public static class MainProgram
    {
		static readonly string _versionNumber = "0.02";

		static readonly string _fontName = "terminal8x8.png";
		static readonly int _consoleWidth = 160;
		static readonly int _consoleHeight = 80;
		static readonly int _fontSize = 8;
		static readonly float _scale = 1.0f;
		static readonly string _windowTitle = "Halfbreed";

		static readonly Levels.LevelEnum _startingLevel = Levels.LevelEnum.TESTLEVEL1; // TODO: Add a function to change these from GM Menu.
		static readonly int _startingXLoc = 42;
		static readonly int _startingYLoc = 5;

		static RLRootConsole rootConsole;
		static bool _quit;

		static GameData _gameData;
		static Entities.Player _player;
		static Level _currentLevel;
		static int _currentTime;

        public static void Main()
        {
			UserDataManager.SetupDirectoriesAndFiles();
			var configParameters = UserDataManager.ReadConfigParameters();

			UserInputHandler.ExtraKeys = configParameters.ExtraKeys;
			UserDataManager.FullLogging = configParameters.FullLogging;
			// TODO: Set the gm option here too.

			rootConsole = new RLRootConsole(_fontName, _consoleWidth, _consoleHeight, _fontSize, _fontSize, _scale,
											_windowTitle);

            rootConsole.Update += RootConsoleUpdate;
            rootConsole.Render += RootConsoleRender;

			var mainLoopThread = new Thread(RunStartMenu);
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
            var key = rootConsole.Keyboard.GetKeyPress();
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

			var gameState = UserDataManager.GetGameState(gameId);
			if (gameState.Summary.CurrentLevelName == "NEWGAME")
			{
				SetupNewGame(gameState.Summary.GameData);
				LevelTransition(_startingLevel, _startingXLoc, _startingYLoc);
			}
			else
				LoadGame(gameState);

			RunGame();
			Quit();

		}

		static void RunGame()
		{
			while (true)
			{
				_player.Update(_currentLevel);
				_currentLevel.ActivateEntities();
				_currentTime++;

				if (_quit)
				{
					SaveGame();
					return;
				}
			}
		}

		public static void Quit()
		{
			_quit = true;
			rootConsole.Close();
		}

		static void SaveGame()
		{
			var summary = new UserData.SaveGameSummary(_gameData, _currentLevel.Title, true, DateTime.Now);
			var levelDetails = _currentLevel.GetSerialisationDetails();
			var saveGame = new UserData.SaveGame(summary, levelDetails, _player, _currentTime);
			UserDataManager.SaveGame(saveGame);
		}

		static void LoadGame(UserData.SaveGame gameState)
		{
			_currentLevel = new Level(gameState.CurrentLevelDetails);
			_player = gameState.Player;
			_currentTime = gameState.CurrentTime;
			_gameData = gameState.Summary.GameData;
			_currentLevel.AddActor(_player);
		}

		public static void LevelTransition(Levels.LevelEnum newLevel, int newX, int newY)
		{
			// TODO: Need to use correct update move function so that it moves all minions as well.
			_currentLevel = new Level(newLevel);
			_player.UpdatePosition(newX, newY);
			_currentLevel.AddActor(_player);
			_player.UpdateVisibleTiles(_currentLevel);
			MainGraphicDisplay.UpdateGameScreen();
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

    }
}
