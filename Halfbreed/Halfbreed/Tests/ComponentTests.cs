using NUnit.Framework;
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

	}
}
