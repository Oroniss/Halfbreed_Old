using System.Collections.Generic;

namespace Halfbreed
{
	public partial class Entity
	{
		private static Dictionary<int, List<ComponentTypes>> _entityComponentTypes = new Dictionary<int, List<ComponentTypes>>();
		private static Dictionary<int, List<Component>> _entityComponents = new Dictionary<int, List<Component>>();
		private static Dictionary<ComponentTypes, List<int>> _allEntitiesWithComponents = new Dictionary<ComponentTypes, List<int>>()
		{
			{ComponentTypes.DISPLAY, new List<int>()},
			{ComponentTypes.POSITION, new List<int>()}
		};

		public static bool HasComponent(int entityId, ComponentTypes componentType)
		{
			return _allEntitiesWithComponents[componentType].Contains(entityId);
		}

		public static void AddComponent(int entityId, Component component)
		{
			// TODO: Figure out whether to allow multiple components of the same type or not.
			// TODO: If not, check for it here.
			if (_entityComponentTypes.ContainsKey(entityId) && _entityComponents.ContainsKey(entityId))
			{
				_entityComponentTypes[entityId].Add(component.ComponentType);
				_entityComponents[entityId].Add(component);
				_allEntitiesWithComponents[component.ComponentType].Add(entityId);
			}
			else
				ErrorLogger.AddDebugText(string.Format("Tried to add component to unknown entity ID: {0}", entityId.ToString()));
			return;
		}

		public static void RemoveComponent(int entityId, Component component)
		{
			if (_entityComponentTypes.ContainsKey(entityId))
			{
				if (_entityComponentTypes[entityId].Contains(component.ComponentType))
					_entityComponentTypes[entityId].Remove(component.ComponentType);
				else
					ErrorLogger.AddDebugText(string.Format(string.Format("Tried to remove component on entity ID: {0}" +
					                                                     " but component type: {1} not present", entityId.ToString(), component.ComponentType)));
			}
			else
				ErrorLogger.AddDebugText(string.Format("Tried to remove component on unknown entity ID: {0}", entityId.ToString()));

			if (_entityComponents.ContainsKey(entityId))
			{
				if (_entityComponents[entityId].Contains(component))
					_entityComponents[entityId].Remove(component);
				else
					ErrorLogger.AddDebugText(string.Format("Tried to remove component {0} from entity ID {1}" +
														   " but component not present", component, entityId.ToString()));
			}
			else
				ErrorLogger.AddDebugText(string.Format("Tried to remove component on unknown entity ID: {0}", entityId.ToString()));

			if (_allEntitiesWithComponents[component.ComponentType].Contains(entityId))
				_allEntitiesWithComponents[component.ComponentType].Remove(entityId);
			else
				ErrorLogger.AddDebugText(string.Format("Tried to remove component {0} from entity id {1}" +
													   " but entity not present in all entities dict", component, entityId.ToString()));

		}

		public static void RemoveComponent(int entityId, ComponentTypes componentType)
		{
			if (_entityComponentTypes.ContainsKey(entityId))
			{
				while (_entityComponentTypes[entityId].Contains(componentType))
					_entityComponentTypes[entityId].Remove(componentType);
			}

			if (_entityComponents.ContainsKey(entityId))
			{
				for (int i = _entityComponents[entityId].Count; i >= 0; i--)
				{
					if (_entityComponents[entityId][i].ComponentType == componentType)
						_entityComponents[entityId].RemoveAt(i);
				}
			}

			while (_allEntitiesWithComponents[componentType].Contains(entityId))
				_allEntitiesWithComponents[componentType].Remove(entityId);
		}

		public static Component GetComponent(int entityId, ComponentTypes componentType)
		{
			if (_entityComponents.ContainsKey(entityId))
			{
				foreach (Component component in _entityComponents[entityId])
				{
					// TODO: Figure out whether to allow multiple components of the same type here.
					if (component.ComponentType == componentType)
						return component;
				}
			}
			// TODO: Print an error message here - might even need to be an exception.
			return null;
		}


	}
}
