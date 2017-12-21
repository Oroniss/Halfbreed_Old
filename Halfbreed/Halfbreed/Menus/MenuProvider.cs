using Halfbreed.Menus;

namespace Halfbreed
{
	public static class MenuProvider
	{
		private static CharacterCreationMenu _characterCreationMenu = null;
		private static LoadGameMenu _loadGameMenu = null;
		private static MainMenu _mainMenu = null;

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

	}
}
