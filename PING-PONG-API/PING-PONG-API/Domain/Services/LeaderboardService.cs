using PING_PONG_API.Domain.DTO;
using PING_PONG_API.Interfaces;
using PING_PONG_API.Models;
using System.Diagnostics;

namespace PING_PONG_API.Domain.Services
{
    public class LeaderboardService : ILeaderBoardService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ILogger<LeaderboardService> _logger;

        public LeaderboardService(IMatchRepository matchRepository, ILogger<LeaderboardService> logger)
        {
            _matchRepository = matchRepository;
            _logger = logger;
        }

        public async Task<bool> RegisterNewMatchResultAsync(MatchDTO matchDto)
        {
            try
            {
                _logger.LogInformation("Registering new match: {@matchDto}", matchDto);

                var match = new Match(matchDto);
                var isMatchInserted = await _matchRepository.InsertMatchResult(match);
                if (isMatchInserted)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during registration of match: {@match}", matchDto);
                throw;
            }
        }

        public async Task<List<Match>?> GetAllMatchesAsync()
        {
            try
            {
                var matches = await _matchRepository.GetAllMatches();

                if (matches is null)
                    return null;
                else
                    return matches;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when trying to fetch all matches");
                return null;
            }
        }


    }
}
