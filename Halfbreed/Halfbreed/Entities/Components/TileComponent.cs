using System;
using Halfbreed.Levels;

namespace Halfbreed.Entities
{
	public class TileComponent:Component
	{
		private MapTileDetails _tileDetails;

		public TileComponent(Entity entity, string tileName)
			:base(entity)
		{
			_componentType = ComponentType.TILE;
			_tileDetails = StaticData.GetMapTileDetails((TileType)Enum.Parse(typeof(TileType), tileName));
		}

		public MapTileDetails MapTileDetails
		{
			get { return _tileDetails; }
		}
	}
}
