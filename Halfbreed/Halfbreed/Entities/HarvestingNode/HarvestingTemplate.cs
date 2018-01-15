namespace Halfbreed.Entities
{
	public struct HarvestingTemplate
	{
		private string _nodeName;
		private HarvestingNodeType _nodeType;
		private object _lootLists; // TODO: Figure out the best way to store and parse these.

		public HarvestingTemplate(string nodeName, HarvestingNodeType nodeType, object lootLists)
		{
			_nodeName = nodeName;
			_nodeType = nodeType;
			_lootLists = lootLists;
		}

		public string NodeName
		{
			get { return _nodeName; }
		}

		public HarvestingNodeType NodeType
		{
			get { return _nodeType; }
		}

		public object LootLists
		{
			get { return _lootLists; }
		}
	}
}
