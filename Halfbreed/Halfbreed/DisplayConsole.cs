using RLNET;
namespace Halfbreed
{
	public class DisplayConsole
	{
		int _height;
		int _width;
		int _top;
		int _left;
		BackConsole _backConsole;
		protected RLConsole _console;

		public DisplayConsole(int height, int width, int top, int left, RLColor backColor, BackConsole backConsole)
		{
			_height = height;
			_width = width;
			_left = left;
			_top = top;
			_backConsole = backConsole;

			_console = new RLConsole(_width, _height);
			_console.SetBackColor(0, 0, _width, _height, backColor);
		}

		protected void CopyToBackConsole()
		{
			lock (_backConsole)
			{
				RLConsole.Blit(_console, 0, 0, _width, _height, _backConsole, _left, _top);
				_backConsole.SetDirty();
			}
		}
	}
}
