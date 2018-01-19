using RLNET;

namespace Halfbreed.Display
{
	public class TextDisplay:BaseConsole
	{
		private string[] _outputText;
		private int _currentIndex;

		private int _textXOffset = 1; // 1 Blank on the left
		private int _textWidth;
		private int _textYOffset = 3; // 1 Blank, 1 header, 1 more blank.
		private int _textHeight;
		private int _arrayLength = 100;

		public TextDisplay(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
			:base(width, height, left, top, backColor, backConsole)
		{
			_textWidth = _console.Width - 2; // 1 on each side
			_textHeight = _console.Height - 4; // 3 at the top, 1 at the bottom.

			_currentIndex = 0;
			_outputText = new string[_arrayLength];
			for (int i = 0; i < _outputText.Length; i++)
				_outputText[i] = "";
		}

		public void AddOutputText(string text)
		{
			// Don't want two blank lines in a row.
			if (text == "" && CheckIfLastTextIsEmpty())
				return;

			if (text.Contains("\n"))
			{
				string[] splitText = text.Split('\n');
				for (int i = splitText.Length - 1; i >= 0; i--)
					AddOutputText(splitText[i]);
			}

			if (text.Length > _textWidth)
			{
				string[] splitText = WrapText(text);
				for (int i = splitText.Length - 1; i >= 0; i--)
					AddOutputText(splitText[i]);
			}

			_outputText[_currentIndex] = text;
			_currentIndex = (_currentIndex + 1) % _arrayLength;
		}

		private string[] WrapText(string Text)
		{
			return new string[] { };
		}

		private bool CheckIfLastTextIsEmpty()
		{
			return _outputText[(_currentIndex + (_arrayLength - 1)) % _arrayLength] == "";
		}

		public void DrawOutputText()
		{
			Clear();

			_console.Print(_textXOffset, 1, "Message Log", Palette.GetColor(Colors.WHITE));

			for (int i = 0; i < _textHeight; i++)
			{
				int textIndex = (_currentIndex - 1 - i + _arrayLength) % _arrayLength;
				_console.Print(_textXOffset, _textYOffset + i, _outputText[textIndex], Palette.GetColor(Colors.WHITE));
			}

			CopyToBackConsole();
		}
	}
}
