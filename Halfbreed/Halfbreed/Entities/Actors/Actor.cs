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

		public void UpdatePosition(int newX, int newY)
		{
			_xLoc = newX;
			_yLoc = newY;
		}

		public override void Update(Level currentLevel)
		{
			base.Update(currentLevel);

			if (!_destroyed)
				GetNextMove(currentLevel);
		}
	}
}
