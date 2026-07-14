import { useState, useEffect, useCallback } from 'react';
import Header from './components/Header';
import Board from './components/Board';
import ScoreBoard from './components/ScoreBoard';
import GameStatus from './components/GameStatus';
import { fetchNewGame, sendMove, checkHealth } from './services/api';
import type { GameResponse } from './types';
import './App.css';

function App() {
  const [game, setGame] = useState<GameResponse | null>(null);
  const [connected, setConnected] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const loadNewGame = useCallback(async () => {
    try {
      setError(null);
      const data = await fetchNewGame();
      setGame(data);
    } catch {
      setError('Failed to start new game');
    }
  }, []);

  useEffect(() => {
    checkHealth()
      .then((ok) => {
        setConnected(ok);
        if (ok) return loadNewGame();
      })
      .finally(() => setLoading(false));
  }, [loadNewGame]);

  const handleCellClick = async (row: number, col: number) => {
    if (!game || game.gameOver) return;
    try {
      const data = await sendMove({ row, column: col });
      setGame(data);
      setError(null);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Invalid move');
    }
  };

  const handleRestart = async () => {
    await loadNewGame();
    setError(null);
  };

  if (loading) {
    return <div className="app">Loading...</div>;
  }

  if (!connected) {
    return (
      <div className="app">
        <Header onRestart={() => {}} />
        <p className="connection-error">Backend Connection Failed</p>
      </div>
    );
  }

  return (
    <div className="app">
      <Header onRestart={handleRestart} />
      {game && (
        <>
          <ScoreBoard
            blackScore={game.blackScore}
            whiteScore={game.whiteScore}
            currentPlayer={game.currentPlayer}
          />
          <Board
            board={game.board}
            validMoves={game.validMoves}
            onCellClick={handleCellClick}
          />
          <GameStatus
            currentPlayer={game.currentPlayer}
            gameOver={game.gameOver}
            winner={game.winner}
            skippedTurn={game.skippedTurn}
          />
        </>
      )}
      {error && <div className="error-message">{error}</div>}
    </div>
  );
}

export default App;
