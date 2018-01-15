using Halfbreed.Levels;

namespace Halfbreed
{
	public class TileComponent:Component
	{
		private MapTileDetails _tileDetails;

		public TileComponent(Entity entity, string tileName)
			:base(entity)
		{
			_componentType = ComponentType.TILE;

			_tileDetails = StaticData.GetMapTileDetails(EnumConverter.ConvertStringToTileType(tileName));
		}

		public MapTileDetails MapTileDetails
		{
			get { return _tileDetails; }
		}
	}
}
