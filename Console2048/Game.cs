using System.Diagnostics;

namespace Console2048
{
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
            Debug.WriteLine(rnd.NextDouble());
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
            int x = 11, y = 0, padding = 21;
            
            string msg = 
                $@"/{new string('-', padding)}\" +
                $"\n|{$"Total score:{TotalScore}".PadRight(padding)}|\n" +
                $"|{$"Timer: {stopwatch.Elapsed.ToStringMyFormat()}".PadRight(padding)}|\n" +
                $@"/{new string('-', padding)}\";

            ConsoleHelper.WriteLine(msg, x, y);

        }

        public bool GameOver()
        {
            // TODO: Det bör köras en kontroll om det finns drag som kan göras för att fria upp utrymme
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
                default:
                    return;
            }
            AddRandomCell();
            UpdateCells();
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
                for (int y = Width - 1; y >= 0; y--)
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
            for (int x = Length - 1; x >= 0; x--)
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
                    int? value = Cells[x, y].Value;
                    if (value == null)
                    {
                        Cells[x, y].Erase();
                    } 
                    else
                    {
                        Cells[x, y].Print(value);
                    }
                }
            }
        }

        public void ResetScore()
        {
            TotalScore = 0;
        }

        public int GetScore()
        {
            return TotalScore;
        }
    }
}
