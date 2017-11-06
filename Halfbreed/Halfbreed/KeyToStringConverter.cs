using System;
using System.Collections.Generic;
using RLNET;

namespace Halfbreed
{
    public class KeyToStringConverter
    {
        private static Dictionary<RLKey, String> keyToString = 
        new Dictionary<RLKey, string>(){
            {RLKey.Escape, "ESCAPE"},
            {RLKey.Up, "UP"},
            {RLKey.Down, "DOWN"},
            {RLKey.Left, "LEFT"},
            {RLKey.Right, "RIGHT"}
        };

        public bool checkKeyIsValid(RLKey key)
        {
            return keyToString.ContainsKey(key);
        }

        public string convertKeyToString(RLKey key)
        {
            if (!keyToString.ContainsKey(key))
            {
                // TODO: Print an error message here
                return "ESCAPE";
            }
            return keyToString[key];
        }
    }
}
