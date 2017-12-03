using System.IO;
using System.Data;
using SQLite.Net;
using System.Collections.Generic;
using System;
namespace Halfbreed
{
	public static class DatabaseConnection
	{
		private static string _DatabaseLocation = Directory.GetCurrentDirectory() + "/Halfbreed.db";
		private static SQLiteConnection _connection;

		public static void SetupDirectoriesAndFiles()
		{

		}

		public static int GenerateNextGameId()
		{
			return 0;
		}

		public static void GetSaveGameSummaries()
		{
			SQLite.Net.Platform.Generic.SQLitePlatformGeneric platform = new SQLite.Net.Platform.Generic.SQLitePlatformGeneric();
			_connection = new SQLiteConnection(platform, _DatabaseLocation);


			_connection.Close();
		}



		public static void SwitchToTestDatabase()
		{

		}
	}

}
