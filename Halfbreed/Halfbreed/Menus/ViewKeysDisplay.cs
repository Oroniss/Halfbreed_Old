// Tidied up for version 0.02.

using System.Collections.Generic;

namespace Halfbreed.Menus
{
	public class ViewKeysDisplay
	{
		readonly List<string> _keys = new List<string>
		{
			"A:  Display the active ability menu",
			"B:  Display the contents of your backpack",
			"C:  Display the character menu",
			"E:  Examine an object or creature",
			"G:  Gather from a resource node such as ore or lumber",
			"H:  Setup or clear a hot-key slot",
			"I:  Use an item from your inventory",
			"J:  Display your quest journal",
			"K:  Display this list of commands",
			"M:  Display the minion stats and abilities menu",
			"P:  Display your passive abilities and talents",
			"R:  Display your crafting recipes known",
			"S:  Search your surroundings",
			"T:  Talk to a creature or NPC",
			"U:  Use an object such as a door or lever",
			"V:  Display the achievement menu",
			"",
			"Spacebar:  Pass your current turn",
			"Direction Keys:  Move in the given direction"
		};

		public void ViewKeys()
		{
			MainGraphicDisplay.MenuConsole.DrawTextBlock("Available commands", _keys, "Press any key to return");
			UserInputHandler.getNextKey();
		}
	}
}
