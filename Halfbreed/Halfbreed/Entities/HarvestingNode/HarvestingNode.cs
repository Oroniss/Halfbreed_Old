using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class HarvestingNode:Entity
	{

		static SortedDictionary<int, HarvestingNode> harvestingNodes = new SortedDictionary<int, HarvestingNode>();
		static int maxHarvestingId = 0;
		static List<int> unusedHarvestingIds = new List<int>();

		int _harvestingId;

		public HarvestingNode(string harvestableName, int xLoc, int yLoc, List<string> otherParameters)
			:base(harvestableName, xLoc, yLoc, otherParameters)
		{
			if (unusedHarvestingIds.Count > 0)
			{
				_harvestingId = unusedHarvestingIds[0];
				unusedHarvestingIds.RemoveAt(0);
			}
			else
			{
				_harvestingId = maxHarvestingId;
				maxHarvestingId++;
			}
			harvestingNodes[_harvestingId] = this;
		}

		public int HarvestingId
		{
			get { return _harvestingId; }
		}

		public static HarvestingNode GetHarvestingNode(int id)
		{
			return harvestingNodes[id];
		}
	}
}
