using System.Collections.Generic;

namespace Halfbreed
{
	public static class Traits
	{
		static readonly List<string> _traits = new List<string>()
		{
		"Animal",
		"Blind",
		"BlindSight",
		"BlockFly",
		"BlockLOS",
		"BlockSwim",
		"BlockWalk",
		"Climbing",
		"Cloth",
		"DarkVision",
		"ElevationChange",
		"Flying",
		"Furnishing",
		"Gem",
		"Immobilised",
		"Impassible",
		"ImmuneToAcid",
		"ImmuneToCold",
		"ImmuneToDisease",
		"ImmuneToElectricity",
		"ImmuneToFire",
		"ImmuneToLight",
		"ImmuneToMental",
		"ImmuneToNether",
		"ImmuneToPhysical",
		"ImmuneToPoison",
		"ImmuneToShadow",
		"Indestructible",
		"Inorganic",
		"Leather",
		"Liquid",
		"Metal",
		"Plant",
		"Player",
		"Organic",
		"Ruined",
		"Scent",
		"Stone",
		"Swimming",
		"Trap",
		"TrueSeeing",
		"Walking",
		"Wood"
		};

		public static bool IsValidTrait(string trait)
		{
			return _traits.Contains(trait);
		}
	}
}
