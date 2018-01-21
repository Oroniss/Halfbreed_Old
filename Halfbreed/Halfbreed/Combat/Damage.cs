namespace Halfbreed.Combat
{
	public class Damage
	{
		private DamageType _damageType;
		private int _originalDamageAmount;
		private int _damageModifiers;
		private bool _negateDamage;

		public Damage(DamageType damageType, int originalDamageAmount)
		{
			_damageType = damageType;
			_originalDamageAmount = originalDamageAmount;
			_damageModifiers = 0;
			_negateDamage = false;
		}

		public void ModifyDamage(int amount)
		{
			_damageModifiers += amount;
		}

		public void NegateDamage()
		{
			_negateDamage = true;
		}

		public DamageType DamageType
		{
			get { return _damageType; }
		}

		public int OriginalDamageAmount
		{
			get { return _originalDamageAmount; }
		}

		public int FinalDamageAmount
		{
			get
			{
				if (_negateDamage)
					return 0;
				return System.Math.Max(0, _originalDamageAmount + _damageModifiers);
			}
		}
	}
}
