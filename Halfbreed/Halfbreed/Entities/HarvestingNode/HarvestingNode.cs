// Tidied up for Version 0.02.

using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class HarvestingNode:Entity
	{

		public HarvestingNode(string harvestableName, int xLoc, int yLoc, List<string> otherParameters)
			:base(harvestableName, xLoc, yLoc, otherParameters)
		{
			
		}
	}
}
