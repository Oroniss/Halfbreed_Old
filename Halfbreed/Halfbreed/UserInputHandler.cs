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
    }
}
