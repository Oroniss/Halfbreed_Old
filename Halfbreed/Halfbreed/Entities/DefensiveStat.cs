using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class DefensiveStat
	{
		List<Dice> _dice;

		public DefensiveStat()
		{
			_dice = new List<Dice>();
			for (int i = 0; i < 5; i++)
				_dice.Add(new Dice(DiceType.D3, 1));
		}

		void OrderDice()
		{
			_dice.Sort();
			_dice.Reverse();
		}

		public void AddDefensiveDice(DiceType diceType, int upgradeLevel)
		{
			_dice.Add(new Dice(diceType, upgradeLevel));
			OrderDice();
		}

		public void RemoveDefensiveDice(DiceType diceType, int upgradeLevel)
		{
			for (int i = 0; i < _dice.Count; i++)
			{
				if (_dice[i].DiceType == diceType && _dice[i].UpgradeLevel == upgradeLevel)
				{
					_dice.RemoveAt(i); // No need to sort here.
					return;
				}
			}
			// TODO: Print an error message here.
		}

		public Dice[] GetDefensiveDice()
		{
			var returnArray = new Dice[5];
			for (int i = 0; i < 5; i++)
				returnArray[i] = new Dice(_dice[i].DiceType, _dice[i].UpgradeLevel);
			return returnArray;
		}
	}
}
