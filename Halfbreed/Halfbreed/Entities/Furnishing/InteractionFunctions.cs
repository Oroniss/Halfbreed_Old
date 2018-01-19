namespace Halfbreed.Entities
{
	public partial class InteractibleComponent
	{
		private static void UseDoor(Entity interactible, Entity actor, int currentTime)
		{
			DoorComponent doorComponent = (DoorComponent)interactible.GetComponent(ComponentType.DOOR);
			doorComponent.Use(actor, currentTime);
		}

		private static void NoUse(Entity interactible, Entity actor, int currentTime)
		{
			// TODO: Add text output.
		}

	}
}
