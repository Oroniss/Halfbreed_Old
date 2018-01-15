namespace Halfbreed
{
	public class MaterialProperties
	{
		private readonly Entities.EntityDefensiveStatTemplate _defensiveStatTemplate;
		private readonly int _hpPerUnitVolume;
		private readonly int _weightPerUnitVolume;
		private readonly int _hardness;
		private readonly Colors _fgColor;
		private readonly EntityTraits[] _traits;
		private readonly string _adjective;

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
