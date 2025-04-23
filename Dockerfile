# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Solution va project fayllarni ko‘chiramiz
COPY ["Portfolio.sln", "./"]
COPY ["src/Portfolio.WebApi/Portfolio.WebApi.csproj", "src/Portfolio.WebApi/"]
COPY ["src/Portfolio.DataAccess/Portfolio.DataAccess.csproj", "src/Portfolio.DataAccess/"]
COPY ["src/Portfolio.Domain/Portfolio.Domain.csproj", "src/Portfolio.Domain/"]
COPY ["src/Portfolio.Service/Portfolio.Service.csproj", "src/Portfolio.Service/"]

# Restore dependencies
RUN dotnet restore "src/Portfolio.WebApi/Portfolio.WebApi.csproj"

# Source code'ni to‘liq ko‘chiramiz
COPY . .

# Build loyihani
WORKDIR "/src/src/Portfolio.WebApi"
RUN dotnet build "Portfolio.WebApi.csproj" -c Release -o /app/build

# 2. Publish stage
FROM build AS publish
RUN dotnet publish "Portfolio.WebApi.csproj" -c Release -o /app/publish

# 3. Runtime stage (lightweight image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Render.com PORT ni eshitishi uchun
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Published fayllarni qo‘shamiz
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Portfolio.WebApi.dll"]
