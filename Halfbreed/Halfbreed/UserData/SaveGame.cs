using System;

namespace Halfbreed.UserData
{
	[Serializable]
	public class SaveGame
	{
		public SaveGameSummary Summary;
		public Level CurrentLevel;
		public Entities.Player Player;
		public int CurrentTime;
		public Levels.TileType[] MapGrid;

		public SaveGame(SaveGameSummary summary, Level currentLevel, Entities.Player player, int currentTime,
		               Levels.TileType[] mapGrid)
		{
			Summary = summary;
			CurrentLevel = currentLevel;
			Player = player;
			CurrentTime = currentTime;
			MapGrid = mapGrid;
		}
	}
}
