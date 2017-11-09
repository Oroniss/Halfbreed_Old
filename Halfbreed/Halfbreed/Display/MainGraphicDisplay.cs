using RLNET;

namespace Halfbreed
{
    public static class MainGraphicDisplay
    {
		// TODO: Refactor out the magic numbers here
		private static BackConsole _backConsole = new BackConsole(120, 90);
		private static MenuConsole _menuConsole = new MenuConsole(86, 116, 2, 2, RLColor.LightGray, _backConsole);
		private static MapConsole _mapConsole = new MapConsole(60, 60, 0, 0, Palette.BLACK, _backConsole);

		static MainGraphicDisplay()
		{
			_backConsole.Print(5, 5, "Hello", RLColor.Cyan);
		}

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
				RLConsole.Blit(_backConsole, 0, 0, 120, 90, destination, 0, 0);
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
