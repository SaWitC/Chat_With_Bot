#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BotServer/BotServer.csproj", "BotServer/"]
COPY ["BotServer.SignalR/BotServer.SignalRforChats.csproj", "BotServer.SignalR/"]
COPY ["BotServer.Services/BotServer.Services.csproj", "BotServer.Services/"]
COPY ["BotServer.Features/BotServer.Features.csproj", "BotServer.Features/"]
COPY ["BotServer.SignalR_info/BotServer.SignalR_info.csproj", "BotServer.SignalR_info/"]
COPY ["Botserve.MigrationApp/Botserve.MigrationApp.csproj", "Botserve.MigrationApp/"]
COPY ["BotServer.Data/BotServer.Data.csproj", "BotServer.Data/"]
COPY ["BotServer.Application/BotServer.Application.csproj", "BotServer.Application/"]
COPY ["BotServer.Domain/BotServer.Domain.csproj", "BotServer.Domain/"]
RUN dotnet restore "BotServer/BotServer.csproj"
COPY . .
WORKDIR "/src/BotServer"
RUN dotnet build "BotServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BotServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BotServer.dll"]