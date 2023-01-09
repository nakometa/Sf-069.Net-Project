#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SportsHub.Api/SportsHub.Api.csproj", "SportsHub.Api/"]
COPY ["SportsHub.Application/SportsHub.AppService.csproj", "SportsHub.Application/"]
COPY ["SportsHub.Core/SportsHub.Domain.csproj", "SportsHub.Core/"]
COPY ["SportsHub.DAL/SportsHub.DAL.csproj", "SportsHub.DAL/"]
RUN dotnet restore "SportsHub.Api/SportsHub.Api.csproj"
COPY . .
WORKDIR "/src/SportsHub.Api"
RUN dotnet build "SportsHub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportsHub.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportsHub.Api.dll"]