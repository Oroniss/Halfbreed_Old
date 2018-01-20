using RLNET;

namespace Halfbreed.Tests
{
	public static class KeyBoardInputSimulator
	{

		public static void AddKeyBoardInput(RLKey key)
		{
			UserInputHandler.addKeyboardInput(key);
		}

		public static void AddKeyBoardInput(RLKey[] keys)
		{
			foreach (RLKey key in keys)
			{
				UserInputHandler.addKeyboardInput(key);
			}
		}
	}
}
