using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public class Furnishing:Entity
	{
		private static List<Traits> _furnishingTraits = new List<Traits>() {
			Traits.ImmuneToDisease, Traits.ImmuneToMental, Traits.ImmuneToPoison };

		bool _hasBGColor;
		Colors _bgColor;
		bool _hasFogColor;
		Colors _fogColor;
		int _elevation;

		List<string> _interactionFunctions;

		public Furnishing(string furnishingName, int xLoc, int yLoc, List<string> otherParameters)
			:base(furnishingName, xLoc, yLoc, otherParameters)
		{
			foreach (Traits trait in _furnishingTraits)
				AddTrait(trait);

			var template = EntityData.GetFurnishingDetails(furnishingName);

			_hasBGColor = template.HasBGColor;
			_bgColor = template.BGColor;
			_hasFogColor = template.HasFogColor;
			_fogColor = template.FogColor;
			_elevation = template.Elevation;

			// TODO: Call the setup function here too.

		}

		public bool HasBGColor
		{
			get { return _hasBGColor; }
		}

		public bool HasFogColor
		{
			get { return _hasFogColor; }
		}

		public Colors BGColor
		{
			// TODO: Think about whether this should also check for concealed.
			get
			{
				if (!_hasBGColor)
					ErrorLogger.AddDebugText("Asked for BGColor on Entity without it: " + this.ToString());
				return _bgColor;
			}
		}

		public Colors FogColor
		{
			// TODO: Think about whether this should also check for concealed.
			get
			{
				if (!_hasFogColor)
					ErrorLogger.AddDebugText("Asked for FogColor on Entity without it: " + this.ToString());
				return _fogColor;
			}
		}

		public int Elevation
		{
			get { return _elevation; }
		}
	}
}
