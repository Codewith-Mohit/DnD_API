# DnD API: Minimal Viable Backend

A minimal, extensible backend for a Dungeons & Dragons–inspired session runner. This repository is designed to be drop-in runnable for rapid MVP testing and incrementally expandable for future feature development.

The project emphasizes a scalable, modular design for eventual migration to a production database, stateful architecture, and feature-rich APIs.

---

## ✨ Features (Minimum Viable Product)

| Feature | Description | Status |
| :--- | :--- | :--- |
| **Character Management** | Standard **CRUD** operations for character entities. | Implemented |
| **Runs** | Per-character dungeon runs with simple exploration and encounters. | Implemented |
| **Dice Rolling** | Flexible dice formulas (e.g., `2d6+1`) with rich, deterministic results. | Implemented |
| **Dungeon Orchestration** | Lightweight, deterministic MVP dungeon generator. | Implemented |
| **Persistence** | **In-memory** storage (EF Core InMemory) for rapid testing. | Implemented |

---

## 🛠️ Tech Stack

This project is built using modern ASP.NET Core practices. No external dependencies are required to run the MVP.

* **Framework:** .NET 9+ (ASP.NET Core)
* **Language:** C#
* **Database:** EF Core (InMemory Provider)
* **Architecture:** Minimal APIs / Controllers (implemented)

---

## 🚀 Getting Started

### Prerequisites

* .NET 9.0+ SDK
* Git 

### Install & Run

1.  **Clone the repo:**
    ```bash
    git clone https://github.com/Codewith-Mohit/DnD_API.git
    cd DnD_API
    ```

2.  **Restore dependencies and run the application:**
    ```bash
    dotnet restore
    dotnet run
    ```

3.  The API will start, typically on `http://localhost:5251` or `https://localhost:7106`.
   
     Open the displayed URL in your browser to view the **Swagger/OpenAPI** documentation for immediate testing.

---

## 🎯 API Overview

Endpoints are implemented using ASP.NET Core (Minimal APIs/Controllers).

| Feature | Method | Endpoint Example | Description |
| :--- | :--- | :--- | :--- |
| **Characters** | CRUD | `/characters` | Standard create, read, update, delete for game characters. |
| **Dice Roll** | `POST` | `/dice/roll` | Rolls dice based on formula. Body: `{"formula": "2d6+1", "seed": 42}` |
| **New Run** | `POST` | `/runs` | Creates a new dungeon run. Body: `{"characterId": "<ID>", "seed": 123}` |
| **Explore** | `POST` | `/runs/{id}/explore` | Advances the run to the next room/event. |
| **Encounter** | `POST` | `/runs/{id}/encounter/roll` | Resolves a combat turn. |

### Quick Start Snippets

You can test these endpoints using Swagger UI or a tool like Postman:

| Action | HTTP Method | URL Path (Example) | Sample Body |
| :--- | :--- | :--- | :--- |
| **Dice Roll** | `POST` | `/dice/roll` | `{"formula": "2d6+1", "seed": 42}` |
| **Create Run** | `POST` | `/runs` | `{"characterId": "GUID_HERE", "seed": 123}` |
| **Explore** | `POST` | `/runs/<RUN_ID>/explore` | *Empty body* |
| **Resolve Turn** | `POST` | `/runs/<RUN_ID>/encounter/roll` | `{"Formula": "1d20+3"}` |

---

## 📂 Project Structure

This structure follows a typical clean architecture pattern, organizing logic and models distinctly.
DnD_API/ 
├── Controllers/ # API endpoints (may be converted to Minimal APIs) 
│ ├── CharacterController.cs
│ ├── DiceController.cs
│ └── RunController.cs
├── Data/ 
│ └── DnDDbContext.cs # EF Core DbContext with InMemory setup 
├── Models/ # Core business entities 
│ ├── Character.cs 
│ ├── Item.cs 
│ ├── Run.cs 
│ └── Room.cs, Enemy.cs, etc. 
├── DTOs/ # Data Transfer Objects (Request/Response models) 
│ └── CharacterCreateDto.cs 
│ └── DiceRollRequest.cs, 
│ └── RunCreateDto.cs
│ └── RunExploreRequest.cs, etc. 
└── Services/ 
│ └──── Interfaces/ # Service contracts for Dependency Injection         
│ │ └──── ICharacterServices.cs
│ │ └──── IDiceService.cs
│ │ └──── IRunStore.cs
│ │ └──── IDungeonService.cs, etc. 
│ └─ CharaterService.cs
│ └─ DiceService
│ └─ DungeonService
│ └─ RunService
└──────────────

---

## ➡️ Extending the MVP

This repository is designed for easy enhancement:

* **Persistence:** Replace the `InMemory` provider with a persistent database (SQLite, PostgreSQL, SQL Server).
* **Combat:** Implement full combat mechanics (initiative, multi-turn encounters, status effects, death conditions).
* **Authentication:** Add authentication and authorization for per-user runs and security.
* **World:** Integrate richer dungeon generation (graph of rooms, dynamic enemy stats, loot, traps).

---

## 🧪 Testing

The design facilitates easy unit testing.

| Component | Suggested Tests |
| :--- | :--- |
| **`DiceService.Roll`** | Test various formulas (`XdY+Z`, `XdY`, `dY`, etc.) to ensure deterministic results and correct breakdowns. Test edge cases like critical rolls. |
| **`RunService.ResolveEncounter`** | Verify correct Hit/Miss calculation, damage application, and enemy HP update logic. |
| **Integration Tests** | (Optional) Use `WebApplicationFactory` to spin up an in-memory host and hit actual API endpoints to ensure the full request pipeline works. |

---

## ⚠️ Known Limitations & Changelog

* **MVP Persistence:** Uses **In-memory EF Core**; all data is lost on application shutdown. ( as Stateless services are required.)
* **Dice Logic:** Roll parsing is intentionally lightweight; supports common forms (`XdY+Z`, `XdY`, `XdY-Z`).
* **Combat:** MVP combat features a single dummy enemy per encounter; can be in future.. extension is required for complex battles.
* **Security:** **No authentication** or authorization implemented yet.

---

**Guidelines:**
* Write **tests** for business logic (`RunService`, `DiceService`).
* Keep code modular and DI-friendly.
* Document public APIs and expected payloads where necessary.
