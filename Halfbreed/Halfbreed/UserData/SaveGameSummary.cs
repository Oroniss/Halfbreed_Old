using System;

namespace Halfbreed.UserData
{
	public struct SaveGameSummary
	{
		public int GameId;
		public int DifficultySetting;
		public CharacterClasses CharacterClass;
		public bool UseAchievements;
		public int CurrentAct;
		public int CurrentChapter;
		public bool StillAlive;
		public long LastSaveTime;

		public SaveGameSummary(int gameId, int difficultySetting, CharacterClasses characterClass,
							   bool useAchievements, int currentAct, int currentChapter, bool stillAlive, DateTime lastSaveTime)
		{
			GameId = gameId;
			DifficultySetting = difficultySetting;
			CharacterClass = characterClass;
			UseAchievements = useAchievements;
			CurrentAct = currentAct;
			CurrentChapter = currentChapter;
			StillAlive = stillAlive;
			LastSaveTime = ((DateTimeOffset)lastSaveTime).ToUnixTimeSeconds();
		}

		public SaveGameSummary(int gameId, int difficultySetting, CharacterClasses characterClass,
					   bool useAchievements, int currentAct, int currentChapter, bool stillAlive, long lastSaveTime)
		{
			GameId = gameId;
			DifficultySetting = difficultySetting;
			CharacterClass = characterClass;
			UseAchievements = useAchievements;
			CurrentAct = currentAct;
			CurrentChapter = currentChapter;
			StillAlive = stillAlive;
			LastSaveTime = lastSaveTime;
		}

		public override string ToString()
		{
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds( LastSaveTime ).ToLocalTime();

			string achString = "Off";
			if (UseAchievements)
				achString = "On";
			
			string[] items = new string[] {
				CharacterClass.ToString(), 
				DifficultySetting.ToString(),
				achString, 
				CurrentAct.ToString(),
				CurrentChapter.ToString(),
				dtDateTime.ToString(),
				};
			
			string returnString = "Character Class: {0}, Difficulty: {1}, Use Previous Achievements: {2}" +
				"\nCurrent Act: {3}, Current Chapter: {4}, Last Save Time: {5}.";
			
			return string.Format(returnString, items);
		}

	}
}
