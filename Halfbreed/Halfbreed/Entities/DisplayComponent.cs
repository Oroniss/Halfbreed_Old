using System;
using RLNET;

namespace Halfbreed
{
	public class DisplayComponent : Component, IComparable<DisplayComponent>
	{
		private char _displayCharacter;
		private string _fgColorName;
		private RLColor _fgColor;
		private DisplayLayer _layer;

		public DisplayComponent(int entityId, char character, string fgColorName, DisplayLayer layer)
			: base(entityId)
		{
			_componentType = ComponentTypes.DISPLAY;
			_displayCharacter = character;
			_fgColorName = fgColorName;
			_fgColor = NameConverter.StringToColorConverter.ConvertStringToColor(fgColorName);
			_layer = layer;
		}

		public char DisplayCharacter
		{
			get { return _displayCharacter; }
			set { _displayCharacter = value; }
		}

		public string FGColorName
		{
			get { return _fgColorName; }
			set
			{
				_fgColorName = value;
				_fgColor = NameConverter.StringToColorConverter.ConvertStringToColor(value);}
			}

		public RLColor FGColor
		{
			get { return _fgColor; }
		}

		public DisplayLayer Layer
		{
			get { return _layer; }
			set { _layer = value; }
		}

		public int CompareTo(DisplayComponent other)
		{
			return ((int)_layer).CompareTo((int)other.Layer);
		}

	}
}
