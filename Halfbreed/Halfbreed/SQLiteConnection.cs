using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using System;
namespace Halfbreed
{
	public static class SQLiteConnection
	{
		private static string _rootDirectory = Directory.GetCurrentDirectory();

		public static void SetupDirectoriesAndFiles()
		{
			if (!Directory.Exists(_rootDirectory + "/Logs"))
				Directory.CreateDirectory(_rootDirectory + "/Logs");
			if (!Directory.Exists(_rootDirectory + "/Misc"))
				Directory.CreateDirectory(_rootDirectory + "/Misc");

		}

		public static int GenerateNextGameId()
		{
			return 0;
		}



		public static void SwitchToTestDatabase()
		{

		}
	}

}
