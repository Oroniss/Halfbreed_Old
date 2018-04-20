using System;

namespace Halfbreed
{
	[Serializable]
	public class Dice
	{
		static readonly int BASEDICEOFFSET = 35;
		static readonly int MINDICEUPGRADE = -25;
		static readonly int MAXDICEUPGRADE = 30;

		static int[,] DiceUpgrades = {
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // -35
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2},
			{0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2},
			{0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 3},

			{0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 3, 3}, // -30
			{0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 3, 4},
			{0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 4},
			{0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4},
			{0, 0, 0, 0, 1, 1, 1, 2, 3, 3, 3, 4},

			{0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4}, // -25 Worst normally possible
			{0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 4, 4},
			{0, 0, 0, 1, 1, 1, 2, 3, 3, 3, 4, 4},
			{0, 0, 0, 1, 1, 1, 2, 3, 3, 3, 4, 5},
			{0, 0, 0, 1, 1, 2, 2, 3, 3, 3, 4, 5},

			{0, 0, 0, 1, 1, 2, 2, 3, 3, 3, 5, 5}, // -20
			{0, 0, 0, 1, 1, 2, 3, 3, 3, 3, 5, 5},
			{0, 0, 0, 1, 1, 2, 3, 3, 3, 4, 5, 5},
			{0, 0, 0, 1, 1, 2, 3, 3, 3, 4, 5, 6},
			{0, 0, 0, 1, 1, 2, 3, 3, 3, 4, 6, 6},
			{0, 0, 1, 1, 1, 2, 3, 3, 3, 4, 6, 6},
			{0, 0, 1, 1, 1, 2, 3, 3, 3, 4, 6, 7},
			{0, 0, 1, 1, 1, 2, 3, 3, 3, 5, 6, 7},
			{0, 0, 1, 1, 1, 2, 3, 4, 3, 5, 6, 7},
			{0, 0, 1, 1, 1, 2, 3, 4, 3, 5, 6, 8},

			{0, 0, 1, 1, 1, 2, 3, 4, 3, 5, 7, 8}, // -10
			{0, 0, 1, 1, 1, 2, 3, 4, 3, 5, 7, 9},
			{0, 0, 1, 2, 1, 2, 3, 4, 3, 5, 7, 9},
			{0, 0, 1, 2, 1, 3, 3, 4, 3, 5, 7, 9},
			{0, 0, 1, 2, 1, 3, 3, 4, 3, 5, 7, 10},
			{0, 1, 1, 2, 1, 3, 3, 4, 3, 5, 7, 10},
			{0, 1, 1, 2, 1, 3, 3, 4, 4, 5, 7, 10},
			{0, 1, 1, 2, 1, 3, 3, 4, 4, 5, 7, 11},
			{0, 1, 1, 2, 1, 4, 3, 4, 4, 5, 7, 11},
			{0, 1, 2, 2, 1, 4, 3, 4, 4, 5, 7, 11},

			{0, 1, 2, 2, 1, 4, 3, 4, 4, 5, 7, 12}, // Standard, i.e. upgrade level 0.

			{0, 1, 2, 3, 1, 4, 3, 4, 4, 5, 7, 12},
			{0, 2, 2, 3, 1, 4, 3, 4, 4, 5, 7, 12},
			{0, 2, 3, 3, 1, 4, 3, 4, 4, 5, 7, 12},
			{0, 2, 3, 3, 1, 5, 3, 4, 4, 5, 7, 12},
			{0, 2, 3, 3, 2, 5, 3, 4, 4, 5, 7, 12},
			{0, 2, 3, 3, 2, 5, 3, 5, 4, 5, 7, 12},
			{0, 2, 3, 3, 2, 5, 4, 5, 4, 5, 7, 12},
			{0, 2, 3, 3, 3, 5, 4, 5, 4, 5, 7, 12},
			{0, 2, 3, 4, 3, 5, 4, 5, 4, 5, 7, 12},
			{0, 2, 3, 4, 3, 5, 5, 5, 4, 5, 7, 12}, // 10

			{0, 2, 3, 4, 3, 6, 5, 5, 4, 5, 7, 12},
			{0, 2, 3, 4, 3, 6, 5, 5, 5, 5, 7, 12},
			{0, 2, 3, 4, 3, 6, 5, 5, 5, 6, 7, 12},
			{0, 2, 3, 4, 3, 6, 5, 6, 5, 6, 7, 12},
			{0, 2, 3, 4, 3, 6, 6, 6, 5, 6, 7, 12},
			{0, 2, 3, 4, 3, 6, 6, 7, 5, 6, 7, 12},
			{0, 2, 3, 4, 3, 6, 6, 7, 6, 6, 7, 12},
			{0, 2, 3, 4, 3, 6, 6, 7, 6, 7, 7, 12},
			{1, 2, 3, 4, 3, 6, 6, 7, 6, 7, 7, 12},
			{1, 2, 3, 4, 3, 6, 6, 7, 6, 7, 8, 12}, // 20

