using System.Threading;
using RLNET;

namespace Halfbreed
{
    public static class Display
    {
		// TODO: Refactor out the magic numbers here
		private static RLConsole _backConsole = new RLConsole(120, 90);
		private static RLConsole _menuConsole = new RLConsole(116, 86);

		private static bool _isDirty = true;

		static Display()
		{
			_menuConsole.SetBackColor(0, 0, 116, 86, RLColor.LightGray);
			_backConsole.Print(5, 5, "Hello", RLColor.Cyan);
		}

		public static bool IsDirty
		{
			get { return _isDirty; }
		}

		public static RLRootConsole CopyDisplayToMainConsole(RLRootConsole destination)
		{
			lock (_backConsole)
			{
				RLConsole.Blit(_backConsole, 0, 0, 120, 90, destination, 0, 0);
			}
			// TODO: Decide if this needs to be inside a lock too.
			_isDirty = false;
			return destination;
		}
    }
}
