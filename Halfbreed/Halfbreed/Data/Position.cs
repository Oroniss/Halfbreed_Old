using System;
namespace Halfbreed
{
	public struct Position
	{
		private int _x;
		private int _y;

		public Position(int x, int y)
		{
			_x = x;
			_y = y;
		}

		public int X
		{
			get { return _x; }
		}

		public int Y
		{
			get { return _y; }
		}
	}
}
