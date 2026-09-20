# ScrapDealer

A .NET 8 CQRS + DDD application for managing scrap dealership operations — sale orders, contracts, invoices, users, buyers/sellers, and more.

## Architecture

- **Domain** (`ScrapDealer.Domain`) — Entities, value objects, domain events, factories, repository interfaces.
- **Application** (`ScrapDealer.Application`) — Commands/Queries (CQRS), handlers, DTOs, external service interfaces.
- **Infrastructure** (`ScrapDealer.Infrastructure`) — EF Core repositories, read models, migrations, external service implementations.
- **API** (`ScrapDealer.Api`) — ASP.NET Core controllers, dependency injection setup.
- **Shared** (`ScrapDealer.Shared` / `Shared.Abstractions`) — Common models (`PaginationQuery`, `PaginatedResult`, `ApiResponse`), helpers, extensions.

## Key Features

- **Sale Orders** — Create, update, review (admin), nearby search, pagination.
- **Contracts** — Vote/rating system with score history, aggregate score recalculation.
- **Invoices** — CRUD, pending approval, buyer/seller views.
- **Users / Roles / Permissions** — JWT auth, role-based access.
- **Notifications, Tickets, Rewards, Referrals, Dashboard** — Admin analytics.
- **Globalized** — Persian (fa-IR) support with `DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0` + ICU.

## Common Response Wrapper

All endpoints return a consistent `ApiResponse<T>`:

```json
{
  "data": [...],
  "total": 100,
  "success": true,
  "message": "..."
}
```

- Paginated results: `data` = items array, `total` = total count.
- Single objects: `data` = object, `total` = `null`.
- Messages: `data` = string message.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (or Docker)
- Docker (optional, for containerized build)

### Run Locally

```bash
# Restore & build
dotnet restore
dotnet build

# Run API
cd ScrapDealer.Api
dotnet run
```

Swagger UI available at `http://localhost:5000/swagger` (or the configured port).

### Docker

```bash
# Build
docker build -t scrapdealer-api .

# Save image
docker save -o scrapdealer-api.tar scrapdealer-api

# Run (requires SQL Server connection string via env)
docker run -d -p 8080:8080 \
  -e ConnectionStrings__Default="Server=...;Database=...;User=...;Password=..." \
  scrapdealer-api
```

The Dockerfile uses **Alpine runtime** (`aspnet:8.0-alpine`) with `icu-libs` for Persian globalization (~80 MB tar).

## Project Structure

```
ScrapDealer.sln
├── ScrapDealer.Api              # Controllers, DI, middleware
├── ScrapDealer.Application      # Commands, Queries, Handlers, DTOs
├── ScrapDealer.Domain           # Entities, ValueObjects, Factories, Events, Repositories
├── ScrapDealer.Infrastructure   # EF Core, Repositories, Migrations, ReadModels
├── ScrapDealer.Shared           # PaginationQuery, PaginatedResult, ApiResponse, Helpers
├── ScrapDealer.Shared.Abstractions # Interfaces, base classes
└── ScrapDealer.UnitTests        # xUnit tests
```

## Database Migrations

```bash
# Add migration (run from Infrastructure project)
dotnet ef migrations add <Name> --project ScrapDealer.Infrastructure --startup-project ScrapDealer.Api --context ReadDbContext

# Update database
dotnet ef database update --project ScrapDealer.Infrastructure --startup-project ScrapDealer.Api --context ReadDbContext
```

## Testing

```bash
dotnet test ScrapDealer.UnitTests
```

## Configuration

Key settings in `appsettings.json`:

- `ConnectionStrings:Default` — SQL Server connection.
- `JwtSettings` — Secret, issuer, audience, expiry.
- `ExternalServices` — Location API, etc.

## Code Conventions

- CQRS: Commands (`ICommand`) / Queries (`IQuery`) with MediatR.
- DDD: Aggregates, factories, domain events.
- Records for DTOs and query/command objects.
- `ApiResponse<T>` wrapper for all HTTP responses.
- Pagination via `PaginationQuery` (`PageSize`, `PageIndex`, `SortBy`, `Search`).
- Persian globalization via `DNTPersianUtils.Core`.

## License

Internal project.