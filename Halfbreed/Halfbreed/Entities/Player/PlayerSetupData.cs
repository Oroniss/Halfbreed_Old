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

		static readonly Dictionary<int, List<DefensiveStatUpgrade>> _difficultyEffects =
			new Dictionary<int, List<DefensiveStatUpgrade>>
			{
				{1, new List<DefensiveStatUpgrade>{new DefensiveStatUpgrade(Combat.DamageType.Acid, DiceType.D8, 1),
						new DefensiveStatUpgrade(Combat.DamageType.Cold, DiceType.D8, 1), 
						new DefensiveStatUpgrade(Combat.DamageType.Electricity, DiceType.D8, 1),
						new DefensiveStatUpgrade(Combat.DamageType.Fire, DiceType.D8, 1),
						new DefensiveStatUpgrade(Combat.DamageType.Poison, DiceType.D12, 1),
						new DefensiveStatUpgrade(Combat.DamageType.Disease, DiceType.D12, 1),
						new DefensiveStatUpgrade(Combat.DamageType.Mental, DiceType.D12, 1)}},
				{2, new List<DefensiveStatUpgrade>{new DefensiveStatUpgrade(Combat.DamageType.Poison, DiceType.D8, 1),
						new DefensiveStatUpgrade(Combat.DamageType.Disease, DiceType.D8, 1),
						new DefensiveStatUpgrade(Combat.DamageType.Mental, DiceType.D8, 1)}},
				{3, new List<DefensiveStatUpgrade>{}},
				{4, new List<DefensiveStatUpgrade>{}},
				{5, new List<DefensiveStatUpgrade>{}}
			};

		public static PrimaryStatTemplate GetCharacterStartingStats(CharacterClasses characterClass)
		{
			return _primaryStats[characterClass];
		}

		public static List<PrimaryStatUpgrade> GetCharacterStartingUpgrades(CharacterClasses characterClass)
		{
			return _primaryStatUpgrades[characterClass];
		}

		public static List<DefensiveStatUpgrade> GetStartingResistanceModifiers(int difficultySetting)
		{
			return _difficultyEffects[difficultySetting];
		}
	}
}
