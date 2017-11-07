using System.Collections.Generic;
namespace Halfbreed
{
	public static class GameParameters
	{
		private static int _difficultySetting;
		private static CharacterClasses _characterClass;
		private static bool _useAchievements;

		// TODO: Add upper case versions.
		// TODO: Put this somewhere more sensible.
		private static Dictionary<string, CharacterClasses> _stringToCharacterClassConversions =
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

		public static void setStartingParameters(int difficulty, string characterClass, bool useAchievements)
		{
			_difficultySetting = difficulty;
			_characterClass = ConvertStringToCharacterClass(characterClass);
			_useAchievements = useAchievements;
		}

		public static CharacterClasses ConvertStringToCharacterClass(string characterClass)
		{
			return _stringToCharacterClassConversions[characterClass];
		}

		public static int DifficultySetting
		{
			get { return _difficultySetting; }
		}
		public static CharacterClasses CharacterClass
		{
			get { return _characterClass; }
		}
		public static bool UseAchievements
		{
			get { return _useAchievements; }
		}

	}
}
