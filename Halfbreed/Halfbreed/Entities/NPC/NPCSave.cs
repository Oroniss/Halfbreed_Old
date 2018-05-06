using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class NPCSave
	{
		public SortedDictionary<int, NPC> Npcs;
		public int MaxId;
		public List<int> UnusedIds;

		public NPCSave(SortedDictionary<int, NPC> npcs, int maxId, List<int> unusedIds)
		{
			Npcs = npcs;
			MaxId = maxId;
			UnusedIds = unusedIds;
		}
	}
}
