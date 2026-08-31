# Stage 1: Build (kept on Debian SDK for build reliability; discarded before final image)
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

# Restore dependencies (restore the API project; it transitively pulls its referenced projects)
RUN dotnet restore ScrapDealer.Api/ScrapDealer.Api.csproj

# Copy everything else
COPY . .

# Publish the API project
WORKDIR /src/ScrapDealer.Api
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime (Alpine = much smaller than Debian-based aspnet image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Persian (fa-IR) globalization needs ICU; the Alpine base is minimal so install it.
# ca-certificates ensures HTTPS / encrypted SQL Server connections work.
RUN apk add --no-cache icu-libs ca-certificates
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/out ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "ScrapDealer.Api.dll"]
