// Tidied up for version 0.2.

using RLNET;
using System.Collections.Generic;

namespace Halfbreed.Display
{
	public class MenuConsole : BaseConsole
	{
		public MenuConsole(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
			: base(width, height, left, top, backColor, backConsole)
		{
		}


		public void DrawMenu(string title, List<string> options, string bottom)
		{
			Clear();

			_console.Print(5, 5, title, Palette.GetColor(Colors.Black));

			// TODO: Figure out whether we can dynamically space the menu between 1 and 4 spaces.
			// TODO: Split each piece out and put them in a new array - then use the length of the new array to sort it out.
			for (var i = 0; i < options.Count; i++)
			{
				if (options[i].Contains("\n"))
				{
					var pieces = options[i].Split('\n');

					if (pieces.Length > 4)
						ErrorLogger.AddDebugText("Two many lines in menu item" + pieces.ToString());

					for (var j = 0; j < pieces.Length; j++)
						_console.Print(5, 10 + 5 * i + j, pieces[j], Palette.GetColor(Colors.Black));
				}
				else
					_console.Print(5, 10 + 5 * i, options[i], Palette.GetColor(Colors.Black));
			}

			_console.Print(5, 70, bottom, Palette.GetColor(Colors.Black));

			CopyToBackConsole();
		}
	}
}
