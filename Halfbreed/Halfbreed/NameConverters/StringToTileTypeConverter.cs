﻿using System.Collections.Generic;

namespace Halfbreed.Converters
{
	public class StringToTileTypeConverter
	{
		private static Dictionary<string, TileType> _stringToTiles = new Dictionary<string, TileType>()
		{
			{"Wood Floor", TileType.WOODFLOOR},
			{"Wood Wall", TileType.WOODWALL},
			{"Wooden Debris", TileType.WOODENDEBRIS},
			{"Pallet", TileType.PALLET},
			{"Platform", TileType.PLATFORM}
		};

		public TileType ConvertStringToTileType(string tileName)
		{
			return _stringToTiles[tileName];
		}

		public static int GetNumberOfTiles()
		{
			return _stringToTiles.Count;
		}
	}
}
