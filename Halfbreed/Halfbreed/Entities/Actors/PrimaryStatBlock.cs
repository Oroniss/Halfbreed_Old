using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	[Serializable]
	public class PrimaryStatBlock
	{
		Dice[] _agility;
		Dice[] _might;
		Dice[] _mind;
		Dice[] _presence;

		public PrimaryStatBlock()
		{
			_agility = new Dice[5];
			_might = new Dice[5];
			_mind = new Dice[5];
			_presence = new Dice[5];

			for (int i = 0; i < _agility.Length; i++)
			{
				_agility[i] = new Dice(DiceType.D3);
				_might[i] = new Dice(DiceType.D3);
				_mind[i] = new Dice(DiceType.D3);
				_presence[i] = new Dice(DiceType.D3);
			}
		}

		public PrimaryStatBlock(PrimaryStatTemplate template, int challengeRating)
			:this()
		{
			var agilityArray = CalculateStartingDice(template.AgilityRate, template.AgilitySpread, challengeRating);
			var mightArray = CalculateStartingDice(template.MightRate, template.MightSpread, challengeRating);
			var mindArray = CalculateStartingDice(template.MindRate, template.MindSpread, challengeRating);
			var presenceArray = CalculateStartingDice(template.PresenceRate, template.PresenceSpread, challengeRating);

			for (int diceNumber = 0; diceNumber < agilityArray.Length; diceNumber++)
			{
				for (int agilityUpgrades = 0; agilityUpgrades < agilityArray[diceNumber]; agilityUpgrades++)
					_agility[diceNumber].UpgradeDiceType();
				for (int mightUpgrades = 0; mightUpgrades<mightArray[diceNumber]; mightUpgrades++)
					_might[diceNumber].UpgradeDiceType();
				for (int mindUpgrades = 0; mindUpgrades<mindArray[diceNumber]; mindUpgrades++)
					_mind[diceNumber].UpgradeDiceType();
				for (int presenceUpgrades = 0; presenceUpgrades<presenceArray[diceNumber]; presenceUpgrades++)
					_presence[diceNumber].UpgradeDiceType();
			}
		}

		int[] CalculateStartingDice(StatProgressionRates rate, StatProgressionSpread spread, int challengeRating)
		{
			var upgradeLevel = (((int)rate) * challengeRating / 24) + PrimaryStatUpgradeLevels.GetStartingUpgradeLevel(rate);
			return PrimaryStatUpgradeLevels.GetDiceArray(spread, upgradeLevel);
		}

		public Dice[] Agility()
		{
			return _agility;
		}

		public Dice[] Might()
		{
			return _might;
		}

		public Dice[] Mind()
		{
			return _mind;
		}

		public Dice[] Presence()
		{
			return _presence;
		}
	}

	static class PrimaryStatUpgradeLevels
	{
		static readonly Dictionary<StatProgressionSpread, int[,]> _statUpgrades =
			new Dictionary<StatProgressionSpread, int[,]>
		{
			{StatProgressionSpread.Focused, new int[,]
				{
					{0, 0, 0, 0, 0},
					// Level 1
					{1, 0, 0, 0, 0},
					{2, 0, 0, 0, 0},
					{3, 0, 0, 0, 0},
					{4, 0, 0, 0, 0},
					{4, 1, 0, 0, 0},
					{5, 1, 0, 0, 0},
					{5, 2, 0, 0, 0},
					{5, 3, 0, 0, 0},
					{5, 3, 1, 0, 0},
					{5, 4, 1, 0, 0}
					// Level 11
				}
			},
			{StatProgressionSpread.Unbalanced, new int[,]
				{
					{0, 0, 0, 0, 0},
					// Level 1
					{1, 0, 0, 0, 0},
					{2, 0, 0, 0, 0},
					{3, 0, 0, 0, 0},
					{3, 1, 0, 0, 0},
					{3, 2, 0, 0, 0},
					{4, 2, 0, 0, 0},
					{4, 3, 0, 0, 0},
					{4, 3, 1, 0, 0},
					{5, 3, 1, 0, 0},
					{5, 3, 2, 0, 0}
					// Level 11
				}
			},
			{StatProgressionSpread.Balanced, new int[,]
				{
					{0, 0, 0, 0, 0},
					// Level 1
					{1, 0, 0, 0, 0},
					{1, 1, 0, 0, 0},
					{2, 1, 0, 0, 0},
					{2, 2, 0, 0, 0},
					{2, 2, 1, 0, 0},
					{3, 2, 1, 0, 0},
					{3, 2, 2, 0, 0},
					{3, 2, 2, 1, 0},
					{3, 3, 2, 1, 0},
					{4, 3, 2, 1, 0}
					// Level 11
				}
			},
			{StatProgressionSpread.Spread, new int[,]
				{
					{0, 0, 0, 0, 0},
					// Level 1
					{1, 0, 0, 0, 0},
					{1, 1, 0, 0, 0},
					{1, 1, 1, 0, 0},
					{2, 1, 1, 0, 0},
					{2, 1, 1, 1, 0},
					{2, 2, 1, 1, 0},
					{2, 2, 2, 1, 0},
					{2, 2, 2, 1, 1},
					{3, 2, 2, 1, 1},
					{3, 2, 2, 2, 1}
					// Level 11
				}
			}
		};

		static readonly Dictionary<StatProgressionRates, int> _startingUpgrades =
			new Dictionary<StatProgressionRates, int>
		{
			{StatProgressionRates.None, 0},
			{StatProgressionRates.Stunted, 0},
			{StatProgressionRates.Weak, 1},
			{StatProgressionRates.Standard, 2},
			{StatProgressionRates.Advanced, 3},
			{StatProgressionRates.Superior, 5},
			{StatProgressionRates.Exceptional, 8}
		};

		internal static int[] GetDiceArray(StatProgressionSpread spread, int upgradeLevel)
		{
			var returnArray = new int[5];
			for (int i = 0; i < 5; i++)
				returnArray[i] = _statUpgrades[spread][upgradeLevel, i];
			return returnArray;
		}

		internal static int GetStartingUpgradeLevel(StatProgressionRates rate)
		{
			return _startingUpgrades[rate];
		}
	}

}