using System;
using System.IO;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace Halfbreed
{
	public static class DatabaseConnection
	{
		private static string _DatabaseLocation = Directory.GetCurrentDirectory() + "/Halfbreed.db";
        private static SqliteConnection _connection;
		private static bool _testingMode = false;

		public static void SetupDirectoriesAndFiles()
		{

		}

		public static int GenerateNextGameId()
		{
			_connection = new SqliteConnection("Data Source=" + _DatabaseLocation);
			_connection.Open();
			List<SaveGameSummary> saveList = new List<SaveGameSummary>();

			string commandString = "SELECT MAX(GameId) FROM SaveGameSummaries";

			var queryCommand = _connection.CreateCommand();
			queryCommand.CommandText = commandString;
			// Double conversion necessary since SQLite doesn't allow smaller integer types.
			int currentMaxId = (int)(long) queryCommand.ExecuteScalar();
			return currentMaxId + 1;
		}

		public static List<SaveGameSummary> GetSaveGameSummaries()
		{
			_connection = new SqliteConnection("Data Source=" + _DatabaseLocation);
			_connection.Open();
			List<SaveGameSummary> saveList = new List<SaveGameSummary>();

			string commandString = "SELECT * FROM SaveGameSummaries WHERE StillAlive = 1;";

			using (var queryCommand = _connection.CreateCommand())
			{
				queryCommand.CommandText = commandString;

				var reader = queryCommand.ExecuteReader();
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

		public static void InsertNewSaveGameSummary(SaveGameSummary summary)
		{
			_connection = new SqliteConnection("Data Source=" + _DatabaseLocation);
			_connection.Open();

			int useAchievements = 0;
			if (summary.UseAchievements)
				useAchievements = 1;
			int isAlive = 1;
			string[] valueArray = new string[] {
				summary.GameId.ToString(),
				summary.DifficultySetting.ToString(),
				((int)summary.CharacterClass).ToString(),
				useAchievements.ToString(),
				summary.CurrentLevelName,
				isAlive.ToString(),
				summary.LastSaveTime.ToString()};

			string commandString = string.Format(
				"INSERT INTO SaveGameSummaries VALUES({0}, {1}, {2}, {3}, \"{4}\", {5}, {6});", valueArray);

			var insertCommand = _connection.CreateCommand();
			insertCommand.CommandText = commandString;
            var numberOfRows = insertCommand.ExecuteNonQuery();

			_connection.Close();
		}

		public static void UpdateSaveGameSummary(SaveGameSummary summary)
		{
			// Only needs to modify current level, still alive, last save time.
		}


		// Testing functionality
		public static bool CopyAndSwitchToTestDatabase(string TestContext)
		{
			_DatabaseLocation = TestContext + "/TestResources/tempdb.db";
			_testingMode = true;

			if (Directory.Exists(TestContext + "/TestResources/tempdb.db"))
				return false;

			File.Copy(TestContext + "/TestResources/testdb.db", _DatabaseLocation);

			return true;

		}

		// Testing functionality
		public static void RemoveTestDb()
		{
			if (!_testingMode)
			{
				// Add a debug message
				return;
			}

			File.Delete(_DatabaseLocation);
		}
	}

}
