using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class PlayerSave
	{
		public SortedDictionary<int, Player> Players;
		public int MaxId;
		public List<int> UnusedIds;

		public PlayerSave(SortedDictionary<int, Player> players, int maxId, List<int> unusedIds)
		{
			Players = players;
			MaxId = maxId;
			UnusedIds = unusedIds;
		}
	}
}
