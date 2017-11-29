using System.Collections.Generic;
using NUnit.Framework;
namespace Halfbreed
{
	[TestFixture]
	public class SerializationTests
	{
		
		public void TestReadWriteSummaryFile()
		{
			// TODO: Rewrite all these based on DB connection instead of serialisation.
			// TODO: This now works, so need to make sure it doesn't overwrite the actual save file.

			DatabaseConnection.SetRootPathForTests(TestContext.CurrentContext.TestDirectory);

			string save1Path = TestContext.CurrentContext.TestDirectory + "testSave1";
			SaveGameSummary save1 = new SaveGameSummary(1, CharacterClasses.FIGHTER, true, 0, System.DateTime.Now,
			                                            "Wall Market Square");
			List<SaveGameSummary> test1List = new List<SaveGameSummary>() { save1 };
			DatabaseConnection.WriteSaveSummary(test1List);

			Assert.AreEqual(test1List, DatabaseConnection.ReadSaveSummary());

		}
	}
}
