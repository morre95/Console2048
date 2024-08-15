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
                game.MakeMove(Console.ReadKey(true));
                game.DisplayStats(stopwatch);

                bool gameOver = game.GameOver();
                bool isWon = game.Is2048();

                if (gameOver || isWon)
                {
                    Console.SetCursorPosition(4, 3);
                    if (gameOver)
                    {
                        ConsoleHelper.Write("=============", ConsoleColor.Yellow);
                        Console.SetCursorPosition(4, 4);
                        ConsoleHelper.Write("|", ConsoleColor.Yellow);
                        ConsoleHelper.Write(" You lost! ", ConsoleColor.Red);
                        ConsoleHelper.Write("|", ConsoleColor.Yellow);
                        Console.SetCursorPosition(4, 5);
                        ConsoleHelper.WriteLine("=============", ConsoleColor.Yellow);
                    }
                    else
                    {
                        ConsoleHelper.Write("============", ConsoleColor.DarkBlue);
                        Console.SetCursorPosition(4, 4);
                        ConsoleHelper.Write("|", ConsoleColor.DarkBlue);
                        ConsoleHelper.Write(" You Won! ", ConsoleColor.Green);
                        ConsoleHelper.Write("|", ConsoleColor.DarkBlue);
                        Console.SetCursorPosition(4, 5);
                        ConsoleHelper.WriteLine("============", ConsoleColor.DarkBlue);
                    }
                    Thread.Sleep(2500);
                    break;
                }
            } while (true);
        }
    }
}
