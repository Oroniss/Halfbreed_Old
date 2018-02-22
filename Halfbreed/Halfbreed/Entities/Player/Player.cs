using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class Player:Actor
	{
		int _lightRadius = 0;

		public Player(GameData playerParameters)
			:base("Player", 0 ,0 , new List<string>())
		{
			AddTrait(Traits.Player);
			AddTrait(Traits.Walking);
			AddTrait(Traits.Swimming);
			AddTrait(Traits.Climbing);

		}

		protected override void GetNextMove(Level currentLevel)
		{
			var visibleTiles = currentLevel.GetFOV(XLoc, YLoc, currentLevel.Elevation(XLoc, YLoc), ViewDistance, 
			                                       _lightRadius, HasTrait(Traits.DarkVision), 
			                                       HasTrait(Traits.BlindSight));
			
			foreach (XYCoordinateStruct tile in visibleTiles)
				currentLevel.RevealTile(tile.X, tile.Y);

			MainProgram.VisibleTiles = visibleTiles;

			MainGraphicDisplay.UpdateGameScreen();
			MainGraphicDisplay.TextConsole.AddOutputText("");

			var madeValidMove = false;

			while (!madeValidMove)
			{
				var key = UserInputHandler.getNextKey();

				if (UserInputHandler.DirectionKeys.ContainsKey(key))
					madeValidMove = Move(currentLevel, key);

				if (key == "U")
					madeValidMove = InteractWithFurnishing(currentLevel);

				if (key == "S")
					madeValidMove = Search(currentLevel);

				if (key == "ESCAPE")
				{
					MainProgram.Quit();
					return;
				}
			}
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

		bool Search(Level currentLevel)
		{
			var tileSet = currentLevel.GetFOV(XLoc, YLoc, currentLevel.Elevation(XLoc, YLoc), ViewDistance,
									    	  _lightRadius, HasTrait(Traits.DarkVision), HasTrait(Traits.BlindSight));

			var concealedEntities = currentLevel.GetConcealedEntities(tileSet);
			foreach (var entity in concealedEntities)
			{
				entity.PlayerSpotted = true;  // TODO: Flesh this out properly.
				MainGraphicDisplay.TextConsole.AddOutputText("You spot " + entity.ToString());
			}
	
			return true;
		}
	}
}
