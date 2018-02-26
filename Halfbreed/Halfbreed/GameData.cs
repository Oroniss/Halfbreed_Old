using System;
namespace Halfbreed
{
	[Serializable]
	public class GameData
	{
		public int DifficultySetting;
		public CharacterClasses CharacterClass;
		public bool UseAchievements;
		public string CharacterNote;
		public int GameID;

		public GameData()
		{
			DifficultySetting = 1;
			CharacterClass = CharacterClasses.Fighter;
			UseAchievements = true;
			CharacterNote = "";
		}

		public GameData(int difficulty, CharacterClasses characterClass, bool useAchievements, string characterNote,
						int gameId)
		{
			DifficultySetting = difficulty;
			CharacterClass = characterClass;
			UseAchievements = useAchievements;
			CharacterNote = characterNote;
			GameID = gameId;
		}
	}
}
