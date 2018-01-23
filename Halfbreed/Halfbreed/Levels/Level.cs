﻿using System;
using System.Collections.Generic;
using System.IO;
using Halfbreed.Levels;
using Halfbreed.Entities;

namespace Halfbreed
{
	[Serializable]
	public class Level
	{

		private string _levelTitle;
		private int _threatLevel;
		private int _height;
		private int _width;
		private MapTileDetails[] _mapGrid;
		private bool[] _revealed;

		private int _visibility;
		private float _anathemaMultiplier;

		private List<Entity> _entities;
		private List<Entity> _entitiesFlaggedForDestruction;
		private Dictionary<int, List<Entity>> _entityLocations;
		private Dictionary<int, MapTileDetails> _tilesWithTileEntities;

		public Level(string LevelFilePath)
		{
			// TODO: Move this to a separate function to allow easy switching to resources.
			FileStream levelStream = File.Open(LevelFilePath + ".txt", FileMode.Open);
			StreamReader LevelSpecificationFile = new StreamReader(levelStream);

			Dictionary<int, TileType> tileDict = new Dictionary<int, TileType>();

			_levelTitle = LevelSpecificationFile.ReadLine();
			_threatLevel = int.Parse(LevelSpecificationFile.ReadLine());
			_height = int.Parse(LevelSpecificationFile.ReadLine());
			_width = int.Parse(LevelSpecificationFile.ReadLine());

			_visibility = int.Parse(LevelSpecificationFile.ReadLine());
			_anathemaMultiplier = float.Parse(LevelSpecificationFile.ReadLine());

			_entities = new List<Entity>();
			_entitiesFlaggedForDestruction = new List<Entity>();
			_entityLocations = new Dictionary<int, List<Entity>>();
			_tilesWithTileEntities = new Dictionary<int, MapTileDetails>();

			LevelSpecificationFile.ReadLine(); // Move to start of dictionary.

			while (true)
			{
				string line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				string[] splitLine = line.Split(',');
				tileDict[int.Parse(splitLine[0])] = (TileType)Enum.Parse(typeof(TileType), splitLine[1]);
			}

			_mapGrid = new MapTileDetails[_width * _height];
			_revealed = new bool[_width * _height];

			for (int y = 0; y < _height; y++)
			{
				string[] _row = LevelSpecificationFile.ReadLine().Trim().Split(',');
				for (int x = 0; x < _width; x++)
				{
					_mapGrid[y * _width + x] = StaticData.GetMapTileDetails(tileDict[int.Parse(_row[x])]);
					_revealed[y * _width + x] = false;
				}
			}

			// Remove the next break line
			LevelSpecificationFile.ReadLine();
			// TODO: Throw an exception if it isn't "###"


			// Furnishings
			const int FURNISHINGPREFIXLENGTH = 4;
			while (true)
			{
				string line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
				   break;

				string[] splitLine = line.Split(',');
				string furnishingName = splitLine[0];
				Materials material = (Materials)Enum.Parse(typeof(Materials), splitLine[1]);
				int xLoc = int.Parse(splitLine[2]);
				int yLoc = int.Parse(splitLine[3]);
				string[] otherParams = GetOtherParams(splitLine, FURNISHINGPREFIXLENGTH);
				Entity newFurnishing = EntityFactory.CreateFurnishing(furnishingName, material,
																					   xLoc, yLoc, otherParams);
				AddEntity(newFurnishing);
			}

			// Harvesting Nodes
			while (true)
			{
				string line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				string[] splitLine = line.Split(',');
				string harvestingName = splitLine[0];
				int xLoc = int.Parse(splitLine[1]);
				int yLoc = int.Parse(splitLine[2]);
				Entity newHarvestingNode = EntityFactory.CreateHarvestingNode(harvestingName, xLoc, yLoc);

				AddEntity(newHarvestingNode);
			}

			// Traps
			const int TRAPPREFIXLENGTH = 3;
			while (true)
			{
				string line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				string[] splitLine = line.Split(',');
				string trapName = splitLine[0];
				int xLoc = int.Parse(splitLine[1]);
				int yLoc = int.Parse(splitLine[2]);
				string[] otherParams = GetOtherParams(splitLine, TRAPPREFIXLENGTH);

				Entity newTrap = EntityFactory.CreateMovementTrap(trapName, xLoc, yLoc, otherParams);

				AddEntity(newTrap);
			}

			levelStream.Close();

		}

