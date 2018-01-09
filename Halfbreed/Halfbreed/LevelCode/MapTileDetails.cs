namespace Halfbreed
{

	public class MapTileDetails
	{
		public readonly string Name;
		public readonly int Elevation;
		public readonly MovementModes MoveModes;
		public readonly bool AllowLOS;
		public readonly Colors BGColor;
		public readonly Colors FogColor;

		public MapTileDetails(string name, int elevation, MovementModes moveTypes, bool allowLOS, string bgColorName,
							string fogColorName)
		{
			Name = name;
			Elevation = elevation;
			MoveModes = moveTypes;
			AllowLOS = allowLOS;
			BGColor = EnumConverter.ConvertStringToColor(bgColorName);
			FogColor = EnumConverter.ConvertStringToColor(fogColorName);
		}
	}

}

