using RLNET;

namespace Halfbreed
{
    public static class MainGraphicDisplay
    {
		// TODO: See if this can be cleaned up a little.
		private static int _backConsoleWidth = 160;
		private static int _backConsoleHeight = 80;
		private static BackConsole _backConsole = new BackConsole(_backConsoleWidth, _backConsoleHeight);

		private static int _menuConsoleOffset = 2;
		private static MenuConsole _menuConsole = new MenuConsole(_backConsoleWidth - 2 * _menuConsoleOffset,
		                                                          _backConsoleHeight - 2 * _menuConsoleOffset,
		                                                          _menuConsoleOffset,
		                                                          _menuConsoleOffset, 
		                                                          Palette.LIGHTGREY, 
		                                                          _backConsole);

		private static int _mapConsoleWidth = 80;
		private static int _mapConsoleHeight = 80;
		private static int _mapConsoleOffset = 0;
		private static MapConsole _mapConsole = new MapConsole(_mapConsoleWidth, _mapConsoleHeight, 0, 0, 
		                                                       Palette.BLACK, _backConsole);

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
