using System.Collections.Generic;
using RLNET;

namespace Halfbreed.Entities
{
	public class InputComponent:Component
	{
		private static List<RLKey> _movementKeys = new List<RLKey>() { RLKey.Up, RLKey.Down, RLKey.Left, RLKey.Right };
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
			MainGraphicDisplay.TextConsole.AddOutputText("");
			MainGraphicDisplay.TextConsole.AddOutputText(string.Format("Elapsed time = {0}", (System.DateTime.Now - _lastPlayerTurn)));

			bool MadeValidMove = false;

			if (_entity.HasTrait(EntityTraits.PLAYER) && _entity.HasComponent(ComponentType.SENSORY))
			{
				System.DateTime startLOS = System.DateTime.Now;

				var currentViewDistance = ((SensoryComponent)_entity.GetComponent(ComponentType.SENSORY)).CurrentViewDistance;
				var elevation = GameEngine.CurrentLevel.GetElevation(_entity.XLoc, _entity.YLoc);
				var blindsight = _entity.HasTrait(EntityTraits.BLINDSIGHT);
				var trueseeing = _entity.HasTrait(EntityTraits.TRUESEEING);
				var darkvision = _entity.HasTrait(EntityTraits.DARKVISION);

				var ViewList = GameEngine.CurrentLevel.CalculateFOV(_entity.XLoc, _entity.YLoc, currentViewDistance,
																	elevation, blindsight, trueseeing, darkvision);
	
				MainGraphicDisplay.TextConsole.AddOutputText(string.Format("LOS TIME = {0}", (System.DateTime.Now - startLOS)));

				foreach (Position position in ViewList)
					GameEngine.CurrentLevel.revealTile(position.X, position.Y);
				GameEngine.VisibleTiles = ViewList;
			}

			while (!MadeValidMove)
			{
				MainGraphicDisplay.UpdateGameScreen();

				RLKey key = UserInputHandler.getNextKey();
				if (key == RLKey.Escape)
				{
					GameEngine.Quit();
					MadeValidMove = true;
				}
				if (_movementKeys.Contains(key) && _entity.HasComponent(ComponentType.MOVEMENT))
				{
					int newX = _entity.XLoc;
					int newY = _entity.YLoc;

					if (key == RLKey.Up)
						newY--;
					if (key == RLKey.Down)
						newY++;
					if (key == RLKey.Left)
						newX--;
					if (key == RLKey.Right)
						newX++;

					MadeValidMove = ((MovementComponent)_entity.GetComponent(ComponentType.MOVEMENT)).AttemptMove(newX, newY);
				}
				if (key == RLKey.U && _entity.HasTrait(EntityTraits.CANINTERACT))
				{
					Direction direction = UserInputHandler.GetDirection("", true);
					if (direction == null)
						continue;

					List<Entity> entities = GameEngine.CurrentLevel.GetEntitiesWithComponent(_entity.XLoc + direction.XDirection,
																							 _entity.YLoc + direction.YDirection,
																							 ComponentType.INTERACTIBLE);
					if (entities.Count == 0)
					{
						MainGraphicDisplay.TextConsole.AddOutputText("There is nothing there you can use");
						continue;
					}
					int choice = 0;
					if (entities.Count > 1)
					{
						List<string> options = new List<string>();
						for (int i = 0; i < entities.Count; i++)
							options.Add(entities[i].GetDescription());

						choice = UserInputHandler.SelectFromMenu("Select object:", options, "Escape to cancel");

						if (choice == -1)
							continue;
					}

					MainGraphicDisplay.UpdateGameScreen();

					((InteractibleComponent)entities[choice].GetComponent(ComponentType.INTERACTIBLE)).InteractWith(_entity, currentTime);
					MadeValidMove = true;
				}
			}
			MainGraphicDisplay.UpdateGameScreen();
			_lastPlayerTurn = System.DateTime.Now;
		}

		private void GetAIInput(int currentTime)
		{
		}
			

	}
}
