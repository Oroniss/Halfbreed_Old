using RLNET;

namespace Halfbreed
{
    public static class MainGraphicDisplay
    {
		// TODO: Refactor out the magic numbers here
		private static BackConsole _backConsole = new BackConsole(120, 90);
		private static MenuConsole _menuConsole = new MenuConsole(86, 116, 2, 2, RLColor.LightGray, _backConsole);

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

		public static RLRootConsole CopyDisplayToMainConsole(RLRootConsole destination)
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

    }
}
