using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class MoveOnComponent
	{
		private static Dictionary<string, MoveOnFunction> _moveOnFunctionDictionary = new Dictionary<string, MoveOnFunction>()
		{{"PitMoveOn", PitTrapMoveOn}
		};

		private static void PitTrapMoveOn(Entity furnishing, Entity actor, int originX, int originY)
		{
			actor.ProcessDamage(furnishing, new Combat.Damage(Combat.DamageType.PHYSICAL, 20));
		}

	}
}
