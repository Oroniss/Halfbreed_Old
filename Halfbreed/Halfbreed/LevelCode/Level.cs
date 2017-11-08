using System.Collections.Generic;
using System.IO;
using System;
namespace Halfbreed
{
	public class Level
	{
		private string _levelName;
		private int _levelHeight;
		private int _levelWidth;
		private int _lightingLevel;
		private float _anathemaMultiplier;

		public Level(string levelID)
		{
			FileStream levelFile = File.Open(levelID + ".txt", FileMode.Open);

			StreamReader reader = new StreamReader(levelFile);

			_levelName = reader.ReadLine();
			_levelHeight = Int32.Parse(reader.ReadLine());
			_levelWidth = Int32.Parse(reader.ReadLine());
			_lightingLevel = Int32.Parse(reader.ReadLine());
			_anathemaMultiplier = float.Parse(reader.ReadLine());

			levelFile.Close();
		}

		public string LevelName
		{
			get { return _levelName; }
		}
		public int Height
		{
			get { return _levelHeight; }
		}
		public int Width
		{
			get { return _levelWidth; }
		}
		public int LightingLevel
		{
			get { return _lightingLevel; }
		}
		public float AnathemaModifier
		{
			get { return _anathemaMultiplier; }
		}
	}
}
