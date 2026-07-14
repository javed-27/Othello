using OthelloAPI.DTOs;
using OthelloAPI.Models;

namespace OthelloAPI.Services;

public class GameService : IGameService
{
    private GameState _state = null!;
    private const int BoardSize = 8;

    private static readonly (int Row, int Col)[] Directions = new[]
    {
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),          (0, 1),
        (1, -1),  (1, 0),  (1, 1)
    };

    public GameResponse CreateNewGame()
    {
        _state = new GameState();

        _state.Board.Cells[3, 3].Value = Player.White;
        _state.Board.Cells[3, 4].Value = Player.Black;
        _state.Board.Cells[4, 3].Value = Player.Black;
        _state.Board.Cells[4, 4].Value = Player.White;

        return BuildResponse(false);
    }

    public GameResponse MakeMove(int row, int column)
    {
        if (_state.GameOver)
            throw new InvalidOperationException("Game is already over.");

        if (!IsWithinBounds(row, column))
            throw new ArgumentException("Move is out of bounds.");

        if (_state.Board.Cells[row, column].Value != null)
            throw new ArgumentException("Cell is already occupied.");

        var flippedDiscs = GetFlippedDiscs(row, column, _state.CurrentPlayer);

        if (flippedDiscs.Count == 0)
            throw new ArgumentException("Invalid move: no discs would be flipped.");

        _state.Board.Cells[row, column].Value = _state.CurrentPlayer;

        foreach (var (r, c) in flippedDiscs)
        {
            _state.Board.Cells[r, c].Value = _state.CurrentPlayer;
        }

        _state.CurrentPlayer = _state.CurrentPlayer == Player.Black ? Player.White : Player.Black;

        bool skippedTurn = false;
        if (!HasAnyValidMoves(_state.CurrentPlayer))
        {
            _state.CurrentPlayer = _state.CurrentPlayer == Player.Black ? Player.White : Player.Black;
            skippedTurn = true;
        }

        bool gameOver = !HasAnyValidMoves(_state.CurrentPlayer) || IsBoardFull();

        if (gameOver)
        {
            _state.GameOver = true;
        }

        return BuildResponse(skippedTurn);
    }

    private List<(int Row, int Col)> GetFlippedDiscs(int row, int col, Player player)
    {
        var flipped = new List<(int, int)>();

        foreach (var (dr, dc) in Directions)
        {
            var candidates = new List<(int, int)>();
            int r = row + dr;
            int c = col + dc;

            while (IsWithinBounds(r, c) && _state.Board.Cells[r, c].Value != null
                   && _state.Board.Cells[r, c].Value != player)
            {
                candidates.Add((r, c));
                r += dr;
                c += dc;
            }

            if (candidates.Count > 0 && IsWithinBounds(r, c)
                && _state.Board.Cells[r, c].Value == player)
            {
                flipped.AddRange(candidates);
            }
        }

        return flipped;
    }

    private bool HasAnyValidMoves(Player player)
    {
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (_state.Board.Cells[row, col].Value == null
                    && GetFlippedDiscs(row, col, player).Count > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsBoardFull()
    {
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (_state.Board.Cells[row, col].Value == null)
                    return false;
            }
        }
        return true;
    }

    private bool IsWithinBounds(int row, int col)
    {
        return row >= 0 && row < BoardSize && col >= 0 && col < BoardSize;
    }

    private List<int[]> GetValidMoves(Player player)
    {
        var moves = new List<int[]>();
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (_state.Board.Cells[row, col].Value == null
                    && GetFlippedDiscs(row, col, player).Count > 0)
                {
                    moves.Add(new[] { row, col });
                }
            }
        }
        return moves;
    }

    private (int Black, int White) CountDiscs()
    {
        int black = 0, white = 0;
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (_state.Board.Cells[row, col].Value == Player.Black) black++;
                else if (_state.Board.Cells[row, col].Value == Player.White) white++;
            }
        }
        return (black, white);
    }

    private string? DetermineWinner()
    {
        var (black, white) = CountDiscs();
        if (black > white) return "Black";
        if (white > black) return "White";
        return "Draw";
    }

    private GameResponse BuildResponse(bool skippedTurn)
    {
        var (black, white) = CountDiscs();
        var validMoves = GetValidMoves(_state.CurrentPlayer);

        return new GameResponse
        {
            Board = ConvertBoard(),
            CurrentPlayer = _state.CurrentPlayer.ToString(),
            BlackScore = black,
            WhiteScore = white,
            GameOver = _state.GameOver,
            Winner = _state.GameOver ? DetermineWinner() : null,
            SkippedTurn = skippedTurn,
            ValidMoves = validMoves
        };
    }

    private string[][] ConvertBoard()
    {
        var result = new string[BoardSize][];
        for (int row = 0; row < BoardSize; row++)
        {
            result[row] = new string[BoardSize];
            for (int col = 0; col < BoardSize; col++)
            {
                var cell = _state.Board.Cells[row, col].Value;
                result[row][col] = cell switch
                {
                    Player.Black => "B",
                    Player.White => "W",
                    _ => "."
                };
            }
        }
        return result;
    }
}
