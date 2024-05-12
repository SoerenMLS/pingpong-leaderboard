using Dapper;
using PING_PONG_API.Interfaces;
using PING_PONG_API.Models;
using System.Data;

namespace PING_PONG_API.Domain.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<PlayerRepository> _logger;

        public PlayerRepository(IDbConnection dbConnection, ILogger<PlayerRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        public async Task<bool> RegisterPlayer(Player player)
        {
            _logger.LogInformation("Inserting player with name: {playerName} and id: {playerId} into DB", player.Name, player.Id);

            try
            {
                var sql = "INSERT INTO Players (Id, Name, MatchesWon, MatchesLost) " +
                          "VALUES (@Id, @Name, @MatchesWon, @MatchesLost);";

                var rowsAffected = await _dbConnection.ExecuteAsync(sql, player);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during registration of player: {@player}", player);
                return false;
            }
        }

        public async Task<bool> UpdatePlayerLoss(string playerId)
        {
            _logger.LogInformation("Updating loss count for player ID: {playerId}", playerId);

            try
            {
                var sql = "UPDATE Players " +
                          "SET MatchesLost = MatchesLost + 1 " +
                          "WHERE Id = @PlayerId;";
                var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { PlayerId = playerId });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update loss count for player ID: {playerId}", playerId);
                return false;
            }
        }

        public async Task<bool> UpdatePlayerWin(string playerId)
        {
            _logger.LogInformation("Updating win count for player ID: {playerId}", playerId);

            try
            {
                var sql = "UPDATE Players " +
                          "SET MatchesWon = MatchesWon + 1 " +
                          "WHERE Id = @PlayerId;";
                var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { PlayerId = playerId });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update win count for player ID: {playerId}", playerId);
                return false;
            }
        }
    }
}
