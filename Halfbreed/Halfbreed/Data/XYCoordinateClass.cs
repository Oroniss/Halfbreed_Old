namespace Halfbreed
{
	public class Direction
	{
		readonly int _xDirection;
		readonly int _yDirection;

		public Direction(int x, int y)
		{
			_xDirection = x;
			_yDirection = y;
		}

		public int XDirection
		{
			get { return _xDirection; }
		}

		public int YDirection
		{
			get { return _yDirection; }
		}
	}
}
