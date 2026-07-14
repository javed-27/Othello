using Microsoft.AspNetCore.Mvc;
using OthelloAPI.DTOs;
using OthelloAPI.Services;

namespace OthelloAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("new")]
    public IActionResult NewGame()
    {
        var response = _gameService.CreateNewGame();
        return Ok(response);
    }

    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest request)
    {
        try
        {
            var response = _gameService.MakeMove(request.Row, request.Column);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
