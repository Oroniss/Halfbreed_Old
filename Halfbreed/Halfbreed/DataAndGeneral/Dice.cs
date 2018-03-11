using System;

namespace Halfbreed
{
	[Serializable]
	public class Dice:IComparable
	{
		static int[,] _upgradeBonuses = new int[,]
		{
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0},
			{0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
			{0, 1, 1, 2, 1, 1, 0, 0, 0, 0, 0, 0},
			{0, 1, 1, 2, 1, 1, 1, 0, 0, 0, 0, 0},
			{0, 1, 1, 2, 1, 1, 1, 1, 0, 0, 0, 0},
			{0, 1, 1, 2, 2, 1, 1, 1, 0, 0, 0, 0},
			{0, 1, 1, 2, 2, 2, 1, 1, 0, 0, 0, 0},
			{0, 1, 1, 2, 2, 2, 1, 1, 1, 1, 0, 0},
			{0, 1, 1, 2, 2, 2, 2, 1, 1, 1, 0, 0},
			{0, 1, 1, 2, 2, 2, 2, 2, 1, 1, 0, 0},
			{0, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 0},
			{0, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1},
			{0, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 1},
			{0, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1},
			{1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1},
			{1, 1, 1, 2, 2, 2, 3, 2, 2, 2, 1, 1},
			{1, 1, 1, 2, 2, 2, 3, 3, 2, 2, 1, 1},
			{1, 1, 1, 2, 2, 2, 3, 3, 2, 2, 2, 1},
			{1, 1, 1, 2, 2, 2, 3, 3, 2, 2, 2, 2},
			{1, 1, 1, 2, 2, 2, 3, 3, 3, 2, 2, 2},
			{1, 1, 1, 2, 2, 2, 3, 3, 3, 2, 2, 4},
			{1, 1, 1, 2, 2, 2, 3, 3, 3, 2, 4, 4},
			{1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4},
			{1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 7},
			{1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 7, 7},
			{1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 7, 13}
		};

		static Random _diceRoller = new Random(13);

		DiceType _diceType;
		int _upgradeLevel;

		public Dice(DiceType diceType, int upgradeLevel)
		{
			_diceType = diceType;
			_upgradeLevel = upgradeLevel;
		}

		public Dice(int diceType, int upgradeLevel)
		{
			_diceType = (DiceType)diceType;
			_upgradeLevel = upgradeLevel;
		}

		public DiceType DiceType
		{
			get { return _diceType; } // { return "D" + ((int)_diceType).ToString();}
		}

		public override string ToString()
		{
			return string.Format("D{0}|{1}", (int)DiceType, UpgradeLevel);
		}

		public int UpgradeLevel
		{
			get { return _upgradeLevel; }
		}

		public void Upgrade()
		{
			if (_upgradeLevel == 30)
				// TODO: Add error message here
				return;
			_upgradeLevel++;
		}

		public void UpgradeDiceType()
		{
			if (_diceType == Halfbreed.DiceType.D3)
				_diceType = Halfbreed.DiceType.D4;
			else if (_diceType == Halfbreed.DiceType.D12)
				// TODO: Add error message
				return;
			else
				_diceType = (DiceType)((int)_diceType + 2);
		}

		public int Roll()
		{
			var roll = _diceRoller.Next((int)_diceType);
			return roll + _upgradeBonuses[_upgradeLevel - 1, roll] + 1;
		}

		public int CompareTo(object other)
		{
			var otherDice = (Dice)other;
			if (_diceType == otherDice.DiceType)
				return _upgradeLevel.CompareTo(otherDice.UpgradeLevel);
			return _diceType.CompareTo(otherDice.DiceType);
		}

		public static void SetSeed(int newSeed)
		{
			_diceRoller = new Random(newSeed);
		}
	}
}
