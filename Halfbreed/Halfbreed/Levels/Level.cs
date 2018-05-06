using System;
using System.Collections.Generic;
using System.IO;
using Halfbreed.Levels;
using Halfbreed.Entities;

namespace Halfbreed
{
	public partial class Level
	{
		string _levelTitle;
		int _threatLevel;
		int _height;
		int _width;
		int _lightLevel;
		int _smokeLevel;

		MapTileDetails[] _mapGrid;
		bool[] _revealed;

		SortedDictionary<int, int> _triggers; // TODO: Swap to Triggers later
		SortedDictionary<int, int> _furnishings;
		SortedDictionary<int, int> _harvestingNodes;
		SortedDictionary<int, int> _npcs;
		SortedDictionary<int, int> _players;

		List<XYCoordinateStruct> _visibleTiles = new List<XYCoordinateStruct>();

		// Standard constructor
		public Level(LevelEnum level)
		{
			var levelStream = File.Open(GetFilePath(level), FileMode.Open);
			var LevelSpecificationFile = new StreamReader(levelStream);

			var tileDict = new Dictionary<int, TileType>();

			_levelTitle = LevelSpecificationFile.ReadLine();
			_threatLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_height = int.Parse(LevelSpecificationFile.ReadLine());
			_width = int.Parse(LevelSpecificationFile.ReadLine());
			_lightLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_smokeLevel = int.Parse(LevelSpecificationFile.ReadLine());

			_triggers = new SortedDictionary<int, int>();
			_furnishings = new SortedDictionary<int, int>();
			_harvestingNodes = new SortedDictionary<int, int>();
			_npcs = new SortedDictionary<int, int>();
			_players = new SortedDictionary<int, int>();

			LevelSpecificationFile.ReadLine(); // Move to start of dictionary.

			while (true)
			{
				var line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				var splitLine = line.Split(',');
				tileDict[int.Parse(splitLine[0])] = (TileType)Enum.Parse(typeof(TileType), splitLine[1]);
			}

			_mapGrid = new MapTileDetails[_width * _height];
			_revealed = new bool[_width * _height];

			for (int y = 0; y < _height; y++)
			{
				var _row = LevelSpecificationFile.ReadLine().Trim().Split(',');
				for (int x = 0; x < _width; x++)
				{
					_mapGrid[y * _width + x] = TileDictionary.getTileDetails(tileDict[int.Parse(_row[x])]);
					_revealed[y * _width + x] = false;
				}
			}

			// Remove the next break line
			LevelSpecificationFile.ReadLine();
			// TODO: Throw an exception if it isn't "###"

			// Furnishings
			const int FURNISHINGPREFIXLENGTH = 3;
			while (true)
			{
				var line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				var splitLine = line.Split(',');
				var furnishingName = splitLine[0];
				var xLoc = int.Parse(splitLine[1]);
				var yLoc = int.Parse(splitLine[2]);
				var otherParams = GetOtherParams(FURNISHINGPREFIXLENGTH, splitLine);
				var newFurnishing = new Furnishing(furnishingName, xLoc, yLoc, otherParams);
				AddFurnishing(newFurnishing);
			}

			// Harvesting Nodes
			const int HARVESTINGNODEPREFIXLENGTH = 3;
			while (true)
			{
				var line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				var splitLine = line.Split(',');
				var harvestingName = splitLine[0];
				var xLoc = int.Parse(splitLine[1]);
				var yLoc = int.Parse(splitLine[2]);
				var otherParams = GetOtherParams(HARVESTINGNODEPREFIXLENGTH, splitLine);
				var newHarvestingNode = new HarvestingNode(harvestingName, xLoc, yLoc, otherParams);

				AddHarvestingNode(newHarvestingNode);
			}

			levelStream.Close();
		}

