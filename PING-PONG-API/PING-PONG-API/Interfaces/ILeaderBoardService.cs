using PING_PONG_API.Domain.DTO;
using PING_PONG_API.Models;

namespace PING_PONG_API.Interfaces
{
    public interface ILeaderBoardService
    {
        Task<List<Match>?> GetAllMatchesAsync();
        Task<List<Player>?> GetAllPlayersAsync();
        Task<bool> RegisterNewMatchResultAsync(MatchDTO matchDto);
        Task<Player?> RegisterPlayer(PlayerDTO playerDto);
    }
}
