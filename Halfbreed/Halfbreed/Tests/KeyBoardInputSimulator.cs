// Updated for version 0.2

namespace Halfbreed.Tests
{
	public static class KeyBoardInputSimulator
	{

		public static void AddKeyBoardInput(string key)
		{
			UserInputHandler.addKeyboardInput(key);
		}

		public static void AddKeyBoardInput(string[] keys)
		{
			foreach (var key in keys)
			{
				UserInputHandler.addKeyboardInput(key);
			}
		}
	}
}
