using System;
using System.IO;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Halfbreed.Entities;
using Halfbreed.Levels;

namespace Halfbreed
{
	public static class StaticDatabaseConnection
	{
		private static string _DatabaseLocation = Directory.GetCurrentDirectory() + "/HalfbreedComponents.db";
		private static SqliteConnection _connection;
		private static bool _connectionOpen = false;
		private static bool _testingMode = false;


		public static void openDBConnection()
		{
			if (_connectionOpen)
			{
				ErrorLogger.AddDebugText("Tried to re-open already open DB connection");
				return;
			}
			_connection = new SqliteConnection("Data Source=" + _DatabaseLocation);
			_connection.Open();
			_connectionOpen = true;
		}

		public static void closeDBConnection()
		{
			if (_connectionOpen)
			{
				_connection.Close();
				_connectionOpen = false;
				return;
			}
			ErrorLogger.AddDebugText("Tried to close already closed DB connection");
		}

		private static EntityTraits[] GetTraits(string traitAttribute)
		{
			string[] traitNames = traitAttribute.Trim().Split(',');
			EntityTraits[] traits = new EntityTraits[traitNames.Length];
			for (int i = 0; i < traitNames.Length; i++)
				traits[i] = EnumConverter.ConvertStringToTrait(traitNames[i]);
			return traits;
		}

		public static Dictionary<Materials, MaterialProperties> GetMaterialProperties()
		{
			Dictionary<Materials, MaterialProperties> materialDict = new Dictionary<Materials, MaterialProperties>();

			string queryText = "SELECT * FROM MaterialProperties;";

			using (var queryCommand = _connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;

				var reader = queryCommand.ExecuteReader();

				while (reader.Read())
				{
					Materials material = EnumConverter.ConvertStringToMaterial(reader.GetString(0));
					int acid = reader.GetInt32(1);
					int cold = reader.GetInt32(2);
					int elec = reader.GetInt32(3);
					int fire = reader.GetInt32(4);
					int poison = reader.GetInt32(5);
					int disease = reader.GetInt32(6);
					int light = reader.GetInt32(7);
					int shadow = reader.GetInt32(8);
					int mental = reader.GetInt32(9);
					int physical = reader.GetInt32(10);
					int nether = reader.GetInt32(11);

					int hppervol = reader.GetInt32(12);
					int wgtpervol = reader.GetInt32(13);
					int hardness = reader.GetInt32(14);

					Colors fgcolor = EnumConverter.ConvertStringToColor(reader.GetString(15));

					EntityTraits[] traits = GetTraits(reader.GetString(16));
					string adjective = reader.GetString(17);

					materialDict.Add(material, new MaterialProperties(acid, cold, elec, fire, poison, disease, light,
																	  shadow, mental, physical, nether, hppervol,
					                                                  wgtpervol, hardness, fgcolor, traits, adjective));
				}
			}

			return materialDict;
		}

		public static Dictionary<TileType, MapTileDetails> GetMapTiles()
		{
			Dictionary<TileType, MapTileDetails> tileDict = new Dictionary<TileType, MapTileDetails>();

			string queryText = "SELECT * FROM MapTiles;";

			using (var queryCommand = _connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;

				var reader = queryCommand.ExecuteReader();

				while (reader.Read())
				{
					string tileName = reader.GetString(0);
					TileType tileType = EnumConverter.ConvertStringToTileType(tileName);

					int elevation = reader.GetInt32(1);
					MovementModes movementMode = (MovementModes)reader.GetInt32(2);
					bool allowLOS = (reader.GetInt32(3) == 1);
					string bgColor = reader.GetString(4);
					string fogColor = reader.GetString(5);

					tileDict.Add(tileType, new MapTileDetails(tileName, elevation, movementMode, allowLOS,
					                                                       bgColor, fogColor));
				}
			}

			return tileDict;

		}

		public static EntityPrimaryStatTemplate GetPrimaryStats(string EntityName)
		{
			return new EntityPrimaryStatTemplate(0, 0, 0, 0, 0);
		}

		public static EntityDefensiveStatTemplate GetDefensiveStats(string EntityName)
		{
			return new EntityDefensiveStatTemplate(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		public static FurnishingTemplate GetFurnishingDetails(string FurnishingName)
		{
			string queryText = string.Format("SELECT * FROM Furnishings WHERE FurnishingName = \"{0}\";", FurnishingName);

			using (var queryCommand = _connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;

				var reader = queryCommand.ExecuteReader();
				reader.Read();

				string furnishingName = reader.GetString(0);
				char symbol = reader.GetString(1)[0];
				int volume = reader.GetInt32(2);
				EntityTraits[] traits = GetTraits(reader.GetString(3));
				bool hasTile = (reader.GetInt32(4) == 1);
				string tileName = "";
				if (hasTile)
					tileName = reader.GetString(5);

				FurnishingTemplate template = new FurnishingTemplate(furnishingName, symbol, volume, traits, hasTile, tileName);

				return template;
			}
		}

		public static HarvestingTemplate GetHarvestingDetails(string HarvestingNodeName)
		{
			string queryText = string.Format("SELECT * FROM HarvestingNodes WHERE NodeName = \"{0}\";", HarvestingNodeName);

			using (var queryCommand = _connection.CreateCommand())
			{
				queryCommand.CommandText = queryText;

				var reader = queryCommand.ExecuteReader();
				reader.Read();

				string nodeName = reader.GetString(0);
				HarvestingNodeType nodeType = (HarvestingNodeType)Enum.Parse(typeof(HarvestingNodeType), reader.GetString(1));
				object lootLists = null; // TODO: Fix this.

				HarvestingTemplate template = new HarvestingTemplate(nodeName, nodeType, lootLists);
				return template;
			}
		}

		// Testing functionality
		public static void SetupTestContext(string TestContext)
		{
			if (_testingMode)
			{
				ErrorLogger.AddDebugText("Component Database Connection already in Testing Mode");
				return;
			}
			_DatabaseLocation = TestContext + "/HalfbreedComponents.db";
			_testingMode = true;
		}

	}
}
