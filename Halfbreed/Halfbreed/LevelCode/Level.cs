using System;
using System.Collections.Generic;

namespace Halfbreed
{
	[Serializable]
	public class Level
	{

		private LevelMap _map;


		public Level(string levelID)
		{
			_map = new LevelMap(levelID + "Map.txt");
		}

		public LevelMap Map
		{
			get { return _map; }
		}

	}
}
