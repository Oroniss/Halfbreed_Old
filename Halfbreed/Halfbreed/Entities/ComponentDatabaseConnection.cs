using System.IO;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Halfbreed.Entities;

namespace Halfbreed
{
	public static class ComponentDatabaseConnection
	{
		private static string _DatabaseLocation = Directory.GetCurrentDirectory() + "/HalfbreedComponents.db";
		private static SqliteConnection _connection;
		private static bool _connectionOpen = false;
		private static bool _testingMode = false;


		public static void openDBConnection()
		{
			if (_connectionOpen)
			{
				ErrorLogger.AddDebugText("Tried to re-open already open DB connection");
				return;
			}
			_connection = new SqliteConnection("Data Source=" + _DatabaseLocation);
			_connection.Open();
			_connectionOpen = true;
		}

		public static void closeDBConnection()
		{
			if (_connectionOpen)
			{
				_connection.Close();
				return;
			}
			ErrorLogger.AddDebugText("Tried to close already closed DB connection");
		}

		public static EntityPrimaryStatTemplate GetPrimaryStats(string EntityName)
		{
			return new EntityPrimaryStatTemplate(0, 0, 0, 0, 0);
		}

		public static EntityDefensiveStatTemplate GetDefensiveStats(string EntityName)
		{
			return new EntityDefensiveStatTemplate(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		public static List<EntityTraits> GetEntityTraits(string EntityName)
		{
			return new List<EntityTraits>();
		}

		// Testing functionality
		public static void SetupTestContext(string TestContext)
		{
			if (_testingMode)
			{
				ErrorLogger.AddDebugText("Component Database Connection already in Testing Mode");
				return;
			}
			_DatabaseLocation = TestContext + "/HalfbreedComponents.db";
			_testingMode = true;
		}

	}
}
