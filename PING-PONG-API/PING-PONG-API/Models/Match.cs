using PING_PONG_API.Domain.DTO;

namespace PING_PONG_API.Models
{
    public class Match
    {
        public string? Id { get; set; }
        public string? WinnerId { get; set; }
        public string? LoserId { get; set; }
        public string? Score { get; set; }  // Format: "11-9", "11-3", etc.

        public Match() { }
        public Match(MatchDTO matchDTO) 
        { 
            Id = Guid.NewGuid().ToString();
            WinnerId = matchDTO.WinnerId;
            LoserId = matchDTO.LoserId;
            Score = matchDTO.Score;
        }

        public Match(string id, string winnerId, string loserId, string? score)
        {
            Id = id;
            WinnerId = winnerId;
            LoserId = loserId;
            Score = score;
        }
    }
}
