namespace Halfbreed.Entities
{
	public struct EntityDefensiveStatTemplate
	{
		private int _acidResistance;
		private int _coldResistance;
		private int _electricityResistance;
		private int _fireResistance;
		private int _poisonResistance;
		private int _diseaseResistance;
		private int _lightResistance;
		private int _shadowResistance;
		private int _mentalResistance;
		private int _physicalResistance;
		private int _netherResistance;

		public EntityDefensiveStatTemplate(int acid, int cold, int electricity, int fire, int poison, int disease,
										   int light, int shadow, int mental, int physical, int nether)
		{
			_acidResistance = acid;
			_coldResistance = cold;
			_electricityResistance = electricity;
			_fireResistance = fire;
			_poisonResistance = poison;
			_diseaseResistance = disease;
			_lightResistance = light;
			_shadowResistance = shadow;
			_mentalResistance = mental;
			_physicalResistance = physical;
			_netherResistance = nether;
		}

		public int AcidResistance
		{
			get { return _acidResistance; }
		}
		public int ColdResistance
		{
			get { return _coldResistance; }
		}
		public int ElectricityResistance
		{
			get { return _electricityResistance; }
		}
		public int FireResistance
		{
			get { return _fireResistance; }
		}
		public int PoisonResistance
		{
			get { return _poisonResistance; }
		}
		public int DiseaseResistance
		{
			get { return _diseaseResistance; }
		}
		public int LightResistance
		{
			get { return _lightResistance; }
		}
		public int ShadowResistance
		{
			get { return _shadowResistance; }
		}
		public int MentalResistance
		{
			get { return _mentalResistance; }
		}
		public int PhysicalResistance
		{
			get { return _physicalResistance; }
		}
		public int NetherResistance
		{
			get { return _netherResistance; }
		}
	}
}
