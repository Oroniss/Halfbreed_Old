using System.Collections.Generic;

namespace Halfbreed.Converters
{
	public class MaterialToStringConverter
	{
		private static Dictionary<string, Materials> _stringToMaterials = new Dictionary<string, Materials>
		{
			{"Copper", Materials.COPPER},
			{"Fur", Materials.FUR},
			{"Hession", Materials.HESSION},
			{"Pine", Materials.PINE},
			{"Tin", Materials.TIN}
		};

		public Materials ConvertStringToMaterial(string inputValue)
		{
			return _stringToMaterials[inputValue];
		}

		public static int NumberOfMaterials
		{
			get { return _stringToMaterials.Count; }
		}
	}
}
