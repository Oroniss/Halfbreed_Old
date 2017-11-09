using System.Collections.Generic;
using RLNET;

namespace Halfbreed
{
	public static class StringToColorConverter
	{
		private static Dictionary<string, RLColor> stringToColorDict = new Dictionary<string, RLColor>()
		{
			// TODO: Keep this up to date.
			{"White", Palette.WHITE},
			{"Black", Palette.BLACK},
			{"Wood Brown", Palette.WOODBROWN},
			{"Dark Wood Brown", Palette.DARKWOODBROWN}
		};

		public static RLColor ConvertStringToColor(string colorName)
		{
			return stringToColorDict[colorName];
		}

	}
}
