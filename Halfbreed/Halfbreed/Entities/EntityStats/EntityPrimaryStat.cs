using System;

namespace Halfbreed.Entities
{
	public class EntityPrimaryStat
	{
		private int _baseRating;
		private int _ratingModifiers;
		private float _currentMultiplier;

		public EntityPrimaryStat(int baseRating)
		{
			_baseRating = baseRating;
			_ratingModifiers = 0;
			recalculateMultiplier();
		}

		public int Rating
		{
			get { return _baseRating + _ratingModifiers; }
		}

		public float Multiplier
		{
			get { return _currentMultiplier; }
		}

		public void addModifier(int modifier)
		{
			_ratingModifiers += modifier;
			recalculateMultiplier();
		}

		public void reset()
		{
			_ratingModifiers = 0;
			recalculateMultiplier();
		}

		private void recalculateMultiplier()
		{
				float chance = (float)((Math.Log(Rating / 100 + 0.8, 2) * 0.4) -
					   ((GameEngine.CurrentChapter - 1) * 0.08) + 1);
				_currentMultiplier = (float)Math.Round(Math.Min(1 + GameEngine.CurrentAct* 0.4, Math.Max(chance, 1)), 3);
		}
	}
}
