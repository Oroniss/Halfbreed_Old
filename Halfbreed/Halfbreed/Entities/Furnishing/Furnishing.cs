using System.Collections.Generic;
using System;

namespace Halfbreed.Entities
{
	public partial class Entity
	{
		private static List<EntityTraits> _furnishingTraits = new List<EntityTraits>() {
			EntityTraits.IMMUNETODISEASE, EntityTraits.IMMUNETOMENTAL, EntityTraits.IMMUNETOPOISON };


		public Entity(string furnishingName, Materials material, FurnishingTemplate template, 
		              int xLoc, int yLoc, string[] otherParameters)
			:this(furnishingName, xLoc, yLoc, template.Traits)
		{
			foreach (EntityTraits trait in _furnishingTraits)
				AddTrait(trait);

			MaterialProperties properties = StaticData.GetProperties(material);

			_displayLayer = DisplayLayer.FURNISHING;
			_fgColor = properties.FGColor;
			_symbol = template.Symbol;

			_components[ComponentType.MATERIAL] = new MaterialComponent(this, material);

			foreach (EntityTraits trait in properties.Traits)
				AddTrait(trait);

			if (template.HasTile)
				_components[ComponentType.TILE] = new TileComponent(this, template.TileTypeName);

			_components[ComponentType.INTERACTIBLE] = new InteractibleComponent(this);

			if (template.HasDoor)
			{
				bool locked = (Array.IndexOf(otherParameters, "Locked") != -1);
				if (locked)
					_components[ComponentType.DOOR] = new DoorComponent(this, true, otherParameters);
				else
				{
					bool isOpen = (Array.IndexOf(otherParameters, "Open") != -1);
					_components[ComponentType.DOOR] = new DoorComponent(this, isOpen);
				}
			}

		}


	}

}