		private string[] GetOtherParams(string[] line, int prefixLength)
		{
			string[] otherParams = new string[line.Length - prefixLength];
			for (int i = 0; i<line.Length - prefixLength; i++)
				otherParams[i] = line[i + prefixLength];
			return otherParams;
		}

		private bool LineIsBreakPoint(string line)
		{
			return line == "###";
		}

		public int ThreatLevel
		{
			get { return _threatLevel; }
		}

		public int Height
		{
			get { return _height; }
		}
		public int Width
		{
			get { return _width; }
		}

		private int ConvertXYToInt(int x, int y)
		{
			return y * Width + x;
		}

		public bool IsValidMapCoord(int x, int y)
		{
			return x >= 0 && x < _width && y >= 0 && y < _height;
		}

		public Colors GetBGColor(int x, int y)
		{
			int index = ConvertXYToInt(x, y);
			if (_tilesWithTileEntities.ContainsKey(index))
				return _tilesWithTileEntities[index].BGColor;
			return _mapGrid[index].BGColor;
		}

		public Colors GetFogColor(int x, int y)
		{
			int index = ConvertXYToInt(x, y);
			if (_tilesWithTileEntities.ContainsKey(index))
				return _tilesWithTileEntities[index].FogColor;
			return _mapGrid[index].FogColor;
		}

		public bool isRevealed(int x, int y)
		{
			return _revealed[ConvertXYToInt(x, y)];
		}
		public void revealTile(int x, int y)
		{
			_revealed[ConvertXYToInt(x, y)] = true;
		}

		public void AddEntity(Entity entity)
		{
			int index = ConvertXYToInt(entity.XLoc, entity.YLoc);

			AddAtLocation(entity, index);
			RegisterEntity(entity);
		}

		public void RemoveEntity(Entity entity)
		{
			int index = ConvertXYToInt(entity.XLoc, entity.YLoc);

			RemoveFromLocation(entity, index);
			DeregisterEntity(entity);
		}

		private void AddAtLocation(Entity entity, int index)
		{
			if (entity.HasComponent(ComponentType.TILE))
                AddTileEntity(entity, index);

			if (_entityLocations.ContainsKey(index))
			{
				_entityLocations[index].Add(entity);
				_entityLocations[index].Sort();
			}
			else
				_entityLocations[index] = new List<Entity>() { entity };
		}

		private void RemoveFromLocation(Entity entity, int index)
		{
			if (entity.HasComponent(ComponentType.TILE))
                RemoveTileEntity(entity, index);

			if (_entityLocations[index].Count == 1)
				_entityLocations.Remove(index);
			else
			{
				_entityLocations[index].Remove(entity);
				_entityLocations[index].Sort();
			}
		}

		private void AddTileEntity(Entity entity, int index)
		{
			_tilesWithTileEntities[index] = ((TileComponent)entity.GetComponent(ComponentType.TILE)).MapTileDetails;
		}

		private void RemoveTileEntity(Entity entity, int index)
		{
			_tilesWithTileEntities.Remove(index);
		}

		private void RegisterEntity(Entity entity)
		{
			_entities.Add(entity);
		}

		private void DeregisterEntity(Entity entity)
		{
			_entities.Remove(entity);
		}

		private int CalculateElevationDifference(int originIndex, int destinationIndex)
		{
			return GetElevation(destinationIndex) - GetElevation(originIndex);
		}

