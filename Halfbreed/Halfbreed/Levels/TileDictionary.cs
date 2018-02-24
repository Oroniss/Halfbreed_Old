// Revised for version 0.2.

using System.Collections.Generic;

namespace Halfbreed.Levels
{
	public static class TileDictionary
	{
		static Dictionary<TileType, MapTileDetails> _tileDictionary = new Dictionary<TileType, MapTileDetails>()
		{
			// TODO: Add functions to OpenWater
			{TileType.OpenWater, new MapTileDetails(TileType.OpenWater, "Open Water", 0, "WaterBlue", "DarkWater", 
			 null, null, new string[]{"BlockWalk"})},
			{TileType.WoodenDebris, new MapTileDetails(TileType.WoodenDebris, "Wooden Debris", 20, "DarkBrown", 
			 "DarkWoodBrown", null, null, new string[]{"Impassible", "BlockLOS"})},
			{TileType.WoodFloor, new MapTileDetails(TileType.WoodFloor, "Wood Floor", 0, "WoodBrown", "WoodFog", 
			 null, null, new string[]{"BlockSwim"})},
			{TileType.WoodWall, new MapTileDetails(TileType.WoodWall, "Wood Wall", 20, "DarkWoodBrown", "DarkBrown", 
			 null, null, new string[]{"Impassible", "BlockLOS"})}
		};

		public static MapTileDetails getTileDetails(TileType tileType)
		{
			return _tileDictionary[tileType];
		}

		public static MapTileDetails getTileDetails(string tileName)
		{
			return _tileDictionary[(TileType)System.Enum.Parse(typeof(TileType), tileName)];
		}

		public static int NumberOfTiles()
		{
			return _tileDictionary.Count;
		}
	}
}
