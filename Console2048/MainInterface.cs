namespace Console2048
{
    public class MainInterface : IMainInterface
    {
        public void StartGame(IGameLoop gameLoop, IGame game)
        {
            Console.CursorVisible = false;
            do
            {
                string key;
                do
                {
                    Console.WriteLine("1. New game");
                    Console.WriteLine("2. Quit");
                    key = Console.ReadLine();
                    Console.Clear();
                } while (key != "1" && key != "2");
                switch (key)
                {
                    case "1":
                        gameLoop.Run(game);
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            } while (true);
        }
    } 
}
