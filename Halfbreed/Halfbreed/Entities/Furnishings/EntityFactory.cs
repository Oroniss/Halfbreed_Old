using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public static class EntityFactory
	{
		private static Dictionary<string, FurnishingTemplate> _furnishings = new Dictionary<string, FurnishingTemplate>();
		private static Dictionary<string, HarvestingTemplate> _harvestingNodes = new Dictionary<string, HarvestingTemplate>();



		public static Entity CreateFurnishing(string furnishingName, Materials material, int xLoc, int yLoc, string[] otherParams)
		{
			if (!_furnishings.ContainsKey(furnishingName))
				_furnishings[furnishingName] = EntityDatabaseConnection.GetFurnishingDetails(furnishingName);

			Entity newFurnishing = new Entity(furnishingName, material, _furnishings[furnishingName],
											  xLoc, yLoc, otherParams);
			return newFurnishing;
		}

		public static Entity CreateHarvestingNode(string harvestingName, int xLoc, int yLoc)
		{
			if (!_harvestingNodes.ContainsKey(harvestingName))
				_harvestingNodes[harvestingName] = EntityDatabaseConnection.GetHarvestingDetails(harvestingName);

			Entity newHarvestible = new Entity(harvestingName, xLoc, yLoc, _harvestingNodes[harvestingName].NodeType);

			return newHarvestible;
		}
	}
}
