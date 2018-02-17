using Halfbreed.UserData;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Halfbreed
{
	// TODO: Put this in application settings.
	public static class UserDataManager
	{
		static bool _fullLogging;

		static string _configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", "config.hb");
		static readonly ConfigParameters _defaultConfigParameters = new ConfigParameters(false, false, false);

		public static void SetupDirectoriesAndFiles()
		{
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Config")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Config"));
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Saves")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Saves"));
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "UserData")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "UserData"));
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Logs")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Logs"));

			if (!File.Exists(_configFilePath))
				CreateNewConfigFile();
		}

		static void CreateNewConfigFile()
		{
			WriteConfigFile(_defaultConfigParameters);
		}

		public static void WriteConfigFile(ConfigParameters configParameters)
		{
			var fileStream = File.OpenWrite(_configFilePath);
			var serialiser = new BinaryFormatter();
			serialiser.Serialize(fileStream, configParameters);
			fileStream.Close();
		}

		public static ConfigParameters ReadConfigParameters()
		{
			var fileStream = File.OpenRead(_configFilePath);
			var serialiser = new BinaryFormatter();
			var configParameters = (ConfigParameters)serialiser.Deserialize(fileStream);
			fileStream.Close();
			return configParameters;
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

		public static bool FullLogging
		{
			get { return _fullLogging; }
			set { _fullLogging = value;}
		}
	}
}
