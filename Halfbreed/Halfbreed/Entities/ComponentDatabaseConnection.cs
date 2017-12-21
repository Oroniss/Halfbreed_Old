using System.IO;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace Halfbreed
{
	public static partial class Entity
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

		private static List<ComponentTypes> GetComponents(string entityName)
		{
			List<ComponentTypes> components = new List<ComponentTypes>();

			string queryText = string.Format("SELECT * FROM ComponentIndex WHERE EntityName = \"{0}\";", entityName);

			using (var queryCommand = _connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;
			}

			return components;
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
