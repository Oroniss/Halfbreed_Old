﻿using System;
using System.Collections.Generic;
using System.IO;
using Halfbreed.Levels;

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

		private Dictionary<int, List<Entity>> _entities;
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

			_entities = new Dictionary<int, List<Entity>>();
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
				AddEntity(xLoc, yLoc, newFurnishing);
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

				AddEntity(xLoc, yLoc, newHarvestingNode);
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

		public void AddEntity(int x, int y, Entity entity)
		{
			int index = ConvertXYToInt(x, y);

			if (entity.HasComponent(ComponentType.TILE))
			{
				if (_tilesWithTileEntities.ContainsKey(index))
				{
					ErrorLogger.AddDebugText(string.Format("Tried to add another tile component to same location." +
														   "New entity = {0}, existing entity = {1}.",
														   _tilesWithTileEntities[index], entity));
					return;
				}
				_tilesWithTileEntities[index] = ((TileComponent)entity.GetComponent(ComponentType.TILE)).MapTileDetails;
			}

			if (_entities.ContainsKey(index))
			{
				_entities[index].Add(entity);
				_entities[index].Sort();
			}
			else
				_entities[index] = new List<Entity>() { entity };
			
		}

		public void RemoveEntity(int x, int y, Entity entity)
		{
			int index = ConvertXYToInt(x, y);

			if (entity.HasComponent(ComponentType.TILE))
			{
				if (_tilesWithTileEntities.ContainsKey(index) && 
				    _tilesWithTileEntities[index] == ((TileComponent)entity.GetComponent(ComponentType.TILE)).MapTileDetails)
					_tilesWithTileEntities.Remove(index);
				else
					ErrorLogger.AddDebugText(string.Format("Tried to remove a tile entity but not present in db" +
														   "Entity = {0}, x = {1}, y = {2}", entity, x, y));
			}

			if (_entities.ContainsKey(index))
			{
				if (_entities[index].Contains(entity))
				{
					if (_entities[index].Count == 1)
						_entities.Remove(index);
					else
					{
						_entities[index].Remove(entity);
						_entities[index].Sort();
					}
					return;
				}
			}
			ErrorLogger.AddDebugText(String.Format("Tried to remove non-existant Entity: {0} at index {1}", entity, index)); 


		}

		public bool HasEntity(int x, int y)
		{
			return _entities.ContainsKey(ConvertXYToInt(x, y));
		}

		public List<Entity> GetEntities(int x, int y)
		{
			List<Entity> returnList = new List<Entity>();

			if (_entities.ContainsKey(ConvertXYToInt(x, y)))
			{
				foreach (Entity entity in _entities[ConvertXYToInt(x, y)])
					returnList.Add(entity);
			}

			return returnList;
		}

		public bool HasDrawingEntity(int x, int y)
		{
			int index = ConvertXYToInt(x, y);
			return _entities.ContainsKey(index) && _entities[index][0].DisplayLayer != DisplayLayer.NOTDISPLAYED;
		}

		public Entity GetDrawingEntity(int x, int y)
		{
			// TODO: Keep an eye on this to see if it needs to be added back.
			// Error check removed since it should only be called from one place.
			// if(_entities.ContainsKey(ConvertXYToInt(x, y)))
			return _entities[ConvertXYToInt(x, y)][0];
		}
	}
}
