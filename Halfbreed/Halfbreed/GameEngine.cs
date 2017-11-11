using System.Collections.Generic;
namespace Halfbreed
{
	public static class GameParameters
	{
		private static int _difficultySetting;
		private static CharacterClasses _characterClass;
		private static bool _useAchievements;

		public static void setStartingDifficulty(int difficulty)
		{
			_difficultySetting = difficulty;
		}
		public static void setStartingCharacterClass(CharacterClasses characterClass)
		{
			_characterClass = characterClass;
		}
		public static void setStartingUseAchievements(bool useAchievements)
		{
			_useAchievements = useAchievements;
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
