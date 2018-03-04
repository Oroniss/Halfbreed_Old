namespace Halfbreed
{
	public class MaterialProperties
	{
		private readonly int _hpPerUnitVolume;
		private readonly int _weightPerUnitVolume;
		private readonly int _hardness;
		private readonly Colors _fgColor;
		private readonly Traits[] _traits;
		private readonly string _adjective;

		public MaterialProperties(int acid, int cold, int electricity, int fire, int poison, int disease, int light,
								  int shadow, int mental, int physical, int nether, int hpPV, int weightPV, 
		                          int hardness, Colors fgColor, Traits[] traits, string adjective)
		{

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

		public Traits[] Traits
		{
			get { return _traits;}
		}

		public string Adjective
		{
			get { return _adjective; }
		}

	}

}
