// Revised for version 0.2 - No changes required.

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

		static readonly int[,] _octantTranslate = new int[4, 8]
		{
			{1, 0, 0, -1, -1, 0, 0, 1},
			{0, 1, -1, 0, 0, -1, 1, 0},
			{0, 1, 1, 0, 0, -1, -1, 0},
			{1, 0, 0, 1, -1, 0, 0, -1}
		};
	}
}
