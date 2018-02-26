// Revised for version 0.02.

using System.Collections.Generic;
using Halfbreed.Levels;

namespace Halfbreed
{
	public partial class Level
	{
		static string _baseFilePath = System.IO.Directory.GetCurrentDirectory();

		static Dictionary<LevelEnum, string> _filePaths = new Dictionary<LevelEnum, string>()
		{
			{LevelEnum.TESTLEVEL1, "/LevelFiles/Testing/TestLevel1"},
			{LevelEnum.TESTLEVEL2, "/LevelFiles/Testing/TestLevel2"}
		};

		static string GetFilePath(LevelEnum level)
		{
			return _baseFilePath + _filePaths[level] + ".txt";
		}

		public static void SetToTestDirectory(string testFilePath)
		{
			_baseFilePath = testFilePath;
		}

		static readonly int[,] _octantTranslate = {
			{1, 0, 0, -1, -1, 0, 0, 1},
			{0, 1, -1, 0, 0, -1, 1, 0},
			{0, 1, 1, 0, 0, -1, -1, 0},
			{1, 0, 0, 1, -1, 0, 0, -1}
		};
	}
}
