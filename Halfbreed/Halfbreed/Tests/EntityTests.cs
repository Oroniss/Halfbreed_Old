using System;
using Halfbreed.Entities;

using NUnit.Framework;

namespace Halfbreed
{
	[TestFixture]
	public class EntityTests
	{

		[SetUp]
		public void OpenTestDatabases()
		{
			EntityDatabaseConnection.SetupTestContext(TestContext.CurrentContext.TestDirectory);
			EntityDatabaseConnection.openDBConnection();
			StaticData.SetupDictionaries();
		}

		[Test]
		public void TestMaterialDictionary()
		{
			Assert.AreEqual(Enum.GetValues(typeof(Materials)).Length, StaticData.GetNumberOfMaterials());

			Assert.AreEqual(200, StaticData.GetProperties(Materials.COPPER).DefensiveStatTemplate.AcidResistance);
			Assert.AreEqual(400, StaticData.GetProperties(Materials.HESSION).DefensiveStatTemplate.LightResistance);
			Assert.AreEqual(10, StaticData.GetProperties(Materials.TIN).Hardness);
			Assert.AreEqual(Colors.TAN, StaticData.GetProperties(Materials.HESSION).FGColor);
		}

		[TearDown]
		public void CloseTestDatabases()
		{
			EntityDatabaseConnection.closeDBConnection();
		}

	}
}
