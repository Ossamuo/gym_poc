# Gym - Mediator + DDD POC

This repository is a **POC (Proof of Concept)** that demonstrates how to build a modular .NET application using:

- **Mediator Pattern** (custom mediator implementation)
- **DDD (Domain-Driven Design)** with bounded contexts
- **Domain Notifications** for event-driven communication
- **Microservices-style integration with Kafka** for asynchronous messaging

The goal is to showcase architecture and flow patterns rather than provide a production-hardened solution.

## Tech Stack

- .NET 9
- ASP.NET Core Minimal API
- Entity Framework Core + SQL Server
- Kafka (Confluent client)
- OpenTelemetry (traces + metrics)
- Grafana LGTM stack for observability
- MSTest

## Solution Structure

The solution is organized by Clean Architecture layers:

- `src/Gym.Domain` - Entities, value objects, domain events, repository/service contracts
- `src/Gym.Application` - Use-case handlers, mediator orchestration, notification handlers
- `src/Gym.Infrastructure` - EF Core persistence, external adapters (Kafka, SendGrid)
- `src/Gym.Api` - Minimal API endpoints, dependency injection, auth, telemetry wiring
- `src/Gym.Web` - ASP.NET Core web frontend
- `src/Gym.BookingMessage.Producer` - Worker that publishes booking events to Kafka
- `Test/Gym.Domain.Test` - Domain unit tests

## Architectural Focus (POC)

This project focuses on the following architectural ideas:

1. **Mediator Pattern**  
   Requests are sent through a mediator (`IMediator`) that dispatches commands/queries to handlers.

2. **DDD + Bounded Contexts**  
   The domain is split into contexts such as account, activities, partner, and shared abstractions.

3. **Notification-Based Domain Events**  
   Domain events are published as notifications to decouple business actions from side effects.

4. **Kafka-Based Async Integration**  
   Booking-related events are propagated through Kafka to simulate microservices/event-driven boundaries.

## Request Flow

```text
HTTP Request
 -> Minimal API endpoint
 -> IMediator.SendAsync(request)
 -> Application Handler
 -> Domain logic + Repository abstraction
 -> Infrastructure implementation (EF Core, external services)
 -> IMediator.PublishAsync(domain events)
 -> Notification handlers
 -> Kafka producer / integrations
```

## Main API Endpoints

- `GET /` - Health check
- `POST /api/v1/members` - Register member
- `POST /api/v1/members/authenticate` - Authenticate member (JWT)
- `GET /api/v1/members/{id}` - Get member details (authorized)
- `PUT /api/v1/members/{id}` - Edit member (authorized)
- `GET /api/v1/activities` - List activities
- `POST /api/v1/activities/book` - Book a session (authorized)
- `POST /api/v1/partners/book` - Partner booking reservation

## Running the Project

### Build

```bash
dotnet build
```

### Run API

```bash
dotnet run --project src/Gym.Api
```

### Run Booking Message Producer

```bash
dotnet run --project src/Gym.BookingMessage.Producer
```

### Run Tests

```bash
dotnet test Test/Gym.Domain.Test
```

## Database and Migrations

Apply EF Core migrations:

```bash
dotnet ef database update --project src/Gym.Infrastructure --startup-project src/Gym.Api
```

## Configuration and Secrets

Important settings are configured in `appsettings.json` and user secrets:

- `ConnectionStrings:DefaultConnection`
- `Secrets:JwtPrivateKey`
- `Secrets:ApiKey`
- `Secrets:PasswordSaltKey`
- `SendGrid:ApiKey`
- `Kafka:BootstrapServer`
- `Kafka:TopicName`
- `Kafka:SchemaRegistryUrl`

Set secrets with:

```bash
dotnet user-secrets set "Secrets:JwtPrivateKey" "..." --project src/Gym.Api
```

## Observability

Start the observability stack:

```bash
docker-compose -f infra/observability/docker-compose.yml up
```

Default ports:

- Grafana: `3000`
- Prometheus: `9090`
- OTLP collector: `4317`

---

If you are studying architecture patterns, this POC is a practical example of combining **Mediator Pattern + DDD + domain notifications + Kafka messaging** in a single .NET solution.