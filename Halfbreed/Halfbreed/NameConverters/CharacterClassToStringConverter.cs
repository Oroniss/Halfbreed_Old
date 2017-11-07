using System.Collections.Generic;
namespace Halfbreed
{
	public class CharacterClassToStringConverter
	{
		// TODO: Add upper case versions.
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

		public static CharacterClasses ConvertStringToCharacterClass(string characterClassName)
		{
			return _stringToCharacterClass[characterClassName];
		}

	}
}
