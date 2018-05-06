using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	[Serializable]
	public class Actor:Entity
	{
		readonly int _viewDistance;
		protected PrimaryStatBlock _primaryStats;

		public Actor(string actorName, int xLoc, int yLoc, List<string> otherParameters)
			:base(actorName, xLoc, yLoc, otherParameters)
		{
			AddTrait("Impassible");
			_viewDistance = 18; // Fix this properly later on.

			_primaryStats = new PrimaryStatBlock();
			_defensiveStats = new DefensiveStatBlock();
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
	}
}
