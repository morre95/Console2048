using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Console2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
           /* ScoreBoard board = new();
            board.Save(new (1, new(0,0,0,0), "Walle"));*/

            IGame game = new Game();

            IGameLoop loop = new GameLoop();
            IMainInterface mainInterface = new MainInterface();
            mainInterface.StartGame(loop, game);
        }

        
    }
}
