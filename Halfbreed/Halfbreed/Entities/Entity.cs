using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class Entity : IComparable
	{
		private string _entityName;

		private Dictionary<ComponentType, Component> _components;

		private int _xLoc;
		private int _yLoc;

		private DisplayLayer _displayLayer;
		private Colors _fgColor;
		private char _symbol;

		private List<EntityTraits> _traits;

		private bool _hasTile;
		private Levels.MapTileDetails _maptile;

		private ConcealedComponent _concealedComponent;
		private bool _isConcealed;
		private bool _playerSpotted;

		private Entity(string entityName, int xLoc, int yLoc, EntityTraits[] traits)
		{
			_entityName = entityName;
			_xLoc = xLoc;
			_yLoc = yLoc;
			_components = new Dictionary<ComponentType, Component>();
			_traits = new List<EntityTraits>();

			_hasTile = false;
			_isConcealed = false;
			_playerSpotted = true;

			foreach (EntityTraits trait in traits)
				AddTrait(trait);
		}

		private void SetupOtherComponents(string[] otherComponents, string[] otherParameters)
		{
			var allParams = MergeOptionalParameters(otherComponents, otherParameters);

			// Door component
			if (Array.IndexOf(allParams, "AddDoorComponent") != -1)
			{
				bool locked = (Array.IndexOf(allParams, "Locked") != -1);
				if (locked)
					_components[ComponentType.DOOR] = new DoorComponent(this, true, allParams);
				else
				{
					bool isOpen = (Array.IndexOf(allParams, "Open") != -1);
					_components[ComponentType.DOOR] = new DoorComponent(this, isOpen);
				}
			}

			// Trap component
			if (Array.IndexOf(allParams, "AddTrapComponent") != -1)
			{
				string trapType = allParams[Array.IndexOf(allParams, "TrapType") + 1];
				var difficulty = int.Parse(allParams[Array.IndexOf(allParams, "TrapLevel") + 1]);
				_components[ComponentType.TRAP] = new TrapComponent(this, trapType, difficulty);
				((InteractibleComponent)_components[ComponentType.INTERACTIBLE]).AddFunction("TriggerTrap");
			}

			// Light source component
			if (Array.IndexOf(allParams, "AddLightSourceComponent") != -1)
			{
				string lightType = allParams[Array.IndexOf(allParams, "LightType") + 1];
				int radius = int.Parse(allParams[Array.IndexOf(allParams, "Radius") + 1]);
				int durationRemaining = int.Parse(allParams[Array.IndexOf(allParams, "DurationRemaining") + 1]);
				bool permanent = (Array.IndexOf(allParams, "PermanentLight") != -1);
				bool lit = (Array.IndexOf(allParams, "IsLit") != -1);

				_components[ComponentType.LIGHTSOURCE] = new LightSourceComponent(this, lightType, radius, durationRemaining,
																				  permanent, lit);
			}

			if (Array.IndexOf(allParams, "Concealed") != -1)
			{
				// TODO: Set this up properly
				_playerSpotted = false;
				_isConcealed = true;
				_concealedComponent = new ConcealedComponent(this, 1, "", ' ', (int)DisplayLayer.NOTDISPLAYED, true, "WOODWALL");

			}

			if (Array.IndexOf(allParams, "LevelTransition") != -1)
			{
				AddTrait(EntityTraits.INDESTRUCTIBLE);
				((InteractibleComponent)_components[ComponentType.INTERACTIBLE]).AddFunction("UseLevelTransition");
				var destination = (Levels.LevelEnum)Enum.Parse(typeof(Levels.LevelEnum), allParams[Array.IndexOf(allParams, "DestinationLevel") + 1]);
				var newXLoc = int.Parse(allParams[Array.IndexOf(allParams, "NewXLoc") + 1]);
				var newYLoc = int.Parse(allParams[Array.IndexOf(allParams, "NewYLoc") + 1]);
				_components[ComponentType.LEVELTRANSITION] = new LevelTransitionComponent(this, destination, newXLoc, newYLoc);
			}
		}

		private string[] MergeOptionalParameters(string[] otherComponents, string[] otherParameters)
		{
			var returnArray = new string[otherComponents.Length + otherParameters.Length];
			for (int i = 0; i < otherComponents.Length; i++)
				returnArray[i] = otherComponents[i];
			for (int i = 0; i < otherParameters.Length; i++)
				returnArray[i + otherComponents.Length] = otherParameters[i];
			return returnArray;
		}

		public string EntityName
		{
			get { return _entityName; }
		}

		public DisplayLayer DisplayLayer
		{
			get {
				if (!_playerSpotted)
					return _concealedComponent.ConcealedDisplayLayer;
				return _displayLayer;}
			set { _displayLayer = value; }
		}

		public Colors FGColor
		{
			get {
				if (!_playerSpotted)
					return _concealedComponent.ConcealedFGColor;
				return _fgColor;}
			set { _fgColor = value; }
		}

		public char Symbol
		{
			get {
				if (!_playerSpotted)
					return _concealedComponent.ConcealedSymbol;
				return _symbol; }
			set { _symbol = value; }
		}

		public bool Concealed
		{
			get { return _isConcealed; }
		}

		public bool PlayerSpotted
		{
			get { return _playerSpotted; }
			set { _playerSpotted = value; }
		}

		public bool HasTile
		{
			get { return _hasTile || (!_playerSpotted && _concealedComponent.HasConcealedTile); }
		}

		public Levels.MapTileDetails TileDetails
		{
			get { if (!_playerSpotted && _concealedComponent.HasConcealedTile)
					return _concealedComponent.ConcealedMapTile;
				if (_hasTile)
					return _maptile;
				return null;
			}
		}

		public ConcealedComponent ConcealedComponent
		{
			get { return _concealedComponent; }
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

		public void UpdatePosition(Position newPosition)
		{
			_xLoc = newPosition.X;
			_yLoc = newPosition.Y;
		}

		public void UpdatePosition(int newX, int newY)
		{
			_xLoc = newX;
			_yLoc = newY;
		}

		public void MoveEntity(int deltaX, int deltaY)
		{
			_xLoc += deltaX;
			_yLoc += deltaY;
		}

		public void AddTrait(EntityTraits trait) 
		{
			_traits.Add(trait);
		}

		public void RemoveTrait(EntityTraits trait) 
		{
			if (_traits.Contains(trait))
				_traits.Remove(trait);
			else
				ErrorLogger.AddDebugText(string.Format("Tried to remove non-existant trait from entity" +
				                                       "Entity: {0}, Trait: {1}", this, trait));
		}

		public bool HasTrait(EntityTraits trait)
		{
			return _traits.Contains(trait);
		}

		public int CompareTo(object obj)
		{
			return _displayLayer.CompareTo(((Entity)obj).DisplayLayer);
		}

		public bool HasComponent(ComponentType componentType)
		{
			return _components.ContainsKey(componentType);
		}

		public Component GetComponent(ComponentType componentType)
		{
			if (_components.ContainsKey(componentType))
				return _components[componentType];
			ErrorLogger.AddDebugText(string.Format("Asked for component type {0}, which is not present on {1}.", componentType, this));
			return null;
		}

		public void Update(int currentTime)
		{
			// TODO: Go through effects and check if any expire.

			if (HasComponent(ComponentType.LIGHTSOURCE))
			{
				var lightSource = (LightSourceComponent)GetComponent(ComponentType.LIGHTSOURCE);
				if (lightSource.IsLit)
				{
					lightSource.DecreaseDuration(1);
					lightSource.UpdateLitTiles(GameEngine.CurrentLevel);
				}
			}

			if (HasComponent(ComponentType.INPUT))
			{
				InputComponent inputComponent = (InputComponent)_components[ComponentType.INPUT];
				inputComponent.GetNextMove(currentTime);
			}
		}

		public Combat.Damage ProcessDamage(Entity attacker, Combat.Damage Damage)
		{
			MainGraphicDisplay.TextConsole.AddOutputText(string.Format("{0} takes {1} points of {2} damage",
																	   this, Damage.FinalDamageAmount, Damage.DamageType));
			return Damage;
		}

	}
}
