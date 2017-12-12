using System;

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


		public int MapWidth
		{
			get { return _map.Width; }
		}

		public int MapHeight
		{
			get { return _map.Height; }
		}

		public bool IsValidMapCoord(int x, int y)
		{
			return _map.IsValidMapCoord(x, y);
		}

		public TileType getMaptile(int x, int y)
		{
			// TODO: Add in check once furnishings go in.
			return _map.GetTile(x, y);
		}
	}
}
