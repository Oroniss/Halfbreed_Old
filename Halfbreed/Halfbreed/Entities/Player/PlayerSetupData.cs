using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public static class PlayerSetupData
	{
		static readonly Dictionary<CharacterClasses, PrimaryStatTemplate> _primaryStats =
			new Dictionary<CharacterClasses, PrimaryStatTemplate>
			{
			{CharacterClasses.Cleric, new PrimaryStatTemplate(StatProgressionRates.Stunted, StatProgressionSpread.Focused,
															  StatProgressionRates.Advanced, StatProgressionSpread.Balanced,
															  StatProgressionRates.Advanced, StatProgressionSpread.Balanced,
															  StatProgressionRates.Superior, StatProgressionSpread.Balanced)},
			{CharacterClasses.Fighter, new PrimaryStatTemplate(StatProgressionRates.Superior, StatProgressionSpread.Unbalanced,
															   StatProgressionRates.Superior, StatProgressionSpread.Balanced,
															   StatProgressionRates.Standard, StatProgressionSpread.Focused,
															   StatProgressionRates.Standard, StatProgressionSpread.Balanced)},
			{CharacterClasses.Mage, new PrimaryStatTemplate(StatProgressionRates.Weak, StatProgressionSpread.Focused,
															StatProgressionRates.Stunted, StatProgressionSpread.Focused,
															StatProgressionRates.Exceptional, StatProgressionSpread.Balanced,
															StatProgressionRates.Advanced, StatProgressionSpread.Balanced)},
			{CharacterClasses.Thief, new PrimaryStatTemplate(StatProgressionRates.Superior, StatProgressionSpread.Balanced,
															 StatProgressionRates.Advanced, StatProgressionSpread.Balanced,
															 StatProgressionRates.Advanced, StatProgressionSpread.Balanced,
															 StatProgressionRates.Weak, StatProgressionSpread.Focused)},
			// Advanced
			{CharacterClasses.Bard, new PrimaryStatTemplate(StatProgressionRates.Advanced, StatProgressionSpread.Unbalanced,
															StatProgressionRates.Standard, StatProgressionSpread.Balanced,
															StatProgressionRates.Advanced, StatProgressionSpread.Unbalanced,
															StatProgressionRates.Superior, StatProgressionSpread.Focused)},
			{CharacterClasses.Blackguard, new PrimaryStatTemplate(StatProgressionRates.Stunted, StatProgressionSpread.Focused,
																  StatProgressionRates.Exceptional, StatProgressionSpread.Focused,
																  StatProgressionRates.Exceptional, StatProgressionSpread.Focused,
																  StatProgressionRates.Stunted, StatProgressionSpread.Focused)},
			{CharacterClasses.Druid, new PrimaryStatTemplate(StatProgressionRates.Superior, StatProgressionSpread.Unbalanced,
															 StatProgressionRates.Weak, StatProgressionSpread.Focused,
															 StatProgressionRates.Advanced, StatProgressionSpread.Balanced,
															 StatProgressionRates.Superior, StatProgressionSpread.Unbalanced)},
			{CharacterClasses.Necromancer, new PrimaryStatTemplate(StatProgressionRates.Weak, StatProgressionSpread.Focused,
																   StatProgressionRates.Stunted, StatProgressionSpread.Focused,
																   StatProgressionRates.Superior, StatProgressionSpread.Balanced,
																   StatProgressionRates.Superior, StatProgressionSpread.Balanced)},
			{CharacterClasses.Paladin, new PrimaryStatTemplate(StatProgressionRates.Weak, StatProgressionSpread.Focused,
															   StatProgressionRates.Superior, StatProgressionSpread.Balanced,
															   StatProgressionRates.Weak, StatProgressionSpread.Focused,
															   StatProgressionRates.Superior, StatProgressionSpread.Balanced)},
			{CharacterClasses.Ranger, new PrimaryStatTemplate(StatProgressionRates.Exceptional, StatProgressionSpread.Unbalanced,
															  StatProgressionRates.Superior, StatProgressionSpread.Unbalanced,
															  StatProgressionRates.Stunted, StatProgressionSpread.Focused,
															  StatProgressionRates.Standard, StatProgressionSpread.Focused)},
			// Dragonlord
			{CharacterClasses.Dragonlord, new PrimaryStatTemplate(StatProgressionRates.Advanced, StatProgressionSpread.Focused,
																  StatProgressionRates.Advanced, StatProgressionSpread.Focused,
																  StatProgressionRates.Advanced, StatProgressionSpread.Focused,
																  StatProgressionRates.Advanced, StatProgressionSpread.Focused)}
			};

		static readonly Dictionary<CharacterClasses, List<PrimaryStatUpgrade>> _primaryStatUpgrades =
			new Dictionary<CharacterClasses, List<PrimaryStatUpgrade>>
			{
			{CharacterClasses.Cleric, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Might, 1),
					new PrimaryStatUpgrade(PrimaryStat.Mind, 1), new PrimaryStatUpgrade(PrimaryStat.Presence, 0),
					new PrimaryStatUpgrade(PrimaryStat.Presence, 1), new PrimaryStatUpgrade(PrimaryStat.Presence, 3)}},
			{CharacterClasses.Fighter, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Might, 0),
					new PrimaryStatUpgrade(PrimaryStat.Might, 2)}},
			{CharacterClasses.Mage, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Mind, 0),
					new PrimaryStatUpgrade(PrimaryStat.Mind, 1), new PrimaryStatUpgrade(PrimaryStat.Mind, 1),
					new PrimaryStatUpgrade(PrimaryStat.Mind, 3)}},
			{CharacterClasses.Thief, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Might, 1),
					new PrimaryStatUpgrade(PrimaryStat.Agility, 0), new PrimaryStatUpgrade(PrimaryStat.Agility, 0),
					new PrimaryStatUpgrade(PrimaryStat.Agility, 2)}},
			// Advanced
			{CharacterClasses.Bard, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Agility, 1),
					new PrimaryStatUpgrade(PrimaryStat.Mind, 1), new PrimaryStatUpgrade(PrimaryStat.Presence, 1)}},
			{CharacterClasses.Blackguard, new List<PrimaryStatUpgrade>()},
			{CharacterClasses.Druid, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Presence, 0),
					new PrimaryStatUpgrade(PrimaryStat.Presence, 2)}},
			{CharacterClasses.Necromancer, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Mind, 0),
					new PrimaryStatUpgrade(PrimaryStat.Mind, 1), new PrimaryStatUpgrade(PrimaryStat.Mind, 2),
					new PrimaryStatUpgrade(PrimaryStat.Presence, 0), new PrimaryStatUpgrade(PrimaryStat.Presence, 2)}},
			{CharacterClasses.Paladin, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Might, 0),
					new PrimaryStatUpgrade(PrimaryStat.Might, 1), new PrimaryStatUpgrade(PrimaryStat.Presence, 0),
					new PrimaryStatUpgrade(PrimaryStat.Presence, 1)}},
			{CharacterClasses.Ranger, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Agility, 2)}},
			// Dragonlord
			{CharacterClasses.Dragonlord, new List<PrimaryStatUpgrade>{new PrimaryStatUpgrade(PrimaryStat.Agility, 0),
					new PrimaryStatUpgrade(PrimaryStat.Might, 0), new PrimaryStatUpgrade(PrimaryStat.Mind, 0),
					new PrimaryStatUpgrade(PrimaryStat.Presence, 0)}}
			};


		public static PrimaryStatTemplate GetCharacterStartingStats(CharacterClasses characterClass)
		{
			return _primaryStats[characterClass];
		}

		public static List<PrimaryStatUpgrade> GetCharacterStartingUpgrades(CharacterClasses characterClass)
		{
			return _primaryStatUpgrades[characterClass];
		}
	}

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

	public enum StatProgressionRates
	{
		Exceptional=12,
		Superior=8,
		Advanced=6,
		Standard=4,
		Weak=3,
		Stunted=2,
		None=0
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
Cleric - Presence: d8, d8, d4, d4 Mind: d6, d6 Might: d6, d6, Agility: d3 - 8, 4, 4, 0
Fighter - Might: d8, d6, d6 Agility: d8, d6 Mind: d6 Presence: d4, d4 - 7, 5, 2, 2
Mage - Mind: d10, d10, d6, d6 Presence: d6, d4 Agility: d4 Might: d3 - 12, 3, 1, 0
Thief - Agility: d10, d6, d6 Might: d6, d6 Mind: d6, d4 Presence: d4 - 8, 4, 3, 1

Bard - Presence: d10, d6 Mind: d8, d4 Agility: d8, d4 Might: d4, d4
Blackguard - Might: d12, d8 Mind: d12, d8 Presence: d3 Agility: d3
Druid - Presence: d10, d6, d4 Agility: d8, d6 Mind: d6, d4 Might: d4
Necromancer - Mind: d8, d8, d6 Presence: d8, d6, d6 Agility: d4 Might: d3
Paladin - Might: d8, d8, d4 Presence: d8, d8, d4 Agility: d4 Mind: d4
Ranger - Agility: d10, d8, d6 Might: d8, d6 Presence: d6 Mind: d3

Dragonlord - Presence: d10 Might: d10 Mind: d10 Agility: d10

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

25 upgrades
150 levels
per stat, so 
100 upgrades
600 levels
in total

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

HL 4 + upgrade 2 progression track
HL 5 + upgrade 5 progression tracks

HL 2 - downgrade 1 progression track
HL 1 - downgrade 2 progression tracks

Superior - standard - weak - weak.

Resistances - work the same way though None is a bit more common - shouldn't be too common still.
Otherwise high level characters will completely wreck monsters with status effects and procs.

HL 5 upgrade 15 tracks - potentially some become immunities instead
HL 4 upgrade 9 tracks - potentially some become immunities instead
HL 3 upgrade 4 tracks

HL 1 downgrade 1 track

*/

