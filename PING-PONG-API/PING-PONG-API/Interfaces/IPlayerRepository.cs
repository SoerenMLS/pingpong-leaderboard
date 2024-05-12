using PING_PONG_API.Models;

namespace PING_PONG_API.Interfaces
{
    public interface IPlayerRepository
    {
        Task<bool> RegisterPlayer(Player player);
        Task<bool> UpdatePlayerLoss(string playerId);
        Task<bool> UpdatePlayerWin(string playerId);
    }
}