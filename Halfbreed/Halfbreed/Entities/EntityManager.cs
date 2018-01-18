using System.Collections.Generic;
namespace Halfbreed.Entities
{
	public class EntityRegister
	{
		private List<Entity> _currentEntities = new List<Entity>();
		private List<Entity> _toDestroy = new List<Entity>();

		public void RegisterEntity(Entity entity)
		{
			if (_currentEntities.Contains(entity))
				ErrorLogger.AddDebugText("Tried to add existing entity to register: " + entity);
			else
				_currentEntities.Add(entity);
		}

		public void DeregisterEntity(Entity entity)
		{
			if (_currentEntities.Contains(entity))
				_currentEntities.Remove(entity);
			else
				ErrorLogger.AddDebugText("Tried to deregister entity not present: " + entity);
		}

		public void FlagEntityForDestruction(Entity entity)
		{
			_toDestroy.Add(entity);
		}

		public void TakeTurn(int currentTime)
		{
			for (int i = 0; i < _currentEntities.Count; i++)
				_currentEntities[i].Update(currentTime);

			// TODO: Work out the best place to get consistent destruction - needs to cleanup and remove from level.
		}

	}
}
