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
				_dice.Add(new Dice(DiceType.D3));
		}

		public void UpgradeDice(int diceNumber)
		{
			if (diceNumber > 5 || diceNumber < 1)
			{
				ErrorLogger.AddDebugText(string.Format("Invalid dice number: {0}", diceNumber));
				return;
			}
			_dice[diceNumber - 1].UpgradeDiceType();
		}

		public Dice[] GetDefensiveDice()
		{
			var returnArray = new Dice[5];
			for (int i = 0; i < 5; i++)
				returnArray[i] = new Dice(_dice[i].DiceType);
			return returnArray;
		}
	}
}
