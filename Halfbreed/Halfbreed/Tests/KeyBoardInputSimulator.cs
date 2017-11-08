namespace Halfbreed
{
	public static class KeyBoardInputSimulator
	{

		public static void AddKeyBoardInput(string key)
		{
			UserInputHandler.addKeyboardInput(key);
		}

		public static void AddKeyBoardInput(string[] keys)
		{
			foreach (string key in keys)
			{
				UserInputHandler.addKeyboardInput(key);
			}
		}
	}
}
