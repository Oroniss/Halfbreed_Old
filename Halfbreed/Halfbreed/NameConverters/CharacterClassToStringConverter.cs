using System.Collections.Generic;

namespace Halfbreed.Converters
{
	public class CharacterClassToStringConverter
	{
		private Dictionary<string, CharacterClasses> _stringToCharacterClass =
			new Dictionary<string, CharacterClasses>
		{
			{"Cleric", CharacterClasses.CLERIC},
			{"cleric", CharacterClasses.CLERIC},
			{"Fighter", CharacterClasses.FIGHTER},
			{"fighter", CharacterClasses.FIGHTER},
			{"Mage", CharacterClasses.MAGE},
			{"mage", CharacterClasses.MAGE},
			{"Thief", CharacterClasses.THIEF},
			{"thief", CharacterClasses.THIEF},
			{"Bard", CharacterClasses.BARD},
			{"bard", CharacterClasses.BARD},
			{"Blackguard", CharacterClasses.BLACKGUARD},
			{"blackguard", CharacterClasses.BLACKGUARD},
			{"Druid", CharacterClasses.DRUID},
			{"druid", CharacterClasses.DRUID},
			{"Necromancer", CharacterClasses.NECROMANCER},
			{"necromancer", CharacterClasses.NECROMANCER},
			{"Paladin", CharacterClasses.PALADIN},
			{"paladin", CharacterClasses.PALADIN},
			{"Ranger", CharacterClasses.RANGER},
			{"ranger", CharacterClasses.RANGER},
			{"Dragonlord", CharacterClasses.DRAGONLORD},
			{"dragonlord", CharacterClasses.DRAGONLORD}
		};

		private Dictionary<CharacterClasses, string> _characterClassToString =
			new Dictionary<CharacterClasses, string>
		{
			{CharacterClasses.CLERIC, "Cleric"},
			{CharacterClasses.FIGHTER, "Fighter"},
			{CharacterClasses.MAGE, "Mage"},
			{CharacterClasses.THIEF, "Thief"},
			{CharacterClasses.BARD, "Bard"},
			{CharacterClasses.BLACKGUARD, "Blackguard"},
			{CharacterClasses.DRUID, "Druid"},
			{CharacterClasses.NECROMANCER, "Necromancer"},
			{CharacterClasses.PALADIN, "Paladin"},
			{CharacterClasses.RANGER, "Ranger"},
			{CharacterClasses.DRAGONLORD, "Dragonlord"}
		};

		public CharacterClasses ConvertStringToCharacterClass(string characterClassName)
		{
			return _stringToCharacterClass[characterClassName];
		}

		public string ConvertCharacterClassToString(CharacterClasses characterClass)
		{
			return _characterClassToString[characterClass];
		}

	}
}
