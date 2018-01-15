using System;
using System.Threading;
using System.Collections.Generic;

namespace Halfbreed
{
    public static class UserInputHandler
    {
        private static List<String> _queuedInput = new List<String>();

		private static List<String> _numberKeys = new List<String>
		    {"1", "2", "3", "4", "5", "6", "7", "8", "9",  "0"};

        public static void addKeyboardInput(string key)
        {
            lock (_queuedInput)
            {
                _queuedInput.Add(key);
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
						_queuedInput.RemoveAt(0);
                        return toReturn;
                    }
                }
                Thread.Yield();
            }
        }

		public static void clearAllInput()
		{
			lock (_queuedInput)
			{
				_queuedInput = new List<string>();
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
					currentDisplay.Add(((i % 10 + 1) % 10).ToString() + ": " + menuOptions[i]);
				}
				MainGraphicDisplay.MenuConsole.DrawMenu(title, currentDisplay, bottom);

				string key = getNextKey();

				if (_numberKeys.Contains(key))
				{
					int num = Int32.Parse(key);
					if (num == 0)
					{
						if ((page + 1) * 10 <= menuOptions.Count)
						{
							return (page + 1) * 10 - 1;
						}
					}
					else
					{
						if (num + page * 10 <= menuOptions.Count)
						{
							return num - 1 + page * 10;
						}
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
