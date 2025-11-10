# RosaryForToday Project Structure

This is a .NET 9 MAUI application following Domain-Driven Design (DDD) and CQRS (Command Query Responsibility Segregation) architectural patterns.

## Project Overview

The RosaryForToday application helps users identify which Rosary mystery falls on each day, with reflections and reminders for daily prayer.

## Solution Structure

### RosaryForToday.Domain
**Purpose**: Contains the core business entities and domain logic.

**Key Components**:
- `Entities/Language.cs` - Language entity (English, Polish)
- `Entities/RosaryType.cs` - Different types of rosary prayers
- `Entities/RosaryReflection.cs` - Daily reflection content
- `Entities/RosaryDaySchedule.cs` - Maps rosary types to days of the week

**Dependencies**: None (pure domain layer)

### RosaryForToday.Application
**Purpose**: Contains application-level business logic, CQRS commands, and queries.

**Key Components**:
- `Commands/CreateRosaryTypeCommand.cs` - Command for creating new rosary types
- `Queries/GetRosaryTypesQuery.cs` - Query to retrieve rosary types
- `Queries/GetRosaryForTodayQuery.cs` - Query to get today's rosary and reflection

**Dependencies**:
- RosaryForToday.Domain
- Kmaraszkiewicz86.SimpleCqrs (v0.1.1)

### RosaryForToday.Infrastructure
**Purpose**: Contains data access implementation and external concerns.

**Key Components**:
- `Data/RosaryDbContext.cs` - Entity Framework Core DbContext
- `Data/DesignTimeDbContextFactory.cs` - EF Core design-time factory
- `Migrations/` - EF Core database migrations

**Dependencies**:
- RosaryForToday.Domain
- RosaryForToday.Application
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Design

**Database Provider**: SQLite

### RosaryForToday.Maui
**Purpose**: MAUI presentation layer (UI and platform-specific code).

**Dependencies**:
- RosaryForToday.Application
- RosaryForToday.Infrastructure

## Database Schema

### Languages Table
Stores available language translations.

| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Auto-increment |
| Code | string(10) | Required, Unique |
| Name | string(100) | Required |

**Seed Data**:
- English (en)
- Polish (pl)

### RosaryTypes Table
Stores different types of rosary prayers.

| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Auto-increment |
| Name | string(200) | Required |
| Description | string(1000) | Optional |
| LanguageId | int | Foreign Key to Languages |

### RosaryReflections Table
Stores daily reflection content for each rosary type.

| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Auto-increment |
| RosaryTypeId | int | Foreign Key to RosaryTypes |
| Title | string(500) | Required |
| Content | text | Required |
| LanguageId | int | Foreign Key to Languages |

### RosaryDaySchedules Table
Maps which rosary type should be prayed on which day of the week.

| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | Primary Key, Auto-increment |
| RosaryTypeId | int | Foreign Key to RosaryTypes |
| DayOfWeek | int | Required (0=Sunday, 6=Saturday) |

**Unique Constraint**: (RosaryTypeId, DayOfWeek)

## CQRS Pattern

This application uses the SimpleCqrs library to implement CQRS:

### Commands
Commands represent write operations that modify state:
- `CreateRosaryTypeCommand` - Creates a new rosary type

### Queries
Queries represent read operations that don't modify state:
- `GetRosaryTypesQuery` - Retrieves all rosary types (optionally filtered by language)
- `GetRosaryForTodayQuery` - Gets the rosary type and reflection for a specific date

## Building and Running

### Prerequisites
- .NET 9 SDK
- EF Core tools: `dotnet tool install --global dotnet-ef`

### Build
```bash
dotnet restore
dotnet build
```

### Database Migrations

Create a migration:
```bash
cd RosaryForToday.Infrastructure
dotnet ef migrations add MigrationName
```

Update database:
```bash
cd RosaryForToday.Infrastructure
dotnet ef database update
```

## Architecture Principles

1. **Separation of Concerns**: Each layer has a distinct responsibility
2. **Dependency Inversion**: Dependencies flow inward toward the domain
3. **Domain-Driven Design**: Business logic is centralized in the domain layer
4. **CQRS**: Separate models for reading and writing data
5. **Clean Architecture**: Infrastructure and presentation depend on domain, not vice versa
