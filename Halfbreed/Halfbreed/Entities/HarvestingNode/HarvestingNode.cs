using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class Entity
	{

		public Entity(string harvestableName, int xLoc, int yLoc, HarvestingNodeType nodeType)
			:this(harvestableName, xLoc, yLoc, HarvestingNodeData.GetTraits(nodeType))
		{
			_displayLayer = DisplayLayer.HARVESTABLE;

			_symbol = HarvestingNodeData.GetSymbol(nodeType);
			_fgColor = HarvestingNodeData.GetColor(nodeType);

			foreach (EntityTraits trait in _furnishingTraits)
				AddTrait(trait);
			AddTrait(EntityTraits.FURNISHING);

			// TODO: Need to add the defensive stats - should all be the same
			// TODO: Need to set the harvesting component.
			// TODO: Need to add any other required traits.
		}
	}

	// Here since the dictionaries are of fixed size so it really seems silly to put them in the DB.
	internal static class HarvestingNodeData
	{
		private static Dictionary<HarvestingNodeType, char> _symbols = new Dictionary<HarvestingNodeType, char>(){
			{HarvestingNodeType.FISHING, ';'}, {HarvestingNodeType.RICHFISHING, ';'},
			{HarvestingNodeType.FORESTING, '{'}, {HarvestingNodeType.RICHFORESTING, '{'},
			{HarvestingNodeType.GATHERING, '}'}, {HarvestingNodeType.RICHGATHERING, '}'},
			{HarvestingNodeType.MINING, '*'}, {HarvestingNodeType.RICHMINING, '*'},
			{HarvestingNodeType.TRAPPING, ':'}, {HarvestingNodeType.RICHTRAPPING, ':'}};
		private static Dictionary<HarvestingNodeType, Colors> _colors = new Dictionary<HarvestingNodeType, Colors>(){
			{HarvestingNodeType.FISHING, Colors.SILVER}, {HarvestingNodeType.RICHFISHING, Colors.GOLD},
			{HarvestingNodeType.FORESTING, Colors.WOODBROWN}, {HarvestingNodeType.RICHFORESTING, Colors.DARKBROWN},
			{HarvestingNodeType.GATHERING, Colors.PUTRIDGREEN}, {HarvestingNodeType.RICHGATHERING, Colors.VILEGREEN},
			{HarvestingNodeType.MINING, Colors.BLACK}, {HarvestingNodeType.RICHMINING, Colors.GOLD},
			{HarvestingNodeType.TRAPPING, Colors.TAN}, {HarvestingNodeType.RICHTRAPPING, Colors.DARKORANGE}};
		private static Dictionary<HarvestingNodeType, EntityTraits[]> _traits = new Dictionary<HarvestingNodeType, EntityTraits[]>(){
			{HarvestingNodeType.FISHING, new EntityTraits[]{EntityTraits.ANIMAL, EntityTraits.ORGANIC}},
			{HarvestingNodeType.RICHFISHING, new EntityTraits[]{EntityTraits.ANIMAL, EntityTraits.ORGANIC}},
			{HarvestingNodeType.FORESTING, new EntityTraits[]{EntityTraits.PLANT, EntityTraits.WOOD, EntityTraits.ORGANIC}},
			{HarvestingNodeType.RICHFORESTING, new EntityTraits[]{EntityTraits.PLANT, EntityTraits.WOOD, EntityTraits.ORGANIC}},
			{HarvestingNodeType.GATHERING, new EntityTraits[]{EntityTraits.ORGANIC, EntityTraits.PLANT}},
			{HarvestingNodeType.RICHGATHERING, new EntityTraits[]{EntityTraits.ORGANIC, EntityTraits.PLANT}},
			{HarvestingNodeType.MINING, new EntityTraits[]{EntityTraits.INORGANIC}},
			{HarvestingNodeType.RICHMINING, new EntityTraits[]{EntityTraits.INORGANIC}},
			{HarvestingNodeType.TRAPPING, new EntityTraits[]{EntityTraits.ORGANIC, EntityTraits.ANIMAL}},
			{HarvestingNodeType.RICHTRAPPING, new EntityTraits[]{EntityTraits.ORGANIC, EntityTraits.ANIMAL}}};

		internal static char GetSymbol(HarvestingNodeType nodeType)
		{
			return _symbols[nodeType];
		}

		internal static Colors GetColor(HarvestingNodeType nodeType)
		{
			return _colors[nodeType];
		}

		internal static EntityTraits[] GetTraits(HarvestingNodeType nodeType)
		{
			return _traits[nodeType];
		}
	}

	public enum HarvestingNodeType
	{
		FISHING,
		RICHFISHING,
		FORESTING,
		RICHFORESTING,
		GATHERING,
		RICHGATHERING,
		MINING,
		RICHMINING,
		TRAPPING,
		RICHTRAPPING
	}

}
