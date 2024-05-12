using Dapper;
using System.Data;
using PING_PONG_API.Models;
using PING_PONG_API.Interfaces;

namespace PING_PONG_API.Domain.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;

        public MatchRepository(IDbConnection databaseConnection, ILogger<MatchRepository> logger)
        {
            _dbConnection = databaseConnection;
            _logger = logger;
        }

        public async Task<List<Match>?> GetAllMatches()
         {
            try
            {
                var sql = "SELECT * FROM Matches;";
                var matches = await _dbConnection.QueryAsync<Match>(sql);

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
                return null;
            }
        }

        public async Task<bool> InsertMatchResult(Match match)
        {
            try
            {
                var sql = "INSERT INTO Matches (Id, WinnerId, LoserId, Score) " +
                          "VALUES (@Id, @WinnerId, @LoserId, @Score);";

                var rowsAffected = await _dbConnection.ExecuteAsync(sql, match);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Something went wrong during insertion of match between player: {playerId1} and {playerId2}", match.WinnerId, match.LoserId);
                return false;
            }
        }
    }
}
