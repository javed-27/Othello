export interface GameResponse {
  board: string[][];
  currentPlayer: string;
  blackScore: number;
  whiteScore: number;
  gameOver: boolean;
  winner: string | null;
  skippedTurn: boolean;
  validMoves: number[][];
}

export interface MoveRequest {
  row: number;
  column: number;
}
