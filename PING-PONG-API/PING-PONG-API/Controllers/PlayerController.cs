using Microsoft.AspNetCore.Mvc;
using PING_PONG_API.Domain.DTO;
using PING_PONG_API.Interfaces;
using PING_PONG_API.Models;

namespace PING_PONG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderBoardService;

        public PlayerController(ILeaderBoardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Player>> RegisterPlayer([FromBody] PlayerDTO playerDto)
        {
            var player = await _leaderBoardService.RegisterPlayer(playerDto);
            if (player is null)
                return BadRequest();

            return Ok(player);  

        }



    }
}