		public bool SwimAttempt(Entity entity, int originX, int originY, int destinationX, int destinationY)
		{
			if (!IsSwimmable(destinationX, destinationY))
				return false;
			// TODO: Perform swimming checks here.
			return ApplyMovementFunctions(entity, originX, originY, destinationX, destinationY);
		}

		public bool FlyAttempt(Entity entity, int originX, int originY, int destinationX, int destinationY)
		{
			if (!IsFlyable(destinationX, destinationY))
			   return false;
			return ApplyMovementFunctions(entity, originX, originY, destinationX, destinationY);
		}

		public bool WalkAttempt(Entity entity, int originX, int originY, int destinationX, int destinationY)
		{
			// This should eventually go in a "CanClimb" function.
			// Figure out any elevation changes.
			int elevationDifference = CalculateElevationDifference(ConvertXYToInt(originX, originY), ConvertXYToInt(destinationX, destinationY));

			if (Math.Abs(elevationDifference) > 1)
			{
				if (GetEntitiesWithTrait(originX, originY, EntityTraits.ELEVATIONCHANGE).Count > 0 ||
					GetEntitiesWithTrait(destinationX, destinationY, EntityTraits.ELEVATIONCHANGE).Count > 0)
				{
					if (entity.HasTrait(EntityTraits.PLAYER))
					{
						if (elevationDifference > 1)
							MainGraphicDisplay.TextConsole.AddOutputText("You climb up");
						else
							MainGraphicDisplay.TextConsole.AddOutputText("You climb down");
					}
				}
				else
				{
					if (entity.HasTrait(EntityTraits.PLAYER))
					{
						if (elevationDifference > 1)
							MainGraphicDisplay.TextConsole.AddOutputText("You can't climb up there");
						else
							MainGraphicDisplay.TextConsole.AddOutputText("It would be too risky to drop down there");
					}

					return false;
				}
			}
			return ApplyMovementFunctions(entity, originX, originY, destinationX, destinationY);
		}

		private bool ApplyMovementFunctions(Entity entity, int originX, int originY, int destinationX, int destinationY)
		{
			// Try to move off
			List<Entity> movementTriggers = GetEntitiesWithComponent(originX, originY, ComponentType.MOVEOFFATTEMPT);
			bool success = true;
			foreach (Entity trigger in movementTriggers)
			{
				if (!((MoveOffAttemptComponent)trigger.GetComponent(ComponentType.MOVEOFFATTEMPT)).MoveOffAttempt(entity, destinationX, destinationY))
					success = false;
			}
			if (!success)
				return false;

			// Move off
			movementTriggers = GetEntitiesWithComponent(originX, originY, ComponentType.MOVEOFF);
			foreach (Entity trigger in movementTriggers)
				((MoveOffComponent)trigger.GetComponent(ComponentType.MOVEOFF)).MoveOff(entity, destinationX, destinationY);

			// Try to move on
			movementTriggers = GetEntitiesWithComponent(destinationX, destinationY, ComponentType.MOVEONATTEMPT);
			foreach (Entity trigger in movementTriggers)
			{
				if (!((MoveOnAttemptComponent)trigger.GetComponent(ComponentType.MOVEONATTEMPT)).MoveOnAttempt(entity, originX, originY))
					success = false;
			}
			if (success)
			{
	            MoveEntity(entity, destinationX, destinationY);
				movementTriggers = GetEntitiesWithComponent(destinationX, destinationY, ComponentType.MOVEON);
				foreach (Entity trigger in movementTriggers)
					((MoveOnComponent)trigger.GetComponent(ComponentType.MOVEON)).MoveOn(entity, originX, originY);

				return true;
			}
			else
			{
				movementTriggers = GetEntitiesWithComponent(originX, originY, ComponentType.MOVEON);
				foreach (Entity trigger in movementTriggers)
					((MoveOnComponent)trigger.GetComponent(ComponentType.MOVEON)).MoveOn(entity, destinationX, destinationY);
				return false;
			}
		}

