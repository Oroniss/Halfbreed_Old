// Tidied up for version 0.02.

using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public abstract class Entity
	{
		string _entityName;

		protected int _xLoc;
		protected int _yLoc;

		Colors _fgColor;
		char _symbol;

		List<Traits> _traits;

		bool _isConcealed;
		bool _playerSpotted;

		protected bool _destroyed;
		protected DefensiveStatBlock _defensiveStats; // Needs to be protected for some overrides of process damage.
		protected int _maxHealth;
		protected int _currentHealth;

		List<object> _effects;

		Dictionary<string, string> _otherAttributes;

		protected Entity(string entityName, int xLoc, int yLoc, List<string> otherParameters)
		{
			_entityName = entityName;
			_xLoc = xLoc;
			_yLoc = yLoc;

			_traits = new List<Traits>();

			_effects = null;
			_defensiveStats = new DefensiveStatBlock();
			_otherAttributes = new Dictionary<string, string>();

			_destroyed = false;
			_playerSpotted = true;

			var basicDetails = EntityData.GetEntityDetails(entityName);
			_symbol = basicDetails.Symbol;
			_fgColor = basicDetails.FGColor;
			foreach (Traits trait in basicDetails.Traits)
				AddTrait(trait);

		}

		public string EntityName
		{
			get { return _entityName; }
		}

		public virtual Colors FGColor
		{
			get { return _fgColor; }
		}

		public char Symbol
		{
			get {
				if (!_playerSpotted)
					return ' ';
				return _symbol; }
			set { _symbol = value; }
		}

		public bool Concealed
		{
			get { return _isConcealed; }
			set { _isConcealed = value; }
		}

		public bool PlayerSpotted
		{
			get { return _playerSpotted; }
			set { _playerSpotted = value; }
		}

		public override string ToString()
		{
			return _entityName;
		}

		public virtual string GetDescription()
		{
			return _entityName;
		}

		public int XLoc
		{
			get { return _xLoc; }
		}

		public int YLoc
		{
			get { return _yLoc; }
		}

		public void AddTrait(Traits trait) 
		{
			_traits.Add(trait);
		}

		public void RemoveTrait(Traits trait) 
		{
			if (_traits.Contains(trait))
				_traits.Remove(trait);
			else
				ErrorLogger.AddDebugText(string.Format("Tried to remove non-existant trait from entity" +
				                                       "Entity: {0}, Trait: {1}", this, trait));
		}

		public bool HasTrait(Traits trait)
		{
			return _traits.Contains(trait);
		}

		public bool HasOtherAttribute(string attributeName)
		{
			return _otherAttributes.ContainsKey(attributeName);
		}

		public void SetOtherAttribute(string attributeName, string attributeValue)
		{
			_otherAttributes[attributeName] = attributeValue;
		}

		public string GetOtherAttributeValue(string attributeName)
		{
			return _otherAttributes[attributeName];
		}

		public virtual void Update(Level currentLevel)
		{
			// TODO: Go through effects and check if any expire.
		}

		public virtual Combat.Damage ProcessDamage(Entity attacker, Combat.Damage Damage)
		{
			var defensiveDice = getDefensiveStat(Damage.DamageType).GetDefensiveDice();

			var reduction = 0;
			for (int i = 0; i < 5; i++)
				reduction -= (int)defensiveDice[i].Roll(0);

			Damage.ModifyDamage(reduction);

			MainGraphicDisplay.TextConsole.AddOutputText(string.Format("{0} takes {1} points of {2} damage",
														 this, Damage.FinalDamageAmount, Damage.DamageType));
			return Damage;
		}



		DefensiveStat getDefensiveStat(Combat.DamageType damageType)
		{
			switch (damageType)
			{
				case Combat.DamageType.Acid:
					return _defensiveStats.AcidResist;
				case Combat.DamageType.Cold:
					return _defensiveStats.ColdResist;
				case Combat.DamageType.Electricity:
					return _defensiveStats.ElectricityResist;
				case Combat.DamageType.Fire:
					return _defensiveStats.FireResist;
				case Combat.DamageType.Disease:
					return _defensiveStats.DiseaseResist;
				case Combat.DamageType.Poison:
					return _defensiveStats.PoisonResist;
				case Combat.DamageType.Light:
					return _defensiveStats.LightResist;
				case Combat.DamageType.Shadow:
					return _defensiveStats.ShadowResist;
				case Combat.DamageType.Physical:
					return _defensiveStats.PhysicalResist;
				case Combat.DamageType.Mental:
					return _defensiveStats.MentalResist;
				case Combat.DamageType.Nether:
					return _defensiveStats.NetherResist;
			}
			// TODO: Print error here.
			return _defensiveStats.PhysicalResist;
		}
	}
}
