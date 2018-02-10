using RLNET;

namespace Halfbreed.Display
{
	public class MapConsole:BaseConsole
	{
		public MapConsole(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
			:base(width, height, left, top, backColor, backConsole)
		{
		}

		public void DrawMap(Level level, int xCentre, int yCentre) // TODO: Add hashset of visible tiles.
		{
			Clear();

			MapDrawingLimits xLimits = getDrawingLimits(level.Width, _console.Width, xCentre);
			MapDrawingLimits yLimits = getDrawingLimits(level.Height, _console.Height, yCentre);

			for (int y = yLimits.Min; y < yLimits.Max; y++)
			{
				for (int x = xLimits.Min; x < xLimits.Max; x++)
				{
					if (true) // (level.isRevealed(x, y))
					{
						_console.Set(x + xLimits.Offset, y + yLimits.Offset, null,
									 Palette.GetColor(level.GetBackgroundColor(x, y)), ' ');
					}
					if (level.HasDrawingEntity(x, y))
					{
						Entities.Entity entity = level.GetDrawingEntity(x, y);
						_console.Set(x + xLimits.Offset, y + yLimits.Offset, Palette.GetColor(entity.FGColor), null, entity.Symbol);
					}
				}
			}
			/*
			foreach (Position position in GameEngine.VisibleTiles)
			{
				_console.Set(position.X + xLimits.Offset, position.Y + yLimits.Offset, null,
							 Palette.GetColor(level.GetBackgroundColor(position.X, position.Y)), ' ');

				if (level.HasDrawingEntity(position.X, position.Y))
				{
					Entities.Entity entity = level.GetDrawingEntity(position.X, position.Y);
					_console.Set(position.X + xLimits.Offset, position.Y + yLimits.Offset, Palette.GetColor(entity.FGColor),
								 null, entity.Symbol);
				}
			}*/

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