		// Constructor for save games.
		public Level(LevelSerialisationDetails details)
		{
			_levelTitle = details.LevelTitle;
			_threatLevel = details.ThreatLevel;
			_height = details.Height;
			_width = details.Width;
			_lightLevel = details.LightLevel;
			_smokeLevel = details.SmokeLevel;

			var arraySize = _height * _width;
			_mapGrid = new MapTileDetails[arraySize];
			_revealed = new bool[arraySize];
			_triggers = details.Triggers;
			_furnishings = details.Furnishings;
			_harvestingNodes = details.HarvestingNodes;
			_npcs = details.Npcs;
			_players = new SortedDictionary<int, int>();

			for (int i = 0; i < arraySize; i++)
			{
				_mapGrid[i] = TileDictionary.getTileDetails(details.MapGrid[i]);
				_revealed[i] = details.Revealed[i];
			}
		}

		bool LineIsBreakPoint(string line)
		{
			return line.Substring(0, 3) == "###";
		}

		List<string> GetOtherParams(int prefixLength, string[] splitLine)
		{
			var returnList = new List<string>();
			for (int i = 0; i < splitLine.Length - prefixLength; i++)
				returnList.Add(splitLine[i + prefixLength]);
			return returnList;
		}

		// Private helper functions
		int ConvertXYToInt(int x, int y)
		{
			return y * _width + x;
		}

		bool HasTrait(int index, string trait)
		{
			return _mapGrid[index].HasTrait(trait) ||
				                  (_npcs.ContainsKey(index) && GetNpc(index).HasTrait(trait)) ||
				                  (_furnishings.ContainsKey(index) && GetFurnishing(index).HasTrait(trait)) ||
				                  (_harvestingNodes.ContainsKey(index) && GetHarvestingNode(index).HasTrait(trait));
		}

		int GetElevation(int index)
		{
			var elevation = _mapGrid[index].Elevation;
			if (_furnishings.ContainsKey(index))
				return Math.Max(elevation, GetFurnishing(index).Elevation);
			return elevation;
		}

		// Basic properties
		public string Title
		{
			get { return _levelTitle; }
		}

		public int Width
		{
			get { return _width; }
		}

		public int Height
		{
			get { return _height; }
		}

		public bool IsValidMapCoord(int x, int y)
		{
			return x >= 0 && x < Width && y >= 0 && y < Height;
		}

		public int Elevation(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetElevation(index);
		}

		public bool IsRevealed(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _revealed[index];
		}

		public void RevealTile(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			_revealed[index] = true;
		}

		public List<XYCoordinateStruct> VisibleTiles
		{
			get { return _visibleTiles; }
			set { _visibleTiles = value; }
		}

