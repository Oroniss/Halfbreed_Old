using System;

namespace Halfbreed.Levels
{

	public class MapTileDetails
	{
		public readonly string Name;
		public readonly int Elevation;
		public readonly bool Walkable;
		public readonly bool Flyable;
		public readonly bool Swimmable;
		public readonly bool AllowLOS;
		public readonly Colors BGColor;
		public readonly Colors FogColor;

		public MapTileDetails(string name, int elevation, bool walkable, bool flyable, bool swimmable, bool allowLOS, string bgColorName,
							string fogColorName)
		{
			Name = name;
			Elevation = elevation;
			Walkable = walkable;
			Flyable = flyable;
			Swimmable = swimmable;
			AllowLOS = allowLOS;
			BGColor = (Colors)Enum.Parse(typeof(Colors), bgColorName);
			FogColor = (Colors)Enum.Parse(typeof(Colors), fogColorName);
		}
	}
}

