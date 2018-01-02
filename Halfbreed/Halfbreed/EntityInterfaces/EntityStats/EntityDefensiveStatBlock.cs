using System;

namespace Halfbreed.Entities
{
	public class EntityDefensiveStatBlock
	{
		private EntityDefensiveStat _acidResistance;
		private EntityDefensiveStat _coldResistance;
		private EntityDefensiveStat _fireResistance;
		private EntityDefensiveStat _electricityResistance;
		private EntityDefensiveStat _poisonResistance;
		private EntityDefensiveStat _diseaseResistance;
		private EntityDefensiveStat _lightResistance;
		private EntityDefensiveStat _shadowResistance;
		private EntityDefensiveStat _mentalResistance;
		private EntityDefensiveStat _physicalResistance;
		private EntityDefensiveStat _netherResistance;

		public EntityDefensiveStatBlock(EntityDefensiveStatTemplate template)
		{
			_acidResistance = new EntityDefensiveStat(template.AcidResistance);
			_coldResistance = new EntityDefensiveStat(template.ColdResistance);
			_electricityResistance = new EntityDefensiveStat(template.ElectricityResistance);
			_fireResistance = new EntityDefensiveStat(template.FireResistance);
			_poisonResistance = new EntityDefensiveStat(template.PoisonResistance);
			_diseaseResistance = new EntityDefensiveStat(template.DiseaseResistance);
			_lightResistance = new EntityDefensiveStat(template.LightResistance);
			_shadowResistance = new EntityDefensiveStat(template.ShadowResistance);
			_mentalResistance = new EntityDefensiveStat(template.MentalResistance);
			_physicalResistance = new EntityDefensiveStat(template.PhysicalResistance);
			_netherResistance = new EntityDefensiveStat(template.NetherResistance);
		}

		public int GetDefenseChance(EntityDefenseTypes defensiveStat)
		{
			switch (defensiveStat)
			{
				case EntityDefenseTypes.ACIDRESISTANCE:
					return _acidResistance.Chance;
				case EntityDefenseTypes.COLDRESISTANCE:
					return _coldResistance.Chance;
				case EntityDefenseTypes.ELECTRICITYRESISTANCE:
					return _electricityResistance.Chance;
				case EntityDefenseTypes.FIRERESISTANCE:
					return _fireResistance.Chance;
				case EntityDefenseTypes.POISONRESISTANCE:
					return _poisonResistance.Chance;
				case EntityDefenseTypes.DISEASERESISTANCE:
					return _diseaseResistance.Chance;
				case EntityDefenseTypes.LIGHTRESISTANCE:
					return _lightResistance.Chance;
				case EntityDefenseTypes.SHADOWRESISTANCE:
					return _shadowResistance.Chance;
				case EntityDefenseTypes.MENTALRESISTANCE:
					return _mentalResistance.Chance;
				case EntityDefenseTypes.PHYSICALRESISTANCE:
					return _physicalResistance.Chance;
				case EntityDefenseTypes.NETHERRESISTANCE:
					return _netherResistance.Chance;
			}
			return -200;
		}

		public int GetDefenseRating(EntityDefenseTypes defensiveStat)
		{
			switch (defensiveStat)
			{
				case EntityDefenseTypes.ACIDRESISTANCE:
					return _acidResistance.Rating;
				case EntityDefenseTypes.COLDRESISTANCE:
					return _coldResistance.Rating;
				case EntityDefenseTypes.ELECTRICITYRESISTANCE:
					return _electricityResistance.Rating;
				case EntityDefenseTypes.FIRERESISTANCE:
					return _fireResistance.Rating;
				case EntityDefenseTypes.POISONRESISTANCE:
					return _poisonResistance.Rating;
				case EntityDefenseTypes.DISEASERESISTANCE:
					return _diseaseResistance.Rating;
				case EntityDefenseTypes.LIGHTRESISTANCE:
					return _lightResistance.Rating;
				case EntityDefenseTypes.SHADOWRESISTANCE:
					return _shadowResistance.Rating;
				case EntityDefenseTypes.MENTALRESISTANCE:
					return _mentalResistance.Rating;
				case EntityDefenseTypes.PHYSICALRESISTANCE:
					return _physicalResistance.Rating;
				case EntityDefenseTypes.NETHERRESISTANCE:
					return _netherResistance.Rating;
			}
			return 5;
		}
	}
}