		public int Distance(int x1, int y1, int x2, int y2)
		{
			return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2) +
								  Math.Pow(Elevation(x1, y1) - Elevation(x2, y2), 2));
		}

		public int Distance(Entity entity1, Entity entity2)
		{
			return Distance(entity1.XLoc, entity1.YLoc, entity2.XLoc, entity2.YLoc);
		}

		// Furnishings
		public bool HasFurnishing(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _furnishings.ContainsKey(index);
		}

		public Furnishing GetFurnishing(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetFurnishing(index);
		}

		Furnishing GetFurnishing(int index)
		{
			return Furnishing.GetFurnishing(_furnishings[index]);
		}

		public void AddFurnishing(Furnishing furnishing)
		{
			var index = ConvertXYToInt(furnishing.XLoc, furnishing.YLoc);
			_furnishings[index] = furnishing.FurnishingId;
		}

		public void RemoveFurnishing(Furnishing furnishing)
		{
			var index = ConvertXYToInt(furnishing.XLoc, furnishing.YLoc);
			_furnishings.Remove(index);
		}

		// HarvestingNodes
		public bool HasHarvestingNode(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _harvestingNodes.ContainsKey(index);
		}

		public HarvestingNode GetHarvestingNode(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetHarvestingNode(index);
		}

		HarvestingNode GetHarvestingNode(int index)
		{
			return HarvestingNode.GetHarvestingNode(_harvestingNodes[index]);
		}

		public void AddHarvestingNode(HarvestingNode harvestingNode)
		{
			var index = ConvertXYToInt(harvestingNode.XLoc, harvestingNode.YLoc);
			_harvestingNodes[index] = harvestingNode.HarvestingId;
		}

		public void RemoveHarvestingNode(HarvestingNode harvestingNode)
		{
			var index = ConvertXYToInt(harvestingNode.XLoc, harvestingNode.YLoc);
			_harvestingNodes.Remove(index);
		}

		// Actors
		public bool HasNpc(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _npcs.ContainsKey(index);
		}

		public Actor GetNpc(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetNpc(index);
		}

		Actor GetNpc(int index)
		{
			if (_npcs.ContainsKey(index))
				return NPC.GetNpc(_npcs[index]);
			return null;
		}

		public void AddNpc(NPC npc)
		{
			var index = ConvertXYToInt(npc.XLoc, npc.YLoc);
			_npcs[index] = npc.NpcId;
		}

		public void RemoveNpc(NPC npc)
		{
			var index = ConvertXYToInt(npc.XLoc, npc.YLoc);
			_npcs.Remove(index);
		}

		// Player stuff
		public bool HasPlayer(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _players.ContainsKey(index);
		}

		public Player GetPlayer(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return GetPlayer(index);
		}

		public Player GetPlayer(int index)
		{
			return Player.GetPlayer(_players[index]);
		}

		public void AddPlayer(Player player)
		{
			var index = ConvertXYToInt(player.XLoc, player.YLoc);
			_players[index] = player.PlayerId;
		}

		public void RemovePlayer(Player player)
		{
			var index = ConvertXYToInt(player.XLoc, player.YLoc);
			_players.Remove(index);
		}

		// Movement and passibility functions
		public bool isPassible(int x, int y, bool walk, bool fly, bool swim)
		{
			var index = ConvertXYToInt(x, y);
			if (BlockAllMovement(index))
				return false;
			return (walk && !HasTrait(index, "BlockWalk")) ||
					(fly && !HasTrait(index, "BlockFly")) ||
					(swim && !HasTrait(index, "BlockSwim"));
		}

		public bool isPassible(int x, int y, Actor actor)
		{
			return isPassible(x, y, actor.HasTrait("Walking"), actor.HasTrait("Flying"),
			                  actor.HasTrait("Swimming"));
		}

		public bool BlockAllMovement(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return BlockAllMovement(index);
		}

		bool BlockAllMovement(int index)
		{
			return HasTrait(index, "Impassible");
		}

		void MoveActor(int newX, int newY, Actor actor)
		{
			if (actor.HasTrait("Player"))
				MovePlayer(newX, newY, (Player)actor);
			else
				MoveNpc(newX, newY, (NPC)actor);
		}

		void MoveNpc(int newX, int newY, NPC npc)
		{
			RemoveNpc(npc);
			npc.UpdatePosition(newX, newY);
			AddNpc(npc);
		}

		void MovePlayer(int newX, int newY, Player player)
		{
			RemovePlayer(player);
			player.UpdatePosition(newX, newY);
			AddPlayer(player);
		}

		public bool MoveActorAttempt(int destinationX, int destinationY, Actor actor)
		{
			var originIndex = ConvertXYToInt(actor.XLoc, actor.YLoc);
			var destinationIndex = ConvertXYToInt(destinationX, destinationY);
			var originX = actor.XLoc;
			var originY = actor.YLoc;

			if (actor.HasTrait("Flying") && !HasTrait(destinationIndex, "BlockFly"))
			{
				if (ApplyMoveOffFunctions(originIndex, actor, destinationX, destinationY))
				{
					MoveActor(destinationX, destinationY, actor);
					ApplyMoveOnFunctions(destinationIndex, actor, originX, originY);
					return true;
				}
				return false;
			}
			if (actor.HasTrait("Walking") && !HasTrait(destinationIndex, "BlockWalk"))
			{
				var elevationDifference = GetElevation(destinationIndex) - GetElevation(originIndex);

				if (Math.Abs(elevationDifference) <= 1 || HasTrait(originIndex, "ElevationChange") || HasTrait(destinationIndex, "ElevationChange"))
				{
					if (ApplyMoveOffFunctions(originIndex, actor, destinationX, destinationY))
					{
	                    MoveActor(destinationX, destinationY, actor);
						ApplyMoveOnFunctions(destinationIndex, actor, originX, originY);
						return true;
					}
					return false;
				}

				if (elevationDifference > 1)
					MainGraphicDisplay.TextConsole.AddOutputText("It's too high to climb up there");
				else
					MainGraphicDisplay.TextConsole.AddOutputText("Dropping down there would be too dangerous");
				return false;
			}

			if (actor.HasTrait("Swimming") && !HasTrait(destinationIndex, "BlockSwim"))
			{
				if (ApplyMoveOffFunctions(originIndex, actor, destinationX, destinationY))
				{
                    MoveActor(destinationX, destinationY, actor);
					ApplyMoveOnFunctions(destinationIndex, actor, originX, originY);
					return true;
				}
				return false;
			}

			return false;
		}

		bool ApplyMoveOffFunctions(int index, Actor actor, int destinationX, int destinationY)
		{
			// TODO: Call Tile functions.

			if (_furnishings.ContainsKey(index))
				return GetFurnishing(index).MoveOff(actor, this, destinationX, destinationY);
			return true;
		}

		void ApplyMoveOnFunctions(int index, Actor actor, int originX, int originY)
		{
			// TODO: Call Tile functions.

			if (_furnishings.ContainsKey(index))
				GetFurnishing(index).MoveOn(actor, this, originX, originY);
		}

		// LOS and vision functions
		bool BlockLOS(int index)
		{
			return HasTrait(index, "BlockLOS");
		}

		bool BlockLOS(int x, int y, int elevation)
		{
			var index = ConvertXYToInt(x, y);
			return elevation < GetElevation(index) || BlockLOS(index);
		}

		public List<XYCoordinateStruct> GetFOV(int x, int y, int elevation, int viewDistance, int lightRadius,
											   bool Darkvision, bool Blindsight)
		{
			if (!Blindsight)
				viewDistance += _smokeLevel;
			if (!Darkvision)
			{
				viewDistance += _lightLevel;
				viewDistance = Math.Max(viewDistance, lightRadius);
			}

			var viewSet = new HashSet<int> { ConvertXYToInt(x, y) };
			for (int octant = 0; octant < 8; octant++)
			{
				CastLight(x, y, 1, 1.0d, 0.0d, viewDistance, _octantTranslate[0, octant], _octantTranslate[1, octant],
						  _octantTranslate[2, octant], _octantTranslate[3, octant], 0, elevation, viewSet);
			}

			var returnList = new List<XYCoordinateStruct>();
			foreach (int index in viewSet)
				returnList.Add(new XYCoordinateStruct(index % _width, index / _width));

			return returnList;
		}

		void CastLight(int xLoc, int yLoc, int row, double start, double end, int viewDistance, int xx, int xy, int yx,
					   int yy, int recursionNumber, int elevation, HashSet<int> viewSet)
		{
			if (start < end)
				return;

			var newStart = -1.0d;
			var viewDistanceSquared = viewDistance * viewDistance;

			for (int j = row; j < viewDistance + 1; j++)
			{
				var dx = -j - 1;
				var dy = -j;
				var blocked = false;

				while (dx <= 0)
				{
					dx += 1;

					var mapX = xLoc + dx * xx + dy * xy;
					var mapY = yLoc + dx * yx + dy * yy;

					var lSlope = (dx - 0.5) / (dy + 0.5);
					var rSlope = (dx + 0.5) / (dy - 0.5);

					if (start <= rSlope)
						continue;
					if (end >= lSlope)
						break;
					
					// We can see this square
					if (dy * dx + dy * dy < viewDistanceSquared && IsValidMapCoord(mapX, mapY))
						viewSet.Add(ConvertXYToInt(mapX, mapY));
					if (blocked)
					{
						// We are scanning blocked squares
						if (!IsValidMapCoord(mapX, mapY) || BlockLOS(mapX, mapY, elevation))
						{
							newStart = rSlope;
							continue;
						}
						blocked = false;
						start = newStart;
					}
					else if (!IsValidMapCoord(mapX, mapY) || BlockLOS(mapX, mapY, elevation))
					{
						// Start a child scan
						blocked = true;
						CastLight(xLoc, yLoc, j + 1, start, lSlope, viewDistance, xx, xy, yx, yy, recursionNumber + 1, elevation, viewSet);
						newStart = rSlope;
					}
				}
				// Row is scanned  do next row unless last square was blocked.
				if (blocked)
				{
					break;
				}
			}
		}

		public List<Entity> GetConcealedEntities(List<XYCoordinateStruct> visibleTiles)
		{
			var returnList = new List<Entity>();
			foreach (var tile in visibleTiles)
			{
				var index = ConvertXYToInt(tile.X, tile.Y);
				if (_furnishings.ContainsKey(index) && GetFurnishing(index).Concealed)
					returnList.Add(GetFurnishing(index));
				if (_harvestingNodes.ContainsKey(index) && GetHarvestingNode(index).Concealed)
					returnList.Add(GetHarvestingNode(index));
				if (_npcs.ContainsKey(index) && GetNpc(index).Concealed)
					returnList.Add(GetNpc(index));
				// TODO: Add Player here.
			}
			return returnList;
		}

		// Graphical functions
		public bool HasDrawingEntity(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _npcs.ContainsKey(index) || _harvestingNodes.ContainsKey(index) || 
				        _furnishings.ContainsKey(index) || _players.ContainsKey(index);
		}

		public Entity GetDrawingEntity(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			if (_players.ContainsKey(index))
				return GetPlayer(index);
			if (_npcs.ContainsKey(index))
				return GetNpc(index);
			if (_harvestingNodes.ContainsKey(index))
				return GetHarvestingNode(index);
			return GetFurnishing(index);
		}

		public string GetBackgroundColor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			if (_furnishings.ContainsKey(index) && GetFurnishing(index).HasBGColor)
				return GetFurnishing(index).BGColorName;
			return _mapGrid[index].BGColorName;
		}

		public string GetFogColor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			if (_furnishings.ContainsKey(index) && GetFurnishing(index).HasFogColor)
				return GetFurnishing(index).FogColorName;
			return _mapGrid[index].FogColorName;
		}

		// Turn management functions
		public void ActivateEntities()
		{
			foreach (var index in new List<int>(_furnishings.Keys))
				GetFurnishing(index).Update(this);
			foreach (var index in new List<int>(_harvestingNodes.Keys))
				GetHarvestingNode(index).Update(this);
			foreach (var index in new List<int>(_npcs.Keys))
				GetNpc(index).Update(this);
		}

		// Serialisation functions
		public LevelSerialisationDetails GetSerialisationDetails()
		{
			var details = new LevelSerialisationDetails();
			details.LevelTitle = _levelTitle;
			details.ThreatLevel = _threatLevel;
			details.Height = _height;
			details.Width = _width;
			details.LightLevel = _lightLevel;
			details.SmokeLevel = _smokeLevel;

			var arraySize = _height * _width;
			details.MapGrid = new TileType[arraySize];
			details.Revealed = new bool[arraySize];
			details.Triggers = _triggers;
			details.Furnishings = _furnishings;
			details.HarvestingNodes = _harvestingNodes;
			details.Npcs = _npcs;

			int playerIndex = ConvertXYToInt(MainProgram.Player.XLoc, MainProgram.Player.YLoc);
			details.Npcs.Remove(playerIndex);

			for (int i = 0; i < arraySize; i++)
			{
				details.MapGrid[i] = _mapGrid[i].TileType;
				details.Revealed[i] = _revealed[i];
			}

			return details;
		}
	}

	[Serializable]
	public class LevelSerialisationDetails
	{
		public string LevelTitle;
		public int ThreatLevel;
		public int Height;
		public int Width;
		public int LightLevel;
		public int SmokeLevel;

		public TileType[] MapGrid;
		public bool[] Revealed;

		public SortedDictionary<int, int> Triggers; // TODO: Swap to Triggers later
		public SortedDictionary<int, int> Furnishings;
		public SortedDictionary<int, int> HarvestingNodes;
		public SortedDictionary<int, int> Npcs;
	}
}
