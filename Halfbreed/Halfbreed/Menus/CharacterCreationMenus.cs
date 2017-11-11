using System.Collections.Generic;
namespace Halfbreed
{
	public static class CharacterCreationMenus
	{
		private static List<string> _difficultySettings = new List<string>{
					"Thou art a craven knave, worthy only of contempt",
					"Thou art a pathetic weakling, unwilling to challenge thyself",
					"Thou art either brave or foolhardy, only time will tell which",
					"Thou art swimming far out of thy depth",
					"Thou canst not comprehend the horrors that await thee"};
		private static List<string> _hl12classes = new List<string>{
					"Cleric", "Fighter", "Mage", "Thief"};
		private static List<string> _hl34classes = new List<string>{
					"Bard", "Blackguard", "Druid", "Necromancer", "Paladin", "Ranger"};
		private static List<string> _hl5classes = new List<string> { "Dragonlord" };


		public static NewGameParameters StartNewGame()
		{
			NewGameParameters parameters = new NewGameParameters();

			parameters = SelectAndSetDifficulty(parameters);

			if (parameters.Cancel)
				return parameters;

			parameters = SelectAndSetCharacterClass(parameters);

			if (parameters.Cancel)
				return parameters;

			return SelectAndSetUseAchievements(parameters);
		}

		private static NewGameParameters SelectAndSetDifficulty(NewGameParameters parameters)
		{
			int selection = UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?", _difficultySettings,
														  "Escape to quit.");
			if (selection == -1)
				parameters.Cancel = true;
			else
				parameters.DifficultySetting = selection + 1;
			return parameters;
		}

		private static NewGameParameters SelectAndSetCharacterClass(NewGameParameters parameters)
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
				CharacterClasses characterClass = CharacterClassToStringConverter.ConvertStringToCharacterClass(
					classList[selection]);
				parameters.CharacterClass = characterClass;
			}
			return parameters;

		}

		private static NewGameParameters SelectAndSetUseAchievements(NewGameParameters parameters)
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

	public class NewGameParameters
	{
		private bool cancel = false;
		private int difficultySetting = 1;
		private CharacterClasses characterClass = CharacterClasses.FIGHTER;
		private bool useAchievements = true;

		public bool Cancel
		{
			get { return cancel; }
			set { cancel = value; }
		}

		public int DifficultySetting
		{
			get { return difficultySetting; }
			set { difficultySetting = value; }
		}

		public CharacterClasses CharacterClass
		{
			get { return characterClass; }
			set { characterClass = value; }
		}

		public bool UseAchievements
		{
			get { return useAchievements; }
			set { useAchievements = value; }
		}
	}
}
