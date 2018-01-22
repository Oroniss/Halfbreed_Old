namespace Halfbreed.Entities
{
	public partial class TrapComponent:Component
	{
		private string _trapType;
		private int _trapLevel;
		bool _armed;

		public TrapComponent(Entity entity, string trapType, int trapLevel)
			:base(entity)
		{
			_componentType = ComponentType.TRAP;
			_trapType = trapType;
			_trapLevel = trapLevel;
			_armed = true;
		}

		public int TrapLevel
		{
			get { return _trapLevel; }
		}

		public void TriggerTrap(Entity actor)
		{
			if(_armed)
				_trapFunctionDictionary[_trapType](this, actor);
		}

		public bool DisarmTrap(Entity actor)
		{
			// TODO: Flesh this out properly later.
			_armed = false;
			return true;
		}

		private delegate void TrapFunction(TrapComponent trapComponent, Entity actor);
	}
}
