using System.Collections.Generic;
using System;
using Halfbreed.Entities.Furnishings;

namespace Halfbreed.Entities
{
	[Serializable]
	public class Furnishing:Entity
	{
		static readonly List<string> _furnishingTraits = new List<string> {
			"ImmuneToDisease", "ImmuneToMental", "ImmuneToPoison" };

		bool _hasBGColor;
		string _bgColorName;
		bool _hasFogColor;
		string _fogColorName;

		bool _trapped;

		int _elevation;

		string _moveOnFunction;
		string _moveOffFunction;
		readonly List<string> _interactionFunctions;

		public Furnishing(string furnishingName, int xLoc, int yLoc, List<string> otherParameters)
			:base(furnishingName, xLoc, yLoc, otherParameters)
		{
			foreach (string trait in _furnishingTraits)
				AddTrait(trait);

			var template = EntityData.GetFurnishingDetails(furnishingName);

			_hasBGColor = template.HasBGColor;
			_bgColorName = template.BGColorName;
			_hasFogColor = template.HasFogColor;
			_fogColorName = template.FogColorName;
			_elevation = template.Elevation;
			_trapped = false;

			_interactionFunctions = new List<string>();

			FurnishingSetupFunctions.GetSetupFunction(furnishingName)(this, otherParameters);

			if (otherParameters.Contains("LevelTransition"))
				FurnishingSetupFunctions.GetSetupFunction("Level Transition Setup")(this, otherParameters);
			if (otherParameters.Contains("Trapped"))
				FurnishingSetupFunctions.GetSetupFunction("Interaction Trap Setup")(this, otherParameters);
			if (otherParameters.Contains("Concealed"))
				FurnishingSetupFunctions.GetSetupFunction("Concealed Furnishing Setup")(this, otherParameters);
		}

		public bool HasBGColor
		{
			get { return _hasBGColor || (!PlayerSpotted && HasOtherAttribute("ConcealedBGColor")); }
		}

		public bool HasFogColor
		{
			get { return _hasFogColor || (!PlayerSpotted && HasOtherAttribute("ConcealedFogColor")); }
		}

		public override string FGColorName
		{
			get
			{
				if (_trapped && PlayerSpotted)
					return "Red";
				return base.FGColorName;
			}
		}

		public string BGColorName
		{
			get
			{	if (!PlayerSpotted && HasOtherAttribute("ConcealedBGColor"))
					return GetOtherAttributeValue("ConcealedBGColor");
				return _bgColorName;
			}
		}

		public string FogColorName
		{
			get
			{
				if (!PlayerSpotted && HasOtherAttribute("ConcealedFogColor"))
					return GetOtherAttributeValue("ConcealedFogColor");
				return _fogColorName;
			}
		}

		public bool Trapped
		{
			get { return _trapped; }
			set { _trapped = value; }
		}

		public string MoveOnFunction
		{
			get { return _moveOnFunction; }
			set { _moveOnFunction = value; }
		}

		public string MoveOffFunction
		{
			get { return _moveOffFunction; }
			set { _moveOffFunction = value; }
		}

		public bool MoveOff(Actor actor, Level currentLevel, int destinationX, int destinationY)
		{
			if (string.IsNullOrEmpty(_moveOffFunction))
				return true;

			return MovementFunctions.GetMoveOffFunction(_moveOffFunction)(this, actor, currentLevel, destinationX, 
			                                                              destinationY);
		}

		public void MoveOn(Actor actor, Level currentLevel, int originX, int originY)
		{
			if (!string.IsNullOrEmpty(_moveOnFunction))
				MovementFunctions.GetMoveOnFunction(_moveOnFunction)(this, actor, currentLevel, originX, 
				                                                     originY);
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
			if (_interactionFunctions.Count == 0)
			{
				MainGraphicDisplay.TextConsole.AddOutputText("You can't do anything with that");
				return;
			}

			for (int i = 0; i < _interactionFunctions.Count; i++)
				InteractionFunctions.GetInteractionFunction(_interactionFunctions[i])(this, actor, level);
		}
	}
}
