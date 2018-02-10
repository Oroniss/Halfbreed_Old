using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class Player:Actor
	{
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

				if (key == "ESCAPE")
				{
					GameEngine.Quit();
					return;
				}
			}
		}
	}
}
