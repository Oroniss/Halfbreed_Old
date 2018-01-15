namespace Halfbreed.Entities
{
	public abstract class Component
	{
		protected ComponentType _componentType;
		protected Entity _entity;

		protected Component(Entity entity)
		{
			_entity = entity;
		}

		public Entity Entity
		{
			get { return _entity; }
		}

		public ComponentType ComponentType
		{
			get { return _componentType; }
		}
	}
}
