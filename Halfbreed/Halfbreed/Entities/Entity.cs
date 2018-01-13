using System;
using System.Collections.Generic;

namespace Halfbreed
{
	public partial class Entity : IComparable
	{
		private string _entityName;
		private int _entityId;

		private Dictionary<ComponentType, Component> _components;

		protected int _xLoc;
		protected int _yLoc;

		protected DisplayLayer _displayLayer;
		protected Colors _fgColor;
		protected char _symbol;

		protected List<EntityTraits> _traits;

		private Entity(string entityName, int xLoc, int yLoc, EntityTraits[] traits)
		{
			_entityId = GetNextId();
			_entityName = entityName;
			_xLoc = xLoc;
			_yLoc = yLoc;
			_components = new Dictionary<ComponentType, Component>();
			_traits = new List<EntityTraits>();

			foreach (EntityTraits trait in traits)
				AddTrait(trait);
		}

		private static int GetNextId()
		{
			return 0;
		}

		public string EntityName
		{
			get { return _entityName; }
		}

		public DisplayLayer DisplayLayer
		{
			get {return _displayLayer;}
		}

		public Colors FGColor
		{
			get{return _fgColor;}
		}

		public char Symbol
		{
			get { return _symbol; }
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

	}
}
