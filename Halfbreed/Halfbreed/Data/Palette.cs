using RLNET;
using System.Collections.Generic;

namespace Halfbreed
{
	public static class Palette
	{
		private static readonly Dictionary<Colors, RLColor> _colors = new Dictionary<Colors, RLColor>()
		{
			// Basics
			{Colors.Black, new RLColor(0, 0, 0)},
			{Colors.White, new RLColor(255, 255, 255)},
			// Browns
			{Colors.DarkBrown, new RLColor(109, 85, 5)},
			{Colors.DarkWoodBrown, new RLColor(139, 105, 20)},
			{Colors.FadedCloth, new RLColor(238, 220, 130)},
			{Colors.LightBrown, new RLColor(235, 165, 32)},
			{Colors.OldCloth, new RLColor(205, 190, 112)},
			{Colors.RedBrown, new RLColor(165, 102, 0)},
			{Colors.Tan, new RLColor(210, 180, 140)},
			{Colors.WoodBrown, new RLColor(205, 155, 29)},
			{Colors.WoodFog, new RLColor(165, 125, 20)},
			// Greens
			{Colors.PutridGreen, new RLColor(193, 255, 193)},
			{Colors.VileGreen, new RLColor(50, 205, 50)},
			// Metals
			{Colors.Copper, new RLColor(184, 115, 51)},
			{Colors.Gold, new RLColor()},
			{Colors.Silver, new RLColor(190, 190, 190)},
			{Colors.SteelGrey, new RLColor(122, 122, 122)},
			{Colors.Tin, new RLColor(211, 212, 213)},
			// Oranges
			{Colors.DarkOrange, new RLColor(238, 154, 0)},
			// Blues
			{Colors.WaterBlue, new RLColor(0, 0, 238)},
			{Colors.DarkWater, new RLColor(0, 0, 115)},
			// Reds
			{Colors.Red, new RLColor(255, 0, 0)}
		};

		public static RLColor GetColor(Colors colorName)
		{
			return _colors[colorName];
		}

		public static int NumberOfColors
		{
			get { return _colors.Count; }
		}
			
	}

	public enum Colors
	{
		// Basics
		Black,
		White,
		// Browns
		DarkBrown,
		DarkWoodBrown,
		FadedCloth,
		LightBrown,
		OldCloth,
		RedBrown,
		Tan,
		WoodBrown,
		WoodFog,
		// Greens
		PutridGreen,
		VileGreen,
		// Metals
		Copper,
		Gold,
		Silver,
		SteelGrey,
		Tin,
		// Oranges
		DarkOrange,
		// Blues
		WaterBlue,
		DarkWater,
		// Reds
		Red
	}
}
