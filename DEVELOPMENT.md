# Development Guide

## Project Overview

This is a production-ready microservices backend built with:
- **ASP.NET Core 9.0**
- **Clean Architecture**
- **CQRS Pattern with MediatR**
- **PostgreSQL 16**
- **Docker & Docker Compose**

## Architecture Principles

### Clean Architecture Layers

1. **Domain Layer** - Core business logic
   - Entities with business rules
   - Domain events
   - Repository interfaces
   - No dependencies on other layers

2. **Application Layer** - Use cases
   - Commands (write operations)
   - Queries (read operations)
   - DTOs
   - Validation with FluentValidation
   - Uses MediatR for CQRS

3. **Infrastructure Layer** - External concerns
   - Database context & migrations
   - Repository implementations
   - External service integrations
   - Depends on Application layer

4. **API Layer** - Entry point
   - Controllers (thin layer)
   - Middleware
   - Authentication/Authorization
   - Depends on Application & Infrastructure

### Microservices

Each microservice follows the same clean architecture pattern:

#### Identity Service (Port 5001)
- User registration & authentication
- JWT token generation
- Role-based access control
- Database: IdentityDb

#### Product Service (Port 5002)
- Product catalog management
- Categories
- Inventory tracking
- Database: ProductDb

#### Order Service (Port 5003)
- Order processing
- Order history
- Database: OrderDb

#### API Gateway (Port 5000)
- Central entry point
- Request routing
- Rate limiting
- Authentication enforcement

## Getting Started

### Prerequisites

```bash
# Required
- .NET 9.0 SDK
- Docker Desktop
- PostgreSQL 16 (or use Docker)

# Optional
- Visual Studio 2022 / VS Code / Rider
- Postman or similar API testing tool
```

### Quick Start with Docker

```bash
# Clone the repository
git clone <repository-url>
cd microservices-backend

# Start all services
docker-compose up -d

# Check service health
curl http://localhost:5001/health  # Identity
curl http://localhost:5002/health  # Product
curl http://localhost:5003/health  # Order
curl http://localhost:5000/health  # Gateway
```

### Manual Setup

```bash
# 1. Install dependencies
dotnet restore

# 2. Setup databases (or use docker-compose for databases only)
docker-compose up -d postgres-identity postgres-product postgres-order

# 3. Run migrations
cd src/Services/Identity/Identity.Infrastructure
dotnet ef database update --startup-project ../Identity.API

# 4. Start services (in separate terminals)
dotnet run --project src/Services/Identity/Identity.API
dotnet run --project src/Services/Product/Product.API
dotnet run --project src/Services/Order/Order.API
dotnet run --project src/ApiGateway
```

## Development Workflow

### Adding a New Feature

1. **Domain Layer** - Define entities and interfaces
```csharp
public class Product : BaseEntity
{
    public string Name { get; private set; }
    // ... business logic
}
```

2. **Application Layer** - Create command/query
```csharp
public record CreateProductCommand(string Name, decimal Price) 
    : IRequest<Result<ProductDto>>;
```

3. **Application Layer** - Implement handler
```csharp
public class CreateProductCommandHandler 
    : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    // ... implementation
}
```

4. **Infrastructure Layer** - Add repository if needed
```csharp
public class ProductRepository : Repository<Product>, IProductRepository
{
    // ... custom methods
}
```

5. **API Layer** - Create controller endpoint
```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
{
    var command = new CreateProductCommand(dto.Name, dto.Price);
    var result = await _mediator.Send(command);
    return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Errors);
}
```

### Database Migrations

```bash
# Create migration
cd src/Services/Identity/Identity.Infrastructure
dotnet ef migrations add MigrationName --startup-project ../Identity.API

# Apply migration
dotnet ef database update --startup-project ../Identity.API

# Remove last migration
dotnet ef migrations remove --startup-project ../Identity.API
```

### Testing API Endpoints

Use the included Postman collection: `Microservices-API.postman_collection.json`

Or use curl:

```bash
# Register a user
curl -X POST http://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "userName": "testuser",
    "password": "Password123!",
    "firstName": "Test",
    "lastName": "User"
  }'

# Login
curl -X POST http://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123!"
  }'

# Use token in subsequent requests
curl -X GET http://localhost:5002/api/products \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## Best Practices

### 1. Domain-Driven Design
- Rich domain models with business logic
- Private setters, public methods for modifications
- Domain events for cross-aggregate communication

### 2. CQRS Pattern
- Separate read and write operations
- Commands change state, return Result<T>
- Queries return data, no side effects

### 3. Repository Pattern
- Abstract data access
- Generic base repository
- Specific repositories for complex queries

### 4. Result Pattern
- Don't throw exceptions for business logic failures
- Return Result<T> with success/failure status
- Include error messages for failures

### 5. Dependency Injection
- Register services in Program.cs
- Use interfaces for testability
- Scoped lifetime for repositories and DbContext

### 6. Error Handling
- Global exception middleware
- Validation with FluentValidation
- Consistent error responses

### 7. Security
- JWT authentication
- Password hashing with BCrypt
- HTTPS in production
- Environment-specific secrets

## Configuration

### Environment Variables

```bash
# Development
ASPNETCORE_ENVIRONMENT=Development

# Database
ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=IdentityDb;Username=postgres;Password=postgres123

# JWT
JwtSettings__Secret=YourSuperSecretKeyHere
JwtSettings__Issuer=IdentityService
JwtSettings__Audience=MicroservicesApp
```

### appsettings.json Structure

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "..."
  },
  "JwtSettings": {
    "Secret": "...",
    "Issuer": "...",
    "Audience": "..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

## Troubleshooting

### Database Connection Issues
```bash
# Check PostgreSQL is running
docker ps | grep postgres

# Test connection
psql -h localhost -p 5432 -U postgres -d IdentityDb
```

### Migration Issues
```bash
# Reset database
dotnet ef database drop --startup-project ../Identity.API
dotnet ef database update --startup-project ../Identity.API
```

### Port Already in Use
```bash
# Find process using port
lsof -i :5001

# Kill process
kill -9 <PID>
```

## Performance Tips

1. **Use async/await** consistently
2. **Enable response compression** for large payloads
3. **Implement caching** with Redis
4. **Use pagination** for list endpoints
5. **Add database indexes** on frequently queried fields
6. **Monitor with Application Insights** or similar

## Security Checklist

- [ ] HTTPS enabled in production
- [ ] Secrets in environment variables
- [ ] Strong JWT secret (min 32 characters)
- [ ] Password complexity requirements
- [ ] Rate limiting enabled
- [ ] CORS properly configured
- [ ] SQL injection prevention (EF Core parameterizes)
- [ ] Input validation on all endpoints

## Deployment

### Docker Production Build

```bash
# Build images
docker-compose -f docker-compose.prod.yml build

# Push to registry
docker tag identity-api:latest your-registry/identity-api:v1.0
docker push your-registry/identity-api:v1.0
```

### Kubernetes Deployment

See `k8s/` directory for Kubernetes manifests (to be added).

## Contributing

1. Create feature branch
2. Follow clean architecture principles
3. Add unit tests
4. Update documentation
5. Submit pull request

## License

MIT License - see LICENSE file for details
