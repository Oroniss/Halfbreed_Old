namespace Halfbreed.Entities
{
	public struct FurnishingTemplate
	{
		private string _furnishingName;
		private char _symbol;
		private int _volume;
		private bool _hasTile;
		private string _tileTypeName;

		public FurnishingTemplate(string furnishingName, char symbol, int volume, bool hasTile, string tileTypeName)
		{
			_furnishingName = furnishingName;
			_symbol = symbol;
			_volume = volume;
			_hasTile = hasTile;
			_tileTypeName = tileTypeName;
		}

		public string FurnishingName
		{
			get { return _furnishingName; }
		}

		public char Symbol
		{
			get { return _symbol; }
		}

		public int Volume
		{
			get { return _volume; }
		}

		public bool HasTile
		{
			get { return _hasTile; }
		}

		public string TileTypeName
		{
			get { return _tileTypeName; }
		}
	}
}
