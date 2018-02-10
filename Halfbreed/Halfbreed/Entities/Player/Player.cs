using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class Player:Actor
	{
		public Player(Menus.NewGameParameters playerParameters)
			:base("Player", 0 ,0 , new List<string>())
		{
			AddTrait(Traits.Player);
			AddTrait(Traits.Walking);
			AddTrait(Traits.Swimming);
			AddTrait(Traits.Climbing);

		}

		protected override void GetNextMove(Level currentLevel)
		{
			MainGraphicDisplay.UpdateGameScreen();
		}
	}
}
