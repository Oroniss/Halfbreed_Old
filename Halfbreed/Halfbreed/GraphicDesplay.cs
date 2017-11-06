using System.Threading;
using RLNET;

namespace Halfbreed
{
    public static class GraphicDesplay
    {
		// TODO: Refactor out the magic numbers here
		private static BackConsole _backConsole = new BackConsole(120, 90);
		private static DisplayConsole _menuConsole = new DisplayConsole(86, 116, 2, 2, RLColor.LightGray, _backConsole);

		static GraphicDesplay()
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

		public static DisplayConsole MenuConsole
		{
			get { return _menuConsole; }
		}

    }
}
