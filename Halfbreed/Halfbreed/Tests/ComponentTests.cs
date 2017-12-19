using NUnit.Framework;
using System.Collections.Generic;
//using RLNET;

namespace Halfbreed
{
	[TestFixture]
	public class ComponentTests
	{
		[Test]
		public void TestPositionComponent()
		{
			PositionComponent testComponent = new PositionComponent(0, new string[] {
				"xLoc", "5", "yLoc", "10"});

			Assert.AreEqual(5, testComponent.XLoc);
			Assert.AreEqual(10, testComponent.YLoc);
			Assert.AreEqual(ComponentTypes.POSITION, testComponent.ComponentType);

			testComponent.updatePosition(new Position(17, 33));

			Assert.AreEqual(17, testComponent.XLoc);
			Assert.AreEqual(33, testComponent.YLoc);

			testComponent.moveEntity(3, 1);

			Assert.AreEqual(20, testComponent.XLoc);
			Assert.AreEqual(34, testComponent.YLoc);

			testComponent.moveEntity(-4, -6);

			Assert.AreEqual(16, testComponent.XLoc);
			Assert.AreEqual(28, testComponent.YLoc);

			testComponent = new PositionComponent(5, new string[] { });

			Assert.AreEqual(-1, testComponent.XLoc);
			Assert.AreEqual(-1, testComponent.YLoc);
		}

		[Test]
		public void TestDisplayComponent()
		{
			DisplayComponent testComponent1 = new DisplayComponent(0, 'a', "Steel Grey", DisplayLayer.MINION);

			Assert.AreEqual('a', testComponent1.DisplayCharacter);
			Assert.AreEqual("Steel Grey", testComponent1.FGColorName);
			Assert.AreEqual(Palette.STEELGREY, testComponent1.FGColor);

			testComponent1.FGColorName = "Black";

			Assert.AreEqual(Palette.BLACK, testComponent1.FGColor);

			DisplayComponent testComponent2 = new DisplayComponent(1, 'x', "White", DisplayLayer.DOOR);
			DisplayComponent testComponent3 = new DisplayComponent(2, 'x', "White", DisplayLayer.PLAYER);
			DisplayComponent testComponent4 = new DisplayComponent(3, 'x', "White", DisplayLayer.HARVESTABLE);
			DisplayComponent testComponent5 = new DisplayComponent(4, 'x', "White", DisplayLayer.ITEM);

			List<DisplayComponent> componentList = new List<DisplayComponent>();

			componentList.Add(testComponent1);
			componentList.Add(testComponent2);
			componentList.Add(testComponent3);
			componentList.Add(testComponent4);
			componentList.Add(testComponent5);

			componentList.Sort();

			Assert.AreEqual(2, componentList[0].EntityId);
			Assert.AreEqual(3, componentList[4].EntityId);
		}

	}
}
