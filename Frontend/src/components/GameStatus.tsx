interface GameStatusProps {
  currentPlayer: string;
  gameOver: boolean;
  winner: string | null;
  skippedTurn: boolean;
}

function GameStatus({ currentPlayer, gameOver, winner, skippedTurn }: GameStatusProps) {
  if (gameOver) {
    return (
      <div className="game-status game-over">
        Game Over — {winner === 'Draw' ? "It's a draw!" : `${winner} wins!`}
      </div>
    );
  }

  return (
    <div className="game-status">
      {skippedTurn && <div className="skip-message">No valid moves — turn skipped!</div>}
      <div>Current Player: <strong>{currentPlayer}</strong></div>
    </div>
  );
}

export default GameStatus;
