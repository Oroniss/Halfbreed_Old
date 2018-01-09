using System;
using System.Collections.Generic;
using System.IO;

namespace Halfbreed
{
	[Serializable]
	public class Level
	{

		private string _levelTitle;
		private int _height;
		private int _width;
		private MapTileDetails[] _mapGrid;
		private bool[] _revealed;

		private int _visibility;
		private float _anathemaMultiplier;

		private Dictionary<int, List<Entities.Entity>> _entities;
		private Dictionary<int, List<Furnishing>> _furnishings;

		public Level(string LevelFilePath)
		{
			// TODO: Move this to a separate function to allow easy switching to resources.
			FileStream levelStream = File.Open(LevelFilePath + ".txt", FileMode.Open);
			StreamReader LevelSpecificationFile = new StreamReader(levelStream);

			Dictionary<int, TileType> tileDict = new Dictionary<int, TileType>();

			_levelTitle = LevelSpecificationFile.ReadLine();
			_height = Int32.Parse(LevelSpecificationFile.ReadLine());
			_width = Int32.Parse(LevelSpecificationFile.ReadLine());

			_visibility = Int32.Parse(LevelSpecificationFile.ReadLine());
			_anathemaMultiplier = float.Parse(LevelSpecificationFile.ReadLine());

			_entities = new Dictionary<int, List<Entities.Entity>>();
			_furnishings = new Dictionary<int, List<Furnishing>>();
			

			LevelSpecificationFile.ReadLine(); // Move to start of dictionary.

			while (true)
			{
				string line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				string[] splitLine = line.Split(',');
				tileDict[Int32.Parse(splitLine[0])] = (TileType)Enum.Parse(typeof(TileType), splitLine[1]);
			}

			_mapGrid = new MapTileDetails[_width * _height];
			_revealed = new bool[_width * _height];

			for (int y = 0; y < _height; y++)
			{
				string[] _row = LevelSpecificationFile.ReadLine().Trim().Split(',');
				for (int x = 0; x < _width; x++)
				{
					_mapGrid[y * _width + x] = StaticData.GetMapTileDetails(tileDict[Int32.Parse(_row[x])]);
					_revealed[y * _width + x] = false;
				}
			}

			// Remove the next break line
			LevelSpecificationFile.ReadLine();
			// TODO: Throw an exception if it isn't "###"


			// Furnishings
			const int FURNISHINGPREFIXLENGTH = 4;
			while (true)
			{
				string line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
				   break;

				string[] splitLine = line.Split(',');
				string furnishingName = splitLine[0];
				Materials material = EnumConverter.ConvertStringToMaterial(splitLine[1]);
				int xLoc = Int32.Parse(splitLine[2]);
				int yLoc = Int32.Parse(splitLine[3]);
				string[] otherParams = new string[splitLine.Length - FURNISHINGPREFIXLENGTH];
				for (int i = 0; i < splitLine.Length - FURNISHINGPREFIXLENGTH; i++)
					otherParams[i] = splitLine[i + FURNISHINGPREFIXLENGTH];
				Furnishing newFurnishing = Entities.FurnishingFactory.CreateFurnishing(furnishingName, material,
																					   xLoc, yLoc, otherParams);
				AddFurnishing(xLoc, yLoc, newFurnishing);
			}

			levelStream.Close();

		}

		private bool LineIsBreakPoint(string line)
		{
			return line == "###";
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

		public Colors GetBGColor(int x, int y)
		{
			return _mapGrid[ConvertXYToInt(x, y)].BGColor;
		}

		public Colors GetFogColor(int x, int y)
		{
			return _mapGrid[ConvertXYToInt(x, y)].FogColor;
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

		public List<Furnishing> GetFurnishings(int x, int y)
		{
			List<Furnishing> returnList = new List<Furnishing>() { };

			if (_furnishings.ContainsKey(ConvertXYToInt(x, y)))
			{
				foreach (Furnishing furnishing in _furnishings[ConvertXYToInt(x, y)])
					returnList.Add(furnishing);
			}

			return returnList;

		}

		public Entities.Entity GetDrawingEntity(int x, int y)
		{
			if(_entities.ContainsKey(ConvertXYToInt(x, y)))
				return _entities[ConvertXYToInt(x, y)][0];
			// TODO: Throw an exception.
			return null;
		}
	}
}
