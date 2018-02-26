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

		static readonly SortedDictionary<int, SaveGameSummary> _defaultSaveSummary = 
			new SortedDictionary<int, SaveGameSummary>();
		static string _saveSummaryFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Userdata", "SSF.hb");

		static string _saveFileFolder = Path.Combine(Directory.GetCurrentDirectory(), "Saves");

		public static void SetupDirectoriesAndFiles()
		{
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Config")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Config"));
			if (!Directory.Exists(_saveFileFolder))
				Directory.CreateDirectory(_saveFileFolder);
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "UserData")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "UserData"));
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Logs")))
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Logs"));

			if (!File.Exists(_configFilePath))
				CreateNewConfigFile();
			if (!File.Exists(_saveSummaryFilePath))
				CreateNewSaveSummaryFile();
		}

		static void CreateNewConfigFile()
		{
			WriteConfigFile(_defaultConfigParameters);
		}

		static void CreateNewSaveSummaryFile()
		{
			WriteSummaryFile(_defaultSaveSummary);
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

		public static void WriteSaveGameSummary(SaveGameSummary summary)
		{
			var saveSummaries = ReadSummaryFile();
			saveSummaries[summary.GameData.GameID] = summary;
			WriteSummaryFile(saveSummaries);
		}

		public static void SaveGame(SaveGame gameState)
		{
			WriteSaveGameSummary(gameState.Summary);
			var filePath = Path.Combine(_saveFileFolder, string.Format("GID{0}.hbs", gameState.Summary.GameData.GameID));
			var fileStream = File.OpenWrite(filePath);
			var serialiser = new BinaryFormatter();
			serialiser.Serialize(fileStream, gameState);
			fileStream.Close();
		}

		public static SaveGame GetGameState(int gameID)
		{
			SaveGame gameState = null;
			var summary = ReadSummaryFile()[gameID];
			// TODO: Perform a check to see if the character is actually dead - also consider GM options, etc.
			if (summary.CurrentLevelName == "NEWGAME")
			{
				gameState = new SaveGame(summary, null, null, 0);
			}
			else
			{
				var filePath = Path.Combine(_saveFileFolder, string.Format("GID{0}.hbs", summary.GameData.GameID));
				var fileStream = File.OpenRead(filePath);
				var serialiser = new BinaryFormatter();
				gameState = (SaveGame)serialiser.Deserialize(fileStream);
				fileStream.Close();
			}
			return gameState;
		}

		public static List<SaveGameSummary> GetCurrentSaves()
		{
			var returnList = new List<SaveGameSummary>();
			var saveSummaries = ReadSummaryFile();
			foreach (int summaryID in saveSummaries.Keys)
			{
				if (saveSummaries[summaryID].StillAlive)
					returnList.Add(saveSummaries[summaryID]);
			}
			return returnList;
		}

		public static void DeleteSaveGame(int gameID)
		{
			var filePath = Path.Combine(_saveFileFolder, string.Format("GID{0}.hbs", gameID));
			if (File.Exists(filePath))
				File.Delete(filePath);
			var summaryDict = ReadSummaryFile();
			summaryDict[gameID].StillAlive = false;
			WriteSummaryFile(summaryDict);
		}

		public static int GetNextGameId()
		{
			var saveSummaries = ReadSummaryFile();
			return saveSummaries.Count;
			// TODO: Add check to make sure it's not actually in the save summary dict.
		}

		static SortedDictionary<int, SaveGameSummary> ReadSummaryFile()
		{
			var fileStream = File.OpenRead(_saveSummaryFilePath);
			var serialiser = new BinaryFormatter();
			var saveSummaries = (SortedDictionary<int, SaveGameSummary>)serialiser.Deserialize(fileStream);
			fileStream.Close();
			return saveSummaries;
		}

		static void WriteSummaryFile(SortedDictionary<int, SaveGameSummary> saveSummaries)
		{
			var fileStream = File.OpenWrite(_saveSummaryFilePath);
			var serialiser = new BinaryFormatter();
			serialiser.Serialize(fileStream, saveSummaries);
			fileStream.Close();
		}

		public static bool FullLogging
		{
			get { return _fullLogging; }
			set { _fullLogging = value;}
		}
	}
}
