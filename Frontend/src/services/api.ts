import type { GameResponse, MoveRequest } from '../types';

const BASE_URL = 'http://localhost:5028';

export async function fetchNewGame(): Promise<GameResponse> {
  const response = await fetch(`${BASE_URL}/api/game/new`);
  if (!response.ok) throw new Error('Failed to create new game');
  return response.json();
}

export async function sendMove(move: MoveRequest): Promise<GameResponse> {
  const response = await fetch(`${BASE_URL}/api/game/move`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(move),
  });
  if (!response.ok) {
    const err = await response.json();
    throw new Error(err.error || 'Invalid move');
  }
  return response.json();
}

export async function checkHealth(): Promise<boolean> {
  const response = await fetch(`${BASE_URL}/api/health`);
  if (!response.ok) return false;
  const data = await response.json();
  return data.status === 'OK';
}
