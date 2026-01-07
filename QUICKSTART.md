# Quick Start Guide

## 🚀 Get Started in 5 Minutes

### Option 1: Docker Compose (Recommended)

```bash
# 1. Clone the repository
git clone <your-repo-url>
cd microservices-backend

# 2. Start all services
docker-compose up -d

# 3. Wait for services to be ready (~30 seconds)
# Check health endpoints:
curl http://localhost:5001/health  # Identity Service
curl http://localhost:5002/health  # Product Service  
curl http://localhost:5003/health  # Order Service
curl http://localhost:5000/health  # API Gateway

# 4. Test the API
# Register a user
curl -X POST http://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@example.com",
    "userName": "admin",
    "password": "Admin123!",
    "firstName": "Admin",
    "lastName": "User"
  }'

# You're done! 🎉
```

### Option 2: Local Development

```bash
# Prerequisites: .NET 9.0 SDK, PostgreSQL 16

# 1. Clone and restore packages
git clone <your-repo-url>
cd microservices-backend
dotnet restore

# 2. Start PostgreSQL (or use Docker)
docker-compose up -d postgres-identity postgres-product postgres-order

# 3. Run database migrations
cd src/Services/Identity/Identity.Infrastructure
dotnet ef database update --startup-project ../Identity.API
cd ../../../..

# 4. Start services (in separate terminals)
# Terminal 1: Identity Service
dotnet run --project src/Services/Identity/Identity.API

# Terminal 2: Product Service
dotnet run --project src/Services/Product/Product.API

# Terminal 3: Order Service
dotnet run --project src/Services/Order/Order.API

# Terminal 4: API Gateway
dotnet run --project src/ApiGateway
```

## 📝 Test the APIs

### Using Postman
Import the collection: `Microservices-API.postman_collection.json`

### Using cURL

#### 1. Register a User
```bash
curl -X POST http://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "userName": "testuser",
    "password": "Password123!",
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "+1234567890"
  }'
```

#### 2. Login
```bash
curl -X POST http://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123!"
  }'
```

Save the returned `token` for the next steps.

#### 3. Get Products (Authenticated)
```bash
curl -X GET http://localhost:5002/api/products \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## 🌐 Access Points

| Service | URL | Purpose |
|---------|-----|---------|
| Identity API | http://localhost:5001 | Authentication & Users |
| Product API | http://localhost:5002 | Products & Categories |
| Order API | http://localhost:5003 | Orders & Processing |
| API Gateway | http://localhost:5000 | Unified Entry Point |
| Swagger (Identity) | http://localhost:5001/swagger | API Documentation |
| Swagger (Product) | http://localhost:5002/swagger | API Documentation |
| Swagger (Order) | http://localhost:5003/swagger | API Documentation |

## 🗄️ Database Access

### PostgreSQL Databases

```bash
# Identity Database
psql -h localhost -p 5432 -U postgres -d IdentityDb

# Product Database  
psql -h localhost -p 5433 -U postgres -d ProductDb

# Order Database
psql -h localhost -p 5434 -U postgres -d OrderDb

# Default password: postgres123
```

### Using Docker

```bash
# Connect to Identity DB
docker exec -it postgres-identity psql -U postgres -d IdentityDb

# List all tables
\dt

# View users
SELECT * FROM "Users";
```

## 🔧 Common Commands

### Docker

```bash
# Start all services
docker-compose up -d

# Stop all services
docker-compose down

# View logs
docker-compose logs -f identity-api

# Rebuild services
docker-compose up -d --build

# Remove all containers and volumes
docker-compose down -v
```

### .NET

```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run specific project
dotnet run --project src/Services/Identity/Identity.API

# Watch mode (auto-reload)
dotnet watch --project src/Services/Identity/Identity.API

# Run tests
dotnet test
```

### Entity Framework

```bash
# Add migration
cd src/Services/Identity/Identity.Infrastructure
dotnet ef migrations add MigrationName --startup-project ../Identity.API

# Update database
dotnet ef database update --startup-project ../Identity.API

# Drop database
dotnet ef database drop --startup-project ../Identity.API --force
```

## 🐛 Troubleshooting

### Port Already in Use

```bash
# Find process using port 5001
lsof -i :5001
# or on Windows
netstat -ano | findstr :5001

# Kill the process
kill -9 <PID>
```

### Database Connection Failed

```bash
# Check if PostgreSQL is running
docker ps | grep postgres

# Restart PostgreSQL
docker-compose restart postgres-identity
```

### Migration Issues

```bash
# Drop and recreate database
cd src/Services/Identity/Identity.Infrastructure
dotnet ef database drop --startup-project ../Identity.API --force
dotnet ef database update --startup-project ../Identity.API
```

## 📚 Next Steps

1. Read the [DEVELOPMENT.md](DEVELOPMENT.md) for detailed documentation
2. Explore the API using Swagger UI
3. Check out the clean architecture structure
4. Add your own features following the patterns

## 🆘 Need Help?

- Check the logs: `docker-compose logs -f`
- View health endpoints to see what's failing
- Ensure all prerequisites are installed
- Check firewall settings for ports 5000-5003

## 🎯 What's Included

✅ Clean Architecture  
✅ CQRS with MediatR  
✅ Repository Pattern  
✅ JWT Authentication  
✅ PostgreSQL Integration  
✅ Docker Support  
✅ API Gateway  
✅ Swagger Documentation  
✅ Health Checks  
✅ Global Exception Handling  

Happy coding! 🚀
