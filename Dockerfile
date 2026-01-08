FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore and build API Gateway
RUN dotnet restore "src/ApiGateway/ApiGateway.csproj"
RUN dotnet build "src/ApiGateway/ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/ApiGateway/ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
