FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Solution va project fayllarini ko'chiramiz
COPY ["Portfolio.sln", "./"]
COPY ["src/Portfolio.WebApi/Portfolio.WebApi.csproj", "src/Portfolio.WebApi/"]
COPY ["src/Portfolio.DataAccess/Portfolio.DataAccess.csproj", "src/Portfolio.DataAccess/"]
COPY ["src/Portfolio.Domain/Portfolio.Domain.csproj", "src/Portfolio.Domain/"]
COPY ["src/Portfolio.Service/Portfolio.Service.csproj", "src/Portfolio.Service/"]

# 2. Restore dependencies
RUN dotnet restore "src/Portfolio.WebApi/Portfolio.WebApi.csproj"

# 3. Barcha source code ni ko'chiramiz
COPY . .

# 4. Build qilamiz
WORKDIR "/src/src/Portfolio.WebApi"
RUN dotnet build "Portfolio.WebApi.csproj" -c Release -o /app/build

# 5. Publish qilamiz
FROM build AS publish
RUN dotnet publish "Portfolio.WebApi.csproj" -c Release -o /app/publish

# 6. Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.WebApi.dll"]