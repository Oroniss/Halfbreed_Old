using System.Collections.Generic;
using Halfbreed.Entities;

namespace Halfbreed
{
	public abstract class Entity	
	{
		private static int _currentMaxEntityId = 0;
		private static List<int> _freeEntityIds = new List<int>();

		private int _entityId;
		protected EntityClass _entityClass;
		private List<EntityTraits> _traits;

		private int _xLoc;
		private int _yLoc;

		private Colors _fgColor;
		private char _symbol;

		// TODO: Add in something for LOS and Movement here.
		// Also need buff component
		// And Description methods.

		// TODO: Add in three optional components - light source, container, concealed.

		protected EntityDefensiveStatBlock _defensiveStatBlock;

		protected int currentHealth;
		protected int maxHealth; // TODO: Work out whether we need something more than this here.

		protected Entity(string entityName, int xLoc, int yLoc, string[] otherParameters)
		{
			if (_freeEntityIds.Count == 0)
			{
				_entityId = _currentMaxEntityId;
				_currentMaxEntityId += 1;
			}
			else
			{
				_entityId = _freeEntityIds[0];
				_freeEntityIds.RemoveAt(0);
			}

			_traits = new List<EntityTraits>();
			_entityClass = EntityClass.ENTITY;

			foreach (EntityTraits trait in ComponentDatabaseConnection.GetEntityTraits(entityName))
				_traits.Add(trait);

			_xLoc = xLoc;
			_yLoc = yLoc;
		}

		public bool HasTrait(EntityTraits trait)
		{
			return _traits.Contains(trait);
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
				ErrorLogger.AddDebugText("Tried to remove trait: {0} from entity: {0}, but it was not present");
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

		public void MoveEntity(int deltaX, int deltaY)
		{
			_xLoc += deltaX;
			_yLoc += deltaY;
		}

		public EntityClass EntityClass
		{
			get { return _entityClass; }
		}

		public EntityDefensiveStatBlock DefensiveStatBlock
		{
			get { return _defensiveStatBlock; }
		}
	}

}
