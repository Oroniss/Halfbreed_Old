// Revised for version 0.02.

using NUnit.Framework;
using System;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class DataTests
	{
		[Test]
		public void TestPalette()
		{
			// Check each option in the Enum is covered
			foreach (Colors color in Enum.GetValues(typeof(Colors)))
				Palette.GetColor(color);
			Assert.AreEqual(Enum.GetValues(typeof(Colors)).Length, Palette.NumberOfColors);
		}

		// Classes untested - GameData, XYCoordinateClass, XYCoordinateStruct - doesn't seem much point if they
		// don't have any functionality.
	}
}
