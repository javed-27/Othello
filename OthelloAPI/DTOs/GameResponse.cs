namespace OthelloAPI.DTOs;

public class GameResponse
{
    public string[][] Board { get; set; } = Array.Empty<string[]>();
    public string CurrentPlayer { get; set; } = string.Empty;
    public int BlackScore { get; set; }
    public int WhiteScore { get; set; }
    public bool GameOver { get; set; }
    public string? Winner { get; set; }
    public bool SkippedTurn { get; set; }
    public List<int[]> ValidMoves { get; set; } = new();
}
