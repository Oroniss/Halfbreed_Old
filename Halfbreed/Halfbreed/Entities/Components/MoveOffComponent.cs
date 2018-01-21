namespace Halfbreed.Entities
{
	public partial class MoveOffComponent:MovementTriggerComponent
	{
		public MoveOffComponent(Entity entity)
			:base(entity)
		{
			_componentType = ComponentType.MOVEOFF;
		}

	public void MoveOff(Entity actor, int destinationX, int destinationY)
	{
		foreach (string functionName in _functions)
				_moveOffFunctionDictionary[functionName](_entity, actor, destinationX, destinationY);
	}

	private delegate void MoveOnFunction(Entity furnishing, Entity actor, int destinationX, int destinationY);

	}
}
