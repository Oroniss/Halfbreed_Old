// Revised for version 0.02.

using Halfbreed.Combat;
using NUnit.Framework;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class CombatTests
	{
		[Test]
		public void TestDamage()
		{
			var fireDamage = new Damage(DamageType.Fire, 50);

			// Test basic properties
			Assert.AreEqual(DamageType.Fire, fireDamage.DamageType);
			Assert.AreEqual(50, fireDamage.FinalDamageAmount);
			Assert.AreEqual(50, fireDamage.OriginalDamageAmount);

			// Check negative damage modifiers
			fireDamage.ModifyDamage(-20);
			Assert.AreEqual(30, fireDamage.FinalDamageAmount);

			// Check damage minimum is 0
			fireDamage.ModifyDamage(-40);
			Assert.AreEqual(0, fireDamage.FinalDamageAmount);

			// Test a second damage type
			var acidDamage = new Damage(DamageType.Acid, 30);
			Assert.AreEqual(DamageType.Acid, acidDamage.DamageType);

			// Test positive modifiers
			acidDamage.ModifyDamage(50);
			Assert.AreEqual(80, acidDamage.FinalDamageAmount);

			// Test negation
			acidDamage.NegateDamage();
			Assert.AreEqual(0, acidDamage.FinalDamageAmount);

			// Test negation is unaffected by later modifiers
			acidDamage.ModifyDamage(50);
			Assert.AreEqual(0, acidDamage.FinalDamageAmount);

			// Test the original damage amount hasn't changed.
			Assert.AreEqual(30, acidDamage.OriginalDamageAmount);
		}
	}
}
