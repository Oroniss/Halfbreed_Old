using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public static class EntityFactory
	{
		private static Dictionary<string, FurnishingTemplate> _furnishings = new Dictionary<string, FurnishingTemplate>();



		public static Entity CreateFurnishing(string furnishingName, Materials material, int xLoc, int yLoc, string[] otherParams)
		{
			if (!_furnishings.ContainsKey(furnishingName))
				_furnishings[furnishingName] = EntityDatabaseConnection.GetFurnishingDetails(furnishingName);

			Entity newFurnishing = new Entity(furnishingName, material, _furnishings[furnishingName],
											  xLoc, yLoc, otherParams);
			return newFurnishing;
		}
	}
}
