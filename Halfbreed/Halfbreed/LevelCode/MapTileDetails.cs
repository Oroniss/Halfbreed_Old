using System.Collections.Generic;

namespace Halfbreed
{
	public static class MapTileDetails
	{
		public static Dictionary<TileType, TileDetails> MapTileDict = new Dictionary<TileType, TileDetails>()
		{
			// TODO: Eventually this should probably be read in from somewhere.
			// TODO: At least think about it.
			{TileType.WOODFLOOR, new TileDetails("Wood Floor", 0, 0, true, "Wood Brown", "Wood Brown")},
			{TileType.WOODWALL, new TileDetails("Wood Wall", 20, 4, false, "Dark Wood Brown", "Dark Wood Brown")},
			{TileType.WOODENDEBRIS, new TileDetails("Wooden Debris", 20, 4, false, "Dark Wood Brown", "Dark Wood Brown")}
		};

	}

	public struct TileDetails
	{
		public string Name;
		public int Elevation;
		public int MoveTypes;
		public bool AllowLOS;
		public Colors BGColor;
		public Colors FogColor;

		internal TileDetails(string name, int elevation, int moveTypes, bool allowLOS, string bgColorName,
							string fogColorName)
		{
			Name = name;
			Elevation = elevation;
			MoveTypes = moveTypes;
			AllowLOS = allowLOS;
			BGColor = EnumConverter.ConvertStringToColor(bgColorName);
			FogColor = EnumConverter.ConvertStringToColor(fogColorName);
		}
	}

}

