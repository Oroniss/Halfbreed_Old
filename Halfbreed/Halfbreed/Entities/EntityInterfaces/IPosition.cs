namespace Halfbreed.Entities
{
	public interface IPosition
	{
		int XLoc { get; }
		int YLoc { get; }
		void UpdatePosition(Position newPosition);
		void MoveObject(int deltaX, int deltaY);
	}
}
