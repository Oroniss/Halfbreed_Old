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
			// TODO: Pull these magic numbers out and line them up with thet MainGraphicsDisplay
			// TODO: Also sort out a config file to store them.
			MainProgram.rootConsole = new RLRootConsole("terminal8x8.png", 160, 80, 8, 8, 1, "Halfbreed");

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
				if (NameConverter.KeyToStringConverter.checkKeyIsValid(key.Key))
					UserInputHandler.addKeyboardInput(NameConverter.KeyToStringConverter.convertKeyToString(key.Key));
            }
        }

		public static void quit()
		{
			rootConsole.Close();
		}

    }
}
