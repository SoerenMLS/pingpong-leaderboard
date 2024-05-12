namespace PING_PONG_API.Models
{
    public class Player
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesLost { get; set; }

        public Player(string id, string name, int matchesWon, int matchesLost)
        {
            Id = id;
            Name = name;
            MatchesWon = matchesWon;
            MatchesLost = matchesLost;
        }
        public Player() { }
    }
}
