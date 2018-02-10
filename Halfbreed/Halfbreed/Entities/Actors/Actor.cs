using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class Actor:Entity
	{
		public Actor(string actorName, int xLoc, int yLoc, List<string> otherParameters)
			:base(actorName, xLoc, yLoc, otherParameters)
		{
			
		}

		protected virtual void GetNextMove(Level currentLevel)
		{
			
		}

		public override void Update(Level currentLevel)
		{
			base.Update(currentLevel);

			if (!_destroyed)
				GetNextMove(currentLevel);
		}
	}
}
