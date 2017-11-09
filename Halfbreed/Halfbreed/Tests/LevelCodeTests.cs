using System;
using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class LevelCodeTests
	{
		[Test]
		public void TestLevelConstruction()
		{
			// TODO: Put a test in that tries to load every level to make sure it parses.
			string filePath = TestContext.CurrentContext.TestDirectory + "/Tests/TestLevel";
			Level testLevel = new Level(filePath);

			Assert.AreEqual(25, testLevel.Height);
			Assert.AreEqual(44, testLevel.Width);
			Assert.AreEqual("Wall Market Square Storage Facility - Temporary Storage", testLevel.LevelName);
			Assert.AreEqual(-6, testLevel.LightingLevel);
			Assert.IsTrue(Math.Abs( 0.5 -  testLevel.AnathemaModifier) < 0.00001);

		}	

	}
}
