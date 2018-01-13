namespace Halfbreed
{
	public struct MaterialProperties
	{
		private Entities.EntityDefensiveStatTemplate _defensiveStatTemplate;
		private int _hpPerUnitVolume;
		private int _weightPerUnitVolume;
		private int _hardness;
		private Colors _fgColor;
		private EntityTraits[] _traits;
		private string _adjective;

		public MaterialProperties(int acid, int cold, int electricity, int fire, int poison, int disease, int light,
								  int shadow, int mental, int physical, int nether, int hpPV, int weightPV, 
		                          int hardness, Colors fgColor, EntityTraits[] traits, string adjective)
		{
			_defensiveStatTemplate = new Entities.EntityDefensiveStatTemplate(acid, cold, electricity, fire, poison,
																			  disease, light, shadow, mental, physical, nether);

			_hpPerUnitVolume = hpPV;
			_weightPerUnitVolume = weightPV;
			_hardness = hardness;
			_fgColor = fgColor;
			_traits = traits;
			_adjective = adjective;
		}

		public int HPPerUnitVolume
		{
			get { return _hpPerUnitVolume; }
		}
		public int WeightPerUnitVolume
		{
			get{ return _weightPerUnitVolume; }
		}
		public int Hardness
		{
			get { return _hardness; }
		}
		public Colors FGColor
		{
			get { return _fgColor; }
		}

		public EntityTraits[] Traits
		{
			get { return _traits;}
		}

		public string Adjective
		{
			get { return _adjective; }
		}

		public Entities.EntityDefensiveStatTemplate DefensiveStatTemplate
		{
			get { return _defensiveStatTemplate; }
		}

	}

}
