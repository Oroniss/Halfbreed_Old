using System.Collections.Generic;
using System;
using Halfbreed.Entities.Furnishings;

namespace Halfbreed.Entities
{
	[Serializable]
	public class Furnishing:Entity
	{
		private static List<Traits> _furnishingTraits = new List<Traits>() {
			Traits.ImmuneToDisease, Traits.ImmuneToMental, Traits.ImmuneToPoison };

		bool _hasBGColor;
		Colors _bgColor;
		bool _hasFogColor;
		Colors _fogColor;
		int _elevation;
		string _moveOnFunction;
		string _moveOffFunction;

		bool _trapped;

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

		public override Colors FGColor
		{
			get
			{
				if (_trapped && PlayerSpotted)
					return Colors.Red;
				return base.FGColor;
			}
		}

		public Colors BGColor
		{
			get
			{	if (!PlayerSpotted && HasOtherAttribute("ConcealedBGColor"))
					return (Colors)Enum.Parse(typeof(Colors), GetOtherAttributeValue("ConcealedBGColor"));
				if (!_hasBGColor)
					ErrorLogger.AddDebugText("Asked for BGColor on Entity without it: " + this);
				return _bgColor;
			}
		}

		public Colors FogColor
		{
			get
			{
				if (!PlayerSpotted && HasOtherAttribute("ConcealedFogColor"))
					return (Colors)Enum.Parse(typeof(Colors), GetOtherAttributeValue("ConcealedFogColor"));
				if (!_hasFogColor)
					ErrorLogger.AddDebugText("Asked for FogColor on Entity without it: " + this);
				return _fogColor;
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
			if (_moveOffFunction == null || _moveOffFunction == "")
				return true;

			return MovementFunctions.GetMoveOffFunction(_moveOffFunction)(this, actor, currentLevel, destinationX, destinationY);
		}

		public void MoveOn(Actor actor, Level currentLevel, int originX, int originY)
		{
			if (_moveOnFunction != null && _moveOnFunction != "")
				MovementFunctions.GetMoveOnFunction(_moveOnFunction)(this, actor, currentLevel, originX, originY);
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
