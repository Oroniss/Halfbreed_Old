using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class Player:Actor
	{
		int _lightRadius = 0;

		public Player(Menus.NewGameParameters playerParameters)
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

			GameEngine.VisibleTiles = visibleTiles;

			MainGraphicDisplay.UpdateGameScreen();
			MainGraphicDisplay.TextConsole.AddOutputText("");

			var needsToMove = true;

			while (needsToMove)
			{
				var key = UserInputHandler.getNextKey();

				if (UserInputHandler.DirectionKeys.ContainsKey(key))
				{
					var direction = UserInputHandler.DirectionKeys[key];

					if (currentLevel.isPassible(XLoc + direction.X, YLoc + direction.Y, this))
						currentLevel.MoveActorAttempt(_xLoc + direction.X, _yLoc + direction.Y, this);
					else
						MainGraphicDisplay.TextConsole.AddOutputText("You can't move there");
					needsToMove = false;
				}

				if (key == "U")
				{
					var direction = UserInputHandler.GetDirection("Which direction?", true);
					if (direction == null)
					{
						MainGraphicDisplay.TextConsole.AddOutputText("You change your mind");
						continue;
					}

					if (currentLevel.HasFurnishing(_xLoc + direction.X, _yLoc + direction.Y))
					{
						var furnishing = currentLevel.GetFurnishing(_xLoc + direction.X, _yLoc + direction.Y);
						if (furnishing.PlayerSpotted)
						{
							furnishing.InteractWith(this, currentLevel);
							needsToMove = false;
						}
						else
						{
							MainGraphicDisplay.TextConsole.AddOutputText("There is nothing there to use");
							continue;
						}
					}
					else
					{
						MainGraphicDisplay.TextConsole.AddOutputText("There is nothing there to use");
						continue;
					}

				}

				if (key == "S")
				{
					var tileSet = currentLevel.GetFOV(XLoc, YLoc, currentLevel.Elevation(XLoc, YLoc), ViewDistance,
									   _lightRadius, HasTrait(Traits.DarkVision),
									   HasTrait(Traits.BlindSight));
					var concealedEntities = currentLevel.GetConcealedEntities(tileSet);
					foreach (var entity in concealedEntities)
					{
						entity.PlayerSpotted = true;  // TODO: Flesh this out properly.
						MainGraphicDisplay.TextConsole.AddOutputText("You spot " + entity.ToString());
					}

					needsToMove = false;
				}

				if (key == "ESCAPE")
				{
					GameEngine.Quit();
					return;
				}
			}
		}
	}
}
