using System;

namespace Halfbreed.Entities
{
	public abstract class Entity : IPosition, IDescription, IComparable
	{
		protected DisplayLayer _displayLayer;
		protected Colors _fgColor;
		protected char _symbol;

		protected int _xLoc;
		protected int _yLoc;

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
