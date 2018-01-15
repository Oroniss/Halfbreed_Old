using RLNET;
using System.Collections.Generic;

namespace Halfbreed
{
	public static class Palette
	{
		private static readonly Dictionary<Colors, RLColor> _colors = new Dictionary<Colors, RLColor>()
		{
			// Basics
			{Colors.BLACK, new RLColor(0, 0, 0)},
			{Colors.WHITE, new RLColor(255, 255, 255)},
			// Browns
			{Colors.DARKBROWN, new RLColor(109, 85, 5)},
			{Colors.DARKWOODBROWN, new RLColor(139, 105, 20)},
			{Colors.FADEDCLOTH, new RLColor(238, 220, 130)},
			{Colors.LIGHTBROWN, new RLColor(218, 165, 32)},
			{Colors.OLDCLOTH, new RLColor(205, 190, 112)},
			{Colors.REDBROWN, new RLColor(165, 102, 0)},
			{Colors.TAN, new RLColor(210, 180, 140)},
			{Colors.WOODBROWN, new RLColor(205, 155, 29)},
			{Colors.WOODFOG, new RLColor(165, 125, 20)},
			// Greens
			{Colors.PUTRIDGREEN, new RLColor(193, 255, 193)},
			{Colors.VILEGREEN, new RLColor(50, 205, 50)},
			// Metals
			{Colors.COPPER, new RLColor(184, 115, 51)},
			{Colors.GOLD, new RLColor()},
			{Colors.LIGHTGREY, new RLColor(190, 190, 190)},
			{Colors.SILVER, new RLColor()},
			{Colors.STEELGREY, new RLColor(122, 122, 122)},
			{Colors.TIN, new RLColor(211, 212, 213)},
			// Oranges
			{Colors.DARKORANGE, new RLColor(238, 154, 0)}
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
		BLACK,
		WHITE,
		// Browns
		DARKBROWN,
		DARKWOODBROWN,
		FADEDCLOTH,
		LIGHTBROWN,
		OLDCLOTH,
		REDBROWN,
		TAN,
		WOODBROWN,
		WOODFOG,
		// Greens
		PUTRIDGREEN,
		VILEGREEN,
		// Metals
		COPPER,
		GOLD,
		LIGHTGREY,
		SILVER,
		STEELGREY,
		TIN,
		// Oranges
		DARKORANGE
	}
}
