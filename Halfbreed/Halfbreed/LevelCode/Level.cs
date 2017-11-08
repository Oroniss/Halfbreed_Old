using System.Collections.Generic;
using System.IO;
namespace Halfbreed
{
	public class Zone
	{
		public Zone(string zoneID)
		{
			string[] zoneSpecification = File.ReadAllLines("Levels/" + zoneID + ".txt");
		}

	}
}
