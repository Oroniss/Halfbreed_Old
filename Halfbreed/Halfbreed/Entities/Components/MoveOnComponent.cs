namespace Halfbreed.Entities
{
	public partial class MoveOnComponent:MovementTriggerComponent
	{
		// Static dictionaries and functions are in the Entities/Furnishing/MoveOnFunctions file.

		public MoveOnComponent(Entity entity)
			:base(entity)
		{
			_componentType = ComponentType.MOVEON;
		}

		public void MoveOn(Entity actor, int originX, int originY)
		{
			foreach (string functionName in _functions)
				_moveOnFunctionDictionary[functionName](_entity, actor, originX, originY);
		}

		private delegate void MoveOnFunction(Entity furnishing, Entity actor, int originX, int originY);
	}
}
