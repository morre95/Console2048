using System;
using System.Diagnostics;

namespace Console2048
{
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

        public static void PrintScore(this List<Score> board)
        {
            if (board == null || board.Count == 0) 
            {
                ConsoleHelper.WriteLine("There is no scores to display.", ConsoleColor.Yellow);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                return; 
            }

            int padding = 30;
            ConsoleHelper.WriteLine(new string('=', padding), ConsoleColor.Yellow);

            int index = 0;
            foreach (Score score in board.OrderByDescending(x => x.TotalScore).Take(7))
            {
                
                int totalScore    = score.TotalScore;
                TimeSpan timeSpan = score.Time;
                DateTime date     = score.Date;
                string userName   = score.Name;
                
                ConsoleHelper.Write("|", ConsoleColor.Yellow);
                ConsoleHelper.Write($"--#{++index} {userName.PadRight(padding - 7, '-')}", ConsoleColor.Green);
                ConsoleHelper.WriteLine("|", ConsoleColor.Yellow);
                ConsoleHelper.Write("|", ConsoleColor.Yellow);
                ConsoleHelper.Write(" Score: ", ConsoleColor.Green);
                ConsoleHelper.Write(totalScore.ToString().PadRight(padding - 10), ConsoleColor.Green);
                ConsoleHelper.WriteLine("|", ConsoleColor.Yellow);
                ConsoleHelper.Write("|", ConsoleColor.Yellow);
                ConsoleHelper.Write(" Time: ", ConsoleColor.Cyan);
                ConsoleHelper.Write($" {timeSpan.ToStringMyFormat().PadRight(padding - 11)} ", ConsoleColor.Cyan);
                ConsoleHelper.WriteLine("|", ConsoleColor.Yellow);
                ConsoleHelper.Write("|", ConsoleColor.Yellow);
                ConsoleHelper.Write(" Date: ", ConsoleColor.Cyan);
                ConsoleHelper.Write($" {date.ToString("yyyy-MM-dd HH:mm").PadRight(padding - 11)} ", ConsoleColor.Cyan);
                ConsoleHelper.WriteLine("|", ConsoleColor.Yellow);

            }
            ConsoleHelper.WriteLine(new string('=', padding), ConsoleColor.Yellow);
            Console.ReadLine();
        }


    }
}
