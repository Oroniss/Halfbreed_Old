using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class MovementComponent:Component
	{
		List<MovementModes> _movementModes;
		MovementModes _currentMovementMode;

		public MovementComponent(Entity entity, MovementModes[] movementModes)
			:base(entity)
		{
			_movementModes = new List<MovementModes>();
			foreach (MovementModes movementMode in movementModes)
				AddMovementMode(movementMode);
			_componentType = ComponentType.MOVEMENT;

			//TODO: Need to add swimming stuff here too.
		}

		public MovementModes MovementMode
		{
			get { return _currentMovementMode; }
		}

		public void AddMovementMode(MovementModes mode)
		{
			_movementModes.Add(mode);
			if (mode > _currentMovementMode)
				_currentMovementMode = mode;
		}

		public void RemoveMovementMode(MovementModes mode)
		{
			_movementModes.Remove(mode);
			if (mode == _currentMovementMode)
				RecalculateCurrentMode();
		}

		public bool HasMovementMode(MovementModes mode)
		{
			return _movementModes.Contains(mode);
		}

		private void RecalculateCurrentMode()
		{
			_currentMovementMode = MovementModes.IMMOBILE;
			foreach (MovementModes mode in _movementModes)
			{
				if (mode > _currentMovementMode)
					_currentMovementMode = mode;
			}
		}

		public bool AttemptMove(int newX, int newY)
		{
			if (_entity.HasTrait(EntityTraits.IMMOBILISED))
			   return false;
			
			//TODO: Check for swimming here.
			if (GameEngine.CurrentLevel.IsPassible(newX, newY, MovementMode))
			{
				GameEngine.CurrentLevel.MoveEntityAttempt(_entity, newX, newY);
				return true;
			}
			return false;
		}
	}
}
