namespace Halfbreed
{
	public abstract class Component
	{
		protected ComponentTypes _componentType;
		private int _entityId;
		private bool _destroy;

		public Component(int entityId)
		{
			_componentType = ComponentTypes.UNDEFINED;
			_entityId = entityId;
			_destroy = false;
		}

		public ComponentTypes ComponentType
		{
			get { return _componentType; }
		}

		public int EntityId
		{
			get { return _entityId; }
		}

		public virtual void Setup()
		{
			// Anything that needs to happen after initialisation, but before usage.
		}

		public virtual void CleanUp()
		{
			// Anything that needs to happen before destruction.
			_destroy = true;
		}

		public virtual void Destroy()
		{
			// Anything that needs to happen when destroyed.
			if (!_destroy)
				ErrorLogger.AddDebugText("Tried to destroy component before cleanup called");
			else
			{
				
			}
		}

	}
}
