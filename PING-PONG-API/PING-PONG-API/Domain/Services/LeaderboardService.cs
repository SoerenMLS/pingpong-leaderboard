using PING_PONG_API.Domain.DTO;
using PING_PONG_API.Domain.Repositories;
using PING_PONG_API.Interfaces;
using PING_PONG_API.Models;
using System.Diagnostics;

namespace PING_PONG_API.Domain.Services
{
    public class LeaderboardService : ILeaderBoardService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<LeaderboardService> _logger;

        public LeaderboardService(IMatchRepository matchRepository, ILogger<LeaderboardService> logger, IPlayerRepository playerRepository)
        {
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<bool> RegisterNewMatchResultAsync(MatchDTO matchDto)
        {
            try
            {
                _logger.LogInformation("Registering new match: {@matchDto}", matchDto);

                var match = new Match(matchDto);
                var isMatchInserted = await _matchRepository.InsertMatchResult(match);

                if (!isMatchInserted)
                    return false;

                var isPlayerLossUpdated = await _playerRepository.UpdatePlayerLoss(matchDto.LoserId!);
                var isPlayerWinUpdated = await _playerRepository.UpdatePlayerWin(matchDto.WinnerId!);

                if (!(isPlayerLossUpdated && isPlayerWinUpdated))
                    return false;

                _logger.LogInformation("Successfully registred match: {matchId}", match.Id);

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during registration of match: {@match}", matchDto);
                throw;
            }
        }

        public async Task<List<Player>?> GetAllPlayersAsync()
        {
            try
            {
                var players = await _playerRepository.GetAllPlayers();

                if(players is null)
                {
                    _logger.LogWarning("Failed to get players, players is null...");
                    return null;
                }

                return players;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all players");
                return null;
            }
        }

        public async Task<List<Match>?> GetAllMatchesAsync()
        {
            try
            {
                var matches = await _matchRepository.GetAllMatches();

                if (matches is null)
                {
                    _logger.LogWarning("Failed to get matches, matches is null...");
                    return null;
                }

                return matches;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when trying to fetch all matches");
                return null;
            }
        }

        public async Task<Player?> RegisterPlayer(PlayerDTO playerDto)
        {
            try
            {
                var player = new Player(playerDto);

                if (!await _playerRepository.RegisterPlayer(player))
                    return null;

                return player;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during registration of player: {@playerDto}", playerDto);
                return null;
            }
        }
    }
}
