using System;
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
			_height = Int32.Parse(LevelSpecificationFile.ReadLine());
			_width = Int32.Parse(LevelSpecificationFile.ReadLine());

			_visibility = Int32.Parse(LevelSpecificationFile.ReadLine());
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
				tileDict[Int32.Parse(splitLine[0])] = (TileType)Enum.Parse(typeof(TileType), splitLine[1]);
			}

			_mapGrid = new MapTileDetails[_width * _height];
			_revealed = new bool[_width * _height];

			for (int y = 0; y < _height; y++)
			{
				string[] _row = LevelSpecificationFile.ReadLine().Trim().Split(',');
				for (int x = 0; x < _width; x++)
				{
					_mapGrid[y * _width + x] = StaticData.GetMapTileDetails(tileDict[Int32.Parse(_row[x])]);
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
				int xLoc = Int32.Parse(splitLine[2]);
				int yLoc = Int32.Parse(splitLine[3]);
				string[] otherParams = new string[splitLine.Length - FURNISHINGPREFIXLENGTH];
				for (int i = 0; i < splitLine.Length - FURNISHINGPREFIXLENGTH; i++)
					otherParams[i] = splitLine[i + FURNISHINGPREFIXLENGTH];
				Entity newFurnishing = Entities.EntityFactory.CreateFurnishing(furnishingName, material,
																					   xLoc, yLoc, otherParams);
				AddEntity(newFurnishing);
			}

			while (true)
			{
				string line = LevelSpecificationFile.ReadLine().Trim();
				if (LineIsBreakPoint(line))
					break;

				string[] splitLine = line.Split(',');
				string harvestingName = splitLine[0];
				int xLoc = Int32.Parse(splitLine[1]);
				int yLoc = Int32.Parse(splitLine[2]);
				Entity newHarvestingNode = Entities.EntityFactory.CreateHarvestingNode(harvestingName, xLoc, yLoc);

				AddEntity(newHarvestingNode);
			}

			levelStream.Close();

		}

		private bool LineIsBreakPoint(string line)
		{
			return line == "###";
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

		public bool MoveEntityAttempt(Entity entity, int newX, int newY)
		{
			// TODO: Do the checks for movement here.
			// Try to leave.
			// Leave
			// Try to enter
			MoveEntity(entity, newX, newY);
			// TODO: Enter
			return true;
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
			// TODO: Keep an eye on this to see if it needs to be added back.
			// Error check removed since it should only be called from one place.
			// if(_entities.ContainsKey(ConvertXYToInt(x, y)))
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

		public bool IsPassible(int x, int y, MovementModes movementMode)
		{
			if (!IsValidMapCoord(x, y))
				return false;

			int index = ConvertXYToInt(x, y);

			EntityTraits blockingTrait = EntityTraits.BLOCKMOVE;
			if (movementMode == MovementModes.PHASE)
				blockingTrait = EntityTraits.BLOCKPHASING;
			if (IsBlockedByEntity(index, blockingTrait))
				return false;

			return movementMode > GetTileDetails(index).MoveModes;
		}

		private int GetElevation(int index)
		{
			return GetTileDetails(index).Elevation;
		}

		private bool IsBlockedByEntity(int index, EntityTraits blockingTrait)
		{
			if (_entityLocations.ContainsKey(index))
				foreach (Entity entity in _entityLocations[index])
				{
					if (entity.HasTrait(blockingTrait))
						return true;
				}
			return false;
		}

	}
}
