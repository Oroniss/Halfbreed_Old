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

		public LevelMap(string mapFilePath)
		{
			FileStream mapFile = File.Open(mapFilePath, FileMode.Open);
			StreamReader MapSpecificationFile = new StreamReader(mapFile);

			Dictionary<int, TileType > tileDict = new Dictionary<int, TileType>();

			_height = Int32.Parse(MapSpecificationFile.ReadLine());
			_width = Int32.Parse(MapSpecificationFile.ReadLine());

			while (true)
			{
				string line = MapSpecificationFile.ReadLine().Trim();
				if (line == "###")
					break;

				string[] splitLine = line.Split(',');
				tileDict[Int32.Parse(splitLine[0])] = (TileType)Enum.Parse(typeof(TileType), splitLine[1]);
			}

			_mapGrid = new TileType[_width * _height];
			for (int y = 0; y < _height; y++)
			{
				string[] _row = MapSpecificationFile.ReadLine().Trim().Split(',');
				for (int x = 0; x < _width; x++)
				{
					_mapGrid[y * _width + x] = tileDict[Int32.Parse(_row[x])];
				}
			}

			mapFile.Close();
		}
	}
}
