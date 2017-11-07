using RLNET;
using System.Collections.Generic;
namespace Halfbreed
{
	public class MenuConsole : DisplayConsole
	{
		public MenuConsole(int height, int width, int top, int left, RLColor backColor, BackConsole backConsole)
			: base(height, width, top, left, backColor, backConsole)
		{
		}

		// TODO: Think about whether to have an override with an array here?
		public void DrawSelectFromMenu(string title, List<string> options, string bottom)
		{
			_console.Print(5, 5, title, RLColor.Black);

			for (int i = 0; i < options.Count; i++)
			{
				_console.Print(5, 10 + 4 * i, (i+1).ToString() + ": " + options[i], RLColor.Black);
			}

			_console.Print(5, 70, bottom, RLColor.Black);

			CopyToBackConsole();
		}
	}
}
