using System.Collections.Generic;
using Halfbreed.Entities;

namespace Halfbreed
{
	public class Furnishing:Entity
	{

		private static int _currentMaxFurnishingId = 0;
		private static List<int> _freeFurnishingIds = new List<int>();

		private int _furnishingId;

		protected Materials _material;

		public Furnishing(string furnishingName, Materials material, FurnishingTemplate template, int xLoc, int yLoc, string[] otherParameters)
			:base(furnishingName, xLoc, yLoc)
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

			_material = material;
			_displayLayer = DisplayLayer.FURNISHING;

			MaterialProperties properties = EntityData.GetProperties(material);

			_fgColor = properties.FGColor;
			_symbol = template.Symbol;
		}

		public Materials Material
		{
			get { return _material; }
		}

		public override string GetDescription()
		{
			// TODO: Need to actually implement this.
			return EntityName;
		}

	}

}
