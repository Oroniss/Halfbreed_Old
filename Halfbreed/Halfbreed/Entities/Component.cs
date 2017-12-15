namespace Halfbreed
{
	public abstract class Component
	{
		protected ComponentTypes _componentType;
		private int _entityId;

		public Component(int entityId)
		{
			_componentType = ComponentTypes.UNDEFINED;
			_entityId = entityId;
		}

		public ComponentTypes ComponentType
		{
			get { return _componentType; }
		}

		public int EntityId
		{
			get { return _entityId; }
		}

	}
}
