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

		SortedDictionary<int, object> _triggers; // TODO: Swap to Triggers later
		SortedDictionary<int, Furnishing> _furnishings;
		SortedDictionary<int, HarvestingNode> _harvestingNodes;
		SortedDictionary<int, Actor> _actors;

		// Standard constructor
		public Level(LevelEnum level)
		{
			FileStream levelStream = File.Open(GetFilePath(level), FileMode.Open);
			StreamReader LevelSpecificationFile = new StreamReader(levelStream);

			Dictionary<int, TileType> tileDict = new Dictionary<int, TileType>();

			_levelTitle = LevelSpecificationFile.ReadLine();
			_threatLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_height = int.Parse(LevelSpecificationFile.ReadLine());
			_width = int.Parse(LevelSpecificationFile.ReadLine());

			_lightLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_smokeLevel = int.Parse(LevelSpecificationFile.ReadLine());

			_triggers = new SortedDictionary<int, object>();
			_furnishings = new SortedDictionary<int, Furnishing>();
			_harvestingNodes = new SortedDictionary<int, HarvestingNode>();
			_actors = new SortedDictionary<int, Actor>();


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
				string line = LevelSpecificationFile.ReadLine().Trim();
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
			_triggers = new SortedDictionary<int, object>();
			_furnishings = new SortedDictionary<int, Furnishing>();
			_harvestingNodes = new SortedDictionary<int, HarvestingNode>();
			_actors = new SortedDictionary<int, Actor>();

			for (int i = 0; i < arraySize; i++)
			{
				_mapGrid[i] = TileDictionary.getTileDetails(details.MapGrid[i]);
				_revealed[i] = details.Revealed[i];
				if (details.Triggers[i] != null)
					_triggers[i] = details.Triggers[i];
				if (details.Furnishings[i] != null)
					_furnishings[i] = details.Furnishings[i];
				if (details.HarvestingNodes[i] != null)
					_harvestingNodes[i] = details.HarvestingNodes[i];
				if (details.Actors[i] != null)
					_actors[i] = details.Actors[i];
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

		bool HasTrait(int index, Traits trait)
		{
			return _mapGrid[index].HasTrait(trait) ||
				(_actors.ContainsKey(index) && _actors[index].HasTrait(trait)) ||
				(_furnishings.ContainsKey(index) && _furnishings[index].HasTrait(trait)) ||
				(_harvestingNodes.ContainsKey(index) && _harvestingNodes[index].HasTrait(trait));
		}

		int GetElevation(int index)
		{
			var elevation = _mapGrid[index].Elevation;
			if (_furnishings.ContainsKey(index))
				return Math.Max(elevation, _furnishings[index].Elevation);
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
			return _furnishings[index];
		}

		public void AddFurnishing(Furnishing furnishing)
		{
			var index = ConvertXYToInt(furnishing.XLoc, furnishing.YLoc);
			_furnishings[index] = furnishing;
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
			return _harvestingNodes[index];
		}

		public void AddHarvestingNode(HarvestingNode harvestingNode)
		{
			var index = ConvertXYToInt(harvestingNode.XLoc, harvestingNode.YLoc);
			_harvestingNodes[index] = harvestingNode;
		}

		public void RemoveHarvestingNode(HarvestingNode harvestingNode)
		{
			var index = ConvertXYToInt(harvestingNode.XLoc, harvestingNode.YLoc);
			_harvestingNodes.Remove(index);
		}

		// Actors
		public bool HasActor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _actors.ContainsKey(index);
		}

		public Actor GetActor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _actors[index];
		}

		public void AddActor(Actor actor)
		{
			var index = ConvertXYToInt(actor.XLoc, actor.YLoc);
			_actors[index] = actor;
		}

		public void RemoveActor(Actor actor)
		{
			var index = ConvertXYToInt(actor.XLoc, actor.YLoc);
			_actors.Remove(index);
		}

		// Movement and passibility functions
		public bool isPassible(int x, int y, bool walk, bool fly, bool swim)
		{
			var index = ConvertXYToInt(x, y);
			if (BlockAllMovement(index))
				return false;
			return (walk && !HasTrait(index, Traits.BlockWalk)) ||
					(fly && !HasTrait(index, Traits.BlockFly)) ||
					(swim && !HasTrait(index, Traits.BlockSwim));
		}

		public bool isPassible(int x, int y, Actor actor)
		{
			return isPassible(x, y, actor.HasTrait(Traits.Walking), actor.HasTrait(Traits.Flying), actor.HasTrait(Traits.Swimming));
		}

		public bool BlockAllMovement(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return BlockAllMovement(index);
		}

		bool BlockAllMovement(int index)
		{
			return HasTrait(index, Traits.Impassible);
		}

		void MoveActor(int newX, int newY, Actor actor)
		{
			RemoveActor(actor);
			actor.UpdatePosition(newX, newY);
			AddActor(actor);
		}

		public bool MoveActorAttempt(int destinationX, int destinationY, Actor actor)
		{
			var originIndex = ConvertXYToInt(actor.XLoc, actor.YLoc);
			var destinationIndex = ConvertXYToInt(destinationX, destinationY);
			var originX = actor.XLoc;
			var originY = actor.YLoc;

			if (actor.HasTrait(Traits.Flying) && !HasTrait(destinationIndex, Traits.BlockFly))
			{
				if (ApplyMoveOffFunctions(originIndex, actor, destinationX, destinationY))
				{
					MoveActor(destinationX, destinationY, actor);
					ApplyMoveOnFunctions(destinationIndex, actor, originX, originY);
					return true;
				}
				return false;
			}
			if (actor.HasTrait(Traits.Walking) && !HasTrait(destinationIndex, Traits.BlockWalk))
			{
				var elevationDifference = GetElevation(destinationIndex) - GetElevation(originIndex);

				if (Math.Abs(elevationDifference) <= 1 || HasTrait(originIndex, Traits.ElevationChange) || HasTrait(destinationIndex, Traits.ElevationChange))
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

			if (actor.HasTrait(Traits.Swimming) && !HasTrait(destinationIndex, Traits.BlockSwim))
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
				return _furnishings[index].MoveOff(actor, this, destinationX, destinationY);
			return true;
		}

		void ApplyMoveOnFunctions(int index, Actor actor, int originX, int originY)
		{
			// TODO: Call Tile functions.

			if (_furnishings.ContainsKey(index))
				_furnishings[index].MoveOn(actor, this, originX, originY);
		}

		// LOS and vision functions
		bool BlockLOS(int index)
		{
			return HasTrait(index, Traits.BlockLOS);
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

			HashSet<int> viewSet = new HashSet<int>() { ConvertXYToInt(x, y) };
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
					else if (end >= lSlope)
						break;
					else
					{
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
							else
							{
								blocked = false;
								start = newStart;
							}
						}
						else if (!IsValidMapCoord(mapX, mapY) || BlockLOS(mapX, mapY, elevation))
						{
							// Start a child scan
							blocked = true;
							CastLight(xLoc, yLoc, j + 1, start, lSlope, viewDistance, xx, xy, yx, yy, recursionNumber + 1, elevation, viewSet);
							newStart = rSlope;
						}
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
				if (_furnishings.ContainsKey(index) && _furnishings[index].Concealed)
					returnList.Add(_furnishings[index]);
				if (_harvestingNodes.ContainsKey(index) && _harvestingNodes[index].Concealed)
					returnList.Add(_harvestingNodes[index]);
				if (_actors.ContainsKey(index) && _actors[index].Concealed)
					returnList.Add(_actors[index]);
			}
			return returnList;
		}

		// Graphical functions
		public bool HasDrawingEntity(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			return _actors.ContainsKey(index) || _harvestingNodes.ContainsKey(index) || _furnishings.ContainsKey(index);
		}

		public Entity GetDrawingEntity(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			if (_actors.ContainsKey(index))
				return _actors[index];
			if (_harvestingNodes.ContainsKey(index))
				return _harvestingNodes[index];
			return _furnishings[index];
		}

		public Colors GetBackgroundColor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			if (_furnishings.ContainsKey(index) && _furnishings[index].HasBGColor)
				return _furnishings[index].BGColor;
			return _mapGrid[index].BGColor;
		}

		public Colors GetFogColor(int x, int y)
		{
			var index = ConvertXYToInt(x, y);
			if (_furnishings.ContainsKey(index) && _furnishings[index].HasFogColor)
				return _furnishings[index].FogColor;
			return _mapGrid[index].FogColor;
		}

		// Turn management functions
		public void ActivateEntities()
		{
			foreach (var index in new List<int>(_furnishings.Keys))
				_furnishings[index].Update(this);
			foreach (var index in new List<int>(_harvestingNodes.Keys))
				_harvestingNodes[index].Update(this);
			foreach (var index in new List<int>(_actors.Keys))
			{
				if (!_actors[index].HasTrait(Traits.Player))
					_actors[index].Update(this);
			}
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
			details.Triggers = new object[arraySize];
			details.Furnishings = new Furnishing[arraySize];
			details.HarvestingNodes = new HarvestingNode[arraySize];
			details.Actors = new Actor[arraySize];

			for (var i = 0; i < arraySize; i++)
			{
				details.MapGrid[i] = _mapGrid[i].TileType;
				details.Revealed[i] = _revealed[i];
				if (_triggers.ContainsKey(i))
					details.Triggers[i] = _triggers[i];
				else
					details.Triggers[i] = null;
				if (_furnishings.ContainsKey(i))
					details.Furnishings[i] = _furnishings[i];
				else
					details.Furnishings[i] = null;
				if (_harvestingNodes.ContainsKey(i))
					details.HarvestingNodes[i] = _harvestingNodes[i];
				else
					details.HarvestingNodes[i] = null;
				if (_actors.ContainsKey(i) && !_actors[i].HasTrait(Traits.Player))
					details.Actors[i] = _actors[i];
				else
					details.Actors[i] = null;
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

		public object[] Triggers; // TODO: Swap to Triggers later
		public Furnishing[] Furnishings;
		public HarvestingNode[] HarvestingNodes;
		public Actor[] Actors;
	}
}
