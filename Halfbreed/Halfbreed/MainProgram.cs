using System;
using System.Threading;
using RLNET;

namespace Halfbreed
{
    public static class MainProgram
    {
        private static RLRootConsole rootConsole;

        public static void Main()
        {
			// DatabaseConnection.SetupDirectoriesAndFiles();

			MainProgram.rootConsole = new RLRootConsole("terminal8x8.png", 120, 90, 8, 8, 1, "Halfbreed");

            rootConsole.OnLoad += RootConsoleOnLoad;
            rootConsole.Update += RootConsoleUpdate;
            rootConsole.Render += RootConsoleRender;

			Thread mainLoopThread = new Thread(MainMenu.TitleMenu);
			mainLoopThread.Start();

            rootConsole.Run();
        }

        static void RootConsoleOnLoad(object sender, EventArgs e)
        {
            //rootConsole.SetWindowState(RLWindowState.Fullscreen);
        }

        static void RootConsoleRender(object sender, EventArgs e)
        {
			if (MainGraphicDisplay.IsDirty)
			{
				rootConsole.Clear();
				rootConsole = MainGraphicDisplay.CopyDisplayToRootConsole(rootConsole);
				rootConsole.Draw();
			}
		}

        static void RootConsoleUpdate(object sender, EventArgs e)
        {
            RLKeyPress key = rootConsole.Keyboard.GetKeyPress();
            if (key != null)
            {
				if (KeyToStringConverter.checkKeyIsValid(key.Key))
					UserInputHandler.addKeyboardInput(KeyToStringConverter.convertKeyToString(key.Key));
            }
        }

		public static void quit()
		{
			rootConsole.Close();
		}

    }
}
