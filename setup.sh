#!/bin/bash

echo "🚀 Setting up Microservices Backend..."

# Colors for output
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${YELLOW}Step 1: Checking .NET SDK...${NC}"
if command -v dotnet &> /dev/null; then
    echo -e "${GREEN}✓ .NET SDK is installed${NC}"
    dotnet --version
else
    echo "❌ .NET SDK is not installed. Please install .NET 9.0 SDK"
    exit 1
fi

echo -e "\n${YELLOW}Step 2: Restoring NuGet packages...${NC}"
dotnet restore

echo -e "\n${YELLOW}Step 3: Building solution...${NC}"
dotnet build

echo -e "\n${YELLOW}Step 4: Setting up databases...${NC}"
echo "Make sure PostgreSQL is running on:"
echo "  - Identity DB: localhost:5432"
echo "  - Product DB: localhost:5433"
echo "  - Order DB: localhost:5434"

echo -e "\n${YELLOW}Step 5: Running database migrations...${NC}"
cd src/Services/Identity/Identity.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../Identity.API
dotnet ef database update --startup-project ../Identity.API
cd ../../../..

echo -e "\n${GREEN}✓ Setup complete!${NC}"
echo -e "\nTo run the services:"
echo "  1. Using Docker: docker-compose up -d"
echo "  2. Manually:"
echo "     - Identity API: dotnet run --project src/Services/Identity/Identity.API"
echo "     - Product API: dotnet run --project src/Services/Product/Product.API"
echo "     - Order API: dotnet run --project src/Services/Order/Order.API"
echo "     - API Gateway: dotnet run --project src/ApiGateway"
