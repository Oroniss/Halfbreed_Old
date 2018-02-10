using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Halfbreed
{
	// TODO: Put this in application settings.
	public static class UserDataManager
	{
		static string _configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", "config.hb");

		public static void SetupDirectoriesAndFiles()
		{
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Config")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Config"));
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Saves")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Saves"));
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "UserData")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "UserData"));

			if (!File.Exists(_configFilePath))
				CreateNewConfigFile();
		}

		static void CreateNewConfigFile()
		{

		}

		public static void WriteSaveGameSummary()
		{

		}

		public static void ReadSaveGameSummary()
		{

		}

		public static void GetNextGameId()
		{

		}

		static void ReadAllGameSummaries()
		{
			
		}

		static void WriteAllGameSummaries()
		{
			
		}
	}
}
