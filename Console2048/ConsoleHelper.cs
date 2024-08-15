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
    }
}
