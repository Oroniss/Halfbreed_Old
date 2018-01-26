namespace Halfbreed.Entities
{
	public class LightSourceComponent:Component
	{
		bool _lit;
		LightTypes _lightType;
		int _radius;
		int _durationRemaining;
		bool _permanent;


		public LightSourceComponent(Entity entity, string lightType, int radius, int durationRemaining, bool permanent, bool lit)
			:base(entity)
		{
			_componentType = ComponentType.LIGHTSOURCE;

			_radius = radius;
			_durationRemaining = durationRemaining;
			_permanent = permanent;
			_lit = lit;
			_lightType = (LightTypes)System.Enum.Parse(typeof(LightTypes), lightType);
		}

		public void UpdateLitTiles(Level level)
		{
			level.UpdateLightSource(this);
		}

		public bool IsLit
		{
			get { return _lit; }
		}

		public void DecreaseDuration(int amount)
		{
			if (!_permanent && _lit)
			{
				_durationRemaining -= amount;
				if (_durationRemaining <= 0)
					Extinguish();
			}
		}

		public void Light()
		{
			if (_permanent || _durationRemaining > 0)
			{
				_lit = true;
				GameEngine.CurrentLevel.UpdateLightSource(this);
			}
		}

		public void Extinguish()
		{
			_lit = false;
			GameEngine.CurrentLevel.UpdateLightSource(this);
		}

		public int LightRadius
		{
			get { return _radius; }
		}
	}

	public enum LightTypes
	{
		FIRE,
		ALCHEMICAL,
		ELECTRICAL,
		MAGICAL
	}
}
