using System.Collections.Generic;
using RLNET;

namespace Halfbreed.Converters
{
	public class StringToColorConverter
	{
		private Dictionary<string, Colors> stringToColorDict = new Dictionary<string, Colors>()
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
			// Oranges
			{"Dark Orange", Colors.DARKORANGE}
		};

		public Colors ConvertStringToColor(string colorName)
		{
			return stringToColorDict[colorName];
		}

		public int NumberOfColors
		{
			get { return stringToColorDict.Count; }
		}

	}
}
