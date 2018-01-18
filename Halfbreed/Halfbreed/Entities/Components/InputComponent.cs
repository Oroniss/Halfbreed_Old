using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class InputComponent:Component
	{
		private static List<string> _movementKeys = new List<string>() { "UP", "DOWN", "LEFT", "RIGHT" };
		private static System.DateTime _lastPlayerTurn = System.DateTime.Now;

		private bool _active;
		private InputFunction _inputFunction;

		public InputComponent(Entity entity)
			:base(entity)
		{
			_active = true;
			_componentType = ComponentType.INPUT;
			_inputFunction = new InputFunction(GetAIInput);
		}

		public void SetManual()
		{
			_inputFunction = new InputFunction(GetManualInput);
		}

		public void GetNextMove(int currentTime)
		{
			if (_active)
				_inputFunction(currentTime);
		}

		private delegate void InputFunction(int currentTime);

		private void GetManualInput(int currentTime)
		{
			System.Console.WriteLine("Elapsed time = " + (System.DateTime.Now - _lastPlayerTurn));

			bool MadeValidMove = false;

			while (!MadeValidMove)
			{
				string key = UserInputHandler.getNextKey();
				if (key == "ESCAPE")
				{
					GameEngine.Quit();
					MadeValidMove = true;
				}
				if (_movementKeys.Contains(key) && _entity.HasComponent(ComponentType.MOVEMENT))
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

					MadeValidMove = ((MovementComponent)_entity.GetComponent(ComponentType.MOVEMENT)).AttemptMove(newX, newY);
				}
			}
			_lastPlayerTurn = System.DateTime.Now;
		}

		private void GetAIInput(int currentTime)
		{
		}
			

	}
}
