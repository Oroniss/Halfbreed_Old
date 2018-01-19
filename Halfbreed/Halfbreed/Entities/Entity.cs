﻿using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class Entity : IComparable
	{
		private static int _currentMaxEntityId = 0;
		private static List<int> _currentFreeEntityIds = new List<int>();

		private string _entityName;
		private int _entityId;

		private Dictionary<ComponentType, Component> _components;

		private int _xLoc;
		private int _yLoc;

		private DisplayLayer _displayLayer;
		private Colors _fgColor;
		private char _symbol;

		private List<EntityTraits> _traits;

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
			int toReturn = _currentMaxEntityId;
			if (_currentFreeEntityIds.Count > 0)
			{
				toReturn = _currentFreeEntityIds[0];
				_currentFreeEntityIds.Remove(toReturn);
			}
			else
				_currentMaxEntityId++;
			return toReturn;
		}

		public string EntityName
		{
			get { return _entityName; }
		}

		public DisplayLayer DisplayLayer
		{
			get {return _displayLayer;}
			set { _displayLayer = value; }
		}

		public Colors FGColor
		{
			get {return _fgColor;}
			set { _fgColor = value; }
		}

		public char Symbol
		{
			get { return _symbol; }
			set { _symbol = value; }
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
			if (HasComponent(ComponentType.INPUT))
			{
				InputComponent inputComponent = (InputComponent)_components[ComponentType.INPUT];
				inputComponent.GetNextMove(currentTime);
			}
		}

	}
}
