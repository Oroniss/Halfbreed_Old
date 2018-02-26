// Revised for version 0.02.

using System;

namespace Halfbreed.UserData
{
	[Serializable]
	public class SaveGame
	{
		public SaveGameSummary Summary;
		public LevelSerialisationDetails CurrentLevelDetails;
		public Entities.Player Player;
		public int CurrentTime;

		public SaveGame(SaveGameSummary summary, LevelSerialisationDetails currentLevelDetails, Entities.Player player, 
		                int currentTime)
		{
			Summary = summary;
			CurrentLevelDetails = currentLevelDetails;
			Player = player;
			CurrentTime = currentTime;
		}
	}
}
