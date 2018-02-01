using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class Entity
	{
		public Entity(Menus.NewGameParameters playerParameters)
		{
			_entityId = GetNextId();

			_displayLayer = DisplayLayer.PLAYER;
			_symbol = '@';
			_fgColor = Colors.BLACK;
			_entityName = "Player"; // TODO: Think whether this is correct or not.

			_isConcealed = false;
			_playerSpotted = true;

			_components = new Dictionary<ComponentType, Component>();
			_traits = new List<EntityTraits>();
			AddTrait(EntityTraits.PLAYER);
			AddTrait(EntityTraits.BLOCKMOVE);
			AddTrait(EntityTraits.CANINTERACT);
			AddTrait(EntityTraits.CANWALK);
			AddTrait(EntityTraits.CANSWIM);
			AddTrait(EntityTraits.CANCLIMB);

			InputComponent inputComponent = new InputComponent(this);
			inputComponent.SetManual();
			_components[ComponentType.INPUT] = inputComponent;
			_components[ComponentType.MOVEMENT] = new MovementComponent(this);
			_components[ComponentType.SENSORY] = new SensoryComponent(this, 18);
		}
	}
}
