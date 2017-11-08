using System.Collections.Generic;
using NUnit.Framework;

namespace Halfbreed
{
    [TestFixture]
    public class UserInputTests
    {
		[Test]
		public void TestKeyBoardInputSimulator()
		{
			KeyBoardInputSimulator.AddKeyBoardInput("LEFT");
			Assert.AreEqual(UserInputHandler.getNextKey(), "LEFT");

			KeyBoardInputSimulator.AddKeyBoardInput("ESCAPE");
			Assert.AreEqual("ESCAPE", UserInputHandler.getNextKey());

			string[] keyArray = new string[] { "LEFT", "1", "7", "UP", "DOWN" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);

			Assert.AreEqual("LEFT", UserInputHandler.getNextKey());
			Assert.AreEqual("1", UserInputHandler.getNextKey());
			Assert.AreEqual("7", UserInputHandler.getNextKey());
			Assert.AreEqual("UP", UserInputHandler.getNextKey());
			Assert.AreEqual("DOWN", UserInputHandler.getNextKey());

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

			string[] keyArray = new string[] { "3" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(2, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "UP", "7" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(6, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "LEFT", "5" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(4, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "0" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(9, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "UP", "UP", "7" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(6, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "RIGHT", "7" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(16, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "RIGHT", "RIGHT", "RIGHT", "4" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(33, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "RIGHT", "RIGHT", "0" };
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(29, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "RIGHT", "RIGHT", "LEFT", "5"};
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(14, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "RIGHT", "RIGHT", "RIGHT", "8", "LEFT", "6"};
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(25, UserInputHandler.SelectFromMenu(title, menuOptions, end));

			keyArray = new string[] { "RIGHT", "RIGHT", "RIGHT", "8", "ESCAPE"};
			KeyBoardInputSimulator.AddKeyBoardInput(keyArray);
			Assert.AreEqual(-1, UserInputHandler.SelectFromMenu(title, menuOptions, end));

		}
    }
}
