using System;

namespace Halfbreed
{
	public static class NameConverter
	{
		public static KeyToStringConverter KeyToStringConverter = new KeyToStringConverter();
		public static StringToColorConverter StringToColorConverter = new StringToColorConverter();
		public static CharacterClassToStringConverter CharacterClassToStringConverter = new CharacterClassToStringConverter();
	}
}
