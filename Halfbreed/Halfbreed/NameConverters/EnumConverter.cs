using Halfbreed.Converters;

namespace Halfbreed
{
	public static class EnumConverter
	{
		private static KeyToStringConverter KeyToStringConverter = new KeyToStringConverter();
		private static StringToColorConverter StringToColorConverter = new StringToColorConverter();
		private static CharacterClassToStringConverter CharacterClassToStringConverter = new CharacterClassToStringConverter();
		private static MaterialToStringConverter MaterialToStringConverter = new MaterialToStringConverter();

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

		public static Colors ConvertStringToColor(string inputValue)
		{
			return StringToColorConverter.ConvertStringToColor(inputValue);
		}

		public static CharacterClasses ConvertStringToCharacterClass(string inputValue)
		{
			return CharacterClassToStringConverter.ConvertStringToCharacterClass(inputValue);
		}

		public static Materials ConvertStringToMaterial(string inputValue)
		{
			return MaterialToStringConverter.ConvertStringToMaterial(inputValue);
		}
	}
}
