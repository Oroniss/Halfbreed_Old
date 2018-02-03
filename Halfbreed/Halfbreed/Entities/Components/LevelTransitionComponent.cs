namespace Halfbreed.Entities
{
	public class LevelTransitionComponent:Component
	{
		Levels.LevelEnum _destinationLevel;
		int _newXLoc;
		int _newYLoc;

		public LevelTransitionComponent(Entity entity, Levels.LevelEnum destinationLevel, int newXLoc, int newYLoc)
			:base(entity)
		{
			_componentType = ComponentType.LEVELTRANSITION;
			_destinationLevel = destinationLevel;
			_newXLoc = newXLoc;
			_newYLoc = newYLoc;
		}

		// TODO: Currently bool in case something needs to stop this from happening in the future.
		public bool MoveLevel(Entity actor)
		{
			GameEngine.LevelTransition(_destinationLevel, _newXLoc, _newYLoc);
			return true;
		}
	}
}
