using System.Collections.Generic;
using RLNET;

namespace Halfbreed
{
	public static class StringToColorConverter
	{
		private static Dictionary<string, RLColor> stringToColorDict = new Dictionary<string, RLColor>()
		{
			// TODO: Keep this up to date.
			// Basic colors
			{"Black", Palette.BLACK},
			{"White", Palette.WHITE},
			// Browns
			{"Wood Brown", Palette.WOODBROWN},
			{"Dark Wood Brown", Palette.DARKWOODBROWN},
			{"Red Brown", Palette.REDBROWN},
			{"Dark Brown", Palette.DARKBROWN},
			{"Wood Fog", Palette.WOODFOG},
			{"Tan", Palette.TAN},
			{"Old Cloth", Palette.OLDCLOTH},
			{"Faded Cloth", Palette.FADEDCLOTH},
			// Greens
			{"Putrid Green", Palette.PUTRIDGREEN},
			{"Vile Green", Palette.VILEGREEN},
			// Metals
			{"Steel Grey", Palette.STEELGREY},
			// Oranges
			{"Dark Orange", Palette.DARKORANGE}
		};

		public static RLColor ConvertStringToColor(string colorName)
		{
			return stringToColorDict[colorName];
		}

	}
}
