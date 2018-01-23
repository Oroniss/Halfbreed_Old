namespace Halfbreed.Entities
{
	public partial class Entity
	{
		public Entity(string trapName, int xLoc, int yLoc, int trapLevel, string[] otherParameters)
			: this(trapName, xLoc, yLoc, new EntityTraits[] { })
		{
			_displayLayer = DisplayLayer.TRAP;
			_symbol = '^';
			_fgColor = Colors.RED;

			AddTrait(EntityTraits.TRAP);
			AddTrait(EntityTraits.FURNISHING);
			foreach (EntityTraits trait in _furnishingTraits)
				AddTrait(trait);

			_components[ComponentType.TRAP] = new TrapComponent(this, trapName, trapLevel);

			MoveOnComponent moveOn = new MoveOnComponent(this);
			moveOn.AddFunction("TriggerTrap");
			_components[ComponentType.MOVEON] = moveOn;

			SetupOtherComponents(new string[] { }, otherParameters);
		}
	}
}
