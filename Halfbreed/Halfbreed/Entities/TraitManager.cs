using System.Collections.Generic;
namespace Halfbreed
{
	public static partial class EntityManager
	{
		private static Dictionary<int, List<EntityTraits>> _entityTraits = new Dictionary<int, List<EntityTraits>>();

		public static bool HasTrait(int entityId, EntityTraits trait)
		{
			if (_entityTraits.ContainsKey(entityId))
				return _entityTraits[entityId].Contains(trait);
			else
			{
				ErrorLogger.AddDebugText(string.Format("Checked for trait on unknown entity ID: {0}", entityId.ToString()));
				return false;
			}
		}

		public static void AddTrait(int entityId, EntityTraits trait)
		{
			if (_entityTraits.ContainsKey(entityId))
			{
				_entityTraits[entityId].Add(trait);
				// TODO: Should we have an analagous list as per Components?
				return;
			}
			else
			{
				ErrorLogger.AddDebugText(string.Format("Tried to add trait on unknown entity ID: {0}", entityId.ToString()));
				return;
			}
		}

		public static void RemoveTrait(int entityId, EntityTraits trait)
		{
			if (_entityTraits.ContainsKey(entityId))
			{
				if (_entityTraits[entityId].Contains(trait))
					_entityTraits[entityId].Remove(trait);
				else
				{
					ErrorLogger.AddDebugText(string.Format("Tried to remove trait {0} from entity id {1}" +
														   " but it was not present.", trait.ToString(), entityId.ToString()));
					return;
				}
			}
			else
			{
				ErrorLogger.AddDebugText(string.Format("Tried to remove trait from unknown entity ID: {0}", entityId.ToString()));
				return;
			}
		}
	}
}
