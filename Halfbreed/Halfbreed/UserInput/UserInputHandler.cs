using System;
using System.Threading;
using System.Collections.Generic;
using RLNET;

namespace Halfbreed
{
    public static class UserInputHandler
    {
        private static List<RLKey> _queuedInput = new List<RLKey>();

		private static Dictionary<RLKey, int> _numberKeys = new Dictionary<RLKey, int>
			{{RLKey.Number0, 0}, {RLKey.Number1, 1},  {RLKey.Number2, 2}, {RLKey.Number3, 3}, {RLKey.Number4, 4},
			{RLKey.Number5, 5}, {RLKey.Number6, 6}, {RLKey.Number7, 7}, {RLKey.Number8, 8}, {RLKey.Number9, 9}};

		private static Dictionary<RLKey, Direction> _directionKeys = new Dictionary<RLKey, Direction>
			{{RLKey.Up, new Direction(0, -1)}, {RLKey.Down, new Direction(0, 1)}, {RLKey.Left, new Direction(-1, 0)},
			{RLKey.Right, new Direction(1, 0)}};
		// TODO: Add the keypad keys here too.
		// TODO: Make sure the laptop keys options changes this too.

        public static void addKeyboardInput(RLKey key)
        {
            lock (_queuedInput)
            {
                _queuedInput.Add(key);
            }
        }

        public static RLKey getNextKey()
        {
            while (true)
            {
                lock (_queuedInput)
                {
                    if(_queuedInput.Count > 0)
                    {
                        RLKey toReturn = _queuedInput[0];
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
				_queuedInput = new List<RLKey>();
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

				RLKey key = getNextKey();

				if (_numberKeys.ContainsKey(key))
				{
					int num = _numberKeys[key];
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
				if (key == RLKey.Left && page > 0)
				{
					page--;
				}
				if (key == RLKey.Right && ((page + 1) * 10 < menuOptions.Count))
				{
					page++;
				}
				if (key == RLKey.Escape)
				{
					return -1;
				}
			}
		}

		public static Direction GetDirection(string queryText, bool centre)
		{
			if (queryText == "")
				queryText = "Which direction?";

			while (true)
			{
				MainGraphicDisplay.TextConsole.AddOutputText("Which direction");
				RLKey key = getNextKey();

				if (_directionKeys.ContainsKey(key))
					return _directionKeys[key];
				if (key == RLKey.Enter && centre)
					return new Direction(0, 0);
				if (key == RLKey.Escape)
					return null;
			}
		}
    }
}
