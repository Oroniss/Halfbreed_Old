using System.Collections.Generic;
using Halfbreed.Entities;

namespace Halfbreed
{
	public class Furnishing:Entity
	{

		private static int _currentMaxFurnishingId = 0;
		private static List<int> _freeFurnishingIds = new List<int>();

		private int _furnishingId;

		protected string _furnishingName;
		protected Materials _material;

		public Furnishing(string furnishingName, Materials material, int xLoc, int yLoc, string[] otherParameters)
		{
			if (_freeFurnishingIds.Count == 0)
			{
				_furnishingId = _currentMaxFurnishingId;
				_currentMaxFurnishingId += 1;
			}
			else
			{
				_furnishingId = _freeFurnishingIds[0];
				_freeFurnishingIds.RemoveAt(0);
			}

			_xLoc = xLoc;
			_yLoc = yLoc;

			_furnishingName = furnishingName;
			_material = material;
		}

		public override string GetDescription()
		{
			return _furnishingName;
		}

		public Materials Material
		{
			get { return _material; }
		}

	}

}
