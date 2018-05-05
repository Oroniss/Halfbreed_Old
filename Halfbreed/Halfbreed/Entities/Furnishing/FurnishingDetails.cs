// Tidied up for version 0.02. No changes required.

using System;
namespace Halfbreed
{
	public class FurnishingDetails
	{
		public readonly bool HasBGColor;
		public readonly string BGColorName;
		public readonly bool HasFogColor;
		public readonly string FogColorName;
		public readonly int Elevation;
		public readonly string SetupFunction;

		public FurnishingDetails(string bgColorName, string fogColorName, int elevation, string setupFunction)
		{
			if (bgColorName == "")
				HasBGColor = false;
			else
			{
				HasBGColor = true;
				BGColorName = bgColorName;
			}

			if (fogColorName == "")
				HasFogColor = false;
			else
			{
				HasFogColor = true;
				FogColorName = fogColorName;
			}

			Elevation = elevation;
			SetupFunction = setupFunction;
		}
	}
}
