using System.Collections.Generic;
using Halfbreed.Entities;

namespace Halfbreed
{
	public partial class Entity
	{

		public Entity(string furnishingName, Materials material, FurnishingTemplate template, int xLoc, int yLoc, string[] otherParameters)
			
		{
			_entityId = GetNextId();
			_entityName = furnishingName;
			_xLoc = xLoc;
			_yLoc = yLoc;
			_components = new Dictionary<ComponentType, Component>();

			MaterialProperties properties = StaticData.GetProperties(material);

			_displayLayer = DisplayLayer.FURNISHING;
			_fgColor = properties.FGColor;
			_symbol = template.Symbol;

			_components[ComponentType.MATERIAL] = new MaterialComponent(this, material);

			if (template.HasTile)
				_components[ComponentType.TILE] = new TileComponent(this, template.TileTypeName);

		}


	}

}
