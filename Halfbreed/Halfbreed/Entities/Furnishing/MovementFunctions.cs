using System.Collections.Generic;

namespace Halfbreed.Entities.Furnishings
{
	public static class MovementFunctions
	{
		static readonly Dictionary<string, MoveOffFunction> _moveOffFunctions = new Dictionary<string, MoveOffFunction>()
		{
		};

		static readonly Dictionary<string, MoveOnFunction> _moveOnFunctions = new Dictionary<string, MoveOnFunction>()
		{
			{"Pit Trap Move On", new MoveOnFunction(PitTrapMoveOn)},
			{"Flame Vent Trap Move On", new MoveOnFunction(FlameVentTrapMoveOn)}
		};

		public static MoveOffFunction GetMoveOffFunction(string functionName)
		{
			return _moveOffFunctions[functionName];
		}

		public static MoveOnFunction GetMoveOnFunction(string functionName)
		{
			return _moveOnFunctions[functionName];
		}

		public delegate bool MoveOffFunction(Furnishing furnishing, Actor actor, Level level, int destinationX, int destinationY);
		public delegate void MoveOnFunction(Furnishing furnishing, Actor actor, Level level, int originX, int originY);

		private static void PitTrapMoveOn(Furnishing furnishing, Actor actor, Level level, int originX, int originY)
		{
			if (actor.HasTrait(Traits.Flying))
				return;

			var trapLevel = int.Parse(furnishing.GetOtherAttributeValue("TrapLevel"));
			actor.ProcessDamage(furnishing, new Combat.Damage(Combat.DamageType.Physical, 7 * trapLevel));
		}

		private static void FlameVentTrapMoveOn(Furnishing furnishing, Actor actor, Level level, int originX, int originY)
		{
			if (actor.HasTrait(Traits.Flying))
				return;

			var trapLevel = int.Parse(furnishing.GetOtherAttributeValue("TrapLevel"));
			actor.ProcessDamage(furnishing, new Combat.Damage(Combat.DamageType.Fire, 10 * trapLevel));
			furnishing.Trapped = false;
			furnishing.MoveOnFunction = null;
		}
	}
}
