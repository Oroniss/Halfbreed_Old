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
			Entity newPallet = EntityFactory.CreateFurnishing("Pallet", Materials.HESSION, 5, 5, new string[] { });
			Entity newDoor = EntityFactory.CreateFurnishing("Door", Materials.PINE, 10, 10, new string[] { });
			Entity newChest = EntityFactory.CreateFurnishing("Chest", Materials.TIN, 12, 8, new string[] { });

			Assert.AreEqual(5, newPallet.YLoc);
			Assert.AreEqual(5, newPallet.XLoc);
			Assert.AreEqual(Materials.HESSION, ((MaterialComponent)newPallet.GetComponent(ComponentType.MATERIAL)).Material);
			Assert.AreEqual('.', newPallet.Symbol);
			Assert.AreEqual(DisplayLayer.FURNISHING, newPallet.DisplayLayer);
			Assert.IsTrue(newPallet.HasTrait(EntityTraits.FURNISHING));
			Assert.IsFalse(newPallet.HasTrait(EntityTraits.RUINED));
			Assert.IsTrue(newPallet.HasTrait(EntityTraits.IMMUNETOMENTAL));
			Assert.IsTrue(newPallet.HasTrait(EntityTraits.IMMUNETOPOISON));
			Assert.IsTrue(newPallet.HasTrait(EntityTraits.CLOTH));
			Assert.IsTrue(newPallet.HasTrait(EntityTraits.ORGANIC));

			Assert.AreEqual(10, newDoor.YLoc);
			Assert.AreEqual(10, newDoor.XLoc);
			Assert.AreEqual(Materials.PINE, ((MaterialComponent)newDoor.GetComponent(ComponentType.MATERIAL)).Material);
			Assert.AreEqual('+', newDoor.Symbol);
			Assert.AreEqual(DisplayLayer.FURNISHING, newDoor.DisplayLayer);
			Assert.IsTrue(newDoor.HasTrait(EntityTraits.FURNISHING));
			Assert.IsFalse(newDoor.HasTrait(EntityTraits.IMMUNETOCOLD));
			Assert.IsTrue(newDoor.HasTrait(EntityTraits.IMMUNETOMENTAL));
			Assert.IsTrue(newDoor.HasTrait(EntityTraits.IMMUNETOPOISON));
			Assert.IsTrue(newDoor.HasTrait(EntityTraits.WOOD));
			Assert.IsTrue(newDoor.HasTrait(EntityTraits.ORGANIC));

			Assert.AreEqual(8, newChest.YLoc);
			Assert.AreEqual(12, newChest.XLoc);
			Assert.AreEqual(Materials.TIN, ((MaterialComponent)newChest.GetComponent(ComponentType.MATERIAL)).Material);
			Assert.AreEqual('#', newChest.Symbol);
			Assert.AreEqual(DisplayLayer.FURNISHING, newPallet.DisplayLayer);
			Assert.IsTrue(newChest.HasTrait(EntityTraits.FURNISHING));
			Assert.IsFalse(newChest.HasTrait(EntityTraits.IMMUNETOFIRE));
			Assert.IsTrue(newChest.HasTrait(EntityTraits.IMMUNETOMENTAL));
			Assert.IsTrue(newChest.HasTrait(EntityTraits.IMMUNETOPOISON));
			Assert.IsTrue(newChest.HasTrait(EntityTraits.METAL));
			Assert.IsTrue(newChest.HasTrait(EntityTraits.INORGANIC));
		}

		[TearDown]
		public void CloseDBConnection()
		{
			EntityDatabaseConnection.closeDBConnection();
		}

	}
}
