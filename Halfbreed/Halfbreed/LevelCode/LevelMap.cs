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
		}

		public int Height
		{
			get { return _height; }
		}
		public int Width
		{
			get { return _width; }
		}
		public bool IsValidMapCoord(int x, int y)
		{
			return x >= 0 && x < _width && y >= 0 && y < _height;
		}

		public TileType GetTile(int x, int y)
		{
			return _mapGrid[y * _width + x];
		}

		public bool isRevealed(int x, int y)
		{
			return _revealed[y * _width + x];
		}
		public void revealTile(int x, int y)
		{
			_revealed[y * _width + x] = true;
		}

	}
}
