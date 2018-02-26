// Updated for version 0.02.

using System.Collections.Generic;

namespace Halfbreed.Menus
{
	public class CharacterCreationMenu
	{
		readonly List<string> _difficultySettings = new List<string>{
					"Thou art a craven knave, worthy only of contempt",
					"Thou art a pathetic weakling, unwilling to challenge thyself",
					"Thou art either brave or foolhardy, only time will tell which",
					"Thou art swimming far out of thy depth",
					"Thou canst not comprehend the horrors that await thee"};
		readonly List<string> _hl12classes = new List<string>{
					"Cleric", "Fighter", "Mage", "Thief"};
		readonly List<string> _hl34classes = new List<string>{
					"Bard", "Blackguard", "Druid", "Necromancer", "Paladin", "Ranger"};
		readonly List<string> _hl5classes = new List<string> { "Dragonlord" };

		readonly List<string> _startGameText1 = new List<string>
		{
			"A Halfbreed. That's what you are. What you always will be.",
			"Outcast before you were even born, for the blood that runs in your veins.",
			"Your bond with the Aethyr gives you strength, power, but never once",
			"have you counted it a blessing.",
			"Your sister was taken the moment you were born, but your mother escaped with",
			"you, raised you on her own.",
			"Eventually the legion found her and took her off in chains.",
			"You fended for yourself for a time, learning, growing stronger.",
			"In the end they caught you as well.",
			"You surprised them then, with your strength and your rage.",
			"They were expecting a frightened girl, but the fury overcame the fear",
			"and two of them died on your blade.",
			"They made you pay for that on your journey.",
			"Now you have arrived here, Outcasts' Isle, the final destination for all Halfbreeds.",
			"They don't let your kind leave, not even the 'good' ones.",
			"Still, you have too many questions to stay, questions about your mother, your sister.",
			"Questions about your father.",
			"Important questions, and you won't find the answers on Outcasts' Isle.",
			"The world may have condemned you to this rock, but after all the world has done to you,",
			"you care little for its plans.",
			"You step off the boat, telling the captain to save you a berth for the return journey.",
			"You won't be staying here long."
		};


		public GameData StartNewGame()
		{
			GameData parameters = new GameData();

			parameters = SelectDifficultySetting(parameters);

			if (parameters != null)
				parameters = SelectCharacterClass(parameters);

			if (parameters != null)
				parameters = SelectUseAchievements(parameters);

			if (parameters != null)
				parameters = SetCharacterNote(parameters);

			DisplayNewGameText();

			return parameters;
		}

		GameData SelectDifficultySetting(GameData newGameParameters)
		{
			var difficultySelection = UserInputHandler.SelectFromMenu("How great a challenge dost thou seek?",
														_difficultySettings,
														"Escape to quit.");

			if (difficultySelection == -1)
				return null;
			newGameParameters.DifficultySetting = difficultySelection + 1;
			return newGameParameters;
		}

		GameData SelectCharacterClass(GameData newGameParameters)
		{
			var classList = GetClassList(newGameParameters.DifficultySetting);
			var classSelection = UserInputHandler.SelectFromMenu("What is thy calling", classList, "Escape to Quit");

			if (classSelection == -1)
				return null;
			newGameParameters.CharacterClass = (CharacterClasses)System.Enum.Parse(typeof(CharacterClasses), 
				                                                                      classList[classSelection]);
			return newGameParameters;
		}

		List<string> GetClassList(int difficultySetting)
		{
			var classList = _hl12classes;
			if (difficultySetting == 3 || difficultySetting == 4)
				classList = _hl34classes;
			if (difficultySetting == 5)
				classList = _hl5classes;
			return classList;
		}

		GameData SelectUseAchievements(GameData newGameParameters)
		{
			var useAchivementSelection = UserInputHandler.SelectFromMenu(
			"Willst thou make use of the work of thy predecessors",
			new List<string> { "Yes", "No" }, "Escape to Quit");

			if (useAchivementSelection == -1)
				return null;
			newGameParameters.UseAchievements = (useAchivementSelection == 0);
			return newGameParameters;
		}

		GameData SetCharacterNote(GameData newGameParameters)
		{
			var characterNote = UserInputHandler.GetText("Specify Character Note");
			if (characterNote == null)
				return null;
			newGameParameters.CharacterNote = characterNote;
			return newGameParameters;
		}

		void DisplayNewGameText()
		{
			MainGraphicDisplay.MenuConsole.DrawTextBlock("", _startGameText1, "Press any key to continue");
			UserInputHandler.getNextKey();
		}
	}
}
