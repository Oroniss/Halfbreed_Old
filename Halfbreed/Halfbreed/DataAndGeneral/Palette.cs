using RLNET;
using System.Collections.Generic;

namespace Halfbreed
{
	public static class Palette
	{
		// TODO: Add a full set of colors from the web listing so it shouldn't need to change again.
		static readonly Dictionary<string, RLColor> _colors = new Dictionary<string, RLColor>
		{
			// Basics
			{"Black", new RLColor(0, 0, 0)},
			{"White", new RLColor(255, 255, 255)},
			// Browns
			{"DarkBrown", new RLColor(109, 85, 5)},
			{"DarkWoodBrown", new RLColor(139, 105, 20)},
			{"FadedCloth", new RLColor(238, 220, 130)},
			{"LightBrown", new RLColor(235, 165, 32)},
			{"OldCloth", new RLColor(205, 190, 112)},
			{"RedBrown", new RLColor(165, 102, 0)},
			{"Tan", new RLColor(210, 180, 140)},
			{"WoodBrown", new RLColor(205, 155, 29)},
			{"WoodFog", new RLColor(165, 125, 20)},
			// Greens
			{"PutridGreen", new RLColor(193, 255, 193)},
			{"VileGreen", new RLColor(50, 205, 50)},
			// Metals
			{"Copper", new RLColor(184, 115, 51)},
			{"DarkGrey", new RLColor(75, 75, 75)},
			{"Gold", new RLColor()},
			{"Silver", new RLColor(190, 190, 190)},
			{"SteelGrey", new RLColor(122, 122, 122)},
			{"Tin", new RLColor(211, 212, 213)},
			// Oranges
			{"DarkOrange", new RLColor(238, 154, 0)},
			// Blues
			{"DarkWater", new RLColor(0, 0, 115)},
			{"WaterBlue", new RLColor(0, 0, 238)},
			// Reds
			{"Red", new RLColor(255, 0, 0)}
		};

		public static RLColor GetColor(string colorName)
		{
			if (_colors.ContainsKey(colorName))
				return _colors[colorName];
			ErrorLogger.AddDebugText("Unknown color name: " + colorName);
			return _colors["Black"];
		}
	}
}
