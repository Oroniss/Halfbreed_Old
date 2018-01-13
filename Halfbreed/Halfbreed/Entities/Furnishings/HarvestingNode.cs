using Halfbreed.Entities;

namespace Halfbreed
{
	public partial class Entity
	{


		public Entity(string harvestableName, int xLoc, int yLoc, string[] otherParams)
			:this(harvestableName, xLoc, yLoc)
		{

			FurnishingTemplate furnishingTemplate = EntityDatabaseConnection.GetFurnishingDetails(harvestableName);

			_symbol = furnishingTemplate.Symbol;
			_displayLayer = DisplayLayer.HARVESTABLE;
		}
	}
}
