// Tidied up for Version 0.02.

using NUnit.Framework;
using System.Collections.Generic;

namespace Halfbreed.Tests
{
	[TestFixture]
	public class UserInputTests
	{
		[Test]
		public void TestAddKeyboardInput()
		{
			// Also effectively tests get next key.
			var keys = new string[] {"LEFT", "UP", "RIGHT" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);

			Assert.AreEqual("LEFT", UserInputHandler.getNextKey());
			Assert.AreEqual("UP", UserInputHandler.getNextKey());
			Assert.AreEqual("RIGHT", UserInputHandler.getNextKey());

			var key = "1";
			KeyBoardInputSimulator.AddKeyBoardInput(key);
			Assert.AreEqual("1", UserInputHandler.getNextKey());

			UserInputHandler.addKeyboardInput(RLNET.RLKey.D);
			Assert.AreEqual("D", UserInputHandler.getNextKey());
		}

		[Test]
		public void TestClearAllInput()
		{
			var keys = new string[] { "LEFT", "UP", "RIGHT" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			UserInputHandler.clearAllInput();
			var key = "1";
			KeyBoardInputSimulator.AddKeyBoardInput(key);
			Assert.AreEqual("1", UserInputHandler.getNextKey());
		}

		[Test]
		public void TestSelectFromMenu()
		{
			var menuOptions = new List<string>();
			for (int i = 0; i < 35; i++)
				menuOptions.Add(i.ToString());

			// Standard input
			var keys = new string[] {"5" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			var selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(4, selection);

			UserInputHandler.clearAllInput();
			keys = new string[] {"0" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(9, selection);

			UserInputHandler.clearAllInput();
			keys = new string[] {"1" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(0, selection);

			// Test left and right
			UserInputHandler.clearAllInput();
			keys = new string[] {"LEFT", "RIGHT", "0" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(19, selection);

			UserInputHandler.clearAllInput();
			keys = new string[] {"RIGHT", "RIGHT", "LEFT", "1" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(10, selection);

			UserInputHandler.clearAllInput();
			keys = new string[] {"RIGHT", "RIGHT", "RIGHT", "RIGHT", "LEFT", "7" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(26, selection);

			UserInputHandler.clearAllInput();
			keys = new string[] {"RIGHT", "RIGHT", "RIGHT", "0", "4" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(33, selection);

			// Test cancel
			UserInputHandler.clearAllInput();
			keys = new string[] {"ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			selection = UserInputHandler.SelectFromMenu("", menuOptions, "");
			Assert.AreEqual(-1, selection);
		}

		[Test]
		public void TestGetDirection()
		{
			UserInputHandler.clearAllInput();
			var keys = new string[] { "LEFT" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			var direction = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(-1, direction.X);
			Assert.AreEqual(0, direction.Y);

			UserInputHandler.clearAllInput();
			keys = new string[] { "RIGHT" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			direction = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(1, direction.X);
			Assert.AreEqual(0, direction.Y);

			UserInputHandler.clearAllInput();
			keys = new string[] { "UP_LEFT" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			direction = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(-1, direction.X);
			Assert.AreEqual(-1, direction.Y);

			// Test cancel
			UserInputHandler.clearAllInput();
			keys = new string[] { "ESCAPE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			direction = UserInputHandler.GetDirection("", true);
			Assert.IsNull(direction);

			// Test Centre
			UserInputHandler.clearAllInput();
			keys = new string[] { "SPACE", "DOWN" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			direction = UserInputHandler.GetDirection("", false);
			Assert.AreEqual(0, direction.X);
			Assert.AreEqual(1, direction.Y);

			UserInputHandler.clearAllInput();
			keys = new string[] { "SPACE" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			direction = UserInputHandler.GetDirection("", true);
			Assert.AreEqual(0, direction.X);
			Assert.AreEqual(0, direction.Y);

			// Test other option
			UserInputHandler.clearAllInput();
			keys = new string[] { "SPACE", "DOWN" };
			KeyBoardInputSimulator.AddKeyBoardInput(keys);
			direction = UserInputHandler.GetDirection();
			Assert.AreEqual(0, direction.X);
			Assert.AreEqual(0, direction.Y);
		}

		[Test]
		public void TestGetText()
		{
			
		}

		[Test]
		public void TestConfigMenu()
		{
			
		}

		[Test]
		public void TestExtraKeys()
		{
			UserInputHandler.ExtraKeys = true;

			UserInputHandler.addKeyboardInput(RLNET.RLKey.BracketLeft);
			Assert.AreEqual("UP_LEFT", UserInputHandler.getNextKey());

			UserInputHandler.clearAllInput();

			UserInputHandler.ExtraKeys = false;

			UserInputHandler.addKeyboardInput(RLNET.RLKey.BracketLeft);
			UserInputHandler.addKeyboardInput(RLNET.RLKey.Down);
			Assert.AreEqual("DOWN", UserInputHandler.getNextKey());


		}
	}
}
