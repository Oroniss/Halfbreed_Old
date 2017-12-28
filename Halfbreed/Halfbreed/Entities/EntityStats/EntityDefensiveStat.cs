using System;
namespace Halfbreed
{
	public class EntityDefensiveStat
	{
		private int _baseRating;
		private int _ratingModifiers;
		private int _currentChance;
		private int _maxModifiers;

		public EntityDefensiveStat(int baseRating)
		{
			_baseRating = baseRating;
			_ratingModifiers = 0;
			_maxModifiers = 0;
			recalculateChance();
		}

		public int Rating
		{
			get { return _baseRating + _ratingModifiers; }
		}

		public int Chance
		{
			get { return _currentChance; }
		}

		public void addRatingModifier(int modifier)
		{
			_ratingModifiers += modifier;
			recalculateChance();
		}

		public void addMaxModifier(int modifier)
		{
			_maxModifiers += modifier;
			recalculateChance();
		}

		public void reset()
		{
			_ratingModifiers = 0;
			_maxModifiers = 0;
			recalculateChance();
		}

		// Note: No minimum on chance - negative values are possible.
		private void recalculateChance()
		{
			int chance = (int)Math.Floor( Rating / (9 + Math.Pow(GameEngine.CurrentAct, 2))) - 50 * (GameEngine.CurrentAct - 1);
			var max = Math.Min(90, 75 + _maxModifiers);
			_currentChance = Math.Min(max, chance);
		}
	}
}
