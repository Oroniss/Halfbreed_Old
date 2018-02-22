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

		List<object> _effects;
		object _defensiveStats;

		Dictionary<string, string> _otherAttributes;

		protected Entity(string entityName, int xLoc, int yLoc, List<string> otherParameters)
		{
			_entityName = entityName;
			_xLoc = xLoc;
			_yLoc = yLoc;

			_traits = new List<Traits>();

			_effects = null;
			_defensiveStats = null;
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

		public string GetDescription()
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
			MainGraphicDisplay.TextConsole.AddOutputText(string.Format("{0} takes {1} points of {2} damage",
																	   this, Damage.FinalDamageAmount, Damage.DamageType));
			return Damage;
		}

	}
}
