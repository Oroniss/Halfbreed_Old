using System.IO;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace Halfbreed
{
	public static class DatabaseConnection
	{
		private static string _DatabaseLocation = Directory.GetCurrentDirectory() + "/Halfbreed.db";
        private static SqliteConnection _connection;

		public static void SetupDirectoriesAndFiles()
		{

		}

		public static int GenerateNextGameId()
		{
			return 0;
		}

		public static List<SaveGameSummary> GetSaveGameSummaries()
		{
			_connection = new SqliteConnection("Data Source=" + _DatabaseLocation);
			_connection.Open();
			List<SaveGameSummary> saveList = new List<SaveGameSummary>();

			string cmdString = "SELECT * FROM SaveGameSummaries WHERE StillAlive = 1;";

			using (var cmd = _connection.CreateCommand())
			{
				cmd.CommandText = cmdString;

				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					int gameId = reader.GetInt32(0);
					int difficultySetting = reader.GetInt32(1);
					CharacterClasses characterClass = (CharacterClasses)reader.GetInt32(2);
					bool useAchievements = (reader.GetInt32(3) == 1);
					string currentLevelName = reader.GetString(4);
					bool stillAlive = (reader.GetInt32(5) == 1);
					long lastSaveTime = reader.GetInt64(6);

					saveList.Add(new SaveGameSummary(gameId, difficultySetting, characterClass,
					                                useAchievements, currentLevelName, stillAlive, lastSaveTime));
				}
			}
			_connection.Close();

			return saveList;

		}
		



		public static void SwitchToTestDatabase()
		{

		}
	}

}
