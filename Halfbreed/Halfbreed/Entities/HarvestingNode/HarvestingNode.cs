// Tidied up for Version 0.2. - No changes.

using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class HarvestingNode:Entity
	{

		public HarvestingNode(string harvestableName, int xLoc, int yLoc, List<string> otherParameters)
			:base(harvestableName, xLoc, yLoc, otherParameters)
		{
			
		}
	}
}
