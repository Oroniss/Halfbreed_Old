using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class HarvestingNodeSave
	{
		public SortedDictionary<int, HarvestingNode> HarvestingNodes;
		public int MaxId;
		public List<int> UnusedIds;

		public HarvestingNodeSave(SortedDictionary<int, HarvestingNode> harvestingNodes,
								  int maxId, List<int> unusedIds)
		{
			HarvestingNodes = harvestingNodes;
			MaxId = maxId;
			UnusedIds = unusedIds;
		}
	}
}
