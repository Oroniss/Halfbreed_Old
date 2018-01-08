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
			Entities.EntityData.SetupDictionaries();
		}

		[Test]
		public void TestMaterialDictionary()
		{
			Assert.AreEqual(Enum.GetValues(typeof(Materials)).Length, EntityData.GetNumberOfMaterials());

			Assert.AreEqual(200, EntityData.GetProperties(Materials.COPPER).DefensiveStatTemplate.AcidResistance);
			Assert.AreEqual(400, EntityData.GetProperties(Materials.HESSION).DefensiveStatTemplate.LightResistance);
			Assert.AreEqual(10, EntityData.GetProperties(Materials.TIN).Hardness);
			Assert.AreEqual(Colors.TAN, EntityData.GetProperties(Materials.HESSION).FGColor);
		}

		[TearDown]
		public void CloseTestDatabases()
		{
			EntityDatabaseConnection.closeDBConnection();
		}

	}
}
