using OthelloAPI.Models;

namespace OthelloAPI.Services;

public class GameService : IGameService
{
    public GameState CreateNewGame()
    {
        var gameState = new GameState();
        
        // Set up initial board position
        // The starting layout is:
        // ........
        // ........
        // ........
        // ...WB...
        // ...BW...
        // ........
        // ........
        // ........

        gameState.Board.Cells[3, 3].Value = Player.White;
        gameState.Board.Cells[3, 4].Value = Player.Black;
        gameState.Board.Cells[4, 3].Value = Player.Black;
        gameState.Board.Cells[4, 4].Value = Player.White;
        
        return gameState;
    }
}