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

		[Test]
		public void TestDice()
		{
			Dice.SetSeed(6);
			// 6, 4, 6, 6, 6, 5, 4, 3, 5, 3

			var d6 = new Dice(DiceType.D6, 6);
			var results = new int[] { 7, 5, 7, 7, 7, 6, 5, 4, 6, 4 };
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(results[i], d6.Roll());

			d6.Upgrade();
			d6.Upgrade();
			d6.Upgrade();
			Dice.SetSeed(6);
			results = new int[] { 7, 6, 7, 7, 7, 6, 6, 4, 6, 4 };
			for (int i = 0; i< 10; i++)
				Assert.AreEqual(results[i], d6.Roll());

			Dice.SetSeed(12);
			// 11, 4, 1, 1, 7, 0, 3, 5, 5, 8

			var d12 = new Dice(DiceType.D12, 26);
			results = new int[] {16, 7, 3, 3, 11, 2, 6, 8, 8, 12 };
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(results[i], d12.Roll());

			Dice.SetSeed(12);

			d12.Upgrade();
			d12.Upgrade();
			d12.Upgrade();
			d12.Upgrade();

			results = new int[] {25, 7, 3, 3, 11, 2, 6, 8, 8, 12 };
			for (int i = 0; i< 10; i++)
				Assert.AreEqual(results[i], d12.Roll());
		}

		// Classes untested - GameData, XYCoordinateClass, XYCoordinateStruct - doesn't seem much point if they
		// don't have any functionality.
	}
}
