using System.Collections.Generic;
using System;

namespace Halfbreed.Entities.Furnishings
{
	public static class InteractionFunctions
	{
		static readonly Dictionary<string, InteractionFunction> _interactionFunctions = new Dictionary<string, InteractionFunction>()
		{
			// Default
			{"No Use", new InteractionFunction(NoUse)},
			// Furnishings
			{"Door Use", new InteractionFunction(DoorUse)},
			{"Level Transition Use", new InteractionFunction(LevelTransitionUse)},

			// Traps
			{"Trigger Swinging Blade Trap", new InteractionFunction(TriggerSwingingBladeTrap)}
		};

		public delegate void InteractionFunction(Furnishing furnishing, Actor actor, Level currentLevel);

		public static InteractionFunction GetInteractionFunction(string functionName)
		{
			if (_interactionFunctions.ContainsKey(functionName))
				return _interactionFunctions[functionName];
			ErrorLogger.AddDebugText("Unknown interaction function name: " + functionName);
			return _interactionFunctions["No Use"];
		}

		private static void NoUse(Furnishing furnishing, Actor actor, Level currentLevel)
		{
			MainGraphicDisplay.TextConsole.AddOutputText("You can't do anything with that");
		}

		private static void DoorUse(Furnishing furnishing, Actor actor, Level currentLevel)
		{
			if (furnishing.GetOtherAttributeValue("DoorOpen") == "Open")
			{
				if (currentLevel.BlockAllMovement(furnishing.XLoc, furnishing.YLoc))
				{
					MainGraphicDisplay.TextConsole.AddOutputText("Something is blocking the door");
					return;
				}
				furnishing.AddTrait(Traits.Impassible);
				furnishing.AddTrait(Traits.BlockLOS);
				furnishing.Symbol = '+';
				furnishing.SetOtherAttribute("DoorOpen", "Closed");

				if (actor.HasTrait(Traits.Player))
					MainGraphicDisplay.TextConsole.AddOutputText("You close the door");
			}
			else
			{
				if (furnishing.HasOtherAttribute("DoorLocked") && furnishing.GetOtherAttributeValue("DoorLocked") == "True")
				{
					// TODO: Flesh this out.
					MainGraphicDisplay.TextConsole.AddOutputText("The door is locked");
					return;
				}

				furnishing.RemoveTrait(Traits.Impassible);
				furnishing.RemoveTrait(Traits.BlockLOS);
				furnishing.Symbol = '-';
				furnishing.SetOtherAttribute("DoorOpen", "Open");

				if (actor.HasTrait(Traits.Player))
					MainGraphicDisplay.TextConsole.AddOutputText("You open the door");
			}
		}

		private static void LevelTransitionUse(Furnishing furnishing, Actor actor, Level currentLevel)
		{
			var destinationLevel = (Levels.LevelEnum)Enum.Parse(typeof(Levels.LevelEnum),
																furnishing.GetOtherAttributeValue("DestinationLevel"));
			var newXLoc = int.Parse(furnishing.GetOtherAttributeValue("NewXLoc"));
			var newYLoc = int.Parse(furnishing.GetOtherAttributeValue("NewYLoc"));
			GameEngine.InitiateLevelTransition(destinationLevel, newXLoc, newYLoc);
		}

		private static void TriggerSwingingBladeTrap(Furnishing furnishing, Actor actor, Level currentLevel)
		{
			var trapLevel = int.Parse(furnishing.GetOtherAttributeValue("TrapLevel"));

			actor.ProcessDamage(furnishing, new Combat.Damage(Combat.DamageType.Physical, 5 * trapLevel));
			furnishing.RemoveInteractionFunction("Trigger Swinging Blade Trap");
			furnishing.Trapped = false;
		}

	}
}