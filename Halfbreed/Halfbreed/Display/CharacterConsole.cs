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

			_console.Print(2, 1, "Aleasha Silverstar", Palette.GetColor(Colors.White));
			_console.Print(2, 3, player.CharacterClass.ToString(), Palette.GetColor(Colors.White));

			DrawPrimaryStats(player);

			CopyToBackConsole();
		}

		void DrawPrimaryStats(Player player)
		{
			var stats = player.PrimaryStats;

			DrawPrimaryStat("Agil:", stats.Agility(), 0, 8);
			DrawPrimaryStat("Mght:", stats.Might(), 0, 9);
			DrawPrimaryStat("Mind:", stats.Mind(), 0, 10);
			DrawPrimaryStat("Pres:", stats.Presence(), 0, 11);


		}

		void DrawPrimaryStat(string stat, Dice[] dice, int x, int y)
		{
			var topString = string.Format("{0} {1,6} {2,6} {3,6} {4,6} {5,6}", stat, dice[0], 
			                              dice[1], dice[2], dice[3], dice[4]);
			_console.Print(x, y, topString, Palette.GetColor(Colors.White));
		}
	}
}
