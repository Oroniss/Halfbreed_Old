using System.Collections.Generic;
using System;

namespace Halfbreed.Entities.Furnishings
{
	public static class FurnishingSetupFunctions
	{
		static readonly Dictionary<string, FurnishingSetupFunction> _setupFunctions = new Dictionary<string, FurnishingSetupFunction>()
		{
			// The default setup
			{"Default Furnishing Setup", new FurnishingSetupFunction(DefaultFurnishingSetup)},

			// Other setup functions that can be added to any furnishing
			{"Level Transition Setup", new FurnishingSetupFunction(LevelTransitionSetup)},
			{"Interaction Trap Setup", new FurnishingSetupFunction(InteractionTrapSetup)},
			{"Concealed Furnishing Setup", new FurnishingSetupFunction(ConcealedFurnishingSetup)},

			// Furnishing specific functions
			{"Wooden Door", new FurnishingSetupFunction(DoorSetup)},

			// Traps
			{"Pit Trap", new FurnishingSetupFunction(MovementTrapSetup)},
			{"Flame Vent Trap", new FurnishingSetupFunction(MovementTrapSetup)}
		};

		public delegate void FurnishingSetupFunction(Furnishing furnishing, List<string> otherParameters);

		public static FurnishingSetupFunction GetSetupFunction(string furnishingName)
		{
			if (_setupFunctions.ContainsKey(furnishingName))
				return _setupFunctions[furnishingName];
			return _setupFunctions["Default Furnishing Setup"];
		}

		private static void DefaultFurnishingSetup(Furnishing furnishing, List<string> otherParameters)
		{
		}

		private static void DoorSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.AddInteractionFunction("Door Use");

			if (otherParameters.Contains("Open"))
				furnishing.SetOtherAttribute("DoorOpen", "Open");
			else
			{
				furnishing.SetOtherAttribute("DoorOpen", "Closed");
				furnishing.AddTrait(Traits.Impassible);
				furnishing.AddTrait(Traits.BlockLOS);
				furnishing.Symbol = '+';
			}
		}

		private static void LevelTransitionSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.AddInteractionFunction("Level Transition Use");
			furnishing.SetOtherAttribute("DestinationLevel", otherParameters[otherParameters.IndexOf("DestinationLevel") + 1]);
			furnishing.SetOtherAttribute("NewXLoc", otherParameters[otherParameters.IndexOf("NewXLoc") + 1]);
			furnishing.SetOtherAttribute("NewYLoc", otherParameters[otherParameters.IndexOf("NewYLoc") + 1]);
		}

		private static void InteractionTrapSetup(Furnishing furnishing, List<string> otherParameters)
		{
			var trapType = otherParameters[otherParameters.IndexOf("TrapType") + 1];
			furnishing.AddInteractionFunction(string.Format("Trigger {0} Trap", trapType));
			furnishing.SetOtherAttribute("TrapLevel", otherParameters[otherParameters.IndexOf("TrapLevel") + 1]);
			furnishing.Trapped = true;
		}

		private static void MovementTrapSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.MoveOnFunction = string.Format("{0} Move On", furnishing.EntityName);
			furnishing.SetOtherAttribute("TrapLevel", otherParameters[otherParameters.IndexOf("TrapLevel") + 1]);
			furnishing.Trapped = true;
		}

		private static void ConcealedFurnishingSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.Concealed = true;
			furnishing.PlayerSpotted = false;
			furnishing.SetOtherAttribute("ConcealmentLevel", otherParameters[otherParameters.IndexOf("ConcealmentLevel") + 1]);

			if (otherParameters.Contains("ConcealedTile"))
			{
				var tileName = otherParameters[otherParameters.IndexOf("ConcealedTile") + 1];
				var tileDetails = Levels.TileDictionary.getTileDetails(tileName);
				furnishing.SetOtherAttribute("ConcealedBGColor", tileDetails.BGColor.ToString());
				furnishing.SetOtherAttribute("ConcealedFogColor", tileDetails.FogColor.ToString());
			}
		}
	}

}
