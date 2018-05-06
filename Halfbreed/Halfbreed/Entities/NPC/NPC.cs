using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class NPC:Actor
	{
		static SortedDictionary<int, NPC> npcs = new SortedDictionary<int, NPC>();
		static int maxNpcId = 1;
		static List<int> unusedIds = new List<int>();

		readonly int _npcId;

		public NPC(string npcName, int xLoc, int yLoc, List<string> otherParameters)
			:base(npcName, xLoc, yLoc, otherParameters)
		{
			if (unusedIds.Count > 0)
			{
				_npcId = unusedIds[0];
				unusedIds.RemoveAt(0);
			}
			else
			{
				_npcId = maxNpcId;
				maxNpcId++;
			}
			npcs[_npcId] = this;
		}

		public int NpcId
		{
			get { return _npcId; }
		}

		public static NPC GetNpc(int id)
		{
			return npcs[id];
		}

		public static NPCSave GetSaveData()
		{
			return new NPCSave(npcs, maxNpcId, unusedIds);
		}

		public static void LoadSaveData(NPCSave save)
		{
			npcs = save.Npcs;
			maxNpcId = save.MaxId;
			unusedIds = save.UnusedIds;
		}
	}
}
