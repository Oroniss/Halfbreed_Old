namespace Halfbreed.Entities
{
	public struct FurnishingTemplate
	{
		private string _furnishingName;
		private char _symbol;
		private int _volume;

		public FurnishingTemplate(string furnishingName, char symbol, int volume)
		{
			_furnishingName = furnishingName;
			_symbol = symbol;
			_volume = volume;
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
	}
}
