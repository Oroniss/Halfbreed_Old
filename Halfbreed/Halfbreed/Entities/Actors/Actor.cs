// Tidied up for version 0.02.

using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class Actor:Entity
	{
		readonly int _viewDistance;

		public Actor(string actorName, int xLoc, int yLoc, List<string> otherParameters)
			:base(actorName, xLoc, yLoc, otherParameters)
		{
			AddTrait(Traits.Impassible);
			_viewDistance = 18; // Fix this properly later on.
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

		public int ViewDistance
		{
			get { return _viewDistance; }
		}
	}
}
