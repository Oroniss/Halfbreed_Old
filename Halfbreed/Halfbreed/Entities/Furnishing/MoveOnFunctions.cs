using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class MoveOnComponent
	{
		private static Dictionary<string, MoveOnFunction> _moveOnFunctionDictionary = new Dictionary<string, MoveOnFunction>()
		{{"TriggerTrap", TriggerTrap}
		};

		private static void TriggerTrap(Entity furnishing, Entity actor, int originX, int originY)
		{
			((TrapComponent)furnishing.GetComponent(ComponentType.TRAP)).TriggerTrap(actor);
		}
	}
}
