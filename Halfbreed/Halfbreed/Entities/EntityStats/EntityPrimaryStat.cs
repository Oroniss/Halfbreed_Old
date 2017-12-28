using System;

namespace Halfbreed.Entities
{
	public class EntityPrimaryStat
	{
		private int _baseRating;
		private int _ratingModifiers;

		public EntityPrimaryStat(int baseRating)
		{
			_baseRating = baseRating;
			_ratingModifiers = 0;
		}

		public int Rating
		{
			get { return _baseRating + _ratingModifiers; }
		}

		public void addModifier(int modifier)
		{
			_ratingModifiers += modifier;
		}

		public void reset()
		{
			_ratingModifiers = 0;
		}

		public float Multiplier
		{
			get {
				float chance = (float)((Math.Log(Rating / 100 + 0.8, 2) * 0.4) -
				                       ((GameEngine.CurrentChapter - 1) * 0.08) + 1);
				return (float)Math.Round(Math.Min(1 + GameEngine.CurrentAct * 0.4, Math.Max(chance, 1)), 3); }
		}
	}
}
