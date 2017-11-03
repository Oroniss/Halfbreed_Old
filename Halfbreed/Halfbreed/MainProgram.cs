using System;
using RLNET;

namespace Halfbreed
{
    public static class MainProgram
    {
        private static RLRootConsole mainconsole;

        public static void Main()
        {
            MainProgram.mainconsole = new RLRootConsole("terminal8x8.png", 80, 60, 8, 8, 1, "Halfbreed");

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
            mainconsole.Clear();
            mainconsole.Print(5, 5, "Hello World", RLColor.Cyan);
            mainconsole.Draw();
        }
        static void RootConsoleUpdate(object sender, EventArgs e)
        {
            RLKeyPress key = mainconsole.Keyboard.GetKeyPress();
            if (key != null)
            {
                switch (key.Key)
                {
                    case RLKey.Escape:
                    {
                        mainconsole.Close();
                        break;
                    }
                }
            }
        }

    }
}
