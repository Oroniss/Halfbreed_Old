using System.Collections.Generic;

namespace Halfbreed
{
	public static class StaticData
	{
		private static Dictionary<Materials, MaterialProperties> _materialProperties;
		private static Dictionary<Levels.TileType, Levels.MapTileDetails> _mapTiles;


		public static MaterialProperties GetProperties(Materials material)
		{
			return _materialProperties[material];
		}

		public static int GetNumberOfMaterials()
		{
			return _materialProperties.Count;
		}

		// Note reference to actual tile stored here - using Flyweight pattern.
		public static Levels.MapTileDetails GetMapTileDetails(Levels.TileType tileType)
		{
			return _mapTiles[tileType];
		}

		public static int GetNumberOfTiles()
		{
			return _mapTiles.Count;
		}

		public static void SetupDictionaries()
		{
			_materialProperties = StaticDatabaseConnection.GetMaterialProperties();
			_mapTiles = StaticDatabaseConnection.GetMapTiles();
		}

	}
}
