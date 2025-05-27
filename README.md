# WorkApi

WorkApi is a .NET-based web API project designed for managing work-related data, tasks, or other domain entities. The structure suggests a clear separation of concerns using Models, Controllers, Services, and Repositories, following best practices for modern API development.

## Requirements

- [.NET 6.0 or newer](https://dotnet.microsoft.com/download)
- Docker (optional, for running via containers)
- SQL Server or another database supported by EF Core (configure connection in `appsettings.json`)
- Visual Studio or VS Code (recommended for development)

## Project Structure

```
WorkApi/
├── .vs/                      # Visual Studio environment files
├── bin/                      # Build output binaries
├── obj/                      # Intermediate build files
├── Contexts/                 # Database context classes (Entity Framework)
├── Controllers/              # API controllers (e.g., WorkController)
├── Migrations/               # Database migrations (EF Core)
├── Models/                   # Data models (e.g., Work, Task, User)
├── Properties/               # Project configuration files
├── Repositories/             # Data access layer (repositories)
├── Services/                 # Business logic layer (services)
├── Docker-compose.yml        # Docker Compose configuration
├── Dockerfile                # Docker image definition for the app
├── Program.cs                # Application entry point
├── WorkAPI.csproj            # .NET project file
├── WorkAPI.sln               # Visual Studio solution file
├── appsettings.json          # Main application configuration (connection strings, etc.)
├── appsettings.Development.json # Development environment configuration
├── WorkAPI.http              # Sample HTTP requests for API testing
```

> Note: Only a partial file listing is shown above. To view all files, see the [full repository on GitHub](https://github.com/Kitler174/WorkApi/tree/main).

## Getting Started

### Local Development (without Docker)

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Kitler174/WorkApi.git
   cd WorkApi
   ```

2. **Configure your database connection in `appsettings.json`.**

3. **Restore dependencies and apply migrations:**
   ```bash
   dotnet restore
   dotnet ef database update
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```
   By default, the API will be available at `http://localhost:5000` or `https://localhost:5001`.

### Running with Docker/Docker Compose

1. **Build and run the containers:**
   ```bash
   docker-compose up --build
   ```
   This will start the application and any dependent services (such as the database) as defined in `Docker-compose.yml`.

## Main Technologies and Patterns

- **ASP.NET Core Web API** – Fast and scalable REST API development
- **Entity Framework Core** – ORM, migrations, database access
- **Repository Pattern & Service Layer** – Clean separation of business logic and data access
- **Docker** – Simplified deployment and environment management

## Example Requests

Sample HTTP requests for testing API endpoints can be found in the `WorkAPI.http` file.

---

**Note:**  
This overview is based on the repository structure. For more details, source code, and the full file list, visit:  
[https://github.com/Kitler174/WorkApi](https://github.com/Kitler174/WorkApi/tree/main)
