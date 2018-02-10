using System;
using System.Collections.Generic;

namespace Halfbreed.Levels
{

	public class MapTileDetails
	{
		public readonly string Name;
		public readonly int Elevation;
		public readonly Colors BGColor;
		public readonly Colors FogColor;
		public readonly string MoveOnFunction;
		public readonly string MoveOffFunction;
		readonly List<Traits> Traits;

		public MapTileDetails(string name, int elevation, string bgColorName, string fogColorName, 
		                      string moveOnFunction, string moveOffFunction, string[] traits)
		{
			Name = name;
			Elevation = elevation;
			BGColor = (Colors)Enum.Parse(typeof(Colors), bgColorName);
			FogColor = (Colors)Enum.Parse(typeof(Colors), fogColorName);
			MoveOnFunction = moveOnFunction;
			MoveOffFunction = moveOffFunction;

			var tmp = new List<Traits>();

			foreach (var traitName in traits)
				tmp.Add((Traits)Enum.Parse(typeof(Traits), traitName));
			Traits = tmp;
		}
	}
}
