using System.Collections.Generic;

namespace Halfbreed.Converters
{
	public class StringToTraitConverter
	{
		private static Dictionary<string, EntityTraits> _stringToTraits = new Dictionary<string, EntityTraits>()
		{
			{"Furnishing", EntityTraits.FURNISHING},
			{"Ruined", EntityTraits.RUINED},
			{"Elevation Change", EntityTraits.ELEVATIONCHANGE},
			{"Immune to Fire", EntityTraits.IMMUNETOFIRE},
			{"Immune to Cold", EntityTraits.IMMUNETOCOLD},
			{"Immune to Acid", EntityTraits.IMMUNETOACID},
			{"Immune to Electricity", EntityTraits.IMMUNETOELEC},
			{"Immune to Mental", EntityTraits.IMMUNETOMENTAL},
			{"Immune to Poison", EntityTraits.IMMUNETOPOISON},
			{"Immune to Disease", EntityTraits.IMMUNETODISEASE},
			{"Wood", EntityTraits.WOOD},
			{"Organic", EntityTraits.ORGANIC},
			{"Metal", EntityTraits.METAL},
			{"Inorganic", EntityTraits.INORGANIC},
			{"Stone", EntityTraits.STONE},
			{"Gem", EntityTraits.GEM},
			{"Leather", EntityTraits.LEATHER},
			{"Cloth", EntityTraits.CLOTH},
			{"Animal", EntityTraits.ANIMAL},
			{"Plant", EntityTraits.PLANT}
		};

		public EntityTraits ConvertStringToTrait(string traitName)
		{
			return _stringToTraits[traitName];
		}

		public static int GetNumberOfTraits()
		{
			return _stringToTraits.Count;
		}
	}
}
