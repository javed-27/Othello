namespace OthelloAPI.Models;

public class GameState
{
    public Board Board { get; set; } = new Board();
    public Player CurrentPlayer { get; set; } = Player.Black;
    public bool GameOver { get; set; }
}
