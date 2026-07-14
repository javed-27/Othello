using OthelloAPI.Models;

namespace OthelloAPI.Services;

public interface IGameService
{
    GameState CreateNewGame();
}