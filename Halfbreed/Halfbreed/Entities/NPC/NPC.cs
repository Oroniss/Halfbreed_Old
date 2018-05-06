using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class NPC:Actor
	{
		static SortedDictionary<int, NPC> _npcs = new SortedDictionary<int, NPC>();
		static int _maxNpcId = 1;
		static List<int> _unusedNpcIds = new List<int>();

		readonly int _npcId;

		public NPC(string npcName, int xLoc, int yLoc, List<string> otherParameters)
			:base(npcName, xLoc, yLoc, otherParameters)
		{
		}

		public int NpcId
		{
			get { return _npcId; }
		}

		public static NPC GetNpc(int id)
		{
			return _npcs[id];
		}

	}
}
