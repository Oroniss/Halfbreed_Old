using RLNET;

namespace Halfbreed.Display
{
	public abstract class BaseConsole
	{
		int _height;
		int _width;
		int _top;
		int _left;
		RLColor _backColor;
		BackConsole _backConsole;
		protected RLConsole _console;

		public BaseConsole(int width, int height, int top, int left, RLColor backColor, BackConsole backConsole)
		{
			_height = height;
			_width = width;
			_left = left;
			_top = top;
			_backConsole = backConsole;
			_backColor = backColor;

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

		protected void Clear()
		{
			_console.Clear();
			_console.SetBackColor(0, 0, _width, _height, _backColor);
		}
	}
}
