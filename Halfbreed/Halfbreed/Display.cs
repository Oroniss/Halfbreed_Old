using System.Threading;
using RLNET;

namespace Halfbreed
{
    public static class Display
    {
		// TODO: Refactor out the magic numbers here
		private static BackConsole _backConsole = new BackConsole(120, 90);
		private static DisplayConsole _menuConsole = new DisplayConsole(86, 116, 2, 2, RLColor.LightGray);

		static Display()
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
			// TODO: Decide if this needs to be inside a lock too.
			return destination;
		}

		public static void DrawMenu()
		{
			lock (_backConsole)
			{
				_backConsole = _menuConsole.CopyToBackConsole(_backConsole);
				_backConsole.SetDirty();
			}
		}
    }
}
