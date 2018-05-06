using System.Collections.Generic;

namespace Halfbreed.Entities.Furnishings
{
	public static class FurnishingSetupFunctions
	{
		static readonly Dictionary<string, FurnishingSetupFunction> _setupFunctions = 
			new Dictionary<string, FurnishingSetupFunction>
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

		static string GetValueOf(string parameterName, List<string> otherParameters)
		{
			return otherParameters[otherParameters.IndexOf(parameterName) + 1];
		}

		static void DefaultFurnishingSetup(Furnishing furnishing, List<string> otherParameters)
		{
		}

		static void DoorSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.AddInteractionFunction("Door Use");

			if (otherParameters.Contains("Open"))
				furnishing.SetOtherAttribute("DoorOpen", "Open");
			else
			{
				furnishing.SetOtherAttribute("DoorOpen", "Closed");
				furnishing.AddTrait("Impassible");
				furnishing.AddTrait("BlockLOS");
				furnishing.Symbol = '+';
			}
		}

		static void LevelTransitionSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.AddInteractionFunction("Level Transition Use");
			furnishing.SetOtherAttribute("DestinationLevel", GetValueOf("DestinationLevel", otherParameters));
			furnishing.SetOtherAttribute("NewXLoc", GetValueOf("NewXLoc", otherParameters));
			furnishing.SetOtherAttribute("NewYLoc", GetValueOf("NewYLoc", otherParameters));
		}

		static void InteractionTrapSetup(Furnishing furnishing, List<string> otherParameters)
		{
			var trapType = GetValueOf("TrapType", otherParameters);
			furnishing.AddInteractionFunction(string.Format("Trigger {0} Trap", trapType));
			furnishing.SetOtherAttribute("TrapLevel", GetValueOf("TrapLevel", otherParameters));
			furnishing.Trapped = true;
		}

		static void MovementTrapSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.MoveOnFunction = string.Format("{0} Move On", furnishing.EntityName);
			furnishing.SetOtherAttribute("TrapLevel", GetValueOf("TrapLevel", otherParameters));
			furnishing.Trapped = true;
		}

		static void ConcealedFurnishingSetup(Furnishing furnishing, List<string> otherParameters)
		{
			furnishing.Concealed = true;
			furnishing.PlayerSpotted = false;
			furnishing.SetOtherAttribute("ConcealmentLevel", GetValueOf("ConcealmentLevel", otherParameters));

			if (otherParameters.Contains("ConcealedTile"))
			{
				var tileName = GetValueOf("ConcealedTile", otherParameters);
				var tileDetails = Levels.TileDictionary.getTileDetails(tileName);
				furnishing.SetOtherAttribute("ConcealedBGColor", tileDetails.BGColorName.ToString());
				furnishing.SetOtherAttribute("ConcealedFogColor", tileDetails.FogColorName.ToString());
			}
		}
	}

}
