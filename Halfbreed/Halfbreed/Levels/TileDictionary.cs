using System.Collections.Generic;
using System;

namespace Halfbreed.Levels
{
	public static class TileDictionary
	{
		static Dictionary<TileType, MapTileDetails> _tileDictionary = new Dictionary<TileType, MapTileDetails>()
		{
			{TileType.OpenWater, new MapTileDetails("Open Water", 0, "WaterBlue", "DarkWater", "", "", 
			                                        new string[]{"BlockWalk"})},
			{TileType.WoodenDebris, new MapTileDetails("Wooden Debris", 20, "DarkBrown", "DarkWoodBrown", null, null, 
			                                           new string[]{"Impassible", "BlockLOS"})},
			{TileType.WoodFloor, new MapTileDetails("Wood Floor", 0, "WoodBrown", "WoodFog", null, null, 
			                                        new string[]{"BlockSwim"})},
			{TileType.WoodWall, new MapTileDetails("Wood Wall", 20, "DarkWoodBrown", "DarkBrown", null, null, 
			                                       new string[]{"Impassible", "BlockLOS"})}
		};

		public static MapTileDetails getTileDetails(TileType tileType)
		{
			return _tileDictionary[tileType];
		}

		public static MapTileDetails getTileDetails(string tileName)
		{
			return _tileDictionary[(TileType)Enum.Parse(typeof(TileType), tileName)];
		}

		public static int NumberOfTiles()
		{
			return _tileDictionary.Count;
		}
	}
}
