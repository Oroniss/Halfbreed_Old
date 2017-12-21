using RLNET;
using Halfbreed.Display;

namespace Halfbreed
{
    public static class MainGraphicDisplay
    {
		// TODO: Eventually these should probably go in a settings file or similar.
		private static int _backConsoleWidth = 160;
		private static int _backConsoleHeight = 80;
		private static BackConsole _backConsole = new BackConsole(_backConsoleWidth, _backConsoleHeight);

		private static int _menuConsoleWidth = _backConsoleWidth - 4;
		private static int _menuConsoleHeight = _backConsoleHeight - 4;
		private static int _menuConsoleXOffset = 2;
		private static int _menuConsoleYOffset = 2;
		private static MenuConsole _menuConsole = new MenuConsole(_menuConsoleWidth,
		                                                          _menuConsoleHeight,
		                                                          _menuConsoleXOffset,
		                                                          _menuConsoleYOffset, 
		                                                          Palette.GetColor(Colors.LIGHTGREY), 
		                                                          _backConsole);
		
		private static int _mapConsoleWidth = 80;
		private static int _mapConsoleHeight = 80;
		private static int _mapConsoleXOffset = 0;
		private static int _mapConsoleYOffset = 0;
		private static MapConsole _mapConsole = new MapConsole(_mapConsoleWidth, 
		                                                       _mapConsoleHeight,
															   _mapConsoleXOffset, 
		                                                       _mapConsoleYOffset, 
		                                                       Palette.GetColor(Colors.BLACK), 
		                                                       _backConsole);

		public static bool IsDirty
		{
			get
			{
				lock (_backConsole)
				{
					return _backConsole.IsDirty;
				}
			}
		}

		public static RLRootConsole CopyDisplayToRootConsole(RLRootConsole destination)
		{
			lock (_backConsole)
			{
				RLConsole.Blit(_backConsole, 0, 0, _backConsoleWidth, _backConsoleHeight, destination, 0, 0);
				_backConsole.SetClean();
			}
			return destination;
		}

		public static MenuConsole MenuConsole
		{
			get { return _menuConsole; }
		}

		public static MapConsole MapConsole
		{
			get { return _mapConsole; }
		}

    }
}