			{1, 2, 3, 4, 3, 6, 6, 7, 7, 7, 8, 12},
			{1, 2, 3, 4, 3, 6, 6, 7, 7, 8, 8, 12},
			{1, 2, 3, 4, 3, 6, 6, 7, 7, 8, 9, 12},
			{1, 2, 3, 4, 3, 6, 6, 8, 7, 8, 9, 12},
			{1, 2, 3, 4, 3, 6, 7, 8, 7, 8, 9, 12},
			{1, 2, 3, 4, 3, 6, 7, 8, 8, 8, 9, 12},
			{1, 2, 3, 4, 3, 6, 7, 8, 8, 9, 9, 12},
			{1, 2, 3, 4, 3, 6, 7, 8, 8, 9, 10, 12},
			{1, 2, 3, 4, 3, 6, 7, 8, 8, 9, 11, 12},
			{1, 2, 3, 4, 3, 6, 7, 8, 8, 9, 11, 13}, // 30 - i.e. max normally possible.

			{1, 2, 3, 4, 3, 6, 7, 8, 9, 10, 12, 13},
			{1, 2, 3, 4, 3, 7, 8, 9, 9, 10, 12, 13},
			{1, 3, 4, 4, 4, 7, 8, 9, 9, 10, 12, 13},
			{1, 3, 4, 4, 4, 7, 8, 9, 10, 11, 13, 13},
			{1, 3, 4, 4, 4, 8, 9, 10, 10, 11, 13, 13},
			{1, 3, 5, 5, 5, 8, 9, 10, 10, 11, 13, 13},
			{1, 4, 5, 5, 5, 8, 9, 10, 11, 12, 13, 13},
			{1, 4, 5, 5, 5, 9, 10, 11, 11, 12, 13, 13},
			{1, 4, 6, 6, 6, 9, 10, 11, 11, 12, 13, 13},
			{2, 5, 6, 6, 6, 9, 10, 11, 11, 13, 13, 13}, // 40

			{2, 5, 6, 6, 7, 10, 11, 12, 12, 13, 13, 13},		
			{2, 6, 7, 7, 7, 10, 11, 13, 13, 13, 13, 13},		
			{2, 6, 8, 8, 8, 11, 12, 13, 13, 13, 13, 13},		
			{3, 7, 8, 8, 9, 12, 13, 13, 13, 13, 13, 13},		
			{4, 8, 9, 9, 9, 13, 13, 13, 13, 13, 13, 13},		
			{5, 9, 10, 10, 10, 13, 13, 13, 13, 13, 13, 13},		
			{6, 10, 11, 11, 11, 13, 13, 13, 13, 13, 13, 13},		
			{7, 11, 12, 12, 12, 13, 13, 13, 13, 13, 13, 13},		
			{8, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13},		
			{13, 13, 13, 13, 13, 13, 13, 113, 113, 13, 13, 13} // 50
		};

		static Random _diceRoller = new Random(13);

		DiceType _diceType;

		public Dice(DiceType diceType)
		{
			_diceType = diceType;
		}

		public Dice(int diceType)
		{
			_diceType = (DiceType)diceType;
		}

		public DiceType DiceType
		{
			get { return _diceType; }
		}

		public override string ToString()
		{
			return string.Format("D{0}", (int)DiceType);
		}

		public void UpgradeDiceType()
		{
			if (_diceType == Halfbreed.DiceType.D3)
				_diceType = Halfbreed.DiceType.D4;
			else if (_diceType == Halfbreed.DiceType.D12)
			{
				ErrorLogger.AddDebugText("Tried to upgrade Dice type of D12");
				return;
			}
			else
				_diceType = (DiceType)((int)_diceType + 2);
		}

		public DiceResult Roll(int upgradeBonus)
		{
			return Roll(upgradeBonus, MINDICEUPGRADE, MAXDICEUPGRADE);
		}

		public DiceResult Roll(int upgradeBonus, int minUpgrade, int maxUpgrade)
		{
			var roll = _diceRoller.Next((int)_diceType);
			var finalUpgradeLevel = Math.Max(minUpgrade, Math.Min(upgradeBonus, maxUpgrade)) + BASEDICEOFFSET;
			var result = DiceUpgrades[finalUpgradeLevel, roll];
			return (DiceResult)result;
		}

		public static void SetSeed(int newSeed)
		{
			_diceRoller = new Random(newSeed);
		}
	}

	[Serializable]
	public enum DiceType
	{
		D3 = 3,
		D4 = 4,
		D6 = 6,
		D8 = 8,
		D10 = 10,
		D12 = 12
	}

	[Serializable]
	public enum DiceResult
	{
		F = 0, // Failure
		N = 1, // Nothing
		P = 2, // Partial Success
		PP = 3,
		S = 4, // Success
		PS = 5,
		PPS = 6,
		SS = 7,
		PSS = 8,
		PPSS = 9,
		SSS = 10,
		SSSS = 11,
		C = 12, // Critical Success
		CC = 13
	}
}
