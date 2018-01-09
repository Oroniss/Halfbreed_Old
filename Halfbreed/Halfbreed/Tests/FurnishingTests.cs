using System;
using Halfbreed.Entities;
using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class FurnishingTests
	{

		[SetUp]
		public void OpenTestDatabases()
		{
			EntityDatabaseConnection.SetupTestContext(TestContext.CurrentContext.TestDirectory);
			EntityDatabaseConnection.openDBConnection();
			StaticData.SetupDictionaries();
		}

		[Test]
		public void TestFurnishingBasics()
		{
			Furnishing newPallet = FurnishingFactory.CreateFurnishing("Pallet", Materials.HESSION, 5, 5, new string[] { });
			Furnishing newDoor = FurnishingFactory.CreateFurnishing("Door", Materials.PINE, 10, 10, new string[] { });
			Furnishing newChest = FurnishingFactory.CreateFurnishing("Chest", Materials.TIN, 12, 8, new string[] { });

			Assert.AreEqual(5, newPallet.YLoc);
			Assert.AreEqual(5, newPallet.XLoc);
			Assert.AreEqual(Materials.HESSION, newPallet.Material);
			Assert.AreEqual('.', newPallet.Symbol);
			Assert.AreEqual(DisplayLayer.FURNISHING, newPallet.DisplayLayer);

			Assert.AreEqual(10, newDoor.YLoc);
			Assert.AreEqual(10, newDoor.XLoc);
			Assert.AreEqual(Materials.PINE, newDoor.Material);
			Assert.AreEqual('+', newDoor.Symbol);
			Assert.AreEqual(DisplayLayer.FURNISHING, newDoor.DisplayLayer);

			Assert.AreEqual(8, newChest.YLoc);
			Assert.AreEqual(12, newChest.XLoc);
			Assert.AreEqual(Materials.TIN, newChest.Material);
			Assert.AreEqual('#', newChest.Symbol);
			Assert.AreEqual(DisplayLayer.FURNISHING, newPallet.DisplayLayer);
		}

		[TearDown]
		public void CloseDBConnection()
		{
			EntityDatabaseConnection.closeDBConnection();
		}

	}
}
