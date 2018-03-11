using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class DefensiveStatBlock
	{
		DefensiveStat _acidResist;
		DefensiveStat _coldResist;
		DefensiveStat _electricityResist;
		DefensiveStat _fireResist;
		DefensiveStat _diseaseResist;
		DefensiveStat _poisonResist;
		DefensiveStat _lightResist;
		DefensiveStat _shadowResist;
		DefensiveStat _physicalResist;
		DefensiveStat _mentalResist;
		DefensiveStat _netherResist;

		public DefensiveStatBlock()
		{
			_acidResist = new DefensiveStat();
			_coldResist = new DefensiveStat();
			_electricityResist = new DefensiveStat();
			_fireResist = new DefensiveStat();
			_diseaseResist = new DefensiveStat();
			_poisonResist = new DefensiveStat();
			_lightResist = new DefensiveStat();
			_shadowResist = new DefensiveStat();
			_physicalResist = new DefensiveStat();
			_mentalResist = new DefensiveStat();
			_netherResist = new DefensiveStat();
		}

		public DefensiveStat AcidResist
		{
			get { return _acidResist; }
		}

		public DefensiveStat ColdResist
		{
			get { return _coldResist; }
		}

		public DefensiveStat ElectricityResist
		{
			get { return _electricityResist; }
		}

		public DefensiveStat FireResist
		{
			get { return _fireResist; }
		}

		public DefensiveStat DiseaseResist
		{
			get { return _diseaseResist; }
		}

		public DefensiveStat PoisonResist
		{
			get { return _poisonResist; }
		}

		public DefensiveStat LightResist
		{
			get { return _lightResist; }
		}

		public DefensiveStat ShadowResist
		{
			get { return _shadowResist; }
		}

		public DefensiveStat PhysicalResist
		{
			get { return _physicalResist; }
		}

		public DefensiveStat MentalResist
		{
			get { return _mentalResist; }
		}

		public DefensiveStat NetherResist
		{
			get { return _netherResist; }
		}
}
}
