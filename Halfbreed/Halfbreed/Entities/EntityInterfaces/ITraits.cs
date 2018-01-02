namespace Halfbreed.Entities
{
	public interface ITraits
	{
		bool HasTrait();
		void AddTrait(EntityTraits trait);
		void RemoveTrait(EntityTraits trait);
	}
}
