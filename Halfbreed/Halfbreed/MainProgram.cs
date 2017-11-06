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
            mainconsole.Run();
        }

        static void RootConsoleOnLoad(object sender, EventArgs e)
        {
            mainconsole.SetWindowState(RLWindowState.Fullscreen);
        }

        static void RootConsoleRender(object sender, EventArgs e)
        {
			if (Display.IsDirty)
			{
				mainconsole.Clear();
				mainconsole = Display.CopyDisplayToMainConsole(mainconsole);
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
