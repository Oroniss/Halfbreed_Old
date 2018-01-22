namespace Halfbreed.Entities
{
	public class MovementComponent:Component
	{
		public MovementComponent(Entity entity)
			:base(entity)
		{
		}

		public bool AttemptMove(int newX, int newY)
		{
			if (_entity.HasTrait(EntityTraits.IMMOBILISED))
			   return false;

			if (GameEngine.CurrentLevel.IsPassible(newX, newY, _entity.HasTrait(EntityTraits.CANWALK),
			                                       _entity.HasTrait(EntityTraits.CANFLY), _entity.HasTrait(EntityTraits.CANSWIM)))
			{
				GameEngine.CurrentLevel.MoveEntityAttempt(_entity, _entity.XLoc, _entity.YLoc, newX, newY);
				return true;
			}
			return false;
		}
	}
}
