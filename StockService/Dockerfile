# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas os arquivos de projeto
COPY StockService/StockService.csproj StockService/
COPY Shared/Shared.csproj Shared/

# Restaura dependências
RUN dotnet restore StockService/StockService.csproj

# Copia apenas o código fonte, sem bin/obj
COPY StockService/. StockService/
COPY Shared/. Shared/

# Publica o StockService
WORKDIR /src/StockService
RUN dotnet publish -c Release -o /app/out

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "StockService.dll"]