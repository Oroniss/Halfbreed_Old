using System.Collections.Generic;
using Halfbreed.Entities.Furnishings;

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

			_interactionFunctions = new List<string>();

			FurnishingSetupFunctions.GetSetupFunction(furnishingName)(this, otherParameters);
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

		public void AddInteractionFunction(string functionName)
		{
			_interactionFunctions.Add(functionName);
		}

		public void RemoveInteractionFunction(string functionName)
		{
			_interactionFunctions.Remove(functionName);
		}

		public void InteractWith(Actor actor, Level level)
		{
			for (int i = 0; i < _interactionFunctions.Count; i++)
				InteractionFunctions.GetInteractionFunction(_interactionFunctions[i])(this, actor, level);
		}
	}
}
