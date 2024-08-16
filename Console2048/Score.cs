namespace Console2048
{
    public class Score
    {
        public string Name { get; set; } 
        public int TotalScore {  get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public Score() { }

        public Score(int score, TimeSpan time, string name)
        {
            TotalScore = score;
            Time = time;
            Date = DateTime.Now; 
            Name = name;
        }
    }
}
