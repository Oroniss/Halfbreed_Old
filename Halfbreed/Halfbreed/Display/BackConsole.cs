﻿// Cleaned up for Version 0.02. No changes required.

using RLNET;

namespace Halfbreed.Display
{
	public class BackConsole:RLConsole
	{
		bool _isdirty;

		public BackConsole(int width, int height)
			:base(width, height)
		{
			_isdirty = true;
		}

		public bool IsDirty
		{
			get { return _isdirty; }
		}

		public void SetClean()
		{
			_isdirty = false;
		}

		public void SetDirty()
		{
			_isdirty = true;
		}
	}
}
