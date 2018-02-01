using Halfbreed.Levels;

namespace Halfbreed.Entities
{
	public class ConcealedComponent:Component
	{
		int _concealmentLevel;

		Colors _concealedFGColor;
		char _concealedSymbol;
		DisplayLayer _concealedDisplayLayer;

		bool _hasConcealedTile;
		MapTileDetails _concealedMapTile;

		public ConcealedComponent(Entity entity, int concealmentLevel, string concealedFGColor, char concealedSymbol,
		                         int concealedDisplayLayer, bool hasConcealedTile, string concealedTileName)
			:base(entity)
		{
			_concealmentLevel = concealmentLevel;
			if (concealedFGColor != "")
				_concealedFGColor = (Colors)System.Enum.Parse(typeof(Colors), concealedFGColor);
			_concealedSymbol = concealedSymbol;
			_concealedDisplayLayer = (DisplayLayer)concealedDisplayLayer;

			_hasConcealedTile = hasConcealedTile;
			if(hasConcealedTile)
				_concealedMapTile = StaticData.GetMapTileDetails((TileType)System.Enum.Parse(typeof(TileType), concealedTileName));
		}

		public int ConcealmentLevel
		{
			get { return _concealmentLevel; }
		}

		public Colors ConcealedFGColor
		{
			get { return _concealedFGColor; }
		}

		public char ConcealedSymbol
		{
			get { return _concealedSymbol; }
		}

		public DisplayLayer ConcealedDisplayLayer
		{
			get { return _concealedDisplayLayer; }
		}

		public bool HasConcealedTile
		{
			get { return _hasConcealedTile; }
		}

		public MapTileDetails ConcealedMapTile
		{
			get { return _concealedMapTile; }
		}

		public void Reveal()
		{
			// TODO: Fix any interaction functions and anything else required.
		}
	}
}
