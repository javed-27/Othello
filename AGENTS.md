# AGENTS.md

## Project Overview

Othello (Reversi) game — two independent projects in one repo:

- **OthelloAPI/** — ASP.NET Core 10 Web API (C#), game logic lives here
- **Frontend/** — React 18 + TypeScript + Vite, display only

No database, no auth, no multiplayer. Local-only.

## Dev Commands

**Backend** (from `OthelloAPI/`):
```sh
dotnet run        # runs on http://localhost:5028
dotnet build      # build only
```

**Frontend** (from `Frontend/`):
```sh
npm run dev       # Vite dev server
npm run build     # tsc && vite build
```

Both must be running simultaneously for the app to work.

## Key Architecture Facts

- Backend port is **5028** (hardcoded in `Frontend/src/services/api.ts:3` and `OthelloAPI/Properties/launchSettings.json`)
- Game state is held in-memory via a **singleton** `GameService` (`Program.cs:15`) — restarting the backend resets all games
- OpenAPI/Swagger is enabled in development mode only (`Program.cs:23-26`)
- CORS allows all origins (`Program.cs:8-13`)
- `.csproj` targets **net10.0**, not net8.0 — the `Plan.md` spec is outdated on this point

## Code Organization Rules (from Plan.md)

- **All game rules belong in backend services** — never put game logic in React components
- Backend: Controllers (thin) → Services (logic) → Models/DTOs
- Frontend: components display state, `services/api.ts` handles HTTP, `types/index.ts` has interfaces
- Use DTOs for all API request/response shapes
- Use dependency injection for services

## Frontend Conventions

- API responses typed via `GameResponse` interface in `Frontend/src/types/index.ts`
- All HTTP calls go through `Frontend/src/services/api.ts` — no fetch calls elsewhere
- Component tree: `App → Header, ScoreBoard, Board (→ Cell), GameStatus`
- CSS files co-located with components (plain CSS, not CSS modules)

## Definition of Done (per milestone)

Project builds with no compiler warnings, no runtime errors, code is formatted, feature works. Stop after each milestone — do not auto-continue.

## No Tests / No CI

There are no test suites, no linting config, and no CI pipelines. Verification is manual: build both projects and confirm the feature works end-to-end.
