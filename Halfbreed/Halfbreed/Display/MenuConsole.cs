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
			for (int i = 0; i < options.Count; i++)
			{
				if (options[i].Contains("\n"))
				{
					var pieces = options[i].Split('\n');
					// TODO: Add a check for more than 3 pieces.
					for (int j = 0; j < pieces.Length; j++)
						_console.Print(5, 10 + 4 * i + j, pieces[j], Palette.GetColor(Colors.Black));
				}
				else
					_console.Print(5, 10 + 4 * i, options[i], Palette.GetColor(Colors.Black));
			}

			_console.Print(5, 80, bottom, Palette.GetColor(Colors.Black));

			CopyToBackConsole();
		}
	}
}
