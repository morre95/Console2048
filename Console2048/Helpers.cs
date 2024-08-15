namespace Console2048
{
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
}
