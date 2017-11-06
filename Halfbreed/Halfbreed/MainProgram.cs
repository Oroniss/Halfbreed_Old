using System;
using System.Threading;
using RLNET;

namespace Halfbreed
{
    public static class MainProgram
    {
        private static RLRootConsole mainconsole;

        public static void Main()
        {
            getConfigurationParameters();

            MainProgram.mainconsole = new RLRootConsole("terminal8x8.png", 120, 90, 8, 8, 1, "Halfbreed");

            mainconsole.OnLoad += RootConsoleOnLoad;
            mainconsole.Update += RootConsoleUpdate;
            mainconsole.Render += RootConsoleRender;

			// Thread loopThread = new Thread(gameInterface.StartGameMenus);
			// loopThread.start()

            mainconsole.Run();
        }

        static void RootConsoleOnLoad(object sender, EventArgs e)
        {
            mainconsole.SetWindowState(RLWindowState.Fullscreen);
        }

        static void RootConsoleRender(object sender, EventArgs e)
        {
			if (GraphicDesplay.IsDirty)
			{
				mainconsole.Clear();
				mainconsole = GraphicDesplay.CopyDisplayToMainConsole(mainconsole);
				mainconsole.Draw();
			}
		}

        static void RootConsoleUpdate(object sender, EventArgs e)
        {
            RLKeyPress key = mainconsole.Keyboard.GetKeyPress();
            if (key != null)
            {
                UserInputHandler.addKeyboardInput(key.Key);

                // TODO: Remove this once threading properly in.
                if(key.Key == RLKey.Escape)
                {
                    mainconsole.Close();
                }
				if (key.Key == RLKey.Enter)
				{
					GraphicDesplay.MenuConsole.DrawSelectFromMenu("Boo", new string[0], "Goodbye");
				}
            }
        }

        static void checkSetup()
        {
            if (!System.IO.Directory.Exists("Foo"))
            {

            }

        }

        static void getConfigurationParameters()
        {
            
        }

    }
}
