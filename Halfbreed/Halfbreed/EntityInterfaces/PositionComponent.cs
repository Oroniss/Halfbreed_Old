using System;
namespace Halfbreed
{
	public class PositionComponent : Component
	{
		private int _xLoc;
		private int _yLoc;

		public PositionComponent(int entityId, string[] otherParams)
			: base(entityId)
		{
			_componentType = ComponentTypes.POSITION;

			var xIndex = Array.IndexOf(otherParams, "xLoc");
			var yIndex = Array.IndexOf(otherParams, "yLoc");

			if (xIndex >= 0 && yIndex >= 0)
			{
				_xLoc = Int32.Parse(otherParams[xIndex + 1]);
				_yLoc = Int32.Parse(otherParams[yIndex + 1]);
			}
			else
			{
				_xLoc = -1;
				_yLoc = -1;
			}

		}

		public int XLoc
		{
			get { return _xLoc; }
		}

		public int YLoc
		{
			get { return _yLoc; }
		}

		public void updatePosition(Position newPosition)
		{
			_xLoc = newPosition.XLoc;
			_yLoc = newPosition.YLoc;
		}

		public void moveEntity(int deltaX, int deltaY)
		{
			_xLoc += deltaX;
			_yLoc += deltaY;
		}
	}

	public struct Position
	{
		public int XLoc;
		public int YLoc;

		public Position(int xLoc, int yLoc)
		{
			XLoc = xLoc;
			YLoc = yLoc;
		}
	}
}