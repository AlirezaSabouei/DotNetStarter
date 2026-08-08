# DotNetStarter

A reusable .NET project starter template built around Clean Architecture, CQRS, Domain-Driven Design (DDD), and MediatR.

## Overview

DotNetStarter provides a structured starting point for building ASP.NET Core applications without having to recreate the same architectural foundation for every project.

The solution separates application concerns into focused projects and provides common infrastructure for commands, queries, validation, mapping, persistence, cross-cutting behaviors, and API delivery.

## Architecture

The solution is organized into the following main projects:

- **Application** — Application use cases, CQRS requests, DTOs, validators, mappings, and application-level abstractions.
- **Domain** — Domain entities, value objects, domain events, and business rules.
- **Infrastructure** — Persistence and external infrastructure implementations.
- **MVC** — ASP.NET Core API / presentation layer.
- **Builder** — Utilities for generating or scaffolding project components.

The solution also contains a dedicated `tests` solution folder for test projects.

## CQRS

The application layer follows the CQRS pattern using MediatR. Commands and queries are represented separately and handled independently.

The sample `Students` feature demonstrates the intended organization:

```text
Application/
└── Students/
    ├── Commands/
    │   ├── CreateStudentRequest.cs
    │   └── Validators/
    ├── Queries/
    │   ├── GetStudentRequest.cs
    │   └── Validators/
    ├── Dtos/
    └── EventHandlers/
```

## Cross-Cutting Behaviors

The application layer includes MediatR pipeline behaviors for common concerns such as:

- Validation
- Logging
- Unhandled exception processing

This keeps these concerns outside individual command and query handlers.

## Domain-Driven Design

The project structure keeps domain logic isolated from infrastructure and presentation concerns. Business rules should live in the Domain layer, while application orchestration belongs in the Application layer.

This separation helps keep the domain model independent from frameworks and external services.

## Common Application Services

The starter includes abstractions for commonly required services, including:

- Date/time providers
- Email services
- Background jobs
- Password encryption
- Token services
- Data access/context

Implementations can be provided by the Infrastructure layer without coupling application code to concrete infrastructure technologies.

## Technology Stack

The current template targets **.NET 9** and uses several established libraries, including:

- ASP.NET Core
- Entity Framework Core
- MediatR
- FluentValidation
- AutoMapper
- ASP.NET Core Identity
- JWT authentication components

See the individual `.csproj` files for the exact package versions.

## .NET Template

The repository includes `.template.config/template.json`, allowing the project to be packaged and used as a .NET project template.

The template identity is `DotNetStarter` and its short name is `starter`.

## Getting Started

Clone the repository and open `Project.sln` in Visual Studio or another compatible .NET development environment.

Restore dependencies and build the solution:

```bash
dotnet restore
dotnet build
```

Run the appropriate presentation project for local development.

## Project Structure

```text
DotNetStarter/
├── .template.config/
├── src/
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   ├── MVC/
│   └── Builder/
├── tests/
├── Project.sln
└── LICENSE
```

## Purpose

This repository is intended to be a practical starting point rather than a framework. Project-specific business logic, infrastructure implementations, authentication configuration, persistence details, and API endpoints can be added on top of the provided architecture.

## License

See [LICENSE](LICENSE) for licensing information.
