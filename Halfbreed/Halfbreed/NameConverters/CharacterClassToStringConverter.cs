using System.Collections.Generic;
namespace Halfbreed
{
	public class CharacterClassToStringConverter
	{
		// TODO: Add upper case versions and the reverse.
		private static Dictionary<string, CharacterClasses> _stringToCharacterClass =
			new Dictionary<string, CharacterClasses>
		{
			{"Cleric", CharacterClasses.CLERIC},
			{"Fighter", CharacterClasses.FIGHTER},
			{"Mage", CharacterClasses.MAGE},
			{"Thief", CharacterClasses.THIEF},
			{"Bard", CharacterClasses.BARD},
			{"Blackguard", CharacterClasses.BLACKGUARD},
			{"Druid", CharacterClasses.DRUID},
			{"Necromancer", CharacterClasses.NECROMANCER},
			{"Paladin", CharacterClasses.PALADIN},
			{"Ranger", CharacterClasses.RANGER},
			{"Dragonlord", CharacterClasses.DRAGONLORD}
		};

		private static Dictionary<CharacterClasses, string> _characterClassToString =
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

		public static CharacterClasses ConvertStringToCharacterClass(string characterClassName)
		{
			return _stringToCharacterClass[characterClassName];
		}

		public static string ConvertCharacterClassToString(CharacterClasses characterClass)
		{
			return _characterClassToString[characterClass];
		}

	}
}
