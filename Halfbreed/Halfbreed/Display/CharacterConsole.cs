using RLNET;
using Halfbreed.Entities;

namespace Halfbreed.Display
{
	public class CharacterConsole:BaseConsole
	{
		public CharacterConsole(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
			: base(width, height, left, top, backColor, backConsole)
		{
		}

		public void DrawCharacter(Player player)
		{
			if (player == null)
				player = MainProgram.Player;

			Clear();

			_console.Print(2, 1, "Aleasha Silverstar", Palette.GetColor("White"));
			_console.Print(2, 3, player.CharacterClass.ToString(), Palette.GetColor("White"));

			DrawPrimaryStats(player, 8);
			DrawResists(player, 14);

			CopyToBackConsole();
		}

		void DrawPrimaryStats(Player player, int startingYOffset)
		{
			var stats = player.PrimaryStats;

			DrawDiceArray("Agil:", stats.Agility(), 0, startingYOffset);
			DrawDiceArray("Mght:", stats.Might(), 0, startingYOffset + 1);
			DrawDiceArray("Mind:", stats.Mind(), 0, startingYOffset + 2);
			DrawDiceArray("Pres:", stats.Presence(), 0, startingYOffset + 3);
		}

		void DrawResists(Player player, int startingYOffset)
		{
			var resists = player.DefensiveStats;

            DrawDiceArray("Phys:", resists.PhysicalResist.GetDefensiveDice(), 0, startingYOffset);
            DrawDiceArray("Ment:", resists.MentalResist.GetDefensiveDice(), 0, startingYOffset + 1);
			DrawDiceArray("Acid:", resists.AcidResist.GetDefensiveDice(), 0, startingYOffset + 2);
            DrawDiceArray("Cold:", resists.ColdResist.GetDefensiveDice(), 0, startingYOffset + 3);
            DrawDiceArray("Elec:", resists.ElectricityResist.GetDefensiveDice(), 0, startingYOffset + 4);
            DrawDiceArray("Fire:", resists.FireResist.GetDefensiveDice(), 0, startingYOffset + 5);
            DrawDiceArray("Pois:", resists.PoisonResist.GetDefensiveDice(), 0, startingYOffset + 6);
            DrawDiceArray("Dsea:", resists.DiseaseResist.GetDefensiveDice(), 0, startingYOffset + 7);
            DrawDiceArray("Lght:", resists.LightResist.GetDefensiveDice(), 0, startingYOffset + 8);
            DrawDiceArray("Shad:", resists.ShadowResist.GetDefensiveDice(), 0, startingYOffset + 9);
            DrawDiceArray("Neth:", resists.NetherResist.GetDefensiveDice(), 0, startingYOffset + 10);
		}

		void DrawDiceArray(string stat, Dice[] dice, int x, int y)
		{
			_console.Print(x, y, stat, Palette.GetColor("White"));
			for (int i = 0; i < 5; i++)
				DrawDie(dice[i], x + 6 + 4 * i, y);
		}

		void DrawDie(Dice die, int x, int y)
		{
			_console.Print(x, y, die.ToString(), Palette.GetColor("White"));
		}

	}
}
