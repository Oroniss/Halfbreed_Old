// Revised for version 0.02.

using Halfbreed.Menus;

namespace Halfbreed
{
	public static class MenuProvider
	{
		static CharacterCreationMenu _characterCreationMenu = null;
		static LoadGameMenu _loadGameMenu = null;
		static MainMenu _mainMenu = null;
		static ViewKeysDisplay _viewKeysMenu = null;

		public static CharacterCreationMenu CharacterCreationMenu
		{
			get {
				if (_characterCreationMenu == null)
					_characterCreationMenu = new CharacterCreationMenu();
				return _characterCreationMenu;
			}
		}

		public static LoadGameMenu LoadGameMenu
		{
			get
			{
				if (_loadGameMenu == null)
					_loadGameMenu = new LoadGameMenu();
				return _loadGameMenu;
			}
		}

		public static MainMenu MainMenu
		{
			get
			{
				if (_mainMenu == null)
					_mainMenu = new MainMenu();
				return _mainMenu;
			}
		}

		public static ViewKeysDisplay ViewKeysDisplay
		{
			get
			{
				if (_viewKeysMenu == null)
					_viewKeysMenu = new ViewKeysDisplay();
				return _viewKeysMenu;
			}
		}

	}
}
