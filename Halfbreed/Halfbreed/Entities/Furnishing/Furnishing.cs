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

			// TODO: Put this somewhere else and make it generic for every entity type.
			if (Array.IndexOf(otherParameters, "Trapped") != -1)
			{
				string trapType = otherParameters[Array.IndexOf(otherParameters, "TrapType") + 1];
				int difficulty = Int32.Parse(otherParameters[Array.IndexOf(otherParameters, "TrapLevel") + 1]);
				_components[ComponentType.TRAP] = new TrapComponent(this, trapType, difficulty);
				((InteractibleComponent)_components[ComponentType.INTERACTIBLE]).AddFunction("TriggerTrap");
			}
		}


	}

}
