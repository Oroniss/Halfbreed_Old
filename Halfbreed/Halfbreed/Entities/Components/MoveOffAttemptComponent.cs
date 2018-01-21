namespace Halfbreed.Entities
{
	public partial class MoveOffAttemptComponent:MovementTriggerComponent
	{
		public MoveOffAttemptComponent(Entity entity)
			:base(entity)
		{
			_componentType = ComponentType.MOVEOFFATTEMPT;
		}

		public bool MoveOffAttempt(Entity actor, int destinationX, int destinationY)
		{
			bool success = true;
			foreach (string functionName in _functions)
			{
				if (!_moveOffAttemptFunctionDictionary[functionName](_entity, actor, destinationX, destinationY))
					success = false;
			}
			return success;
		}

		private delegate bool MoveOffAttemptFunction(Entity entity, Entity actor, int destinationX, int destinationY);

	}
}
