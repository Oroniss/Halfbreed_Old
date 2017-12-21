using System.IO;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace Halfbreed
{
	public static partial class EntityManager
	{
		private static string _DatabaseLocation = Directory.GetCurrentDirectory() + "/HalfbreedComponents.db";
		private static SqliteConnection _connection;
		private static bool _connectionOpen = false;
		private static bool _testingMode = false;

		private static ComponentTypes[] _componentOrdering = new ComponentTypes[] { 
		ComponentTypes.UNDEFINED, ComponentTypes.POSITION, ComponentTypes.DISPLAY};

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

				var reader = queryCommand.ExecuteReader();
				if (!reader.Read())
				{
					ErrorLogger.AddDebugText(string.Format("Couldn't find entity {0} in Component DB", entityName));
					return components;
				}
				for (int index = 0; index < _componentOrdering.Length; index++)
				{
					if (reader.GetInt64(index) == 1)
						components.Add(_componentOrdering[index]);
				}
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
