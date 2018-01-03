using System;
using System.Collections.Generic;
using System.IO;

namespace Halfbreed
{
	[Serializable]
	public class Level
	{

		private int _height;
		private int _width;
		private TileType[] _mapGrid;
		private bool[] _revealed;

		private Dictionary<int, List<Entities.Entity>> _entities;
		private Dictionary<int, List<Furnishing>> _furnishings;

		public Level(string LevelFilePath)
		{
			// TODO: Move this to a separate function to allow easy switching to resources.
			FileStream mapFile = File.Open(LevelFilePath + "Map.txt", FileMode.Open);
			StreamReader MapSpecificationFile = new StreamReader(mapFile);

			Dictionary<int, TileType> tileDict = new Dictionary<int, TileType>();

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

			_entities = new Dictionary<int, List<Entities.Entity>>();
			_furnishings = new Dictionary<int, List<Furnishing>>();

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

		public void AddFurnishing(int x, int y, Furnishing furnishing)
		{
			int index = ConvertXYToInt(x, y);
			if (_furnishings.ContainsKey(index))
				_furnishings[index].Add(furnishing);
			else
				_furnishings[index] = new List<Furnishing>() { furnishing };
			
			AddEntity(index, furnishing);
		}

		public void RemoveFurnishing(int x, int y, Furnishing furnishing)
		{
			int index = ConvertXYToInt(x, y);
			if (!_furnishings.ContainsKey(index) || !_furnishings[index].Contains(furnishing))
			{
				ErrorLogger.AddDebugText(string.Format("Tried to remove non-existant furnishing {0} at index {1}", furnishing, index));
				return;
			}
			if (_furnishings[index].Count == 1)
				_furnishings.Remove(index);
			else
				_furnishings[index].Remove(furnishing);

			RemoveEntity(index, furnishing);
		}

		private void AddEntity(int index, Entities.Entity entity)
		{
			if (_entities.ContainsKey(index))
			{
				_entities[index].Add(entity);
				_entities[index].Sort();
			}
			else
				_entities[index] = new List<Entities.Entity>() { entity };
			
		}

		private void RemoveEntity(int index, Entities.Entity entity)
		{
			if (_entities.ContainsKey(index))
			{
				if (_entities[index].Contains(entity))
				{
					if (_entities[index].Count == 1)
						_entities.Remove(index);
					else
					{
						_entities[index].Remove(entity);
						_entities[index].Sort();
					}
					return;
				}
			}
			ErrorLogger.AddDebugText(String.Format("Tried to remove non-existant Entity: {0} at index {1}", entity, index)); 
		}

		public bool HasEntity(int x, int y)
		{
			return _entities.ContainsKey(ConvertXYToInt(x, y));
		}

		public List<Entities.Entity> GetEntities(int x, int y)
		{
			List<Entities.Entity> returnList = new List<Entities.Entity>();

			if (_entities.ContainsKey(ConvertXYToInt(x, y)))
			{
				foreach (Entities.Entity entity in _entities[ConvertXYToInt(x, y)])
					returnList.Add(entity);
			}

			return returnList;
		}
	}
}
