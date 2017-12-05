﻿using System;
using System.IO;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace Halfbreed
{
	public static class SaveDatabaseConnection
	{
		private static string _DatabaseLocation = Directory.GetCurrentDirectory() + "/HalfbreedSv.db";
        private static SqliteConnection _connection;
		private static bool _testingMode = false;

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

			// TODO: Decide whether to order by GameId or Last Save Time reversed.
			string commandString = "SELECT * FROM SaveGameSummaries WHERE StillAlive = 1 ORDER BY GameId;";

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

		public static void WriteSaveGame(SaveGameSummary summary, object data)
		{
			UpdateSaveSummary(summary);
			if (summary.StillAlive)
			{
				// Then write the serialized data to the db.
			}
		}

		private static void UpdateSaveSummary(SaveGameSummary summary)
		{
			_connection = new SqliteConnection("Data Source=" + _DatabaseLocation);
			_connection.Open();

			int isAlive = 0;
			if (summary.StillAlive)
				isAlive = 1;

			string commandString = string.Format(
				"UPDATE SaveGameSummaries SET CurrentLevelName = \"{0}\", StillAlive = {1}, LastSaveTime = {2} " + 
				"WHERE GameId = {3};", summary.CurrentLevelName, isAlive, summary.LastSaveTime, summary.GameId);

			var updateCommand = _connection.CreateCommand();
			updateCommand.CommandText = commandString;
            var numberOfRows = updateCommand.ExecuteNonQuery();

			_connection.Close();

		}

		public static object ReadSaveGame(int gameId)
		{
			// Needs to read the serialized data out of the db.
			return true;
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
