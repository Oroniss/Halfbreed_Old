namespace Halfbreed.Entities
{
	public partial class MoveOnAttemptComponent:MovementTriggerComponent
	{
		public MoveOnAttemptComponent(Entity entity)
			:base(entity)
		{
			_componentType = ComponentType.MOVEONATTEMPT;
		}

		public bool MoveOnAttempt(Entity actor, int originX, int originY)
		{
			bool success = true;
			foreach (string functionName in _functions)
			{
				if (!_moveOnAttemptFunctionDictionary[functionName](_entity, actor, originX, originY))
					success = false;
			}
			return success;
		}

		private delegate bool MoveOnAttemptFunction(Entity entity, Entity actor, int originX, int originY);
	}
}
