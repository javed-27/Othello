# Othello (Reversi) Project Specification

## Objective

Develop a complete Othello (Reversi) game using:

- Backend: ASP.NET Core 8 Web API (C#)
- Frontend: React + TypeScript + Vite

The application will run locally only.

There is no multiplayer, authentication, or database.

The game should be developed incrementally. Never generate the entire project in one response. Complete one milestone before moving to the next.

---

# Development Rules

Follow these rules throughout development.

- Write clean and readable code.
- Keep files small.
- Use meaningful names.
- Follow SOLID principles where appropriate.
- Separate business logic from UI.
- Never put game logic inside React components.
- All game rules belong in the backend.
- React is only responsible for displaying the board and sending user actions.

---

# Tech Stack

## Backend

- ASP.NET Core 8 Web API
- C#
- Swagger enabled
- Dependency Injection

## Frontend

- React
- TypeScript
- Vite
- CSS Modules (or plain CSS)
- Fetch API

---

# Folder Structure

Backend

```
Backend
│
├── Controllers
├── Models
├── Services
├── DTOs
├── Program.cs
└── Properties
```

Frontend

```
Frontend
│
├── src
│   ├── components
│   ├── services
│   ├── types
│   ├── App.tsx
│   └── main.tsx
```

---

# Milestone Based Development

Only work on one milestone at a time.

Do not continue automatically.

Wait until the current milestone is finished.

---

# Milestone 1

Create the backend project.

Requirements

- ASP.NET Core 8 Web API
- Enable Swagger
- Configure CORS
- Create a health endpoint

```
GET /api/health
```

Response

```json
{
    "status":"OK"
}
```

---

# Milestone 2

Create the React project.

Requirements

- Vite
- TypeScript
- Home page
- Title

```
Othello
```

Show

```
Backend Connected
```

after successfully calling

```
GET /api/health
```

---

# Milestone 3

Create Board Model

Backend should expose

```
GET /api/game/new
```

Returns

```json
{
  "board":[
      ...
  ],
  "currentPlayer":"Black"
}
```

Board is

8x8

Initial layout

```
........
........
........
...WB...
...BW...
........
........
........
```

---

# Milestone 4

Frontend Board

Render

- 8x8 board
- Green cells
- Black discs
- White discs

No interaction yet.

---

# Milestone 5

Board Click

User clicks a square.

Frontend sends

```
POST /api/game/move
```

Backend validates

If invalid

Return

```
400 Bad Request
```

If valid

Return updated board.

---

# Milestone 6

Implement Rules

Backend must implement

- Valid move detection
- Disc flipping
- Turn switching
- Skip turn when needed
- Game over detection

All rules follow official Othello rules.

---

# Milestone 7

Frontend Updates

Display

- Current player
- Black score
- White score
- Game over message
- Winner

---

# Milestone 8

UI Improvements

Add

- Hover effect
- Valid move highlighting
- Responsive board
- Restart button

---

# API Endpoints

## Health

GET

```
/api/health
```

---

## New Game

GET

```
/api/game/new
```

---

## Make Move

POST

```
/api/game/move
```

Request

```json
{
    "row":2,
    "column":3
}
```

---

Response

```json
{
    "board":[...],
    "currentPlayer":"White",
    "blackScore":4,
    "whiteScore":3,
    "gameOver":false
}
```

---

# Coding Standards

- Use async methods.
- Use DTOs.
- Use dependency injection.
- Keep controllers thin.
- Put game logic inside services.
- No duplicated code.
- No magic numbers.
- Use enums where appropriate.

---

# Frontend Standards

Components should be small.

Example

```
Board
Cell
Disc
ScoreBoard
GameStatus
Header
```

Use TypeScript interfaces for all API responses.

Do not hardcode backend URLs.

Create

```
api.ts
```

for all HTTP requests.

---

# Definition of Done

Each milestone is complete only if

- Project builds
- No compiler warnings
- No runtime errors
- Code is formatted
- Feature works correctly

After finishing one milestone, stop and wait for the next instruction instead of implementing additional milestones.