FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/Identity.API/Identity.API.csproj", "Services/Identity/Identity.API/"]
COPY ["src/Services/Identity/Identity.Application/Identity.Application.csproj", "Services/Identity/Identity.Application/"]
COPY ["src/Services/Identity/Identity.Domain/Identity.Domain.csproj", "Services/Identity/Identity.Domain/"]
COPY ["src/Services/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "Services/Identity/Identity.Infrastructure/"]
COPY ["src/BuildingBlocks/Shared/Shared.csproj", "BuildingBlocks/Shared/"]

RUN dotnet restore "Services/Identity/Identity.API/Identity.API.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
