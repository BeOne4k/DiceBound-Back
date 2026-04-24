# 🎲 DiceBound Backend

Backend API for the **DiceBound RPG system**, built with **ASP.NET Core Web API**.  
Provides authentication, character management, items, enemies, bosses, missions and races.

---

## 🚀 Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- JWT Authentication
- AutoMapper
- Repository + Unit of Work pattern
- BCrypt password hashing
- Swagger (OpenAPI)

---

## 📦 Features

### 🔐 Authentication
- User registration (`/api/Auth/register`)
- User login (`/api/Auth/login`)
- JWT token-based authentication

---

### 🎮 Game System
- 👤 Character system (create, update, delete, list)
- 🧬 Races (base stats for characters)
- 👹 Enemies
- 👑 Bosses
- 🎯 Missions
- 🎒 Items

---

## 🔑 Authentication Flow

1. User registers via:

POST /api/Auth/register


2. User logs in:

POST /api/Auth/login


3. API returns JWT token:
{
"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
Token is used in requests:

Authorization: Bearer {token}

🧠 Architecture

Project uses Clean-ish architecture:


Controllers → Services → Repositories → EF Core → Database


Patterns:

Repository Pattern
Unit of Work
DTO mapping via AutoMapper
📁 Project Structure

DiceBound
│
├── Controllers
├── DTOs
├── Entities
├── Services
├── Interfaces
├── Repositories
├── Data (DbContext)
├── MappingProfiles
└── Program.c

⚙️ Setup & Run
1. Clone project
git clone https://github.com/your-repo/dicebound.git
2. Restore packages
dotnet restore
3. Apply migrations
dotnet ef database update
4. Run project
dotnet run
🌐 API Base URL
http://localhost:5292
Swagger UI:
http://localhost:5292/swagger


🔐 Security
Passwords hashed using BCrypt
JWT authentication for protected endpoints
Role-based access (planned / expandable)

📌 Example Endpoints
Auth
POST /api/Auth/register
POST /api/Auth/login
Characters
GET /api/Character
POST /api/Character
PUT /api/Character
DELETE /api/Character
Items / Enemies / Bosses
CRUD endpoints available under respective controllers


🧪 Example Character JSON
{
  "name": "Arthas",
  "userId": "guid",
  "raceId": "guid"
}
