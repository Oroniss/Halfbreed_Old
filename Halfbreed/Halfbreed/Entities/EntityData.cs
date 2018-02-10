﻿using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public static class EntityData
	{
		private static Dictionary<string, EntityBasicDetails> _entityBasicDetails = new Dictionary<string, EntityBasicDetails>()
		{
			// Furnishings
			{"Broken Wooden Barrel", new EntityBasicDetails('/', "DarkWoodBrown", new string[]{"Ruined"})},
			{"Broken Wooden Chest", new EntityBasicDetails('/', "DarkWoodBrown", new string[]{"Ruined"})},
			{"Broken Wooden Door", new EntityBasicDetails('/', "DarkWoodBrown", new string[]{"Ruined"})},
			{"Pallet", new EntityBasicDetails('.', "DarkWoodBrown", new string[]{})},
			{"Tin Brazier", new EntityBasicDetails('*', "Tin", new string[]{})},
			{"Wooden Barrel", new EntityBasicDetails('#', "DarkWoodBrown", new string[]{})},
			{"Wooden Chest", new EntityBasicDetails('#', "DarkWoodBrown", new string[]{})},
			{"Wooden Door", new EntityBasicDetails('-', "DarkWoodBrown", new string[]{})},
			{"Wooden Ladder", new EntityBasicDetails('\\', "DarkWoodBrown", new string[]{"ElevationChange"})},
			{"Wooden Platform", new EntityBasicDetails('_', "DarkWoodBrown", new string[]{})},
			{"Wooden Stair Down", new EntityBasicDetails('<', "DarkWoodBrown", new string[]{})},

			// Harvesting Nodes
			{"Rat Den", new EntityBasicDetails(':', "Tan", new string[]{"Organic", "Animal"})},
			{"Green Mold", new EntityBasicDetails('}', "PutridGreen", new string[]{"Organic", "Plant"})},

			// Actors

			// Special Entities
			{"Player", new EntityBasicDetails('@', "Black", new string[]{"Player", "Walking"})}
		};

		public static EntityBasicDetails GetEntityDetails(string entityName)
		{
			return _entityBasicDetails[entityName];
		}

		private static Dictionary<string, FurnishingDetails> _furnishingDetails = new Dictionary<string, FurnishingDetails>()
		{
			{"Default", new FurnishingDetails("", "", 0, "Default Furnishing Setup")},
			// Regular furnishings
			{"Pallet", new FurnishingDetails("RedBrown", "DarkWoodBrown", 1, "Default Furnishing Setup")},
			{"Wooden Door", new FurnishingDetails("", "", 0, "Door Setup")},
			{"Wooden Platform", new FurnishingDetails("DarkBrown", "DarkWoodBrown", 2, "Default Furnishing Setup")}
		};

		public static FurnishingDetails GetFurnishingDetails(string furnishingName)
		{
			if (_furnishingDetails.ContainsKey(furnishingName))
				return _furnishingDetails[furnishingName];
			return _furnishingDetails["Default"];
		}
	}
}
