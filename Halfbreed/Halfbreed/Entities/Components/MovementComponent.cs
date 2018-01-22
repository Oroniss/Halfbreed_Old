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
			if (_entity.HasTrait(EntityTraits.CANFLY) && GameEngine.CurrentLevel.IsFlyable(newX, newY))
			{
				GameEngine.CurrentLevel.FlyAttempt(_entity, _entity.XLoc, _entity.YLoc, newX, newY);
				return true;
			}
			if (_entity.HasTrait(EntityTraits.CANWALK) && GameEngine.CurrentLevel.IsWalkable(newX, newY))
			{
				GameEngine.CurrentLevel.WalkAttempt(_entity, _entity.XLoc, _entity.YLoc, newX, newY);
				return true;
			}
			if (_entity.HasTrait(EntityTraits.CANSWIM) && GameEngine.CurrentLevel.IsSwimmable(newX, newY))
			{
				GameEngine.CurrentLevel.SwimAttempt(_entity, _entity.XLoc, _entity.YLoc, newX, newY);
				return true;
			}
			return false;
		}
	}
}
