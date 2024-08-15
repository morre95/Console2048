using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Console2048
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IGame game = new Game();

            IGameLoop loop = new GameLoop();
            IMainInterface mainInterface = new MainInterface();
            mainInterface.StartGame(loop, game);
        }
    }

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

    public class Game : IGame
    {
        private static int Length = 4;
        private static int Width = 4;

        private static Cell[,] Cells = new Cell[Length, Width];

        private static Random rnd = new Random();

        private readonly int[] PossableNumber = [2, 4, 8, 16, 32, 64, 128, 256, 512, 1024];

        private int TotalScore = 0;

        public void PrintBoard()
        {
            Console.Write(@$"/----{string.Concat(Enumerable.Repeat(" ----", Length - 2))} ----\");
            for (int x = 0; x < Length; x++)
            {
                if (x != 0)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Console.Write(" ----");
                    }
                }

                Console.WriteLine();

                for (int y = 0; y < Width; y++)
                {
                    int? val = Cells[x, y].Value;
                    if (val != null)
                    {
                        Console.Write("|");
                        Helpers.GetCellColor(val).Write(val.ToString()!.PadLeft(4));
                    }
                    else
                        Console.Write("|    ");
                }

                Console.WriteLine("|");
            }
            Console.WriteLine(@$"\----{string.Concat(Enumerable.Repeat(" ----", Length - 2))} ----/");
        }

        public void PopulateCells()
        {
            for (int x = 0; x < Length; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    Cells[x, y] = new Cell(null, x, y);
                }
            }
        }

        public void AddRandomCell()
        {
            int x = rnd.Next(Length), y = rnd.Next(Width);
            int value = rnd.NextDouble() < 0.9 ? 2 : 4;

            while (Cells[x, y].Value != null)
            {
                x = rnd.Next(Length);
                y = rnd.Next(Width);
            }

            Cells[x, y] = new Cell(value, x, y);
        }

        public void DisplayStats(Stopwatch stopwatch)
        {
            int x = 11, y = 0, padding = 26;
            
            string msg = 
                $@"/--------------------------\" +
                $"\n|{$"Total score:{TotalScore}".PadRight(padding)}|\n" +
                $"|{$"Timer: {stopwatch.Elapsed}".PadRight(padding)}|\n" +
                $@"/--------------------------\";

            ConsoleHelper.WriteLine(msg, x, y);

        }

        public bool GameOver()
        {
            return !Cells.Cast<Cell>().Any(cell => cell.Value == null);
        }

        public bool Is2048()
        {
            return TotalScore >= 2048;
        }

        public void MakeMove(ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
            }
            UpdateCells();
            AddRandomCell();
        }

        private void MoveDown()
        {
            for (int x = 0; x < Length; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    if (x < Length - 1 && Cells[x, y].Value != null)
                    {
                        if (Cells[x + 1, y].Value != null && Array.IndexOf(PossableNumber, Cells[x, y].Value + Cells[x + 1, y].Value) != -1)
                        {
                            Cells[x + 1, y].Value = Cells[x, y].Value + Cells[x + 1, y].Value;
                            TotalScore += int.Parse((Cells[x, y].Value + Cells[x + 1, y].Value).ToString()!);
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        }
                        else if (Cells[x + 1, y].Value == null)
                        {
                            Cells[x + 1, y].Value = Cells[x, y].Value;
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        } 
                    }
                }
            }
        }

        private void MoveUp()
        {
            for (int x = Length - 1; x >= 0; x--)
            {
                for (int y = 0; y < Width; y++)
                {
                    if (x > 0 && Cells[x, y].Value != null)
                    {
                        if (Cells[x - 1, y].Value != null && Array.IndexOf(PossableNumber, Cells[x, y].Value + Cells[x - 1, y].Value) != -1)
                        {
                            Cells[x - 1, y].Value = Cells[x, y].Value + Cells[x - 1, y].Value;
                            TotalScore += int.Parse((Cells[x, y].Value + Cells[x - 1, y].Value).ToString()!);
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        }
                        else if (Cells[x - 1, y].Value == null)
                        {
                            Cells[x - 1, y].Value = Cells[x, y].Value;
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        } 
                    }
                }
            }
        }

        private void MoveRight()
        {
            for (int x = 0; x < Length; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    if (y < Width - 1 && Cells[x, y].Value != null)
                    {
                        if (Cells[x, y + 1].Value != null && Array.IndexOf(PossableNumber, Cells[x, y].Value + Cells[x, y + 1].Value) != -1)
                        {
                            Cells[x, y + 1].Value = Cells[x, y].Value + Cells[x, y + 1].Value;
                            TotalScore += int.Parse((Cells[x, y].Value + Cells[x, y + 1].Value).ToString()!);
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        }
                        else if (Cells[x, y + 1].Value == null)
                        {
                            Cells[x, y + 1].Value = Cells[x, y].Value;
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        } 
                    }
                }
            }
        }

        private void MoveLeft()
        {
            for (int x = 0; x < Length; x++)
            {
                for (int y = Width - 1; y >= 0; y--)
                {
                    if (y > 0 && Cells[x, y].Value != null)
                    {
                        if (Cells[x, y - 1].Value != null && Array.IndexOf(PossableNumber, Cells[x, y].Value + Cells[x, y - 1].Value) != -1)
                        {
                            Cells[x, y - 1].Value = Cells[x, y].Value + Cells[x, y - 1].Value;
                            TotalScore += int.Parse((Cells[x, y].Value + Cells[x, y - 1].Value).ToString()!);
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        }
                        else if (Cells[x, y - 1].Value == null)
                        {
                            Cells[x, y - 1].Value = Cells[x, y].Value;
                            Cells[x, y].Value = null;
                            Cells[x, y].Erase();
                        } 
                    }
                }
            }
        }

        private void UpdateCells()
        {
            for (int x = 0; x < Length; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    Cells[x, y].Print(Cells[x, y].Value);
                }
            }
        }

        public void ResetScore()
        {
            TotalScore = 0;
        }
    }

    public class Helpers
    {
        public static ConsoleColor GetCellColor(int? value)
        {
            switch (value)
            {
                case 2:
                    return ConsoleColor.Blue;
                case 4:
                    return ConsoleColor.Magenta;
                case 8:
                    return ConsoleColor.Cyan;
                case 16:
                    return ConsoleColor.Green;
                case 32:
                    return ConsoleColor.Yellow;
                case 64:
                    return ConsoleColor.DarkBlue;
                case 128:
                    return ConsoleColor.DarkMagenta;
                case 256:
                    return ConsoleColor.DarkCyan;
                case 512:
                    return ConsoleColor.DarkGreen;
                case 1024:
                    return ConsoleColor.DarkYellow;
                default:
                    return ConsoleColor.Red;
            }
        }
    }

    public class Cell(int? value, int x, int y)
    { 
        public int? Value { get; set; } = value;
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public void Print(int? value)
        {
            if (value == null) return;

            int oldTop = Console.CursorTop;
            int oldLeft = Console.CursorLeft;

            Console.SetCursorPosition((Y) * 5 + 1, (X) * 2 + 1);
            Helpers.GetCellColor(value).Write(value.ToString()!.PadLeft(4));
            Value = value;

            Console.CursorTop = oldTop;
            Console.CursorLeft = oldLeft;
        }

        public void Erase()
        {
            int oldTop = Console.CursorTop;
            int oldLeft = Console.CursorLeft;

            Console.SetCursorPosition((Y) * 5 + 1, (X) * 2 + 1);
            Console.Write("    ");
            Value = null;

            Console.CursorTop = oldTop;
            Console.CursorLeft = oldLeft;
        }
    }

    public static class ConsoleHelper
    {
        public static void WriteLine(string message, ConsoleColor color = ConsoleColor.Red)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLine(this ConsoleColor color, string message)
        {
            WriteLine(message, color);
        }

        public static void Write(string message, ConsoleColor color = ConsoleColor.Red)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = oldColor;
        }
        
        public static void Write(this ConsoleColor color, string message)
        {
            Write(message, color);
        }

        public static void WriteLine(string message, int X, int Y)
        {
            int oldTop = Console.CursorTop;
            int oldLeft = Console.CursorLeft;

            Console.SetCursorPosition(Y, X);
            Console.WriteLine(message);

            Console.CursorTop = oldTop;
            Console.CursorLeft = oldLeft;
        }
    }

    public interface IMainInterface
    {
        void StartGame(IGameLoop gameLoop, IGame game);
    }

    public interface IGameLoop
    {
        void Run(IGame game);
    }

    public interface IGame
    {
        void PopulateCells();
        void PrintBoard();
        void AddRandomCell();
        void DisplayStats(Stopwatch stopwatch);
        void MakeMove(ConsoleKeyInfo cki);
        bool GameOver();
        bool Is2048();
        void ResetScore();
    }

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
