using System;

namespace Halfbreed.Entities
{
	public class EntityPrimaryStatBlock
	{
		private EntityPrimaryStat _might;
		private EntityPrimaryStat _agility;
		private EntityPrimaryStat _mind;
		private EntityPrimaryStat _willpower;
		private EntityPrimaryStat _presence;

		public EntityPrimaryStatBlock(EntityPrimaryStatTemplate template)
		{
			_might = new EntityPrimaryStat(template.Might);
			_agility = new EntityPrimaryStat(template.Agility);
			_mind = new EntityPrimaryStat(template.Mind);
			_willpower = new EntityPrimaryStat(template.Willpower);
			_presence = new EntityPrimaryStat(template.Presence);
		}

		// TODO: Add another constructor for item/furnishing template.
		// TODO: Add another constructor for actors based on actor type and level.

		public int MightRating
		{
			get { return _might.Rating; }
		}

		public float MightMultiplier
		{
			get { return _might.Multiplier; }
		}

		public int AgilityRating
		{
			get { return _agility.Rating; }
		}

		public float AgilityMultiplier
		{
			get { return _agility.Multiplier; }
		}

		public int MindRating
		{
			get { return _mind.Rating; }
		}

		public float MindMultiplier
		{
			get { return _mind.Multiplier; }
		}

		public int WillpowerRating
		{
			get { return _willpower.Rating; }
		}

		public float WillpowerMultiplier
		{
			get { return _willpower.Multiplier; }
		}

		public int PresenceRating
		{
			get { return _presence.Rating; }
		}

		public float PresenceMultiplier
		{
			get { return _presence.Multiplier; }
		}

		public int GetPrimaryStatRating(EntityPrimaryStatTypes stat)
		{
			switch (stat)
			{
				case EntityPrimaryStatTypes.MIGHT:
					return MightRating;
				case EntityPrimaryStatTypes.AGILITY:
					return AgilityRating;
				case EntityPrimaryStatTypes.MIND:
					return MindRating;
				case EntityPrimaryStatTypes.WILLPOWER:
					return WillpowerRating;
				case EntityPrimaryStatTypes.PRESENCE:
					return PresenceRating;
			}
			return 0;
		}

		public float GetPrimaryStatMultiplier(EntityPrimaryStatTypes stat)
		{
			switch (stat)
			{
				case EntityPrimaryStatTypes.MIGHT:
					return MightMultiplier;
				case EntityPrimaryStatTypes.AGILITY:
					return AgilityMultiplier;
				case EntityPrimaryStatTypes.MIND:
					return MindMultiplier;
				case EntityPrimaryStatTypes.WILLPOWER:
					return WillpowerMultiplier;
				case EntityPrimaryStatTypes.PRESENCE:
					return PresenceMultiplier;
			}
			return 1.0f;
		}

		// TODO: Add an "add modifier" function.
		// TOOD: Add a "recalculate all" function.

	}
}
