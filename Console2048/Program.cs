using System.Drawing;
using System.Linq;

namespace Console2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGame game = new Game();

            IGameLoop loop = new GameLoop();
            IMainInterface mainInterface = new MainInterface();
            mainInterface.StartGame(loop, game);
        }
    }
}
