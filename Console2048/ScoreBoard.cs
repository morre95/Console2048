using System.Text.Json;
using System.Text.Json.Serialization;

namespace Console2048
{
    public class ScoreBoard
    {
        public List<Score> Repository { get; set; } = new();

        [JsonIgnore]
        private string _fileName = "scoreBoard.json";

        public List<Score> Load()
        {
            string jsonString = File.ReadAllText(_fileName);
            List<Score> repo = JsonSerializer.Deserialize<List<Score>>(jsonString);
            if (repo == null) return Repository;
            Repository = repo;
            return Repository;
        }

        public void Save(Score score)
        {
            Load();
            Repository.Add(score);
            Save();
        }
        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(Repository);
            File.WriteAllText(_fileName, jsonString);
        }
    }
}
