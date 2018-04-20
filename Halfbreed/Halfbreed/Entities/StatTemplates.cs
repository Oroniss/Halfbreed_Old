namespace Halfbreed.Entities
{

	public struct PrimaryStatTemplate
	{
		public StatProgressionRates AgilityRate;
		public StatProgressionSpread AgilitySpread;
		public StatProgressionRates MightRate;
		public StatProgressionSpread MightSpread;
		public StatProgressionRates MindRate;
		public StatProgressionSpread MindSpread;
		public StatProgressionRates PresenceRate;
		public StatProgressionSpread PresenceSpread;

		public PrimaryStatTemplate(StatProgressionRates agilityRate, StatProgressionSpread agilitySpread,
								  StatProgressionRates mightRate, StatProgressionSpread mightSpread,
								  StatProgressionRates mindRate, StatProgressionSpread mindSpread,
								  StatProgressionRates presenceRate, StatProgressionSpread presenceSpread)
		{
			AgilityRate = agilityRate;
			AgilitySpread = agilitySpread;
			MightRate = mightRate;
			MightSpread = mightSpread;
			MindRate = mindRate;
			MindSpread = mindSpread;
			PresenceRate = presenceRate;
			PresenceSpread = presenceSpread;
		}
	}

	public struct PrimaryStatUpgrade
	{
		public PrimaryStat Stat;
		public int DiceNumber;

		public PrimaryStatUpgrade(PrimaryStat stat, int diceNumber)
		{
			Stat = stat;
			DiceNumber = diceNumber;
		}
	}

	public struct DefensiveStatUpgrade
	{
		public Combat.DamageType Resist;
		public DiceType DiceType;
		public int UpgradeLevel;

		public DefensiveStatUpgrade(Combat.DamageType resist, DiceType diceType, int upgradeLevel)
		{
			Resist = resist;
			DiceType = diceType;
			UpgradeLevel = upgradeLevel;
		}
	}

	public enum StatProgressionRates
	{
		Exceptional = 12,
		Superior = 8,
		Advanced = 6,
		Standard = 4,
		Weak = 3,
		Stunted = 2,
		None = 0
	}

	public enum StatProgressionSpread
	{
		Focused,
		Unbalanced,
		Balanced,
		Spread
	}
}
/*
 * 
focused

d4
d6
d8
d10
d10, d4
d12, d4
d12, d6
d12, d8
d12, d8, d4
d12, d10, d4

unbalanced

d4
d6
d8
d8, d4
d8, d6
d10, d6
d10, d8
d10, d8, d4
d12, d8, d4
d12, d8, d6

balanced

d4,
d4, d4
d6, d4
d6, d6
d6, d6, d4
d8, d6, d4
d8, d6, d6
d8, d6, d6, d4
d8, d8, d6, d4
d10, d8, d6, d4

spread

d4,
d4, d4
d4, d4, d4
d6, d4, d4
d6, d4, d4, d4
d6, d6, d4, d4
d6, d6, d6, d4
d6, d6, d6, d4, d4
d8, d6, d6, d4, d4
d8, d6, d6, d6, d4

Exceptional - 8 + 1 per 2 cr
Superior - 5 + 1 per 3 cr
Advanced - 3 + 1 per 4 cr
Standard - 2 + 1 per 6 cr
Weak - 1 + 1 per 8 cr
stunted - 0 + 1 per 12 cr
None - 0 + 0.

Exceptional - 3 * cr
Superior - 2 * cr
Advanced - 1.5 * cr
Standard - 1 * cr
Weak - 0.75 * cr
Stunted - 0.5 * cr
None - 0 * cr
*/

	

