using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class TrapComponent
	{
		private static Dictionary<string, TrapFunction> _trapFunctionDictionary = new Dictionary<string, TrapFunction>()
		{
			{"Swinging Blade", new TrapFunction(SwingingBladeTrap)},
			{"Flame Vent Trap", new TrapFunction(FlameVentTrap)},
			{"Pit Trap", new TrapFunction(PitTrap)}
		};

		private static void SwingingBladeTrap(TrapComponent trap, Entity actor)
		{
			actor.ProcessDamage(trap.Entity, new Combat.Damage(Combat.DamageType.PHYSICAL, 20 * trap.TrapLevel));
			trap.DisarmTrap(actor);
		}

		private static void FlameVentTrap(TrapComponent trap, Entity actor)
		{
			actor.ProcessDamage(trap.Entity, new Combat.Damage(Combat.DamageType.FIRE, 30 * trap.TrapLevel));
		}

		private static void PitTrap(TrapComponent trap, Entity actor)
		{
			actor.ProcessDamage(trap.Entity, new Combat.Damage(Combat.DamageType.PHYSICAL, 10 * trap.TrapLevel));
		}
	}
}
