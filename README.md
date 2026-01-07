# Microservices Backend - Clean Architecture

## Technologies
- **Framework**: ASP.NET Core 9.0
- **Database**: PostgreSQL 16
- **Architecture**: Clean Architecture with Microservices
- **Patterns**: CQRS, Repository, Mediator

## Microservices

### 1. Identity Service
- User authentication & authorization
- JWT token management
- Role-based access control

### 2. Product Service
- Product catalog management
- Categories & inventory

### 3. Order Service
- Order processing
- Order history & tracking

### 4. API Gateway
- Routes requests to microservices
- Rate limiting & authentication

## Project Structure

```
├── src/
│   ├── ApiGateway/
│   ├── Services/
│   │   ├── Identity/
│   │   │   ├── Identity.API/
│   │   │   ├── Identity.Application/
│   │   │   ├── Identity.Domain/
│   │   │   └── Identity.Infrastructure/
│   │   ├── Product/
│   │   │   ├── Product.API/
│   │   │   ├── Product.Application/
│   │   │   ├── Product.Domain/
│   │   │   └── Product.Infrastructure/
│   │   └── Order/
│   │       ├── Order.API/
│   │       ├── Order.Application/
│   │       ├── Order.Domain/
│   │       └── Order.Infrastructure/
│   └── BuildingBlocks/
│       └── Shared/
├── docker-compose.yml
└── README.md
```

## Clean Architecture Layers

### Domain Layer
- Entities
- Value Objects
- Domain Events
- Interfaces

### Application Layer
- Use Cases (Commands/Queries)
- DTOs
- Mappings
- Interfaces

### Infrastructure Layer
- Database Context
- Repositories
- External Services
- Migrations

### API Layer
- Controllers
- Middleware
- Filters
- API Configuration

## Running the Solution

### Prerequisites
- .NET 9.0 SDK
- Docker & Docker Compose
- PostgreSQL 16

### Using Docker Compose
```bash
docker-compose up -d
```

### Manual Setup
```bash
# Restore packages
dotnet restore

# Run migrations
dotnet ef database update --project src/Services/Identity/Identity.Infrastructure

# Run services
dotnet run --project src/Services/Identity/Identity.API
dotnet run --project src/Services/Product/Product.API
dotnet run --project src/Services/Order/Order.API
dotnet run --project src/ApiGateway/ApiGateway
```

## API Endpoints

### Identity Service (Port 5001)
- POST /api/auth/register
- POST /api/auth/login
- POST /api/auth/refresh
- GET /api/users/profile

### Product Service (Port 5002)
- GET /api/products
- GET /api/products/{id}
- POST /api/products
- PUT /api/products/{id}
- DELETE /api/products/{id}

### Order Service (Port 5003)
- GET /api/orders
- GET /api/orders/{id}
- POST /api/orders
- PUT /api/orders/{id}/status

### API Gateway (Port 5000)
- Routes all requests to appropriate microservices

## Database Configuration

Each microservice has its own PostgreSQL database:
- IdentityDb
- ProductDb
- OrderDb

## Features

- ✅ Clean Architecture
- ✅ CQRS with MediatR
- ✅ Repository Pattern
- ✅ Unit of Work
- ✅ Entity Framework Core
- ✅ PostgreSQL Integration
- ✅ JWT Authentication
- ✅ API Gateway with Ocelot
- ✅ Swagger/OpenAPI
- ✅ Docker Support
- ✅ Health Checks
- ✅ Logging with Serilog
- ✅ Global Exception Handling
- ✅ FluentValidation
- ✅ AutoMapper
