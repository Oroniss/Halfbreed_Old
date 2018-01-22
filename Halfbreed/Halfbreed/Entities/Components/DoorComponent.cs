namespace Halfbreed.Entities
{
	public class DoorComponent:Component
	{
		private bool _isOpen;
		private bool _isLocked;

		public DoorComponent(Entity entity, bool isOpen)
			:base(entity)
		{
			_componentType = ComponentType.DOOR;
			_isLocked = false;
			_isOpen = isOpen;

			((InteractibleComponent)_entity.GetComponent(ComponentType.INTERACTIBLE)).AddFunction("UseDoor");

			if (!isOpen)
			{
				_entity.AddTrait(EntityTraits.BLOCKMOVE);
				_entity.Symbol = '+';
			}
		}

		public DoorComponent(Entity entity, bool isLocked, object key)
			: this(entity, false)
		{
			_isLocked = true;
			// TODO: Implement the locked stuff here.
		}

		public bool IsOpen
		{ 
			get { return _isOpen; }
		}

		public void Use(Entity actor, int currentTime)
		{
			if (_isLocked)
				// TODO: Figure this out.
				return;

			if (_isOpen)
			{
				if (GameEngine.CurrentLevel.IsPassible(_entity.XLoc, _entity.YLoc, true, true, true))
				{
					_isOpen = false;
					_entity.Symbol = '+';
					_entity.AddTrait(EntityTraits.BLOCKMOVE);
				}
				else
					// TODO: Think this through a bit more carefully.
					MainGraphicDisplay.TextConsole.AddOutputText("Something is blocking the door");
			}
			else
			{
				_isOpen = true;
				_entity.Symbol = '-';
				_entity.RemoveTrait(EntityTraits.BLOCKMOVE);
			}
		}
	}
}
