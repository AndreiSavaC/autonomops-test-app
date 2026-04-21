FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["AutonomOpsTestApp.sln", "."]
COPY ["src/AutonomOpsTestApp/AutonomOpsTestApp.csproj", "src/AutonomOpsTestApp/"]
COPY ["tests/<inexistent>.Tests/AutonomOpsTestApp.Tests.csproj", "tests/AutonomOpsTestApp.Tests/"]
RUN dotnet restore

COPY . .
WORKDIR "/src/src/AutonomOpsTestApp"
RUN dotnet publish "AutonomOpsTestApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AutonomOpsTestApp.dll"]

