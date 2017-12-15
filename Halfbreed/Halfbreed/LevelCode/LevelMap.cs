using System.IO;
using System;
using System.Collections.Generic;
namespace Halfbreed
{
	public class LevelMap
	{
		private int _height;
		private int _width;
		private TileType[] _mapGrid;
		private bool[] _revealed;

		private Dictionary<int, List<int>> _entities;

		public LevelMap(string mapFilePath)
		{
			// TODO: Move this to a separate function to allow easy switching to resources.
			FileStream mapFile = File.Open(mapFilePath, FileMode.Open);
			StreamReader MapSpecificationFile = new StreamReader(mapFile);

			Dictionary<int, TileType > tileDict = new Dictionary<int, TileType>();

			_height = Int32.Parse(MapSpecificationFile.ReadLine());
			_width = Int32.Parse(MapSpecificationFile.ReadLine());

			MapSpecificationFile.ReadLine(); // Move to start of dictionary.

			while (true)
			{
				string line = MapSpecificationFile.ReadLine().Trim();
				if (line == "###")
					break;

				string[] splitLine = line.Split(',');
				tileDict[Int32.Parse(splitLine[0])] = (TileType)Enum.Parse(typeof(TileType), splitLine[1]);
			}

			_mapGrid = new TileType[_width * _height];
			_revealed = new bool[_width * _height];

			for (int y = 0; y < _height; y++)
			{
				string[] _row = MapSpecificationFile.ReadLine().Trim().Split(',');
				for (int x = 0; x < _width; x++)
				{
					_mapGrid[y * _width + x] = tileDict[Int32.Parse(_row[x])];
					_revealed[y * _width + x] = false;
				}
			}

			mapFile.Close();

			_entities = new Dictionary<int, List<int>>();
		}

		public int Height
		{
			get { return _height; }
		}
		public int Width
		{
			get { return _width; }
		}

		private int ConvertXYToInt(int x, int y)
		{
			return y * Width + x;
		}

		public bool IsValidMapCoord(int x, int y)
		{
			return x >= 0 && x < _width && y >= 0 && y < _height;
		}

		public TileType GetTile(int x, int y)
		{
			return _mapGrid[ConvertXYToInt(x, y)];
		}

		public bool isRevealed(int x, int y)
		{
			return _revealed[ConvertXYToInt(x, y)];
		}
		public void revealTile(int x, int y)
		{
			_revealed[ConvertXYToInt(x, y)] = true;
		}

		public List<int> getEntities(int x, int y)
		{
			List<int> returnList = new List<int>();

			if (IsValidMapCoord(x, y) && _entities.ContainsKey(ConvertXYToInt(x, y)))
			{
				foreach (int i in _entities[ConvertXYToInt(x, y)])
					returnList.Add(i);
			}

			return returnList;
		}

		public void addEntity(int x, int y, int entityId)
		{
			if (IsValidMapCoord(x, y))
			{
				if (!_entities.ContainsKey(ConvertXYToInt(x, y)))
					_entities[ConvertXYToInt(x, y)] = new List<int>();
				_entities[ConvertXYToInt(x, y)].Add(entityId);
			}
			else
				ErrorLogger.AddDebugText(String.Format("Tried to add entity at invalid map coord <{0}, {1}>", x, y));
		}

		public void removeEntity(int x, int y, int entityId)
		{
			if (IsValidMapCoord(x, y))
			{
				if (_entities.ContainsKey(ConvertXYToInt(x, y)) && _entities[ConvertXYToInt(x, y)].Contains(entityId))
				{
					_entities[ConvertXYToInt(x, y)].Remove(entityId);
					if (_entities[ConvertXYToInt(x, y)].Count == 0)
						_entities.Remove(ConvertXYToInt(x, y));
				}
				else
					ErrorLogger.AddDebugText(String.Format("Tried to remove entity {0} at map coord <{1}, {2}>" +
														   " but key or entity not present", entityId, x, y));

			}
			else
				ErrorLogger.AddDebugText(String.Format("Tried to remove entity at invalid map coord <{0}, {1}>", x, y));
		}

	}
}
