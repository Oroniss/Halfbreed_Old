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
			{Colors.WOODBROWN, new RLColor(205, 155, 29)},
			{Colors.DARKWOODBROWN, new RLColor(139, 105, 20)},
			{Colors.REDBROWN, new RLColor(165, 102, 0)},
			{Colors.DARKBROWN, new RLColor(109, 85, 5)},
			{Colors.LIGHTBROWN, new RLColor(218, 165, 32)},
			{Colors.WOODFOG, new RLColor(165, 125, 20)},
			{Colors.TAN, new RLColor(210, 180, 140)},
			{Colors.OLDCLOTH, new RLColor(205, 190, 112)},
			{Colors.FADEDCLOTH, new RLColor(238, 220, 130)},
			// Greens
			{Colors.PUTRIDGREEN, new RLColor(193, 255, 193)},
			{Colors.VILEGREEN, new RLColor(50, 205, 50)},
			// Metals
			{Colors.LIGHTGREY, new RLColor(190, 190, 190)},
			{Colors.STEELGREY, new RLColor(122, 122, 122)},
			{Colors.TIN, new RLColor(211, 212, 213)},
			{Colors.COPPER, new RLColor(184, 115, 51)},
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
		WOODBROWN,
		DARKWOODBROWN,
		REDBROWN,
		LIGHTBROWN,
		DARKBROWN,
		WOODFOG,
		TAN,
		OLDCLOTH,
		FADEDCLOTH,
		// Greens
		PUTRIDGREEN,
		VILEGREEN,
		// Metals
		LIGHTGREY,
		STEELGREY,
		TIN,
		COPPER,
		// Oranges
		DARKORANGE
	}
}
