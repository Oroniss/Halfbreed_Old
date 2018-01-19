using System;
using System.Collections.Generic;
using RLNET;

namespace Halfbreed.Converters
{
    public class KeyToStringConverter
    {
		// TODO: Add other keys as we go along.
        private Dictionary<RLKey, String> keyToString = 
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

			{RLKey.A, "A"},
			{RLKey.B, "B"},
			{RLKey.C, "C"},
			{RLKey.D, "D"},
			{RLKey.E, "E"},
			{RLKey.F, "F"},
			{RLKey.G, "G"},
			{RLKey.H, "H"},
			{RLKey.I, "I"},
			{RLKey.J, "J"},
			{RLKey.K, "K"},
			{RLKey.L, "L"},
			{RLKey.M, "M"},
			{RLKey.N, "N"},
			{RLKey.O, "O"},
			{RLKey.P, "P"},
			{RLKey.Q, "Q"},
			{RLKey.R, "R"},
			{RLKey.S, "S"},
			{RLKey.T, "T"},
			{RLKey.U, "U"},
			{RLKey.V, "V"},
			{RLKey.W, "W"},
			{RLKey.X, "X"},
			{RLKey.Y, "Y"},
			{RLKey.Z, "Z"}
        };

        public string convertKeyToString(RLKey key)
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