		public void MoveEntity(Entity entity, int newX, int newY)
		{
			int oldIndex = ConvertXYToInt(entity.XLoc, entity.YLoc);
			int newIndex = ConvertXYToInt(newX, newY);

			RemoveFromLocation(entity, oldIndex);
			entity.UpdatePosition(newX, newY);
			AddAtLocation(entity, newIndex);
		}

		public bool HasEntity(int x, int y)
		{
			return _entityLocations.ContainsKey(ConvertXYToInt(x, y));
		}

		public List<Entity> GetEntities(int x, int y)
		{
			List<Entity> returnList = new List<Entity>();

			if (_entityLocations.ContainsKey(ConvertXYToInt(x, y)))
			{
				foreach (Entity entity in _entityLocations[ConvertXYToInt(x, y)])
					returnList.Add(entity);
			}

			return returnList;
		}

		public bool HasDrawingEntity(int x, int y)
		{
			int index = ConvertXYToInt(x, y);
			return _entityLocations.ContainsKey(index) && _entityLocations[index][0].DisplayLayer != DisplayLayer.NOTDISPLAYED;
		}

		public Entity GetDrawingEntity(int x, int y)
		{
			return _entityLocations[ConvertXYToInt(x, y)][0];
		}

		private MapTileDetails GetTileDetails(int index)
		{
			if (_tilesWithTileEntities.ContainsKey(index))
				return _tilesWithTileEntities[index];
			else
				return _mapGrid[index];
		}

		public void ActivateEntities(int currentTime)
		{
			for (int i = 0; i < _entities.Count; i++)
				_entities[i].Update(currentTime);

			while (_entitiesFlaggedForDestruction.Count > 0)
			{
				Entity toDestroy = _entitiesFlaggedForDestruction[0];
				// TODO: Call the destroy method.
				_entitiesFlaggedForDestruction.RemoveAt(0);
			}
		}

		public bool IsPassible(int x, int y)
		{
			if (!IsValidMapCoord(x, y))
				return false;
			if (HasEntityWithTrait(x, y, EntityTraits.BLOCKMOVE))
				return false;

			MapTileDetails tileInfo = GetTileDetails(ConvertXYToInt(x, y));
			return tileInfo.Walkable || tileInfo.Flyable || tileInfo.Swimmable;
		}

		public bool IsSwimmable(int x, int y)
		{
			return IsPassible(x, y) && GetTileDetails(ConvertXYToInt(x, y)).Swimmable;
		}

		public bool IsWalkable(int x, int y)
		{
			return IsPassible(x, y) && GetTileDetails(ConvertXYToInt(x, y)).Walkable;
		}

		public bool IsFlyable(int x, int y)
		{
			return IsPassible(x, y) && GetTileDetails(ConvertXYToInt(x, y)).Flyable;
		}

		private int GetElevation(int index)
		{
			return GetTileDetails(index).Elevation;
		}

		public bool HasEntityWithComponent(int x, int y, ComponentType componentType)
		{
			foreach (Entity entity in GetEntities(x, y))
			{
				if (entity.HasComponent(componentType))
					return true;
			}
			return false;
		}

		public List<Entity> GetEntitiesWithComponent(int x, int y, ComponentType componentType)
		{
			List<Entity> returnList = new List<Entity>();
			foreach (Entity entity in GetEntities(x, y))
			{
				if (entity.HasComponent(componentType))
					returnList.Add(entity);
			}
			return returnList;
		}

		public bool HasEntityWithTrait(int x, int y, EntityTraits trait)
		{
			foreach (Entity entity in GetEntities(x, y))
			{
				if (entity.HasTrait(trait))
					return true;
			}
			return false;
			
		}

		public List<Entity> GetEntitiesWithTrait(int x, int y, EntityTraits trait)
		{
			var returnList = new List<Entity>();
			foreach (var entity in GetEntities(x, y))
			{
				if (entity.HasTrait(trait))
					returnList.Add(entity);
			}
			return returnList;
			
		}

	}
}
