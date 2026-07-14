using Microsoft.AspNetCore.Mvc;
using OthelloAPI.Models;

namespace OthelloAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    [HttpGet("new")]
    public IActionResult NewGame()
    {
        // Create new game board with initial position
        var board = new Cell[8, 8];
        
        // Initialize empty board
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                board[row, col] = new Cell();
            }
        }
        
        // Set up initial position with the correct starting layout
        // The starting layout is:
        // ........
        // ........
        // ........
        // ...WB...
        // ...BW...
        // ........
        // ........
        // ........

        board[3, 3].Value = Player.White;
        board[3, 4].Value = Player.Black;
        board[4, 3].Value = Player.Black;
        board[4, 4].Value = Player.White;
        
        return Ok(new
        {
            board = board,
            currentPlayer = "Black"
        });
    }
}