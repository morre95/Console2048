namespace Console2048
{
    public static class Helpers
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

        public static string ToStringMyFormat(this TimeSpan timeSpan)
        {
            string mil = timeSpan.Milliseconds.ToString("00");
            if (mil.Length == 2 ) mil = mil + "0";
            return timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + "." + mil;
        }
    }
}
