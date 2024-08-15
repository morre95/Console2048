namespace Console2048
{
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
}
