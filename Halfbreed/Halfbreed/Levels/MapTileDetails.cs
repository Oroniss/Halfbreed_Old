// Revised for version 0.02.

using System;
using System.Collections.Generic;

namespace Halfbreed.Levels
{

	public class MapTileDetails
	{
		public readonly TileType TileType;
		public readonly string Name;
		public readonly int Elevation;
		public readonly string BGColorName;
		public readonly string FogColorName;
		public readonly string MoveOnFunction;
		public readonly string MoveOffFunction;
		readonly List<Traits> _traits;

		public MapTileDetails(TileType tileType, string name, int elevation, string bgColorName, string fogColorName, 
		                      string moveOnFunction, string moveOffFunction, string[] traits)
		{
			TileType = tileType;
			Name = name;
			Elevation = elevation;
			BGColorName = bgColorName;
			FogColorName = fogColorName;
			MoveOnFunction = moveOnFunction;
			MoveOffFunction = moveOffFunction;

			var tmp = new List<Traits>();

			foreach (var traitName in traits)
				tmp.Add((Traits)Enum.Parse(typeof(Traits), traitName));
			_traits = tmp;
		}

		public bool HasTrait(Traits trait)
		{
			return _traits.Contains(trait);
		}

		// TODO: Add the movement functions in here.
	}
}
