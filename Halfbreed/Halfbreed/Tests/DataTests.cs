using RLNET;
using System;
using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class DataTests
	{
		[Test]
		public void PaletteTest()
		{
			Assert.AreEqual(new RLColor(190, 190, 190), Palette.GetColor(Colors.LIGHTGREY));
			Assert.AreEqual(new RLColor(255, 255, 255), Palette.GetColor(Colors.WHITE));
			Assert.AreEqual(new RLColor(193, 255, 193), Palette.GetColor(Colors.PUTRIDGREEN));

			Assert.AreEqual(Enum.GetValues(typeof(Colors)).Length, Palette.NumberOfColors);

		}

	}
}
