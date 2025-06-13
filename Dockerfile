# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyecto para restaurar dependencias
COPY *.sln .
COPY StockAPI/*.csproj ./StockAPI/

RUN dotnet restore

# Copiar todo el código y publicar la aplicación
COPY StockAPI/. ./StockAPI/
WORKDIR /src/StockAPI

RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "StockAPI.dll"]
