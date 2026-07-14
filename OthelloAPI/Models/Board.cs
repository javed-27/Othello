namespace OthelloAPI.Models;

public class Board
{
    public Cell[,] Cells { get; set; } = new Cell[8, 8];
    
    public Board()
    {
        // Initialize empty board
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Cells[row, col] = new Cell();
            }
        }
    }
}