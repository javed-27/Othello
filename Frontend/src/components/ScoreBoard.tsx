interface ScoreBoardProps {
  blackScore: number;
  whiteScore: number;
  currentPlayer: string;
}

function ScoreBoard({ blackScore, whiteScore, currentPlayer }: ScoreBoardProps) {
  return (
    <div className="scoreboard">
      <div className={`score black-score ${currentPlayer === 'Black' ? 'active' : ''}`}>
        <div className="score-disc black" />
        <span>Black: {blackScore}</span>
      </div>
      <div className={`score white-score ${currentPlayer === 'White' ? 'active' : ''}`}>
        <div className="score-disc white" />
        <span>White: {whiteScore}</span>
      </div>
    </div>
  );
}

export default ScoreBoard;
