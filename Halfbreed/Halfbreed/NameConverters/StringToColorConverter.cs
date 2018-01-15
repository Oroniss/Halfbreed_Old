using System.Collections.Generic;

namespace Halfbreed.Converters
{
	public class StringToColorConverter
	{
		private static Dictionary<string, Colors> stringToColorDict = new Dictionary<string, Colors>()
		{
			// TODO: Keep this up to date.
			// Basic colors
			{"Black", Colors.BLACK},
			{"White", Colors.WHITE},
			// Browns
			{"Wood Brown", Colors.WOODBROWN},
			{"Dark Wood Brown", Colors.DARKWOODBROWN},
			{"Red Brown", Colors.REDBROWN},
			{"Dark Brown", Colors.DARKBROWN},
			{"Light Brown", Colors.LIGHTBROWN},
			{"Wood Fog", Colors.WOODFOG},
			{"Tan", Colors.TAN},
			{"Old Cloth", Colors.OLDCLOTH},
			{"Faded Cloth", Colors.FADEDCLOTH},
			// Greens
			{"Putrid Green", Colors.PUTRIDGREEN},
			{"Vile Green", Colors.VILEGREEN},
			// Metals
			{"Light Grey", Colors.LIGHTGREY},
			{"Steel Grey", Colors.STEELGREY},
			{"Tin", Colors.TIN},
			{"Copper", Colors.COPPER},
			{"Silver", Colors.SILVER},
			{"Gold", Colors.GOLD},
			// Oranges
			{"Dark Orange", Colors.DARKORANGE}
		};

		public Colors ConvertStringToColor(string colorName)
		{
			return stringToColorDict[colorName];
		}

		public static int NumberOfColors
		{
			get { return stringToColorDict.Count; }
		}

	}
}
