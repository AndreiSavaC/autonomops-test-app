FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/AutonomOpsTestApp/AutonomOpsTestApp.csproj", "src/AutonomOpsTestApp/"]
RUN dotnet restore "src/AutonomOpsTestApp/AutonomOpsTestApp.csproj"

COPY . .
WORKDIR "/src/src/AutonomOpsTestApp"
RUN dotnet publish "AutonomOpsTestApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AutonomOpsTestApp.dll"]
