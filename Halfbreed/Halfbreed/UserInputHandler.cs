using System;
using System.Threading;
using System.Collections.Generic;
using RLNET;

namespace Halfbreed
{
    public static class UserInputHandler
    {
        private static List<String> _queuedInput = new List<String>();
        private static KeyToStringConverter _keyConverter = new KeyToStringConverter();

		private static List<String> _numberKeys = new List<String>
		    {"1", "2", "3", "4", "5", "6", "7", "8", "9",  "0"};

        public static void addKeyboardInput(RLKey key)
        {
            if(_keyConverter.checkKeyIsValid(key))
            {
                lock (_queuedInput)
                {
                    _queuedInput.Add(_keyConverter.convertKeyToString(key));
                }
            }
        }

        public static String getNextKey()
        {
            while (true)
            {
                lock (_queuedInput)
                {
                    if(_queuedInput.Count > 0)
                    {
                        String toReturn = _queuedInput[0];
                        _queuedInput = new List<string>();
                        return toReturn;
                    }
                }
                Thread.Yield();
            }
        }

		public static int SelectFromMenu(string title, List<String> menuOptions, string bottom)
		{
			int page = 0;
			while (true)
			{
				List<String> currentDisplay = new List<string>();
				for (int i = 10 * page; i < Math.Min(10 * (page + 1), menuOptions.Count); i++)
				{
					currentDisplay.Add((i % 10 + 1).ToString() + ": " + menuOptions[i]);
				}
				GraphicDesplay.MenuConsole.DrawMenu(title, currentDisplay, bottom);

				string key = getNextKey();

				if (_numberKeys.Contains(key))
				{
					int num = Int32.Parse(key);
					if (num + page * 10 <= menuOptions.Count)
					{
						return num - 1 + page * 10;
					}
				}
				if (key == "LEFT" && page > 0)
				{
					page--;
				}
				if (key == "RIGHT" && ((page + 1) * 10 < menuOptions.Count))
				{
					page++;
				}
				if (key == "ESCAPE")
				{
					return -1;
				}
			}
		}
    }
}
