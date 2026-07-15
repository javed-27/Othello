#!/usr/bin/env bash

set -e

BACKEND_DIR="OthelloAPI"
FRONTEND_DIR="Frontend"
BACKEND_PORT=5028
FRONTEND_PORT=5173

cleanup() {
  echo ""
  echo "Shutting down..."
  if [ -n "$BACKEND_PID" ]; then
    kill "$BACKEND_PID" 2>/dev/null || true
    wait "$BACKEND_PID" 2>/dev/null || true
  fi
  echo "Done. Thanks for playing!"
}
trap cleanup EXIT INT TERM

echo "==================================="
echo "   Othello - Reversi Game"
echo "==================================="
echo ""

# Check prerequisites
echo "Checking prerequisites..."

if ! command -v dotnet &> /dev/null; then
  echo "Error: .NET SDK is not installed."
  echo "Download it from: https://dotnet.microsoft.com/download"
  exit 1
fi

if ! command -v node &> /dev/null; then
  echo "Error: Node.js is not installed."
  echo "Download it from: https://nodejs.org/"
  exit 1
fi

if ! command -v npm &> /dev/null; then
  echo "Error: npm is not installed."
  echo "It usually comes with Node.js: https://nodejs.org/"
  exit 1
fi

echo "All prerequisites found!"
echo ""

# Install backend dependencies
echo "Installing backend dependencies..."
dotnet restore "$BACKEND_DIR" --verbosity quiet
echo "Backend dependencies ready."
echo ""

# Install frontend dependencies
echo "Installing frontend dependencies..."
(cd "$FRONTEND_DIR" && npm install --silent)
echo "Frontend dependencies ready."
echo ""

# Start backend
echo "Starting backend server on port $BACKEND_PORT..."
dotnet run --project "$BACKEND_DIR" --no-build --verbosity quiet &
BACKEND_PID=$!

# Wait for backend to be ready
echo "Waiting for backend to start..."
for i in $(seq 1 30); do
  if curl -s "http://localhost:$BACKEND_PORT/api/health" > /dev/null 2>&1; then
    echo "Backend is ready!"
    break
  fi
  if [ "$i" -eq 30 ]; then
    echo "Error: Backend failed to start within 30 seconds."
    exit 1
  fi
  sleep 1
done
echo ""

# Start frontend
echo "Starting frontend on port $FRONTEND_PORT..."
echo ""
echo "==================================="
echo " Game is ready!"
echo " Open http://localhost:$FRONTEND_PORT"
echo " in your browser to play."
echo " Press Ctrl+C to stop."
echo "==================================="
echo ""

(cd "$FRONTEND_DIR" && npm run dev)
