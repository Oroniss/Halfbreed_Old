namespace Halfbreed.Entities
{
	public struct EntityPrimaryStatTemplate
	{
		private int _might;
		private int _agility;
		private int _mind;
		private int _willpower;
		private int _presence;

		public EntityPrimaryStatTemplate(int might, int agility, int mind, int willpower, int presence)
		{
			_might = might;
			_agility = agility;
			_mind = mind;
			_willpower = willpower;
			_presence = presence;
		}

		public int Might
		{
			get { return _might; }
		}

		public int Agility
		{
			get { return _agility; }
		}

		public int Mind
		{
			get { return _mind; }
		}

		public int Willpower
		{
			get { return _willpower; }
		}

		public int Presence
		{
			get { return _presence; }
		}
	}
}
