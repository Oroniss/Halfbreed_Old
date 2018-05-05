// Revised for version 0.02.

namespace Halfbreed
{
	public static class ErrorLogger
	{

		public static void AddDebugText(string text)
		{
			AddDebugText(text, "White");
		}

		public static void AddDebugText(string text, string colorName)
		{
			// TODO: Fix this properly.
			System.Console.WriteLine(text);
		}
	}
}
