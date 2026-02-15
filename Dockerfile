# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY *.sln ./

# Copy ALL project files (so restore works)
COPY ScrapDealer.Api/*.csproj ./ScrapDealer.Api/
COPY ScrapDealer.Domain/*.csproj ./ScrapDealer.Domain/
COPY ScrapDealer.Application/*.csproj ./ScrapDealer.Application/
COPY ScrapDealer.Infrastructure/*.csproj ./ScrapDealer.Infrastructure/
COPY ScrapDealer.Shared/*.csproj ./ScrapDealer.Shared/
COPY ScrapDealer.Shared.Abstractions/*.csproj ./ScrapDealer.Shared.Abstractions/
COPY ScrapDealer.UnitTests/*.csproj ./ScrapDealer.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Publish the API project
WORKDIR /src/ScrapDealer.Api
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "ScrapDealer.Api.dll"]
