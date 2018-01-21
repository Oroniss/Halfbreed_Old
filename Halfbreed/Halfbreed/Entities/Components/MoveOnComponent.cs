using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class MovementTriggerComponent:Component
	{
		// Static dictionaries and functions are in the Entities/Furnishing/MovementFunctions file.

		private List<string> _moveOffFunctions;
		private List<string> _moveOnFunctions;
		private List<string> _moveOffAttemptFunctions;
		private List<string> _moveOnAttemptFunctions;

		public MovementTriggerComponent(Entity entity)
			:base(entity)
		{
			_componentType = ComponentType.MOVEMENTTRIGGER;
		}

		public void MoveOff(Entity actor, int destinationX, int destinationY)
		{
			if (_moveOffFunctions == null)
				return;
			foreach (string functionName in _moveOffFunctions)
				_moveOffFunctionDictionary[functionName](_entity, actor, destinationX, destinationY);
		}

		public void MoveOn(Entity actor, int originX, int originY)
		{
			if (_moveOnFunctions == null)
				return;
			foreach (string functionName in _moveOnFunctions)
				_moveOnFunctionDictionary[functionName](_entity, actor, originX, originY);
		}

		public bool MoveOffAttempt(Entity actor, int destinationX, int destinationY)
		{
			if(_moveOffAttemptFunctions == null)
				return true;
			bool success = true;
			foreach (string functionName in _moveOffAttemptFunctions)
			{
				if (!_moveOffAttemptFunctionDictionary[functionName](_entity, actor, destinationX, destinationY))
					success = false;
			}
			return success;
		}

		public bool MoveOnAttempt(Entity actor, int originX, int originY)
		{
			if(_moveOnAttemptFunctions == null)
				return true;
			bool success = true;
			foreach (string functionName in _moveOnAttemptFunctions)
			{
				if (!_moveOnAttemptFunctionDictionary[functionName](_entity, actor, originX, originY))
					success = false;
			}
			return success;
		}

		public void AddMoveOffFunction(string functionName)
		{
			if (_moveOffFunctions == null)
				_moveOffFunctions = new List<string>() { functionName };
			else
				_moveOffFunctions.Add(functionName);
		}

		public void RemoveMoveOffFunction(string functionName)
		{
			if (_moveOffFunctions.Count == 1)
				_moveOffFunctions = null;
			else
				_moveOffFunctions.Remove(functionName);
		}

		public void AddMoveOnFunction(string functionName)
		{
			if (_moveOnFunctions == null)
				_moveOnFunctions = new List<string>() { functionName };
			else
				_moveOnFunctions.Add(functionName);
		}

		public void RemoveMoveOnFunction(string functionName)
		{
			if (_moveOnFunctions.Count == 1)
				_moveOnFunctions = null;
			else
				_moveOnFunctions.Remove(functionName);
		}

		public void AddMoveOffAttemptFunction(string functionName)
		{
			if (_moveOffAttemptFunctions == null)
				_moveOffAttemptFunctions = new List<string>() { functionName };
			else
				_moveOffAttemptFunctions.Add(functionName);
		}

		public void RemoveMoveAttemptOffFunction(string functionName)
		{
			if (_moveOffAttemptFunctions.Count == 1)
				_moveOffAttemptFunctions = null;
			else
				_moveOffAttemptFunctions.Remove(functionName);
		}

		public void AddMoveOnAttemptFunction(string functionName)
		{
			if (_moveOnAttemptFunctions == null)
				_moveOnAttemptFunctions = new List<string>() { functionName };
			else
				_moveOnAttemptFunctions.Add(functionName);
		}

		public void RemoveMoveOnAttemptFunction(string functionName)
		{
			if (_moveOnAttemptFunctions.Count == 1)
				_moveOnAttemptFunctions = null;
			else
				_moveOnAttemptFunctions.Remove(functionName);
		}

		private delegate void MoveOffFunction(Entity furnishing, Entity actor, int destinationX, int destinationY);
		private delegate void MoveOnFunction(Entity furnishing, Entity actor, int originX, int originY);
		private delegate bool MoveOffAttemptFunction(Entity furnishing, Entity actor, int destinationX, int destinationY);
		private delegate bool MoveOnAttemptFunction(Entity furnishing, Entity actor, int originX, int originY);
	}
}
