using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	public static class EntityFactory
	{
		private static Dictionary<string, FurnishingTemplate> _furnishings = new Dictionary<string, FurnishingTemplate>();
		private static Dictionary<string, HarvestingTemplate> _harvestingNodes = new Dictionary<string, HarvestingTemplate>();


		public static Entity CreateFurnishing(string furnishingName, Materials material, int xLoc, int yLoc, string[] otherParams)
		{
			if (!_furnishings.ContainsKey(furnishingName))
				_furnishings[furnishingName] = StaticDatabaseConnection.GetFurnishingDetails(furnishingName);

			Entity newFurnishing = new Entity(furnishingName, material, _furnishings[furnishingName],
											  xLoc, yLoc, otherParams);
			return newFurnishing;
		}

		public static Entity CreateHarvestingNode(string harvestingName, int xLoc, int yLoc)
		{
			if (!_harvestingNodes.ContainsKey(harvestingName))
				_harvestingNodes[harvestingName] = StaticDatabaseConnection.GetHarvestingDetails(harvestingName);

			Entity newHarvestible = new Entity(harvestingName, xLoc, yLoc, _harvestingNodes[harvestingName].NodeType);

			return newHarvestible;
		}

		public static Entity CreateMovementTrap(string trapName, int xLoc, int yLoc, string[] otherParams)
		{
			int trapLevel = int.Parse(otherParams[Array.IndexOf(otherParams, "TrapLevel") + 1]);
			Entity newTrap = new Entity(trapName, xLoc, yLoc, trapLevel, otherParams);

			return newTrap;
		}
	}
}
