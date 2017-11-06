using RLNET;
namespace Halfbreed
{
	public class DisplayConsole
	{
		int _height;
		int _width;
		int _top;
		int _left;
		RLConsole _console;

		public DisplayConsole(int height, int width, int top, int left, RLColor backColor)
		{
			_height = height;
			_width = width;
			_left = left;
			_top = top;
			_console = new RLConsole(_width, _height);
			_console.SetBackColor(0, 0, _width, _height, backColor);
		}

		public BackConsole CopyToBackConsole(BackConsole backConsole)
		{
			lock (backConsole)
			{
				RLConsole.Blit(_console, 0, 0, _width, _height, backConsole, _left, _top);
				backConsole.SetDirty();
			}
			return backConsole;
		}
	}
}
