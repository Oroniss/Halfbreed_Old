using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;
namespace Halfbreed
{
	public static class ReaderWriter
	{
		private static string _rootDirectory = Directory.GetCurrentDirectory();
		private static string _saveSummaryFilePath = _rootDirectory + "Misc/HBSummary.HB";

		public static void SetupDirectoriesAndFiles()
		{
			if (!Directory.Exists(_rootDirectory + "Saves"))
				Directory.CreateDirectory(_rootDirectory + "Saves");
			if (!Directory.Exists(_rootDirectory + "Logs"))
				Directory.CreateDirectory(_rootDirectory + "Logs");
			if (!Directory.Exists(_rootDirectory + "Achievements"))
				Directory.CreateDirectory(_rootDirectory + "Achievements");
			if (!Directory.Exists(_rootDirectory + "Misc"))
				Directory.CreateDirectory(_rootDirectory + "Misc");

			if (!File.Exists(_saveSummaryFilePath))
			{
				WriteSaveSummary(GetEmptySaveList());
			}
		}

		private static List<SaveGameSummary> GetEmptySaveList()
		{
			return new List<SaveGameSummary>();
		}

		public static void WriteSaveSummary(List<SaveGameSummary> saveSummaryList)
		{
			BinaryFormatter saveFormatter = new BinaryFormatter();
			Stream saveStream = new FileStream(_saveSummaryFilePath, FileMode.Create, FileAccess.Write);
			saveFormatter.Serialize(saveStream, saveSummaryList);
			saveStream.Flush();
			saveStream.Close();
		}

		public static List<SaveGameSummary> ReadSaveSummary()
		{
			BinaryFormatter saveFormatter = new BinaryFormatter();
			Stream saveStream = new FileStream(_saveSummaryFilePath, FileMode.Open, FileAccess.Read);
			List<SaveGameSummary> saveSummaryList =  
				(List<SaveGameSummary>)saveFormatter.Deserialize(saveStream);
			saveStream.Flush();
			saveStream.Close();

			return saveSummaryList;
		}
		
	}

	public struct SaveGameSummary
	{
		public int difficultySetting;
		public CharacterClasses characterClass;
		public bool useAchievements;
		public string saveFilePath;
		public string logFilePath;
		public string achievementFilePath;
		public DateTime lastSaveTime;
		public string currentLevelName;
	}
}
