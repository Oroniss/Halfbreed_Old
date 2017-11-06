using System.Threading;
using RLNET;

namespace Halfbreed
{
    public static class Display
    {
		// TODO: Refactor out the magic numbers here
		private static RLConsole _backConsole = new RLConsole(120, 90);
		private static bool _isDirty = true;

		public static bool IsDirty
		{
			get { return _isDirty; }
		}
    }
}
