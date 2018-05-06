namespace Halfbreed
{
	public class EntityBasicDetails
	{
		public readonly char Symbol;
		public readonly string FGColorName;
		public readonly string[] Traits;

		public EntityBasicDetails(char symbol, string colorName, string[] traits)
		{
			Symbol = symbol;
			FGColorName = colorName;

			var tmp = new string[traits.Length];
			for (int i = 0; i < traits.Length; i++)
				tmp[i] = traits[i];
			Traits = tmp;
		}
	}
}
