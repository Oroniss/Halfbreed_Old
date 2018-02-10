using System;
using System.Collections.Generic;
using System.IO;
using Halfbreed.Levels;
using Halfbreed.Entities;

namespace Halfbreed
{
	[Serializable]
	public partial class Level
	{

		private string _levelTitle;
		private int _threatLevel;
		private int _height;
		private int _width;
		private MapTileDetails[] _mapGrid;
		private bool[] _revealed;

		private int _lightLevel;
		private int _smokeLevel;
		private float _anathemaMultiplier;

		SortedDictionary<int, object> _triggers; // TODO: Swap to Triggers later
		SortedDictionary<int, Furnishing> _furnishings;
		SortedDictionary<int, HarvestingNode> _harvestingNodes;
		SortedDictionary<int, Actor> _actors;

		public Level(LevelEnum level)
		{
			// TODO: Move this to a separate function to allow easy switching to resources.
			FileStream levelStream = File.Open(GetFilePath(level), FileMode.Open);
			StreamReader LevelSpecificationFile = new StreamReader(levelStream);

			Dictionary<int, TileType> tileDict = new Dictionary<int, TileType>();

			_levelTitle = LevelSpecificationFile.ReadLine();
			_threatLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_height = int.Parse(LevelSpecificationFile.ReadLine());
			_width = int.Parse(LevelSpecificationFile.ReadLine());

			_lightLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_smokeLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_anathemaMultiplier = float.Parse(LevelSpecificationFile.ReadLine());

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

		public bool MoveActorAttempt(int newX, int newY, Actor actor)
		{
			// TODO: Fix this up.
			var currentIndex = ConvertXYToInt(actor.XLoc, actor.YLoc);
			var newIndex = ConvertXYToInt(newX, newY);

			if (actor.HasTrait(Traits.Flying) && !HasTrait(newIndex, Traits.BlockFly))
			{
				// TODO: Call flying functions here.
	            MoveActor(newX, newY, actor);
				return true;
			}
			if (actor.HasTrait(Traits.Walking) && !HasTrait(newIndex, Traits.BlockWalk))
			{
				var elevationDifference = GetElevation(newIndex) - GetElevation(currentIndex);
				if (Math.Abs(elevationDifference) <= 1 || HasTrait(currentIndex, Traits.ElevationChange) || HasTrait(newIndex, Traits.ElevationChange))
				{
					// TODO: Call walking functions here.
					MoveActor(newX, newY, actor);
					return true;
				}
				if (elevationDifference > 1)
					MainGraphicDisplay.TextConsole.AddOutputText("It's too high to climb up there");
				else
					MainGraphicDisplay.TextConsole.AddOutputText("Dropping down there would be too dangerous");
				return false;
			}
			if (actor.HasTrait(Traits.Swimming) && !HasTrait(newIndex, Traits.BlockSwim))
			{
				// TODO: Call swimming functions here.
				MoveActor(newX, newY, actor);
				return true;
			}

			return false;
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
				_actors[index].Update(this);
		}

	}
}
