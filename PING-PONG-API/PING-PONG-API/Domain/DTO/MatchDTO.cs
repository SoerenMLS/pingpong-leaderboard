namespace PING_PONG_API.Domain.DTO
{
    public class MatchDTO
    {
        public string? WinnerId { get; set; }
        public string? LoserId { get; set; }
        public string? Score { get; set; }  // Format: "11-9", "11-3", etc.

    }
}
