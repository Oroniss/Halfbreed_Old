using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class Player:Actor
	{
		static SortedDictionary<int, Player> players = new SortedDictionary<int, Player>();
		static int maxPlayerId = 0;
		static List<int> unusedPlayerIds = new List<int>();

		readonly int _playerId;

		CharacterClasses _characterClass;
		int _difficultySetting;
		bool _useAchievements;

		int _lightRadius = 0;

		public Player(GameData playerParameters)
			:base("Player", 0 ,0 , new List<string>())
		{
			if (unusedPlayerIds.Count > 0)
			{
				_playerId = unusedPlayerIds[0];
				unusedPlayerIds.RemoveAt(0);
			}
			else
			{
				_playerId = maxPlayerId;
				maxPlayerId++;
			}
			players[_playerId] = this;

			_characterClass = playerParameters.CharacterClass;
			_difficultySetting = playerParameters.DifficultySetting;
			_useAchievements = playerParameters.UseAchievements;

			AddTrait("Player");
			AddTrait("Walking");
			AddTrait("Swimming");
			AddTrait("Climbing");

			_primaryStats = new PrimaryStatBlock(PlayerSetupData.GetCharacterStartingStats(_characterClass), 1);
			foreach (var upgrade in PlayerSetupData.GetCharacterStartingUpgrades(_characterClass))
				UpgradePrimaryStatDice(upgrade);

			//foreach (var resist in PlayerSetupData.GetStartingResistanceModifiers(_difficultySetting))
			//	AddDefensiveDice(resist.Resist, resist.DiceType, resist.UpgradeLevel);
		}

		public int PlayerId
		{ 
			get { return _playerId; }
		}

		public CharacterClasses CharacterClass
		{
			get { return _characterClass; }
		}

		protected override void GetNextMove(Level currentLevel)
		{
			UpdateVisibleTiles(currentLevel);

			MainGraphicDisplay.TextConsole.AddOutputText("");

			bool madeValidMove = false;
			while (!madeValidMove)
			{
				MainGraphicDisplay.UpdateGameScreen();
				var key = UserInputHandler.getNextKey();

				if (UserInputHandler.DirectionKeys.ContainsKey(key))
					madeValidMove = Move(currentLevel, key);

				if (key == "K")
					madeValidMove = DisplayKeys();

				if (key == "S")
					madeValidMove = Search(currentLevel);

				if (key == "U")
					madeValidMove = InteractWithFurnishing(currentLevel);

				if (key == "SPACE")
					madeValidMove = Pause();

				if (key == "ESCAPE")
				{
					MainProgram.Quit();
					return;
				}
			}
		}

		public void UpdateVisibleTiles(Level currentLevel)
		{
			var visibleTiles = currentLevel.GetFOV(XLoc, YLoc, currentLevel.Elevation(XLoc, YLoc), ViewDistance,
									   _lightRadius, HasTrait("DarkVision"),
									   HasTrait("BlindSight"));
			foreach (XYCoordinateStruct tile in visibleTiles)
				currentLevel.RevealTile(tile.X, tile.Y);
			currentLevel.VisibleTiles = visibleTiles;
		}

		bool Move(Level currentLevel, string key)
		{
			var direction = UserInputHandler.DirectionKeys[key];

			if (currentLevel.isPassible(XLoc + direction.X, YLoc + direction.Y, this))
				currentLevel.MoveActorAttempt(_xLoc + direction.X, _yLoc + direction.Y, this);
			else
				MainGraphicDisplay.TextConsole.AddOutputText("You can't move there");
			return true;
		}


		bool DisplayKeys()
		{
			MenuProvider.ViewKeysDisplay.ViewKeys();
			return false;
		}

		bool Search(Level currentLevel)
		{
			var tileSet = currentLevel.GetFOV(XLoc, YLoc, currentLevel.Elevation(XLoc, YLoc), ViewDistance,
											  _lightRadius, HasTrait("DarkVision"), HasTrait("BlindSight"));

			var concealedEntities = currentLevel.GetConcealedEntities(tileSet);
			foreach (var entity in concealedEntities)
			{
				entity.PlayerSpotted = true;  // TODO: Flesh this out properly.
				MainGraphicDisplay.TextConsole.AddOutputText("You spot " + entity);
			}

			return true;
		}

		bool InteractWithFurnishing(Level currentLevel)
		{
			var direction = UserInputHandler.GetDirection("Which direction?", true);
			if (direction == null)
			{
				MainGraphicDisplay.TextConsole.AddOutputText("You change your mind");
				return false;
			}

			if (currentLevel.HasFurnishing(_xLoc + direction.X, _yLoc + direction.Y))
			{
				var furnishing = currentLevel.GetFurnishing(_xLoc + direction.X, _yLoc + direction.Y);
				if (furnishing.PlayerSpotted)
				{
					furnishing.InteractWith(this, currentLevel);
					return true;
				}
			}

			MainGraphicDisplay.TextConsole.AddOutputText("There is nothing there to use");
			return false;
		}

		bool Pause()
		{
			MainGraphicDisplay.TextConsole.AddOutputText("You pause for a moment");
			return true;
		}

		public PrimaryStatBlock PrimaryStats
		{
			get { return _primaryStats; }
		}

		public DefensiveStatBlock DefensiveStats
		{
			get { return _defensiveStats; }
		}

		public static Player GetPlayer(int id)
		{
			return players[id];
		}

		public static PlayerSave GetSaveData()
		{
			return new PlayerSave(players, maxPlayerId, unusedPlayerIds);
		}

		public static void LoadSaveData(PlayerSave save)
		{
			players = save.Players;
			maxPlayerId = save.MaxId;
			unusedPlayerIds = save.UnusedIds;
		}
	}
}
