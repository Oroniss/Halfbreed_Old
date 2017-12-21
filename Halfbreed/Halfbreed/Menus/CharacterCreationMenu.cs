using System.Collections.Generic;

namespace Halfbreed.Menus
{
	public class CharacterCreationMenu
	{
		private List<string> _difficultySettings = new List<string>{
					"Thou art a craven knave, worthy only of contempt",
					"Thou art a pathetic weakling, unwilling to challenge thyself",
					"Thou art either brave or foolhardy, only time will tell which",
					"Thou art swimming far out of thy depth",
					"Thou canst not comprehend the horrors that await thee"};
		private List<string> _hl12classes = new List<string>{
					"Cleric", "Fighter", "Mage", "Thief"};
		private List<string> _hl34classes = new List<string>{
					"Bard", "Blackguard", "Druid", "Necromancer", "Paladin", "Ranger"};
		private List<string> _hl5classes = new List<string> { "Dragonlord" };


		public NewGameParameters StartNewGame()
		{
			NewGameParameters parameters = new NewGameParameters();

			parameters = ChooseDifficulty(parameters);

			if (parameters.Cancel)
				return parameters;

			parameters = ChooseCharacterClass(parameters);

			if (parameters.Cancel)
				return parameters;

			return ChooseUseAchievements(parameters);
		}

		private NewGameParameters ChooseDifficulty(NewGameParameters parameters)
		{
			int selection = UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?", _difficultySettings,
														  "Escape to quit.");
			if (selection == -1)
				parameters.Cancel = true;
			else
				parameters.DifficultySetting = selection + 1;
			return parameters;
		}

		private NewGameParameters ChooseCharacterClass(NewGameParameters parameters)
		{
			List<string> classList = _hl12classes;

			if (parameters.DifficultySetting == 3 || parameters.DifficultySetting == 4)
				classList = _hl34classes;

			if (parameters.DifficultySetting == 5)
				classList = _hl5classes;

			int selection = UserInputHandler.SelectFromMenu("What is thy calling", classList, "Escape to Quit");

			if (selection == -1)
			{
				parameters.Cancel = true;
			}
			else
			{
				CharacterClasses characterClass = NameConverter.CharacterClassToStringConverter.ConvertStringToCharacterClass(
					classList[selection]);
				parameters.CharacterClass = characterClass;
			}
			return parameters;

		}

		private NewGameParameters ChooseUseAchievements(NewGameParameters parameters)
		{
			int selection = UserInputHandler.SelectFromMenu("Willst thou make use of the work of thy predecessors",
															new List<string> { "Yes", "No" }, "Escape to Quit");

			if (selection == -1)
				parameters.Cancel = true;
			else
				parameters.UseAchievements = (selection == 0);
			return parameters;
		}
	}

	[System.Serializable]
	public class NewGameParameters
	{
		private bool cancel = false;
		private int _difficultySetting = 1;
		private CharacterClasses _characterClass = CharacterClasses.FIGHTER;
		private bool _useAchievements = true;
		private int _gameId;

		public NewGameParameters()
		{
		}

		public NewGameParameters(int difficulty, CharacterClasses characterClass, bool useAchievements, int gameId)
		{
			_difficultySetting = difficulty;
			_characterClass = characterClass;
			_useAchievements = useAchievements;
			_gameId = gameId;
		}

		public bool Cancel
		{
			get { return cancel; }
			set { cancel = value; }
		}

		public int DifficultySetting
		{
			get { return _difficultySetting; }
			set { _difficultySetting = value; }
		}

		public CharacterClasses CharacterClass
		{
			get { return _characterClass; }
			set { _characterClass = value; }
		}

		public bool UseAchievements
		{
			get { return _useAchievements; }
			set { _useAchievements = value; }
		}

		public int GameId
		{
			get { return _gameId; }
			set { _gameId = value; }
		}
	}
}
