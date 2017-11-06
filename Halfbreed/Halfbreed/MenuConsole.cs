using RLNET;
namespace Halfbreed
{
	public class MenuConsole : DisplayConsole
	{
		public MenuConsole(int height, int width, int top, int left, RLColor backColor, BackConsole backConsole)
			: base(height, width, top, left, backColor, backConsole)
		{
		}

		public void DrawSelectFromMenu(string title, string[] options, string bottom)
		{
			_console.Print(5, 5, title, RLColor.Black);

			for (int i = 0; i < options.Length; i++)
			{
				_console.Print(5, 10 + 4 * i, options[i], RLColor.Black);
			}

			_console.Print(5, 70, bottom, RLColor.Black);

			CopyToBackConsole();
		}
	}
}
