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
                int choice;
                do
                {
                    Console.WriteLine("1. New game");
                    Console.WriteLine("2. Scores");
                    Console.WriteLine("3. Quit");
                    key = Console.ReadLine();
                    Console.Clear();
                    if (!int.TryParse(key, out choice))
                    {
                        Console.WriteLine($"'{key}' is not a number. Try again");
                    }
                } while (choice < 1 && choice > 3);
                switch (key)
                {
                    case "1":
                        gameLoop.Run(game);
                        break;
                    case "2":
                        ScoreBoard board = new();
                        board.Load().PrintScore();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            } while (true);
        }
    } 
}
