using Dapper;
using System.Data;
using PING_PONG_API.Models;
using PING_PONG_API.Interfaces;

namespace PING_PONG_API.Domain.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IDbConnection _databaseConnection;
        private readonly ILogger _logger;

        public MatchRepository(IDbConnection databaseConnection, ILogger<MatchRepository> logger)
        {
            _databaseConnection = databaseConnection;
            _logger = logger;
        }

        public async Task<List<Match>?> GetAllMatches()
        {
            try
            {
                var sql = "SELECT * FROM Matches;";
                var matches = await _databaseConnection.QueryAsync<Match>(sql);

                if (matches is null)
                {
                    _logger.LogWarning("Failed fetch any matches, matches is null");
                    return null;
                }

                return matches.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve matches from the database.");
                return null;  // Return an empty list on failure
            }
        }

        public async Task<bool> InsertMatchResult(Match match)
        {
            try
            {
                var sql = "INSERT INTO Matches (WinnerId, LoserId, Score) " +
                          "VALUES (@WinnerId, @LoserId, @Score);";

                var rowsAffected = await _databaseConnection.ExecuteAsync(sql, match);

                if (rowsAffected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Something went wrong during insertion of match between player: {playerId1} and {playerId2}", match.WinnerId, match.LoserId);
                return false;
            }
        }
    }
}
