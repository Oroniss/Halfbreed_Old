using System;
using System.Collections.Generic;
using RLNET;

namespace Halfbreed
{
    public static class KeyToStringConverter
    {
		// TODO: Add other keys as we go along.
        private static Dictionary<RLKey, String> keyToString = 
        new Dictionary<RLKey, string>(){
            {RLKey.Escape, "ESCAPE"},
            {RLKey.Up, "UP"},
            {RLKey.Down, "DOWN"},
            {RLKey.Left, "LEFT"},
            {RLKey.Right, "RIGHT"},
			{RLKey.Number0, "0"},
			{RLKey.Number1, "1"},
			{RLKey.Number2, "2"},
			{RLKey.Number3, "3"},
			{RLKey.Number4, "4"},
			{RLKey.Number5, "5"},
			{RLKey.Number6, "6"},
			{RLKey.Number7, "7"},
			{RLKey.Number8, "8"},
			{RLKey.Number9, "9"},
        };

        public static bool checkKeyIsValid(RLKey key)
        {
            return keyToString.ContainsKey(key);
        }

        public static string convertKeyToString(RLKey key)
        {
            if (!keyToString.ContainsKey(key))
            {
				ErrorLogger.AddDebugText(string.Format("Tried to convert unknown key: {0} to a string", key.ToString()));
                return "ESCAPE";
            }
            return keyToString[key];
        }
    }
}
