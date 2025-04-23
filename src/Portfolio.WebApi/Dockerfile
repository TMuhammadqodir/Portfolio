# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file first (agar bor bo'lsa)
COPY ["Portfolio.sln", "./"]
COPY ["../Portfolio.DataAccess/Portfolio.DataAccess.csproj", "../Portfolio.DataAccess/"]
COPY ["../Portfolio.Domain/Portfolio.Domain.csproj", "../Portfolio.Domain/"]
COPY ["../Portfolio.Service/Portfolio.Service.csproj", "../Portfolio.Service/"]
COPY ["Portfolio.WebApi.csproj", "./"]

# Restore dependencies
RUN dotnet restore "Portfolio.WebApi.csproj"

# Copy all source code
COPY . .

# Build the project
RUN dotnet build "Portfolio.WebApi.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "Portfolio.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image for production
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.WebApi.dll"]