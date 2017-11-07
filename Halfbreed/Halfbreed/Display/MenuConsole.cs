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


		public void DrawMenu(string title, List<string> options, string bottom)
		{
			Clear();

			_console.Print(5, 5, title, RLColor.Black);

			// TODO: Figure out whether we can dynamically space the menu between 1 and 4 spaces.
			for (int i = 0; i < options.Count; i++)
			{
				_console.Print(5, 10 + 4 * i, options[i], RLColor.Black);
			}

			_console.Print(5, 80, bottom, RLColor.Black);

			CopyToBackConsole();
		}
	}
}
