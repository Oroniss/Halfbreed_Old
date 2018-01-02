using System.Collections.Generic;
using Halfbreed.Entities;

namespace Halfbreed
{
	public class Furnishing:IPosition,IDescription	
	{

		private static int _currentMaxFurnishingId = 0;
		private static List<int> _freeFurnishingIds = new List<int>();

		private int _furnishingId;

		private int _xLoc;
		private int _yLoc;

		protected Colors _fgColor;
		protected char _symbol;
		protected DisplayLayer _displayLayer;
		protected string _furnishingName;
		protected Materials _material;

		public Furnishing(string furnishingName, Materials material, int xLoc, int yLoc, string[] otherParameters)
		{
			if (_freeFurnishingIds.Count == 0)
			{
				_furnishingId = _currentMaxFurnishingId;
				_currentMaxFurnishingId += 1;
			}
			else
			{
				_furnishingId = _freeFurnishingIds[0];
				_freeFurnishingIds.RemoveAt(0);
			}

			_xLoc = xLoc;
			_yLoc = yLoc;

			_furnishingName = furnishingName;
			_material = material;
		}

		// IPosition
		public int XLoc
		{
			get { return _xLoc; }
		}

		public int YLoc
		{
			get { return _yLoc; }
		}

		public void UpdatePosition(Position newPosition)
		{
			_xLoc = newPosition.X;
			_yLoc = newPosition.Y;
		}

		public void MoveObject(int deltaX, int deltaY)
		{
			_xLoc += deltaX;
			_yLoc += deltaY;
		}

		// IDescription
		public char Symbol
		{
			get	{ return _symbol; }
		}

		public Colors FGColor
		{
			get { return _fgColor; }
		}

		public DisplayLayer DisplayLayer
		{
			get { return _displayLayer; }
		}

		public string GetDescription()
		{
			return _furnishingName;
		}

		public Materials Material
		{
			get { return _material; }
		}

	}

}
