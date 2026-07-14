interface CellProps {
  value: string;
  row: number;
  col: number;
  isValidMove: boolean;
  onClick: (row: number, col: number) => void;
}

function Cell({ value, row, col, isValidMove, onClick }: CellProps) {
  const isEmpty = value === '.';

  return (
    <div
      className={`cell ${isValidMove && isEmpty ? 'valid-move' : ''} ${isEmpty ? 'empty' : ''}`}
      onClick={() => onClick(row, col)}
    >
      {value === 'B' && <div className="disc black" />}
      {value === 'W' && <div className="disc white" />}
      {value === '.' && isValidMove && <div className="disc hint" />}
    </div>
  );
}

export default Cell;
