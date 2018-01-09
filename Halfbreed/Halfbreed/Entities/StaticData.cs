using System.Collections.Generic;

namespace Halfbreed
{
	public static class StaticData
	{
		private static Dictionary<Materials, MaterialProperties> _materialProperties;
		private static Dictionary<TileType, MapTileDetails> _mapTiles;


		public static MaterialProperties GetProperties(Materials material)
		{
			return _materialProperties[material];
		}

		public static int GetNumberOfMaterials()
		{
			return _materialProperties.Count;
		}

		// Note reference to actual tile stored here - using Flyweight pattern.
		public static MapTileDetails GetMapTileDetails(TileType tileType)
		{
			return _mapTiles[tileType];
		}

		public static int GetNumberOfTiles()
		{
			return _mapTiles.Count;
		}

		public static void SetupDictionaries()
		{
			_materialProperties = EntityDatabaseConnection.GetMaterialProperties();
			_mapTiles = EntityDatabaseConnection.GetMapTiles();
		}

	}
}
