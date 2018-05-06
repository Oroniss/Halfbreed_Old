using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class Actor:Entity
	{
		static SortedDictionary<int, Actor> _actors = new SortedDictionary<int, Actor>();
		static int _maxActorId = 1;
		static List<int> _unusedEntityIds = new List<int>();

		readonly int _viewDistance;
		protected PrimaryStatBlock _primaryStats;
		readonly int _actorId;

		public Actor(string actorName, int xLoc, int yLoc, List<string> otherParameters)
			:base(actorName, xLoc, yLoc, otherParameters)
		{
			if (_unusedEntityIds.Count > 0)
			{
				_actorId = _unusedEntityIds[0];
				_unusedEntityIds.RemoveAt(0);
			}
			else
			{
				_actorId = _maxActorId;
				_maxActorId++;
			}
			_actors[_actorId] = this;

			AddTrait("Impassible");
			_viewDistance = 18; // Fix this properly later on.

			_primaryStats = new PrimaryStatBlock();
			_defensiveStats = new DefensiveStatBlock();
		}

		public int ActorId
		{
			get { return _actorId; }
		}

		protected virtual void GetNextMove(Level currentLevel)
		{
			
		}

		public void UpdatePosition(int newX, int newY)
		{
			_xLoc = newX;
			_yLoc = newY;
		}

		public override void Update(Level currentLevel)
		{
			base.Update(currentLevel);

			if (!_destroyed)
				GetNextMove(currentLevel);
		}

		public int ViewDistance
		{
			get { return _viewDistance; }
		}

		protected void UpgradePrimaryStatDice(PrimaryStatUpgrade upgrade)
		{
			switch (upgrade.Stat)
			{
				case PrimaryStat.Agility:
					{
						_primaryStats.Agility()[upgrade.DiceNumber].UpgradeDiceType();
						return;
					}
				case PrimaryStat.Might:
					{
						_primaryStats.Might()[upgrade.DiceNumber].UpgradeDiceType();
						return;
					}
				case PrimaryStat.Mind:
					{
						_primaryStats.Mind()[upgrade.DiceNumber].UpgradeDiceType();
						return;
					}
				case PrimaryStat.Presence:
					{
						_primaryStats.Presence()[upgrade.DiceNumber].UpgradeDiceType();
						return;
					}
			}
		}

		public static Actor GetActor(int id)
		{
			return _actors[id];
		}
	}
}
