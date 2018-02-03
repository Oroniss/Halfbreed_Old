using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class InteractibleComponent
	{
		private static Dictionary<string, InteractionFunction> _interactionFunctionDictionary = new
			Dictionary<string, InteractionFunction>()
		{
			{"NoUse", new InteractionFunction(NoUse)},
			{"UseDoor", new InteractionFunction(UseDoor)},
			{"UseLevelTransition", new InteractionFunction(UseLevelTransition)},
			{"TriggerTrap", new InteractionFunction(TriggerTrap)}
		};

		private static void UseDoor(Entity interactible, Entity actor, int currentTime)
		{
			DoorComponent doorComponent = (DoorComponent)interactible.GetComponent(ComponentType.DOOR);
			doorComponent.Use(actor, currentTime);
		}

		private static void NoUse(Entity interactible, Entity actor, int currentTime)
		{
			MainGraphicDisplay.TextConsole.AddOutputText("You can't do anything with that!");
		}

		private static void TriggerTrap(Entity interactible, Entity actor, int currentTime)
		{
			TrapComponent trapComponent = (TrapComponent)interactible.GetComponent(ComponentType.TRAP);
			trapComponent.TriggerTrap(actor);
		}

		private static void UseLevelTransition(Entity interactible, Entity actor, int currentTime)
		{
			LevelTransitionComponent transitionComponent = (LevelTransitionComponent)interactible.GetComponent(ComponentType.LEVELTRANSITION);
			transitionComponent.MoveLevel(actor);
		}

	}
}
