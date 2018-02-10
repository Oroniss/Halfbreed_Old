using System.Collections.Generic;

namespace Halfbreed.Entities.Furnishings
{
	public static class FurnishingSetupFunctions
	{
		static readonly Dictionary<string, FurnishingSetupFunction> _setupFunctions = new Dictionary<string, FurnishingSetupFunction>()
		{
			{"Default Furnishing Setup", new FurnishingSetupFunction(DefaultFurnishingSetup)},
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
			furnishing.AddInteractionFunction("No Use");
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
	}

}
