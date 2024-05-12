using PING_PONG_API.Models;

namespace PING_PONG_API.Interfaces
{
    public interface IMatchRepository
    {
        Task<bool> InsertMatchResult(Match match);
        Task<List<Match>?> GetAllMatches();
    }
}
