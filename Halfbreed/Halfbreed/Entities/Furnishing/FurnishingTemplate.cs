namespace Halfbreed.Entities
{
	public struct FurnishingTemplate
	{
		string _furnishingName;
		char _symbol;
		int _volume;
		EntityTraits[] _traits;
		bool _hasTile;
		string _tileTypeName;
		string[] _otherComponents;

		public FurnishingTemplate(string furnishingName, char symbol, int volume, EntityTraits[] traits, bool hasTile, string tileTypeName,
		                         string[] otherComponents)
		{
			_furnishingName = furnishingName;
			_symbol = symbol;
			_volume = volume;
			_traits = traits;
			_hasTile = hasTile;
			_tileTypeName = tileTypeName;
			_otherComponents = otherComponents;
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

		public EntityTraits[] Traits
		{
			get { return _traits; }
		}

		public bool HasTile
		{
			get { return _hasTile; }
		}

		public string TileTypeName
		{
			get { return _tileTypeName; }
		}

		public string[] OtherComponents
		{
			get { return _otherComponents; }
		}
	}
}
