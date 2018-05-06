using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class FurnishingSave
	{
		public SortedDictionary<int, Furnishing> Furnishings;
		public int MaxId;
		public List<int> UnusedIds;

		public FurnishingSave(SortedDictionary<int, Furnishing> furnishings, int maxId, List<int> unusedIds)
		{
			Furnishings = furnishings;
			MaxId = maxId;
			UnusedIds = unusedIds;
		}
	}
}
