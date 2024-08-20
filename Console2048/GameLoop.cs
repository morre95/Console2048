using System.Collections.Generic;
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

            bool gameOver = false;
            bool isWon = false;

            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Run(() => {
                while (!cts.IsCancellationRequested) 
                {
                    game.DisplayStats(stopwatch);
                    Thread.Sleep(10);
                }
            }, cts.Token);
            do
            {
                game.MakeMove(Console.ReadKey(true));

                gameOver = game.GameOver();
                isWon = game.Is2048();

                if (gameOver || isWon)
                {
                    stopwatch.Start();

                    cts.Cancel();

                    Console.SetCursorPosition(4, 3);
                    if (gameOver)
                    {
                        ConsoleHelper.Write("================", ConsoleColor.Yellow);
                        Console.SetCursorPosition(4, 4);
                        ConsoleHelper.Write("|", ConsoleColor.Yellow);
                        ConsoleHelper.Write(" You lost!    ", ConsoleColor.Red);
                        ConsoleHelper.Write("|", ConsoleColor.Yellow);
                        Console.SetCursorPosition(4, 5);
                        ConsoleHelper.Write("|", ConsoleColor.Yellow);
                        ConsoleHelper.Write($" {stopwatch.Elapsed.ToStringMyFormat().PadRight(12)} ", ConsoleColor.Cyan);
                        ConsoleHelper.Write("|", ConsoleColor.Yellow);
                        Console.SetCursorPosition(4, 6);
                        ConsoleHelper.WriteLine("================", ConsoleColor.Yellow);
                    }
                    else
                    {
                        ConsoleHelper.Write("================", ConsoleColor.DarkBlue);
                        Console.SetCursorPosition(4, 4);
                        ConsoleHelper.Write("|", ConsoleColor.DarkBlue);
                        ConsoleHelper.Write(" You Won!     ", ConsoleColor.Green);
                        ConsoleHelper.Write("|", ConsoleColor.DarkBlue);
                        Console.SetCursorPosition(4, 5);
                        ConsoleHelper.Write("|", ConsoleColor.DarkBlue);
                        ConsoleHelper.Write($" {stopwatch.Elapsed.ToStringMyFormat().PadRight(12)} ", ConsoleColor.Yellow);
                        ConsoleHelper.Write("|", ConsoleColor.DarkBlue);
                        Console.SetCursorPosition(4, 6);
                        ConsoleHelper.WriteLine("================", ConsoleColor.DarkBlue);


                        Console.SetCursorPosition(4, 7);
                        Console.Write(new string(' ', 30));
                        Console.SetCursorPosition(4, 7);

                        Console.Write("Your name: ");
                        string username = Console.ReadLine();

                        ScoreBoard board = new();
                        Score score = new Score(game.GetScore(), stopwatch.Elapsed, username);
                        board.Save(score);
                    }

                    Thread.Sleep(1000);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);

                    Console.Clear();
                    break;
                }
            } while (true);

            
            
        }
    }
}
