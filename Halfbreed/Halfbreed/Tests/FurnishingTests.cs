using Halfbreed.Entities;
using NUnit.Framework;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class FurnishingTests
	{

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
			Assert.IsTrue(newPallet.HasTrait(Traits.Furnishing));
			Assert.IsFalse(newPallet.HasTrait(Traits.Ruined));
			Assert.IsTrue(newPallet.HasTrait(Traits.ImmuneToMental));
			Assert.IsTrue(newPallet.HasTrait(Traits.ImmuneToPoison));
			Assert.IsTrue(newPallet.HasTrait(Traits.Cloth));
			Assert.IsTrue(newPallet.HasTrait(Traits.Organic));

			Assert.AreEqual(10, newDoor.YLoc);
			Assert.AreEqual(10, newDoor.XLoc);
			Assert.AreEqual('+', newDoor.Symbol);
			Assert.IsTrue(newDoor.HasTrait(Traits.Furnishing));
			Assert.IsFalse(newDoor.HasTrait(Traits.ImmuneToCold));
			Assert.IsTrue(newDoor.HasTrait(Traits.ImmuneToMental));
			Assert.IsTrue(newDoor.HasTrait(Traits.ImmuneToPoison));
			Assert.IsTrue(newDoor.HasTrait(Traits.Wood));
			Assert.IsTrue(newDoor.HasTrait(Traits.Organic));

			Assert.AreEqual(8, newChest.YLoc);
			Assert.AreEqual(12, newChest.XLoc);
			Assert.AreEqual('#', newChest.Symbol);
			Assert.IsTrue(newChest.HasTrait(Traits.Furnishing));
			Assert.IsFalse(newChest.HasTrait(Traits.ImmuneToFire));
			Assert.IsTrue(newChest.HasTrait(Traits.ImmuneToMental));
			Assert.IsTrue(newChest.HasTrait(Traits.ImmuneToPoison));
			Assert.IsTrue(newChest.HasTrait(Traits.Metal));
			Assert.IsTrue(newChest.HasTrait(Traits.Inorganic));
		}

		[Test]
		public void TestHarvestingNodes()
		{
			Entity newDen = EntityFactory.CreateHarvestingNode("Rat Den", 5, 10);
			Entity newMold = EntityFactory.CreateHarvestingNode("Green Mold", 10, 10);
			Entity newOre = EntityFactory.CreateHarvestingNode("Copper Ore", 15, 15);
			Entity newRichOre = EntityFactory.CreateHarvestingNode("Rich Copper Ore", 20, 20);

			Assert.AreEqual(5, newDen.XLoc);
			Assert.AreEqual(10, newDen.YLoc);
			Assert.AreEqual(':', newDen.Symbol);
			Assert.AreEqual(Colors.Tan, newDen.FGColor);
			Assert.IsFalse(newDen.HasTrait(Traits.Inorganic));
			Assert.IsTrue(newDen.HasTrait(Traits.Furnishing));
			Assert.IsTrue(newDen.HasTrait(Traits.Organic));
			Assert.IsTrue(newDen.HasTrait(Traits.ImmuneToMental));
			Assert.IsTrue(newDen.HasTrait(Traits.Animal));

			Assert.AreEqual('}', newMold.Symbol);
			Assert.AreEqual(Colors.PutridGreen, newMold.FGColor);
			Assert.IsFalse(newMold.HasTrait(Traits.Animal));
			Assert.IsTrue(newMold.HasTrait(Traits.Plant));

			Assert.AreEqual('*', newOre.Symbol);
			Assert.AreEqual(Colors.Black, newOre.FGColor);
			Assert.IsFalse(newOre.HasTrait(Traits.Organic));
			Assert.IsTrue(newOre.HasTrait(Traits.Inorganic));

			Assert.AreEqual('*', newRichOre.Symbol);
			Assert.AreEqual(Colors.Gold, newRichOre.FGColor);
		}

	}
}
