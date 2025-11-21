# CliniqueCare Backend (ASP.NET Core + PostgreSQL)

This backend is a lightweight ASP.NET Core Web API project using Entity Framework Core with PostgreSQL.

## Overview
- Framework: .NET 8 (net8.0)
- ORM: Entity Framework Core (Npgsql provider)
- DB: PostgreSQL (script provided)

## PostgreSQL Setup
1. Install PostgreSQL and ensure it is running.
2. Run the SQL script `scripts/create_postgres.sql` as a superuser (e.g. `psql -U postgres -f scripts/create_postgres.sql`).
   - This creates user `clinique_user` with password `admin` and database `clinique_care`.

If you prefer to use the existing `postgres` user, update `appsettings.json` connection string accordingly.

## Run the backend
1. Ensure you have the .NET 8 SDK installed (or a compatible runtime).
2. From the `backend-dotnet` folder run:

```powershell
```powershell
# restore packages
dotnet restore

# (optional) install EF tools if not installed
dotnet tool install --global dotnet-ef --version 8.0.0

# create initial migration (already created in the repository, but safe to run)
dotnet ef migrations add InitialCreate

# apply migration to the database
dotnet ef database update

# run the app
dotnet run
```
```

The API will be available at `https://localhost:5001` (or the port shown in the console). Swagger UI is available in development mode at `/swagger`.

## API Endpoints (examples)
- GET /api/employees
- GET /api/employees/{id}
- POST /api/employees
- PUT /api/employees/{id}
- DELETE /api/employees/{id}

Similar endpoints exist for: `/api/contracts`, `/api/presences`, `/api/leaves`, `/api/payroll`, `/api/trainings`.

## Notes
- The connection string in `appsettings.json` uses the credentials you provided (user `clinique_user`, password `admin`).
- For production consider storing secrets in environment variables or a secret store, and don't use passwords with spaces in production.
