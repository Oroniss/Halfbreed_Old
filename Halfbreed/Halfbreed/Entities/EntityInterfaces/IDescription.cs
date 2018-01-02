namespace Halfbreed.Entities
{
	public interface IDescription
	{
		char Symbol { get; }
		Colors FGColor { get; }
		DisplayLayer DisplayLayer { get; }
		string GetDescription();
	}
}
