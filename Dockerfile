# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ACME.CargoExpress.API/ACME.CargoExpress.API.csproj", "ACME.CargoExpress.API/"]
COPY ["CargoExpress.UnitTests/CargoExpress.UnitTests.csproj", "CargoExpress.UnitTests/"]
COPY ["ACME.CargoExpress.API.sln", "."]

RUN dotnet restore "ACME.CargoExpress.API.sln"

COPY . .

WORKDIR "/src/ACME.CargoExpress.API"
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
EXPOSE ${PORT:-8080}

ENTRYPOINT ["dotnet", "ACME.CargoExpress.API.dll"]

