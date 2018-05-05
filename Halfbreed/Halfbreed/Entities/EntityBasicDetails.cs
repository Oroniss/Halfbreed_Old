// Tidied for version 0.02. No changes required.

using System;

namespace Halfbreed
{
	public class EntityBasicDetails
	{
		public readonly char Symbol;
		public readonly string FGColorName;
		public readonly Traits[] Traits;

		public EntityBasicDetails(char symbol, string colorName, string[] traits)
		{
			Symbol = symbol;
			FGColorName = colorName;

			var tmp = new Traits[traits.Length];
			for (int i = 0; i < traits.Length; i++)
				tmp[i] = (Traits)Enum.Parse(typeof(Traits), traits[i]);
			Traits = tmp;
		}
	}
}
