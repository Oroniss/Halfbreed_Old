using System.Collections.Generic;
namespace Halfbreed.Entities
{
	public class EntityManager
	{

		private List<Entity> _currentEntities = new List<Entity>();
		private List<Entity> _toDestroy = new List<Entity>();

		public void TakeTurn(int currentTime)
		{
			for (int i = 0; i < _currentEntities.Count; i++)
				_currentEntities[i].Update(currentTime);

			// TODO: Work out the best place to get consistent destruction - needs to cleanup and remove from level.
		}

	}
}
