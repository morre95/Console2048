using System.Diagnostics;

namespace Console2048
{
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
}
