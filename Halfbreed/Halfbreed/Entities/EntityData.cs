using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public static class EntityData
	{
		private static Dictionary<Materials, MaterialProperties> _materialProperties;


		public static MaterialProperties GetProperties(Materials material)
		{
			return _materialProperties[material];
		}

		public static void SetupDictionaries()
		{
			_materialProperties = EntityDatabaseConnection.GetMaterialProperties();
		}

	}
}
