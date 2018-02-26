// Tidied up for version 0.02. No changes required.

using System;
namespace Halfbreed
{
	public class FurnishingDetails
	{
		public readonly bool HasBGColor;
		public readonly Colors BGColor;
		public readonly bool HasFogColor;
		public readonly Colors FogColor;
		public readonly int Elevation;
		public readonly string SetupFunction;

		public FurnishingDetails(string bgColor, string fogColor, int elevation, string setupFunction)
		{
			if (bgColor == "")
				HasBGColor = false;
			else
			{
				HasBGColor = true;
				BGColor = (Colors)Enum.Parse(typeof(Colors), bgColor);
			}

			if (fogColor == "")
				HasFogColor = false;
			else
			{
				HasFogColor = true;
				FogColor = (Colors)Enum.Parse(typeof(Colors), fogColor);
			}

			Elevation = elevation;
			SetupFunction = setupFunction;
		}
	}
}
