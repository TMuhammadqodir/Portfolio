# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only project files for restore
COPY ["src/Portfolio.WebApi/Portfolio.WebApi.csproj", "src/Portfolio.WebApi/"]
COPY ["src/Portfolio.DataAccess/Portfolio.DataAccess.csproj", "src/Portfolio.DataAccess/"]
COPY ["src/Portfolio.Domain/Portfolio.Domain.csproj", "src/Portfolio.Domain/"]
COPY ["src/Portfolio.Service/Portfolio.Service.csproj", "src/Portfolio.Service/"]

# Restore dependencies
RUN dotnet restore "src/Portfolio.WebApi/Portfolio.WebApi.csproj"

# Copy all source code
COPY . .

# Build the project
WORKDIR "/src/src/Portfolio.WebApi"
RUN dotnet build "Portfolio.WebApi.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "Portfolio.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image for production
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.WebApi.dll"]
