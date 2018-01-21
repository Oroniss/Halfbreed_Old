using System.Collections.Generic;
using NUnit.Framework;
using RLNET;

namespace Halfbreed.Tests
{
    [TestFixture]
    public class UserInputTests
    {
		[Test]
		public void TestKeyBoardInputSimulator()
		{
			UserInputHandler.clearAllInput();

			KeyBoardInputSimulator.AddKeyBoardInput(RLKey.Left);
			Assert.AreEqual(UserInputHandler.getNextKey(), RLKey.Left);

			UserInputHandler.clearAllInput();

			KeyBoardInputSimulator.AddKeyBoardInput(RLKey.Escape);
			Assert.AreEqual(RLKey.Escape, UserInputHandler.getNextKey());

			UserInputHandler.clearAllInput();

			RLKey[] keyArray = new RLKey[] {RLKey.Left, RLKey.Number1, RLKey.Number7, RLKey.Up, RLKey.Down };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);

			Assert.AreEqual(RLKey.Left, UserInputHandler.getNextKey());
			Assert.AreEqual(RLKey.Number1, UserInputHandler.getNextKey());
			Assert.AreEqual(RLKey.Number7, UserInputHandler.getNextKey());
			Assert.AreEqual(RLKey.Up, UserInputHandler.getNextKey());
			Assert.AreEqual(RLKey.Down, UserInputHandler.getNextKey());

			UserInputHandler.clearAllInput();
		}

		[Test]
		public void TestSelectFromMenu()
		{
			List<string> menuOptions = new List<string>();
			for (int i = 0; i < 35; i++)
			{
				menuOptions.Add(i.ToString());
			}

			string title = "";
			string end = "";

			UserInputHandler.clearAllInput();

			RLKey[] keyArray = new RLKey[] { RLKey.Number3 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(2, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Up, RLKey.Number7 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(6, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Left, RLKey.Number5 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(4, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Number0 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(9, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Up, RLKey.Up, RLKey.Number7 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(6, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Right, RLKey.Number7 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(16, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Right, RLKey.Right, RLKey.Right, RLKey.Number4 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(33, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Right, RLKey.Right, RLKey.Number0 };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(29, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Right, RLKey.Right, RLKey.Left, RLKey.Number5};
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(14, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Right, RLKey.Right, RLKey.Right, RLKey.Number8, RLKey.Left, RLKey.Number6};
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(25, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Right, RLKey.Right, RLKey.Right, RLKey.Number8, RLKey.Escape};
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(-1, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			UserInputHandler.clearAllInput();
		}

		[Test]
		public void TestGetDirection()
		{
			UserInputHandler.clearAllInput();

			RLKey[] keyArray = new RLKey[] { RLKey.Up };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Direction intendedDirection = new Direction(0, -1);
			Direction actualDirection = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(intendedDirection.XDirection, actualDirection.XDirection);
			Assert.AreEqual(intendedDirection.YDirection, actualDirection.YDirection);

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Down };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			intendedDirection = new Direction(0, 1);
			actualDirection = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(intendedDirection.XDirection, actualDirection.XDirection);
			Assert.AreEqual(intendedDirection.YDirection, actualDirection.YDirection);

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Number5, RLKey.Left };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			intendedDirection = new Direction(-1, 0);
			actualDirection = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(intendedDirection.XDirection, actualDirection.XDirection);
			Assert.AreEqual(intendedDirection.YDirection, actualDirection.YDirection);

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Enter };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			intendedDirection = new Direction(0, 0);
			actualDirection = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(intendedDirection.XDirection, actualDirection.XDirection);
			Assert.AreEqual(intendedDirection.YDirection, actualDirection.YDirection);

			UserInputHandler.clearAllInput();

			keyArray = new RLKey[] { RLKey.Enter, RLKey.Escape };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.IsNull(UserInputHandler.GetDirection("", false));

		}
    }
}
