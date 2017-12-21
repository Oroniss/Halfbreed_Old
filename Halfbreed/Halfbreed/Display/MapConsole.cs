using RLNET;

namespace Halfbreed.Display
{
	public class MapConsole:BaseConsole
	{
		public MapConsole(int height, int width, int top, int left, RLColor backColor, BackConsole backConsole)
			:base(height, width, top, left, backColor, backConsole)
		{
		}

		public void DrawMap(Level level, int xCentre, int yCentre) // TODO: Add hashset of visible tiles.
		{
			Clear();

			MapDrawingLimits xLimits = getDrawingLimits(level.Map.Width, _console.Width, xCentre);
			MapDrawingLimits yLimits = getDrawingLimits(level.Map.Height, _console.Height, yCentre);

			for (int y = yLimits.Min; y < yLimits.Max; y++)
			{
				for (int x = xLimits.Min; x < xLimits.Max; x++)
				{
					TileType tile = level.Map.GetTile(x, y);
					_console.SetBackColor(x + xLimits.Offset, 
					                      y + yLimits.Offset, 
					                      Palette.GetColor( MapTileDetails.MapTileDict[tile].BGColor));
				}
			}

			CopyToBackConsole();
		}

		private MapDrawingLimits getDrawingLimits(int levelSize, int consoleSize, int drawingCentre)
		{
			int drawingSize = consoleSize - 4;
			int drawingMin = 0;
			int drawingMax = levelSize;
			int drawingOffset = 2;

			// We can fit the entire level
			if (levelSize <= drawingSize)
			{
				drawingOffset = (drawingSize - levelSize) / 2;
			}
			else
			{
				if (drawingCentre < drawingSize / 2)
				{
					drawingMin = drawingSize;
				}
				else
				{
					if (levelSize - drawingCentre < drawingSize / 2)
					{
						drawingMin = levelSize - drawingSize;
						drawingOffset -= drawingMin;
					}
					else
					{
						drawingMin = drawingCentre - drawingSize / 2;
						drawingOffset -= drawingMin;
						drawingMax = drawingCentre + (drawingSize / 2 - 1);
					}
				}
			}
			return new MapDrawingLimits(drawingMin, drawingMax, drawingOffset);
		}
	}

	internal struct MapDrawingLimits
	{
		public int Min;
		public int Max;
		public int Offset;

		public MapDrawingLimits(int min, int max, int offset)
		{
			Min = min;
			Max = max;
			Offset = offset;
		}
	}
}
