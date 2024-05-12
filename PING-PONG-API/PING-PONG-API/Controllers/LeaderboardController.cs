using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PING_PONG_API.Domain.DTO;
using PING_PONG_API.Interfaces;
using PING_PONG_API.Models;

namespace PING_PONG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private ILeaderBoardService _leaderBoardService;

        public LeaderboardController(ILeaderBoardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;
        }

        [HttpPost("matches/create")]
        public async Task<ActionResult> CreateMatch([FromBody] MatchDTO match)
        {

            if (!await _leaderBoardService.RegisterNewMatchResultAsync(match))
                return BadRequest();

            return Ok();
        }

        [HttpGet("matches/all")]
        public async Task<ActionResult<List<Match>>> GetAllMatches()
        {

            var matches = await _leaderBoardService.GetAllMatchesAsync();

            if (matches is null)
                return BadRequest();

            return Ok(new());
        }

    }
}
