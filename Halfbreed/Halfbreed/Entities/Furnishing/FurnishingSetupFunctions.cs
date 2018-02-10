using System.Collections.Generic;

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

			// Furnishing specific functions
			{"Wooden Door", new FurnishingSetupFunction(DoorSetup)}
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
			furnishing.AddInteractionFunction("LevelTransitionUse");
			furnishing.SetOtherAttribute("DestinationLevel", otherParameters[otherParameters.IndexOf("DestinationLevel") + 1]);
			furnishing.SetOtherAttribute("NewXLoc", otherParameters[otherParameters.IndexOf("NewXLoc") + 1]);
			furnishing.SetOtherAttribute("NewYLoc", otherParameters[otherParameters.IndexOf("NewYLoc") + 1]);
		}
	}

}
