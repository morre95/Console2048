using System.Diagnostics;

namespace Console2048
{
    public class GameLoop : IGameLoop
    {
        Stopwatch stopwatch = new();
        public void Run(IGame game)
        {
            stopwatch.Start();

            game.ResetScore();
            game.PopulateCells();
            game.AddRandomCell();
            game.AddRandomCell();
            game.PrintBoard();
            game.DisplayStats(stopwatch);

            do
            {
                if (Console.KeyAvailable)
                {
                    game.MakeMove(Console.ReadKey(true));
                    game.DisplayStats(stopwatch);
                    Console.SetCursorPosition(0, 16);

                    bool gameOver = game.GameOver();
                    bool isWon = game.Is2048();

                    if (gameOver || isWon)
                    {
                        Console.SetCursorPosition(0, 16);
                        if (gameOver)
                        {
                            ConsoleHelper.WriteLine("-------------", ConsoleColor.Yellow);
                            ConsoleHelper.Write("|", ConsoleColor.Yellow);
                            ConsoleHelper.Write(" You lost! ", ConsoleColor.Red);
                            ConsoleHelper.WriteLine("|", ConsoleColor.Yellow);
                            ConsoleHelper.WriteLine("-------------", ConsoleColor.Yellow);
                        }
                        else
                        {
                            ConsoleHelper.WriteLine("------------", ConsoleColor.DarkBlue);
                            ConsoleHelper.Write("|", ConsoleColor.DarkBlue);
                            ConsoleHelper.Write(" You Won! ", ConsoleColor.Green);
                            ConsoleHelper.WriteLine("|", ConsoleColor.DarkBlue);
                            ConsoleHelper.WriteLine("------------", ConsoleColor.DarkBlue);
                        }
                        Thread.Sleep(2500);
                        break;
                    }
                }
            } while (true);
        }
    }
}
