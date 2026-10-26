# PortCall — Charter Fixture and Approval Manager

A portfolio starter for recording charter fixtures, routing approval requests, and tracking decisions with an Angular client, ASP.NET Core REST API, SQL Server, Redis cache, Swagger, xUnit, and Azure DevOps pipeline configuration.

## Prerequisites
- .NET 8 SDK
- Node.js 20+ and Angular CLI (or use `npx ng`)
- SQL Server and Redis for full persistence/cache integration

## Run the API
From `src/PortCall.Api`:
```bash
dotnet restore
dotnet run
```
Open the Swagger URL printed by ASP.NET Core (typically `/swagger`). The API uses an in-memory repository by default so it runs without local SQL Server or Redis. `appsettings.json` documents connection strings for replacing the demo repository with production persistence.

## Run the Angular app
```bash
cd client
npm install
npm start
```
Open http://localhost:4200. Configure `src/environments/environment.ts` if the API is running on a different URL.

## Main endpoints
- `GET /api/fixtures`
- `GET /api/fixtures/{id}`
- `POST /api/fixtures`
- `POST /api/fixtures/{id}/submit`
- `POST /api/fixtures/{id}/decision`

## Notes
This project demonstrates the workflow and architecture, not a verified production-scale performance benchmark. Redis and SQL Server are included as intended integration points; the default repository is in-memory for easy startup. Add authentication/authorization, EF Core migrations, distributed cache invalidation, and audit retention policies before production use.
