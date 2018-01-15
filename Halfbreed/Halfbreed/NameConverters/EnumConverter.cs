﻿using Halfbreed.Converters;

namespace Halfbreed
{
	public static class EnumConverter
	{
		private static KeyToStringConverter KeyToStringConverter = new KeyToStringConverter();
		private static CharacterClassToStringConverter CharacterClassToStringConverter = new CharacterClassToStringConverter();

		public static string ConvertEnumToString(object enumValue)
		{
			if(enumValue is RLNET.RLKey)
			{
				return KeyToStringConverter.convertKeyToString((RLNET.RLKey)enumValue);
			}

			if (enumValue is CharacterClasses)
			{
				return CharacterClassToStringConverter.ConvertCharacterClassToString(
					(CharacterClasses)enumValue);
			}

			return "";
		}

		public static CharacterClasses ConvertStringToCharacterClass(string inputValue)
		{
			return CharacterClassToStringConverter.ConvertStringToCharacterClass(inputValue);
		}
	}
}
