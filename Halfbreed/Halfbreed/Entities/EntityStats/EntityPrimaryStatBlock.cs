using System;

namespace Halfbreed.Entities
{
	public class EntityStatBlock
	{
		public EntityStatBlock()
		{

		}

		public int MightRating
		{
			get { return 0; }
		}

		public float MightMultiplier
		{
			get { return 1.0f; }
		}

		public int AgilityRating
		{
			get { return 0; }
		}

		public float AgilityMultiplier
		{
			get { return 1.0f; }
		}

		public int MindRating
		{
			get { return 0; }
		}

		public float MindMultiplier
		{
			get { return 1.0f; }
		}

		public int WillpowerRating
		{
			get { return 0; }
		}

		public float WillpowerMultiplier
		{
			get { return 1.0f; }
		}

		public int PresenceRating
		{
			get { return 0; }
		}

		public float PresenceMultiplier
		{
			get { return 1.0f; }
		}

		public int GetPrimaryStatRating(EntityPrimaryStat stat)
		{
			switch (stat)
			{
				case EntityPrimaryStat.MIGHT:
					return MightRating;
				case EntityPrimaryStat.AGILITY:
					return AgilityRating;
				case EntityPrimaryStat.MIND:
					return MindRating;
				case EntityPrimaryStat.WILLPOWER:
					return WillpowerRating;
				case EntityPrimaryStat.PRESENCE:
					return PresenceRating;
			}
			ErrorLogger.AddDebugText("Invalid stat type: " + stat.ToString());
			return 0;
		}

		public float GetPrimaryStatMultiplier(EntityPrimaryStat stat)
		{
			switch (stat)
			{
				case EntityPrimaryStat.MIGHT:
					return MightMultiplier;
				case EntityPrimaryStat.AGILITY:
					return AgilityMultiplier;
				case EntityPrimaryStat.MIND:
					return MindMultiplier;
				case EntityPrimaryStat.WILLPOWER:
					return WillpowerMultiplier;
				case EntityPrimaryStat.PRESENCE:
					return PresenceRating;
			}
			ErrorLogger.AddDebugText("Invalid stat type: " + stat.ToString());
			return 1.0f;
		}

	}
}
