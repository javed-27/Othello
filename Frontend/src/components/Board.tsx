import Cell from './Cell';

interface BoardProps {
  board: string[][];
  validMoves: number[][];
  onCellClick: (row: number, col: number) => void;
}

function Board({ board, validMoves, onCellClick }: BoardProps) {
  const isValidMove = (row: number, col: number): boolean => {
    return validMoves.some(([r, c]) => r === row && c === col);
  };

  return (
    <div className="board">
      {board.map((row, rowIndex) => (
        <div key={rowIndex} className="board-row">
          {row.map((cell, colIndex) => (
            <Cell
              key={`${rowIndex}-${colIndex}`}
              value={cell}
              row={rowIndex}
              col={colIndex}
              isValidMove={isValidMove(rowIndex, colIndex)}
              onClick={onCellClick}
            />
          ))}
        </div>
      ))}
    </div>
  );
}

export default Board;
