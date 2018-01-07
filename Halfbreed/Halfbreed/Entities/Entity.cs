using System;

namespace Halfbreed.Entities
{
	public abstract class Entity : IPosition, IDescription, IComparable
	{
		private string _entityName;

		protected int _xLoc;
		protected int _yLoc;

		protected DisplayLayer _displayLayer;
		protected Colors _fgColor;
		protected char _symbol;

		protected Entity(string entityName, int xLoc, int yLoc)
		{
			_entityName = entityName;
			_xLoc = xLoc;
			_yLoc = yLoc;
		}

		public string EntityName
		{
			get { return _entityName; }
		}

		public DisplayLayer DisplayLayer
		{
			get {return _displayLayer;}
		}

		public Colors FGColor
		{
			get{return _fgColor;}
		}

		public char Symbol
		{
			get { return _symbol; }
		}

		public abstract string GetDescription();

		public int XLoc
		{
			get { return _xLoc; }
		}

		public int YLoc
		{
			get { return _yLoc; }
		}

		public int CompareTo(object obj)
		{
			return _displayLayer.CompareTo(((Entity)obj).DisplayLayer);
		}

	}
}
