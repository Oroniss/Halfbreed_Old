using System;
using RLNET;

namespace Halfbreed
{
	public class DisplayComponent : Component, IComparable<DisplayComponent>
	{
		private char _displayCharacter;
		private Colors _fgColor;
		private DisplayLayer _layer;

		public DisplayComponent(int entityId, char character, string fgColorName, DisplayLayer layer)
			: base(entityId)
		{
			_componentType = ComponentTypes.DISPLAY;
			_displayCharacter = character;
			_fgColor = EnumConverter.ConvertStringToColor(fgColorName);
			_layer = layer;
		}

		public char DisplayCharacter
		{
			get { return _displayCharacter; }
			set { _displayCharacter = value; }
		}

		public Colors FGColor
		{
			get { return _fgColor; }
			set { _fgColor = value; }
		}

		public void ChangeColor(string colorName)
		{
			_fgColor = EnumConverter.ConvertStringToColor(colorName);
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
