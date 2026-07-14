using OthelloAPI.DTOs;

namespace OthelloAPI.Services;

public interface IGameService
{
    GameResponse CreateNewGame();
    GameResponse MakeMove(int row, int column);
}
