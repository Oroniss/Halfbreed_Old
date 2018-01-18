using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class PlayerInputComponent:InputComponent,IActor
	{
		private static List<string> _movementKeys = new List<string>() { "UP", "DOWN", "LEFT", "RIGHT" };
		private static System.DateTime _lastPlayerTurn = System.DateTime.Now;

		public PlayerInputComponent(Entity entity)
			:base(entity)
		{
		}

		public void GetNextMove(int currentTime)
		{
			System.Console.WriteLine("Elapsed time = " + (System.DateTime.Now - _lastPlayerTurn));

			bool NeedsToMove = true;

			while (NeedsToMove)
			{
				string key = UserInputHandler.getNextKey();
				if (key == "ESCAPE")
				{
					GameEngine.Quit();
					NeedsToMove = false;
				}
				if (_movementKeys.Contains(key))
				{
					int newX = _entity.XLoc;
					int newY = _entity.YLoc;

					if (key == "UP")
						newY--;
					if (key == "DOWN")
						newY++;
					if (key == "LEFT")
						newX--;
					if (key == "RIGHT")
						newX++;
					if (GameEngine.CurrentLevel.IsPassible(newX, newY, (MovementComponent)_entity.GetComponent(ComponentType.MOVEMENT)))
					{
						GameEngine.CurrentLevel.MoveEntity(_entity, newX, newY);
						NeedsToMove = false;
					}
				}
			}
			_lastPlayerTurn = System.DateTime.Now;
		}
	}
}
